
using GlobalDataDefineClsLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisionClsLib;
using VisionControlAppClsLib;

namespace VisionGUI
{
    public partial class VisualCircleFindControlGUI : UserControl
    {
        #region Private File

        //private static readonly object _lockObj = new object();
        //private static volatile VisualCircleFindControlGUI _instance = null;
        //public static VisualCircleFindControlGUI Instance
        //{
        //    get
        //    {
        //        if (_instance == null)
        //        {
        //            lock (_lockObj)
        //            {
        //                if (_instance == null)
        //                {
        //                    _instance = new VisualCircleFindControlGUI();
        //                }
        //            }
        //        }
        //        return _instance;
        //    }
        //}

        VisualControlApplications VisualApp;

        CameraWindowGUI CameraWindow;

        public bool ImageShow = false;

        public bool Inited = false;


        #endregion

        #region 算法参数

        private int _RingLightintensity = 0;
        private int _DirectLightintensity = 0;

        private float _Score = 0.5f;
        public int MinR { get; set; } = 10;
        public int MaxR { get; set; } = 300;
        public int Rrange = 200;

        public PointF TemplateRoiCenter { get; set; }
        public float TemplateRoiR { get; set; }

        public RectangleFV SearchRoi
        {
            get
            {
                RectangleFV rectangle = new RectangleFV();
                rectangle.X = _SearchRoi.X;
                rectangle.Y = _SearchRoi.Y;
                rectangle.Width = _SearchRoi.Width;
                rectangle.Height = _SearchRoi.Height;

                return rectangle;
            }
            set
            {
                RectangleF rectangle = new RectangleF(value.X, value.Y, value.Width, value.Height);
                _SearchRoi = rectangle;
            }
        }
        private RectangleF _SearchRoi;

        bool TemplateRoiMove = false;
        bool TemplateRoiResize = false;
        bool SearchRoiMove = false;
        bool SearchRoiResize = false;

        int RoiMoveInterval = 16;
        int RoiResizeInterval = 16;

        public string CircleFindTemplatefilepath{ get; set; }

        /// <summary>
        /// 环光强度
        /// </summary>
        public int RingLightintensity
        {
            get
            {
                return _RingLightintensity;
            }
            set
            {

                RingLightBar.Value = value;
                RingLightNumlabel.Text = value.ToString();
                _RingLightintensity = value;
                if (VisualApp != null)
                {
                    if (value > -1 && value < 256)
                    {
                        VisualApp.SetRingLightintensity(value);
                    }
                }
            }
        }

        /// <summary>
        /// 直光强度
        /// </summary>
        public int DirectLightintensity
        {
            get
            {
                return _DirectLightintensity;
            }
            set
            {

                DirectLightBar.Value = value;
                DirectLightNumlabel.Text = value.ToString();
                _DirectLightintensity = value;
                if (VisualApp != null)
                {
                    if (value > -1 && value < 256)
                    {
                        VisualApp.SetDirectLightintensity(value);
                    }
                }
            }
        }


        /// <summary>
        /// 识别分数
        /// </summary>
        public int Score
        {
            get
            {
                return (int)(_Score * 100);
            }
            set
            {
                QualityBar.Value = value;
                MinimunqualityNumlabel.Text = ((float)value/100).ToString();
                _Score = ((float)value / 100);
            }
        }

        #endregion

        #region 相机视窗控制

        public void CameraWindowShow(Graphic graphic, bool Show)
        {
            if (ImageShow && CameraWindow != null)
                CameraWindow.GraphicDraw(graphic, Show);
        }

        public void RoiResize(ImageDirection Direct, int interval)
        {
            if (ImageShow && CameraWindow != null)
                CameraWindow.ROIReSize(Direct, interval);
        }

        public void RoiMove(ImageDirection Direct, int interval)
        {
            if (ImageShow && CameraWindow != null)
                CameraWindow.ROIMove(Direct, interval);
        }

        public void CameraWindowShowInit(RectangleF rect)
        {
            if (ImageShow && CameraWindow != null)
                CameraWindow.GraphicDrawInit(rect);
        }

        public RectangleF GetROI()
        {
            if (ImageShow && CameraWindow != null)
                return CameraWindow.GetROI();
            else
                return RectangleF.Empty;
        }

