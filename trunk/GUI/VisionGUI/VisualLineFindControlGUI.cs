
using DevExpress.XtraEditors;
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
    public partial class VisualLineFindControlGUI : XtraUserControl
    {
        #region Private File

        //private static readonly object _lockObj = new object();
        //private static volatile VisualLineFindControlGUI _instance = null;
        //public static VisualLineFindControlGUI Instance
        //{
        //    get
        //    {
        //        if (_instance == null)
        //        {
        //            lock (_lockObj)
        //            {
        //                if (_instance == null)
        //                {
        //                    _instance = new VisualLineFindControlGUI();
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
        public float Score
        {
            get
            {
                return _Score;
            }
            set
            {
                QualityBar.Value = (int)(value * 100);
                MinimunqualityNumlabel.Text = value.ToString();
                _Score = value;
            }
        }

        public int MinAngle { get; set; } = -15;
        public int MaxAngle { get; set; } = 15;

        public int UpEdgeScore { get; set; } = 0;
        public int DownEdgeScore { get; set; } = 0;
        public int LeftEdgeScore { get; set; } = 0;
        public int RightEdgeScore { get; set; } = 0;

        public RectangleFV UpEdgeRoi
        {
            get
            {
                RectangleFV rectangle = new RectangleFV();
                rectangle.X = _UpEdgeRoi.X;
                rectangle.Y = _UpEdgeRoi.Y;
                rectangle.Width = _UpEdgeRoi.Width;
                rectangle.Height = _UpEdgeRoi.Height;

                return rectangle;
            }
            set
            {
                RectangleF rectangle = new RectangleF(value.X, value.Y, value.Width, value.Height);
                _UpEdgeRoi = rectangle;
            }
        }
        public RectangleFV DownEdgeRoi
        {
            get
            {
                RectangleFV rectangle = new RectangleFV();
                rectangle.X = _DownEdgeRoi.X;
                rectangle.Y = _DownEdgeRoi.Y;
                rectangle.Width = _DownEdgeRoi.Width;
                rectangle.Height = _DownEdgeRoi.Height;

                return rectangle;
            }
            set
            {
                RectangleF rectangle = new RectangleF(value.X, value.Y, value.Width, value.Height);
                _DownEdgeRoi = rectangle;
            }
        }
        public RectangleFV LeftEdgeRoi
        {
            get
            {
                RectangleFV rectangle = new RectangleFV();
                rectangle.X = _LeftEdgeRoi.X;
                rectangle.Y = _LeftEdgeRoi.Y;
                rectangle.Width = _LeftEdgeRoi.Width;
                rectangle.Height = _LeftEdgeRoi.Height;

                return rectangle;
            }
            set
            {
                RectangleF rectangle = new RectangleF(value.X, value.Y, value.Width, value.Height);
                _LeftEdgeRoi = rectangle;
            }
        }
        public RectangleFV RightEdgeRoi
        {
            get
            {
                RectangleFV rectangle = new RectangleFV();
                rectangle.X = _RightEdgeRoi.X;
                rectangle.Y = _RightEdgeRoi.Y;
                rectangle.Width = _RightEdgeRoi.Width;
                rectangle.Height = _RightEdgeRoi.Height;

                return rectangle;
            }
            set
            {
                RectangleF rectangle = new RectangleF(value.X, value.Y, value.Width, value.Height);
                _RightEdgeRoi = rectangle;
            }
        }

        private RectangleF _UpEdgeRoi;
        private RectangleF _DownEdgeRoi;
        private RectangleF _LeftEdgeRoi;
        private RectangleF _RightEdgeRoi;

        bool UpEdgeRoiMove = false;
        bool UpEdgeRoiResize = false;
        bool DownEdgeRoiMove = false;
        bool DownEdgeRoiResize = false;
        bool LeftEdgeRoiMove = false;
        bool LeftEdgeRoiResize = false;
        bool RightEdgeRoiMove = false;
        bool RightEdgeRoiResize = false;

        int RoiMoveInterval = 16;
        int RoiResizeInterval = 16;

        public string UpEdgefilepath { get; set; }
        public string DownEdgefilepath { get; set; }
        public string LeftEdgefilepath { get; set; }
        public string RightEdgefilepath { get; set; }

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


        public VisualLineFindControlGUI()
        {
            InitializeComponent();

            _UpEdgeRoi = new RectangleF();
            _DownEdgeRoi = new RectangleF();
            _LeftEdgeRoi = new RectangleF();
            _RightEdgeRoi = new RectangleF();

            string currentDirectory = Directory.GetCurrentDirectory();
            string newFilePath = Path.Combine(currentDirectory, "UpEdgeFind.xml");
            UpEdgefilepath = newFilePath;
            newFilePath = Path.Combine(currentDirectory, "DownEdgeFind.xml");
            DownEdgefilepath = newFilePath;
            newFilePath = Path.Combine(currentDirectory, "LeftEdgeFind.xml");
            LeftEdgefilepath = newFilePath;
            newFilePath = Path.Combine(currentDirectory, "RightEdgeFind.xml");
            RightEdgefilepath = newFilePath;



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

            _UpEdgeRoi = GetROI();
            _DownEdgeRoi = GetROI();
            _LeftEdgeRoi = GetROI();
            _RightEdgeRoi = GetROI();

            VisualApp.LineFindSave(UpEdgefilepath, 0, true);
            VisualApp.LineFindSave(DownEdgefilepath, 0, true);
            VisualApp.LineFindSave(LeftEdgefilepath, 0, false);
            VisualApp.LineFindSave(RightEdgefilepath, 0, false);
            Inited = true;

        }

        public bool InitForm()
        {
            try
            {
                if (VisualApp != null)
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


        public void SetVisualParam(GlobalDataDefineClsLib.LineFindIdentificationParam param)
        {
            this.RingLightintensity = param.RingLightintensity;
            this.DirectLightintensity = param.DirectLightintensity;
            this.UpEdgefilepath = param.UpEdgefilepath;
            this.DownEdgefilepath = param.DownEdgefilepath;
            this.LeftEdgefilepath = param.LeftEdgefilepath;
            this.RightEdgefilepath = param.RightEdgefilepath;
            this.UpEdgeScore = param.UpEdgeScore;
            this.DownEdgeScore = param.DownEdgeScore;
            this.LeftEdgeScore = param.LeftEdgeScore;
            this.RightEdgeScore = param.RightEdgeScore;
            this.UpEdgeRoi = param.UpEdgeRoi;
            this.DownEdgeRoi = param.DownEdgeRoi;
            this.LeftEdgeRoi = param.LeftEdgeRoi;
            this.RightEdgeRoi = param.RightEdgeRoi;
        }

        public GlobalDataDefineClsLib.LineFindIdentificationParam GetVisualParam()
        {
            GlobalDataDefineClsLib.LineFindIdentificationParam param1 = new GlobalDataDefineClsLib.LineFindIdentificationParam();

            param1.RingLightintensity = this.RingLightintensity;
            param1.DirectLightintensity = this.DirectLightintensity;
            param1.UpEdgefilepath = this.UpEdgefilepath;
            param1.DownEdgefilepath = this.DownEdgefilepath;
            param1.LeftEdgefilepath = this.LeftEdgefilepath;
            param1.RightEdgefilepath = this.RightEdgefilepath;
            param1.UpEdgeScore = this.UpEdgeScore;
            param1.DownEdgeScore = this.DownEdgeScore;
            param1.LeftEdgeScore = this.LeftEdgeScore;
            param1.RightEdgeScore = this.RightEdgeScore;
            param1.UpEdgeRoi = this.UpEdgeRoi;
            param1.DownEdgeRoi = this.DownEdgeRoi;
            param1.LeftEdgeRoi = this.LeftEdgeRoi;
            param1.RightEdgeRoi = this.RightEdgeRoi;

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

        private void UpEdgeRoiShowBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (VisualApp != null)
            {
                CameraWindowShowInit(_UpEdgeRoi);
                CameraWindowShow(Graphic.rectRoi, UpEdgeRoiShowBtn.Checked);
            }

        }

        private void DownEdgeRoiShowBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (VisualApp != null)
            {
                CameraWindowShowInit(_DownEdgeRoi);
                CameraWindowShow(Graphic.rectRoi, DownEdgeRoiShowBtn.Checked);
            }
        }

        private void LeftEdgeRoiShowBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (VisualApp != null)
            {
                CameraWindowShowInit(_LeftEdgeRoi);
                CameraWindowShow(Graphic.rectRoi, LeftEdgeRoiShowBtn.Checked);
            }
        }

        private void RightEdgeRoiShowBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (VisualApp != null)
            {
                CameraWindowShowInit(_RightEdgeRoi);
                CameraWindowShow(Graphic.rectRoi, RightEdgeRoiShowBtn.Checked);
            }
        }

        private void UpEdgeMoveBtn_CheckedChanged(object sender, EventArgs e)
        {
            UpEdgeRoiMove = UpEdgeMoveBtn.Checked;
            if (UpEdgeRoiMove)
            {
                if (UpEdgeResizeBtn.Checked)
                {
                    UpEdgeResizeBtn.Checked = false;
                    UpEdgeRoiResize = false;
                }
                if (DownEdgeMoveBtn.Checked)
                {
                    DownEdgeMoveBtn.Checked = false;
                    DownEdgeRoiMove = false;
                }
                if (DownEdgeResizeBtn.Checked)
                {
                    DownEdgeResizeBtn.Checked = false;
                    DownEdgeRoiResize = false;
                }
                if (LeftEdgeMoveBtn.Checked)
                {
                    LeftEdgeMoveBtn.Checked = false;
                    LeftEdgeRoiMove = false;
                }
                if (LeftEdgeResizeBtn.Checked)
                {
                    LeftEdgeResizeBtn.Checked = false;
                    LeftEdgeRoiResize = false;
                }

                if (RightEdgeMoveBtn.Checked)
                {
                    RightEdgeMoveBtn.Checked = false;
                    RightEdgeRoiMove = false;
                }
                if (RightEdgeResizeBtn.Checked)
                {
                    RightEdgeResizeBtn.Checked = false;
                    RightEdgeRoiResize = false;
                }
            }
        }

        private void UpEdgeResizeBtn_CheckedChanged(object sender, EventArgs e)
        {
            UpEdgeRoiResize = UpEdgeResizeBtn.Checked;
            if (UpEdgeRoiResize)
            {
                if (UpEdgeMoveBtn.Checked)
                {
                    UpEdgeMoveBtn.Checked = false;
                    UpEdgeRoiMove = false;
                }
                if (DownEdgeMoveBtn.Checked)
                {
                    DownEdgeMoveBtn.Checked = false;
                    DownEdgeRoiMove = false;
                }
                if (DownEdgeResizeBtn.Checked)
                {
                    DownEdgeResizeBtn.Checked = false;
                    DownEdgeRoiResize = false;
                }
                if (LeftEdgeMoveBtn.Checked)
                {
                    LeftEdgeMoveBtn.Checked = false;
                    LeftEdgeRoiMove = false;
                }
                if (LeftEdgeResizeBtn.Checked)
                {
                    LeftEdgeResizeBtn.Checked = false;
                    LeftEdgeRoiResize = false;
                }

                if (RightEdgeMoveBtn.Checked)
                {
                    RightEdgeMoveBtn.Checked = false;
                    RightEdgeRoiMove = false;
                }
                if (RightEdgeResizeBtn.Checked)
                {
                    RightEdgeResizeBtn.Checked = false;
                    RightEdgeRoiResize = false;
                }
            }
        }


        private void LeftEdgeMoveBtn_CheckedChanged(object sender, EventArgs e)
        {
            LeftEdgeRoiMove = LeftEdgeMoveBtn.Checked;
            if (LeftEdgeRoiMove)
            {
                if (UpEdgeMoveBtn.Checked)
                {
                    UpEdgeMoveBtn.Checked = false;
                    UpEdgeRoiMove = false;
                }
                if (UpEdgeResizeBtn.Checked)
                {
                    UpEdgeResizeBtn.Checked = false;
                    UpEdgeRoiResize = false;
                }

                if (DownEdgeMoveBtn.Checked)
                {
                    DownEdgeMoveBtn.Checked = false;
                    DownEdgeRoiMove = false;
                }
                if (DownEdgeResizeBtn.Checked)
                {
                    DownEdgeResizeBtn.Checked = false;
                    DownEdgeRoiResize = false;
                }

                if (LeftEdgeResizeBtn.Checked)
                {
                    LeftEdgeResizeBtn.Checked = false;
                    LeftEdgeRoiResize = false;
                }

                if (RightEdgeMoveBtn.Checked)
                {
                    RightEdgeMoveBtn.Checked = false;
                    RightEdgeRoiMove = false;
                }
                if (RightEdgeResizeBtn.Checked)
                {
                    RightEdgeResizeBtn.Checked = false;
                    RightEdgeRoiResize = false;
                }
            }
        }

        private void LeftEdgeResizeBtn_CheckedChanged(object sender, EventArgs e)
        {
            LeftEdgeRoiResize = LeftEdgeResizeBtn.Checked;
            if (LeftEdgeRoiResize)
            {
                if (UpEdgeMoveBtn.Checked)
                {
                    UpEdgeMoveBtn.Checked = false;
                    UpEdgeRoiMove = false;
                }
                if (UpEdgeResizeBtn.Checked)
                {
                    UpEdgeResizeBtn.Checked = false;
                    UpEdgeRoiResize = false;
                }

                if (DownEdgeMoveBtn.Checked)
                {
                    DownEdgeMoveBtn.Checked = false;
                    DownEdgeRoiMove = false;
                }
                if (DownEdgeResizeBtn.Checked)
                {
                    DownEdgeResizeBtn.Checked = false;
                    DownEdgeRoiResize = false;
                }
                if (LeftEdgeMoveBtn.Checked)
                {
                    LeftEdgeMoveBtn.Checked = false;
                    LeftEdgeRoiMove = false;
                }

                if (RightEdgeMoveBtn.Checked)
                {
                    RightEdgeMoveBtn.Checked = false;
                    RightEdgeRoiMove = false;
                }
                if (RightEdgeResizeBtn.Checked)
                {
                    RightEdgeResizeBtn.Checked = false;
                    RightEdgeRoiResize = false;
                }
            }
        }

        private void RightEdgeMoveBtn_CheckedChanged(object sender, EventArgs e)
        {
            RightEdgeRoiMove = RightEdgeMoveBtn.Checked;
            if (RightEdgeRoiMove)
            {
                if (UpEdgeMoveBtn.Checked)
                {
                    UpEdgeMoveBtn.Checked = false;
                    UpEdgeRoiMove = false;
                }
                if (UpEdgeResizeBtn.Checked)
                {
                    UpEdgeResizeBtn.Checked = false;
                    UpEdgeRoiResize = false;
                }

                if (DownEdgeMoveBtn.Checked)
                {
                    DownEdgeMoveBtn.Checked = false;
                    DownEdgeRoiMove = false;
                }
                if (DownEdgeResizeBtn.Checked)
                {
                    DownEdgeResizeBtn.Checked = false;
                    DownEdgeRoiResize = false;
                }
                if (LeftEdgeMoveBtn.Checked)
                {
                    LeftEdgeMoveBtn.Checked = false;
                    LeftEdgeRoiMove = false;
                }
                if (LeftEdgeResizeBtn.Checked)
                {
                    LeftEdgeResizeBtn.Checked = false;
                    LeftEdgeRoiResize = false;
                }

                if (RightEdgeResizeBtn.Checked)
                {
                    RightEdgeResizeBtn.Checked = false;
                    RightEdgeRoiResize = false;
                }
            }
        }

        private void RightEdgeResizeBtn_CheckedChanged(object sender, EventArgs e)
        {
            RightEdgeRoiResize = RightEdgeResizeBtn.Checked;
            if (RightEdgeRoiResize)
            {
                if (UpEdgeMoveBtn.Checked)
                {
                    UpEdgeMoveBtn.Checked = false;
                    UpEdgeRoiMove = false;
                }
                if (UpEdgeResizeBtn.Checked)
                {
                    UpEdgeResizeBtn.Checked = false;
                    UpEdgeRoiResize = false;
                }

                if (DownEdgeMoveBtn.Checked)
                {
                    DownEdgeMoveBtn.Checked = false;
                    DownEdgeRoiMove = false;
                }
                if (DownEdgeResizeBtn.Checked)
                {
                    DownEdgeResizeBtn.Checked = false;
                    DownEdgeRoiResize = false;
                }
                if (LeftEdgeMoveBtn.Checked)
                {
                    LeftEdgeMoveBtn.Checked = false;
                    LeftEdgeRoiMove = false;
                }
                if (LeftEdgeResizeBtn.Checked)
                {
                    LeftEdgeResizeBtn.Checked = false;
                    LeftEdgeRoiResize = false;
                }

                if (RightEdgeMoveBtn.Checked)
                {
                    RightEdgeMoveBtn.Checked = false;
                    RightEdgeRoiMove = false;
                }
            }
        }

        private void DownEdgeMoveBtn_CheckedChanged(object sender, EventArgs e)
        {
            DownEdgeRoiMove = DownEdgeMoveBtn.Checked;
            if (DownEdgeRoiMove)
            {
                if (UpEdgeMoveBtn.Checked)
                {
                    UpEdgeMoveBtn.Checked = false;
                    UpEdgeRoiMove = false;
                }
                if (UpEdgeResizeBtn.Checked)
                {
                    UpEdgeResizeBtn.Checked = false;
                    UpEdgeRoiResize = false;
                }

                if (DownEdgeResizeBtn.Checked)
                {
                    DownEdgeResizeBtn.Checked = false;
                    DownEdgeRoiResize = false;
                }
                if (LeftEdgeMoveBtn.Checked)
                {
                    LeftEdgeMoveBtn.Checked = false;
                    LeftEdgeRoiMove = false;
                }
                if (LeftEdgeResizeBtn.Checked)
                {
                    LeftEdgeResizeBtn.Checked = false;
                    LeftEdgeRoiResize = false;
                }

                if (RightEdgeMoveBtn.Checked)
                {
                    RightEdgeMoveBtn.Checked = false;
                    RightEdgeRoiMove = false;
                }
                if (RightEdgeResizeBtn.Checked)
                {
                    RightEdgeResizeBtn.Checked = false;
                    RightEdgeRoiResize = false;
                }
            }
        }

        private void DownEdgeResizeBtn_CheckedChanged(object sender, EventArgs e)
        {
            DownEdgeRoiResize = DownEdgeResizeBtn.Checked;
            if (DownEdgeRoiResize)
            {
                if (UpEdgeMoveBtn.Checked)
                {
                    UpEdgeMoveBtn.Checked = false;
                    UpEdgeRoiMove = false;
                }
                if (UpEdgeResizeBtn.Checked)
                {
                    UpEdgeResizeBtn.Checked = false;
                    UpEdgeRoiResize = false;
                }

                if (DownEdgeMoveBtn.Checked)
                {
                    DownEdgeMoveBtn.Checked = false;
                    DownEdgeRoiMove = false;
                }

                if (LeftEdgeMoveBtn.Checked)
                {
                    LeftEdgeMoveBtn.Checked = false;
                    LeftEdgeRoiMove = false;
                }
                if (LeftEdgeResizeBtn.Checked)
                {
                    LeftEdgeResizeBtn.Checked = false;
                    LeftEdgeRoiResize = false;
                }

                if (RightEdgeMoveBtn.Checked)
                {
                    RightEdgeMoveBtn.Checked = false;
                    RightEdgeRoiMove = false;
                }
                if (RightEdgeResizeBtn.Checked)
                {
                    RightEdgeResizeBtn.Checked = false;
                    RightEdgeRoiResize = false;
                }
            }
        }

        private void VisualRoiTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            TabPage currentTab = VisualRoiTab.SelectedTab;

            if (currentTab.Text == "UpEdge")
            {
                if (VisualApp != null)
                {
                    UpEdgeRoiShowBtn.Checked = true;
                }

                if (UpEdgeMoveBtn.Checked)
                {
                    UpEdgeMoveBtn.Checked = false;
                    UpEdgeRoiMove = false;
                }
                if (UpEdgeResizeBtn.Checked)
                {
                    UpEdgeResizeBtn.Checked = false;
                    UpEdgeRoiResize = false;
                }

                if (DownEdgeMoveBtn.Checked)
                {
                    DownEdgeMoveBtn.Checked = false;
                    DownEdgeRoiMove = false;
                }
                if (DownEdgeResizeBtn.Checked)
                {
                    DownEdgeResizeBtn.Checked = false;
                    DownEdgeRoiResize = false;
                }

                if (LeftEdgeMoveBtn.Checked)
                {
                    LeftEdgeMoveBtn.Checked = false;
                    LeftEdgeRoiMove = false;
                }
                if (LeftEdgeResizeBtn.Checked)
                {
                    LeftEdgeResizeBtn.Checked = false;
                    LeftEdgeRoiResize = false;
                }

                if (RightEdgeMoveBtn.Checked)
                {
                    RightEdgeMoveBtn.Checked = false;
                    RightEdgeRoiMove = false;
                }
                if (RightEdgeResizeBtn.Checked)
                {
                    RightEdgeResizeBtn.Checked = false;
                    RightEdgeRoiResize = false;
                }
            }

            if (currentTab.Text == "DownEdge")
            {
                if (VisualApp != null)
                {
                    DownEdgeRoiShowBtn.Checked = true;
                }

                if (UpEdgeMoveBtn.Checked)
                {
                    UpEdgeMoveBtn.Checked = false;
                    UpEdgeRoiMove = false;
                }
                if (UpEdgeResizeBtn.Checked)
                {
                    UpEdgeResizeBtn.Checked = false;
                    UpEdgeRoiResize = false;
                }

                if (DownEdgeMoveBtn.Checked)
                {
                    DownEdgeMoveBtn.Checked = false;
                    DownEdgeRoiMove = false;
                }
                if (DownEdgeResizeBtn.Checked)
                {
                    DownEdgeResizeBtn.Checked = false;
                    DownEdgeRoiResize = false;
                }

                if (LeftEdgeMoveBtn.Checked)
                {
                    LeftEdgeMoveBtn.Checked = false;
                    LeftEdgeRoiMove = false;
                }
                if (LeftEdgeResizeBtn.Checked)
                {
                    LeftEdgeResizeBtn.Checked = false;
                    LeftEdgeRoiResize = false;
                }

                if (RightEdgeMoveBtn.Checked)
                {
                    RightEdgeMoveBtn.Checked = false;
                    RightEdgeRoiMove = false;
                }
                if (RightEdgeResizeBtn.Checked)
                {
                    RightEdgeResizeBtn.Checked = false;
                    RightEdgeRoiResize = false;
                }
            }

            if (currentTab.Text == "LeftEdge")
            {
                if (VisualApp != null)
                {
                    LeftEdgeRoiShowBtn.Checked = true;
                }

                if (UpEdgeMoveBtn.Checked)
                {
                    UpEdgeMoveBtn.Checked = false;
                    UpEdgeRoiMove = false;
                }
                if (UpEdgeResizeBtn.Checked)
                {
                    UpEdgeResizeBtn.Checked = false;
                    UpEdgeRoiResize = false;
                }

                if (DownEdgeMoveBtn.Checked)
                {
                    DownEdgeMoveBtn.Checked = false;
                    DownEdgeRoiMove = false;
                }
                if (DownEdgeResizeBtn.Checked)
                {
                    DownEdgeResizeBtn.Checked = false;
                    DownEdgeRoiResize = false;
                }

                if (LeftEdgeMoveBtn.Checked)
                {
                    LeftEdgeMoveBtn.Checked = false;
                    LeftEdgeRoiMove = false;
                }
                if (LeftEdgeResizeBtn.Checked)
                {
                    LeftEdgeResizeBtn.Checked = false;
                    LeftEdgeRoiResize = false;
                }

                if (RightEdgeMoveBtn.Checked)
                {
                    RightEdgeMoveBtn.Checked = false;
                    RightEdgeRoiMove = false;
                }
                if (RightEdgeResizeBtn.Checked)
                {
                    RightEdgeResizeBtn.Checked = false;
                    RightEdgeRoiResize = false;
                }
            }

            if (currentTab.Text == "RightEdge")
            {
                if (VisualApp != null)
                {
                    RightEdgeRoiShowBtn.Checked = true;
                }

                if (UpEdgeMoveBtn.Checked)
                {
                    UpEdgeMoveBtn.Checked = false;
                    UpEdgeRoiMove = false;
                }
                if (UpEdgeResizeBtn.Checked)
                {
                    UpEdgeResizeBtn.Checked = false;
                    UpEdgeRoiResize = false;
                }

                if (DownEdgeMoveBtn.Checked)
                {
                    DownEdgeMoveBtn.Checked = false;
                    DownEdgeRoiMove = false;
                }
                if (DownEdgeResizeBtn.Checked)
                {
                    DownEdgeResizeBtn.Checked = false;
                    DownEdgeRoiResize = false;
                }

                if (LeftEdgeMoveBtn.Checked)
                {
                    LeftEdgeMoveBtn.Checked = false;
                    LeftEdgeRoiMove = false;
                }
                if (LeftEdgeResizeBtn.Checked)
                {
                    LeftEdgeResizeBtn.Checked = false;
                    LeftEdgeRoiResize = false;
                }

                if (RightEdgeMoveBtn.Checked)
                {
                    RightEdgeMoveBtn.Checked = false;
                    RightEdgeRoiMove = false;
                }
                if (RightEdgeResizeBtn.Checked)
                {
                    RightEdgeResizeBtn.Checked = false;
                    RightEdgeRoiResize = false;
                }
            }


        }

        private void VisualRoiTab_KeyDown(object sender, KeyEventArgs e)
        {
            if (VisualApp != null)
            {
                if (e.Modifiers == Keys.Control && UpEdgeMoveBtn.Checked)
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
                    _UpEdgeRoi = GetROI();
                }

                if (e.Modifiers == Keys.Control && UpEdgeResizeBtn.Checked)
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
                    _UpEdgeRoi = GetROI();
                }


                if (e.Modifiers == Keys.Control && DownEdgeMoveBtn.Checked)
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
                    _DownEdgeRoi = GetROI();
                }

                if (e.Modifiers == Keys.Control && DownEdgeResizeBtn.Checked)
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
                    _DownEdgeRoi = GetROI();
                }

                if (e.Modifiers == Keys.Control && LeftEdgeMoveBtn.Checked)
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
                    _LeftEdgeRoi = GetROI();
                }

                if (e.Modifiers == Keys.Control && LeftEdgeResizeBtn.Checked)
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
                    _LeftEdgeRoi = GetROI();
                }


                if (e.Modifiers == Keys.Control && RightEdgeMoveBtn.Checked)
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
                    _RightEdgeRoi = GetROI();
                }

                if (e.Modifiers == Keys.Control && RightEdgeResizeBtn.Checked)
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
                    _RightEdgeRoi = GetROI();
                }


            }

        }

        private void TemplateBtn_ClickAsync(object sender, EventArgs e)
        {
            if (VisualApp != null)
            {
                List<string> LinesFile = new List<string>();

                TabPage currentTab = VisualRoiTab.SelectedTab;

                List<RectangleF> ROIs = new List<RectangleF>();

                List<bool> Scans = new List<bool>();

                List<int> Scores = new List<int>();

                if (currentTab.Text == "UpEdge")
                {
                    LinesFile.Add(UpEdgefilepath);
                    ROIs.Add(_UpEdgeRoi);
                    Scans.Add(true);

                    UpEdgeScore = (int)(_Score * 100);
                    Scores.Add((int)(_Score * 100));

                    VisualApp.LineFindSave(UpEdgefilepath, (int)(_Score * 100), true);


                }
                else if (currentTab.Text == "DownEdge")
                {
                    LinesFile.Add(DownEdgefilepath);
                    ROIs.Add(_DownEdgeRoi);
                    Scans.Add(true);

                    DownEdgeScore = (int)(_Score * 100);
                    Scores.Add((int)(_Score * 100));

                    VisualApp.LineFindSave(DownEdgefilepath, (int)(_Score * 100), true);
                }
                else if (currentTab.Text == "LeftEdge")
                {
                    LinesFile.Add(LeftEdgefilepath);
                    ROIs.Add(_LeftEdgeRoi);
                    Scans.Add(false);

                    LeftEdgeScore = (int)(_Score * 100);
                    Scores.Add((int)(_Score * 100));

                    VisualApp.LineFindSave(LeftEdgefilepath, (int)(_Score * 100), false);
                }
                else if (currentTab.Text == "RightEdge")
                {
                    LinesFile.Add(RightEdgefilepath);
                    ROIs.Add(_RightEdgeRoi);
                    Scans.Add(false);

                    RightEdgeScore = (int)(_Score * 100);
                    Scores.Add((int)(_Score * 100));

                    VisualApp.LineFindSave(RightEdgefilepath, (int)(_Score * 100), false);
                }

                VisualApp.ContinuousGetImage(false);

                List<LineResult> Lines = new List<LineResult>();



                Lines = VisualApp.LineFindAsync(LinesFile, Scores, ROIs, Scans);

                if (Lines != null && CameraWindow != null)
                {
                    if (Lines.Count > 0)
                    {

                        CameraWindow.ClearGraphicDraw();

                        CameraWindow.GraphicDrawInit(Lines);

                        CameraWindow.GraphicDraw(Graphic.line, true);
                    }

                }


                VisualApp.ContinuousGetImage(true);

            }

        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            if (VisualApp != null)
            {
                VisualApp.ContinuousGetImage(false);

                List<int> Scores = new List<int>();

                List<string> LinesFile = new List<string>();

                TabPage currentTab = VisualRoiTab.SelectedTab;

                List<RectangleF> ROIs = new List<RectangleF>();
                List<bool> Scans = new List<bool>();

                LinesFile.Add(UpEdgefilepath);
                ROIs.Add(_UpEdgeRoi);
                Scans.Add(true);
                Scores.Add(UpEdgeScore);

                LinesFile.Add(DownEdgefilepath);
                ROIs.Add(_DownEdgeRoi);
                Scans.Add(true);
                Scores.Add(DownEdgeScore);

                LinesFile.Add(LeftEdgefilepath);
                ROIs.Add(_LeftEdgeRoi);
                Scans.Add(false);
                Scores.Add(LeftEdgeScore);

                LinesFile.Add(RightEdgefilepath);
                ROIs.Add(_RightEdgeRoi);
                Scans.Add(false);
                Scores.Add(RightEdgeScore);

                VisualApp.ContinuousGetImage(false);

                List<LineResult> Lines = new List<LineResult>();



                Lines = VisualApp.LineFindAsync(LinesFile, Scores, ROIs, Scans);

                if (Lines != null && CameraWindow != null)
                {
                    if (Lines.Count > 0)
                    {

                        CameraWindow.ClearGraphicDraw();

                        CameraWindow.GraphicDrawInit(Lines);

                        CameraWindow.GraphicDraw(Graphic.line, true);
                    }

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