        public void CircleRoiResize(int interval)
        {
            if (ImageShow && CameraWindow != null)
                CameraWindow.CircleROIReSize(interval);
        }

        public void CircleRoiMove(ImageDirection Direct, int interval)
        {
            if (ImageShow && CameraWindow != null)
                CameraWindow.CircleROIMove(Direct, interval);
        }

        public void CameraWindowShowInit(PointF Center, float Radius)
        {
            if (ImageShow && CameraWindow != null)
                CameraWindow.GraphicDrawInit(Center, Radius);
        }

        public (PointF Center, float Radius) GetCircleROI()
        {
            if (ImageShow && CameraWindow != null)
                return CameraWindow.GetCircleROI();
            else
                return (PointF.Empty, -1);
        }

        public void CameraWindowClear()
        {
            if (ImageShow && CameraWindow != null)
                CameraWindow.ClearGraphicDraw();
        }

        #endregion


        public VisualCircleFindControlGUI()
        {
            InitializeComponent();

            TemplateRoiCenter = new PointF();
            TemplateRoiR = 10;
            _SearchRoi = new RectangleF();

            string currentDirectory = Directory.GetCurrentDirectory();
            string newFilePath = Path.Combine(currentDirectory, "CircleTemplate.contourmxml");
            CircleFindTemplatefilepath = newFilePath;

        }

        public void InitVisualControl(CameraWindowGUI CameraWindow, VisualControlApplications VisualApp)
        {
            if (CameraWindow == null || VisualApp == null)
            {
                Inited = false;
                return;
            }

            this.VisualApp = VisualApp;
            this.CameraWindow = CameraWindow;

            ImageShow = true;

            (TemplateRoiCenter, TemplateRoiR) = GetCircleROI();
            _SearchRoi = GetROI();
            Inited = true;
            
        }

        public bool InitForm()
        {
            try
            {
                if(VisualApp != null)
                {
                    int RingInt = VisualApp.GetRingLightintensity();

                    int DirectInt = VisualApp.GetDirectLightintensity();

                    RingLightBar.Value = RingInt;

                    DirectLightBar.Value = DirectInt;

                }
                return true;
            }
            catch
            {
                return false;
            }



        }

        public void SetVisualParam(GlobalDataDefineClsLib.CircleFindIdentificationParam param)
        {
            this.RingLightintensity = param.RingLightintensity;
            this.DirectLightintensity = param.DirectLightintensity;
            this.CircleFindTemplatefilepath = param.CircleFindTemplatefilepath;
            this.Score = param.Score;
            this.MinR = param.MinR;
            this.MaxR = param.MaxR;
            this.TemplateRoiCenter = param.TemplateRoiCenter;
            this.TemplateRoiR = param.TemplateRoiR;
            this.SearchRoi = param.SearchRoi;
        }

        public GlobalDataDefineClsLib.CircleFindIdentificationParam GetVisualParam()
        {
            GlobalDataDefineClsLib.CircleFindIdentificationParam param1 = new GlobalDataDefineClsLib.CircleFindIdentificationParam();

            param1.RingLightintensity = this.RingLightintensity;
            param1.DirectLightintensity = this.DirectLightintensity;
            param1.CircleFindTemplatefilepath = this.CircleFindTemplatefilepath;
            param1.Score = this.Score;
            param1.MinR = this.MinR;
            param1.MaxR = this.MaxR;
            param1.TemplateRoiCenter = this.TemplateRoiCenter;
            param1.TemplateRoiR = this.TemplateRoiR;
            param1.SearchRoi = this.SearchRoi;

            return param1;
        }

        private void RingLightBar_Scroll(object sender, EventArgs e)
        {
            int value = RingLightBar.Value;
            if (VisualApp != null)
            {
                if (value > -1 && value < 256)
                {
                    VisualApp.SetRingLightintensity(value);
                    RingLightNumlabel.Text = value.ToString();
                    _RingLightintensity = value;
                }
            }

        }

        private void DirectLightBar_Scroll(object sender, EventArgs e)
        {
            int value = DirectLightBar.Value;
            if (VisualApp != null)
            {
                if (value > -1 && value < 256)
                {
                    VisualApp.SetDirectLightintensity(value);
                    DirectLightNumlabel.Text = value.ToString();
                    _DirectLightintensity = value;
                }
            }
        }

        private void QualityBar_Scroll(object sender, EventArgs e)
        {
            int value = QualityBar.Value;

            _Score = (float)(value) / 100.0f;

            MinimunqualityNumlabel.Text = _Score.ToString();
        }

        private void TemplateRoiShowBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (VisualApp != null)
            {
                CameraWindowShowInit(TemplateRoiCenter, TemplateRoiR);
                CameraWindowShow(Graphic.circleRoi, TemplateRoiShowBtn.Checked);
            }
                
        }

        private void SearchAreaRoiShowBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (VisualApp != null)
            {
                CameraWindowShowInit(_SearchRoi);
                CameraWindowShow(Graphic.rectRoi, SearchAreaRoiShowBtn.Checked);
            }
        }

        private void TemplateMoveBtn_CheckedChanged(object sender, EventArgs e)
        {
            TemplateRoiMove = TemplateMoveBtn.Checked;
            if(TemplateRoiMove)
            {
                if(TemplateResizeBtn.Checked)
                {
                    TemplateResizeBtn.Checked = false;
                    TemplateRoiResize = false;
                }
                if (SearchAreaMoveBtn.Checked)
                {
                    SearchAreaMoveBtn.Checked = false;
                    SearchRoiMove = false;
                }
                if (SearchAreaResizeBtn.Checked)
                {
                    SearchAreaResizeBtn.Checked = false;
                    SearchRoiResize = false;
                }
            }
        }

        private void TemplateResizeBtn_CheckedChanged(object sender, EventArgs e)
        {
            TemplateRoiResize = TemplateResizeBtn.Checked;
            if (TemplateRoiResize)
            {
                if (TemplateMoveBtn.Checked)
                {
                    TemplateMoveBtn.Checked = false;
                    TemplateRoiMove = false;
                }
                if (SearchAreaMoveBtn.Checked)
                {
                    SearchAreaMoveBtn.Checked = false;
                    SearchRoiMove = false;
                }
                if (SearchAreaResizeBtn.Checked)
                {
                    SearchAreaResizeBtn.Checked = false;
                    SearchRoiResize = false;
                }
            }
        }

        private void SearchAreaMoveBtn_CheckedChanged(object sender, EventArgs e)
        {
            SearchRoiMove = SearchAreaMoveBtn.Checked;
            if (SearchRoiMove)
            {
                if (TemplateMoveBtn.Checked)
                {
                    TemplateMoveBtn.Checked = false;
                    TemplateRoiMove = false;
                }
                if (TemplateResizeBtn.Checked)
                {
                    TemplateResizeBtn.Checked = false;
                    TemplateRoiResize = false;
                }
                if (SearchAreaResizeBtn.Checked)
                {
                    SearchAreaResizeBtn.Checked = false;
                    SearchRoiResize = false;
                }
            }
        }

        private void SearchAreaResizeBtn_CheckedChanged(object sender, EventArgs e)
        {
            SearchRoiResize = SearchAreaResizeBtn.Checked;
            if (SearchRoiResize)
            {
                if (TemplateMoveBtn.Checked)
                {
                    TemplateMoveBtn.Checked = false;
                    TemplateRoiMove = false;
                }
                if (TemplateResizeBtn.Checked)
                {
                    TemplateResizeBtn.Checked = false;
                    TemplateRoiResize = false;
                }
                if (SearchAreaMoveBtn.Checked)
                {
                    SearchAreaMoveBtn.Checked = false;
                    SearchRoiMove = false;
                }
            }
        }

        private void VisualRoiTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabPage currentTab = VisualRoiTab.SelectedTab;

            if(currentTab.Text == "Circle")
            {
                if (VisualApp != null)
                {
                    TemplateRoiShowBtn.Checked = true;
                }

                if (SearchAreaMoveBtn.Checked)
                {
                    SearchAreaMoveBtn.Checked = false;
                    SearchRoiMove = false;
                }
                if (SearchAreaResizeBtn.Checked)
                {
                    SearchAreaResizeBtn.Checked = false;
                    SearchRoiResize = false;
                }
                
            }
            if (currentTab.Text == "SearchArea")
            {
                if (VisualApp != null)
                {
                    SearchAreaRoiShowBtn.Checked = true;
                }

                if (TemplateMoveBtn.Checked)
                {
                    TemplateMoveBtn.Checked = false;
                    TemplateRoiMove = false;
                }
                if (TemplateResizeBtn.Checked)
                {
                    TemplateResizeBtn.Checked = false;
                    TemplateRoiResize = false;
                }
            }
        }

        private void VisualRoiTab_KeyDown(object sender, KeyEventArgs e)
        {
            if (VisualApp != null)
            {
                if (e.Modifiers == Keys.Control && TemplateMoveBtn.Checked)
                {
                    switch (e.KeyCode)
                    {
                        case Keys.Up:
                            CircleRoiMove(ImageDirection.Y, -RoiMoveInterval);
                            break;
                        case Keys.Down:
                            CircleRoiMove(ImageDirection.Y, +RoiMoveInterval);
                            break;
                        case Keys.Left:
                            CircleRoiMove(ImageDirection.X, -RoiMoveInterval);
                            break;
                        case Keys.Right:
                            CircleRoiMove(ImageDirection.X, +RoiMoveInterval);
                            break;
                    }
                    (TemplateRoiCenter, TemplateRoiR) = GetCircleROI();
                }

                if (e.Modifiers == Keys.Control && TemplateResizeBtn.Checked)
                {
                    switch (e.KeyCode)
                    {
                        case Keys.Up:
                            CircleRoiResize(+RoiResizeInterval);
                            break;
                        case Keys.Down:
                            CircleRoiResize(-RoiResizeInterval);
                            break;
                        case Keys.Left:
                            CircleRoiResize(-RoiResizeInterval);
                            break;
                        case Keys.Right:
                            CircleRoiResize(+RoiResizeInterval);
                            break;
                    }
                    (TemplateRoiCenter, TemplateRoiR) = GetCircleROI();
                }


                if (e.Modifiers == Keys.Control && SearchAreaMoveBtn.Checked)
                {
                    switch (e.KeyCode)
                    {
                        case Keys.Up:
                            RoiMove(ImageDirection.Y, -RoiMoveInterval);
                            break;
                        case Keys.Down:
                            RoiMove(ImageDirection.Y, +RoiMoveInterval);
                            break;
                        case Keys.Left:
                            RoiMove(ImageDirection.X, -RoiMoveInterval);
                            break;
                        case Keys.Right:
                            RoiMove(ImageDirection.X, +RoiMoveInterval);
                            break;
                    }
                    _SearchRoi = GetROI();
                }

                if (e.Modifiers == Keys.Control && SearchAreaResizeBtn.Checked)
                {
                    switch (e.KeyCode)
                    {
                        case Keys.Up:
                            RoiResize(ImageDirection.Y, +RoiResizeInterval);
                            break;
                        case Keys.Down:
                            RoiResize(ImageDirection.Y, -RoiResizeInterval);
                            break;
                        case Keys.Left:
                            RoiResize(ImageDirection.X, -RoiResizeInterval);
                            break;
                        case Keys.Right:
                            RoiResize(ImageDirection.X, +RoiResizeInterval);
                            break;
                    }
                    _SearchRoi = GetROI();
                }

            }
            
        }


        private void SearchBtn_Click(object sender, EventArgs e)
        {
            if (VisualApp != null)
            {
                CircleResult results = new CircleResult();

                VisualApp.ContinuousGetImage(false);

                bool Done = VisualApp.CircleFindInit(CircleFindTemplatefilepath);

                MinR = (int)TemplateRoiR / 2 - Rrange;
                MaxR = (int)TemplateRoiR / 2 + Rrange;

                results = VisualApp.CircleFindAsync((int)(_Score * 100), _SearchRoi, (int)TemplateRoiR/2 - Rrange, (int)TemplateRoiR/2 + Rrange);


                Done = VisualApp.CircleFindSave(CircleFindTemplatefilepath);

                if (results != null && CameraWindow != null)
                {
                    CameraWindow.ClearGraphicDraw();

                    CameraWindow.GraphicDrawInit(results);

                    CameraWindow.GraphicDraw(Graphic.circle, true);

                }

                VisualApp.ContinuousGetImage(true);
            }
        }

        private void CameraWindowClearBtn_Click(object sender, EventArgs e)
        {
            if (VisualApp != null)
            {
                CameraWindowClear();
            }
        }
    }
}
