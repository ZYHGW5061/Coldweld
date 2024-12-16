
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
    public partial class VisualMatchControlGUI : XtraUserControl
    {
        #region Private File

        //private static readonly object _lockObj = new object();
        //private static volatile VisualMatchControlGUI _instance = null;
        //public static VisualMatchControlGUI Instance
        //{
        //    get
        //    {
        //        if (_instance == null)
        //        {
        //            lock (_lockObj)
        //            {
        //                if (_instance == null)
        //                {
        //                    _instance = new VisualMatchControlGUI();
        //                }
        //            }
        //        }
        //        return _instance;
        //    }
        //}

        VisualControlApplications VisualApp;

        CameraWindowGUI CameraWindow;

        public bool ImageShow = false;


        #endregion

        #region 算法参数

        private int _RingLightintensity = 0;
        private int _DirectLightintensity = 0;
        private float _Score = 0.5f;
        private int _MinAngle = -15;
        private int _MaxAngle = 15;

        private PointF Benchmark = new PointF(-1, -1);

        private RectangleF _TemplateRoi;
        private RectangleF _SearchRoi;

        bool TemplateRoiMove = false;
        bool TemplateRoiResize = false;
        bool SearchRoiMove = false;
        bool SearchRoiResize = false;
        bool OutlineMove = false;
        bool OutlineRotate = false;

        public int RoiMoveInterval = 10;
        public int RoiResizeInterval = 10;
        public float OutlineRotateInterval = 0.5f;

        private string _MatchTemplatefilepath;
        private string _MatchTemplateParampath;
        private string _MatchRunfilepath;

        private MatchTemplateResult _Templateresult = new MatchTemplateResult();
        private PointF _OutlineDeviation = new PointF();
        private float _OutlineAngle = 0.0f;

        public bool Inited = false;

        private MatchResult matchresult = null;

        #endregion

        #region 轮廓识别属性

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

        /// <summary>
        /// 角度范围
        /// </summary>
        public int AngleRange
        {
            get
            {
                return _MaxAngle;
            }
            set
            {
                AngleBar.Value = value;
                _MinAngle = -value;
                _MaxAngle = +value;
                AngleDeviationNumlabel.Text = value.ToString();
            }
        }

        /// <summary>
        /// 训练区域
        /// </summary>
        public RectangleFV TemplateRoi
        {
            get
            {
                RectangleFV rectangle = new RectangleFV();
                rectangle.X = _TemplateRoi.X;
                rectangle.Y = _TemplateRoi.Y;
                rectangle.Width = _TemplateRoi.Width;
                rectangle.Height = _TemplateRoi.Height;

                return rectangle;
            }
            set
            {
                RectangleF rectangle = new RectangleF(value.X, value.Y, value.Width, value.Height);
                _TemplateRoi = rectangle;
            }
        }

        /// <summary>
        /// 识别区域
        /// </summary>
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

        /// <summary>
        /// 训练结果
        /// </summary>
        public MatchTemplateResult Templateresult
        {
            get
            {
                return _Templateresult;
            }
            set
            {
                _Templateresult = value;
            }
        }

        /// <summary>
        /// 轮廓偏移
        /// </summary>
        public PointF OutlineDeviation
        {
            get
            {
                return _OutlineDeviation;
            }
            set
            {
                _OutlineDeviation = value;
            }
        }

        /// <summary>
        /// 轮廓角度
        /// </summary>
        public float OutlineAngle
        {
            get
            {
                return _OutlineAngle;
            }
            set
            {
                _OutlineAngle = value;
            }
        }

        /// <summary>
        /// 训练模板路径
        /// </summary>
        public string MatchTemplatefilepath
        {
            get
            {
                if (string.IsNullOrEmpty(_MatchTemplatefilepath))
                    return _MatchTemplatefilepath;

                Uri basePath = new Uri(AppDomain.CurrentDomain.BaseDirectory);
                Uri filePath = new Uri(_MatchTemplatefilepath);
                return basePath.MakeRelativeUri(filePath).ToString();
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _MatchTemplatefilepath = value;
                }
                else
                {
                    _MatchTemplatefilepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, value);
                }
            }
        }

        /// <summary>
        /// 训练参数路径
        /// </summary>
        public string MatchTemplateParampath
        {
            get
            {
                if (string.IsNullOrEmpty(_MatchTemplateParampath))
                    return _MatchTemplateParampath;

                Uri basePath = new Uri(AppDomain.CurrentDomain.BaseDirectory);
                Uri filePath = new Uri(_MatchTemplateParampath);
                return basePath.MakeRelativeUri(filePath).ToString();
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _MatchTemplateParampath = value;
                }
                else
                {
                    _MatchTemplateParampath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, value);
                }
            }
        }


        /// <summary>
        /// 识别参数路径
        /// </summary>
        public string MatchRunfilepath
        {
            get
            {
                if (string.IsNullOrEmpty(_MatchRunfilepath))
                    return _MatchRunfilepath;

                Uri basePath = new Uri(AppDomain.CurrentDomain.BaseDirectory);
                Uri filePath = new Uri(_MatchRunfilepath);
                return basePath.MakeRelativeUri(filePath).ToString();
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _MatchRunfilepath = value;
                }
                else
                {
                    _MatchRunfilepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, value);
                }
            }
        }

        /// <summary>
        /// 识别结果
        /// </summary>
        public MatchResult Matchresult
        {
            get
            { 
                return matchresult;
            }
            set
            {
                matchresult = value;
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
        public void MatchMove(float DeviationX, float DeviationY, float Angle)
        {
            if (ImageShow && CameraWindow != null)
                CameraWindow.MatchMove(DeviationX, DeviationY, Angle);
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


        public VisualMatchControlGUI()
        {
            InitializeComponent();

            _TemplateRoi = new RectangleF();
            _SearchRoi = new RectangleF();

            string currentDirectory = Directory.GetCurrentDirectory();
            string newFilePath = Path.Combine(currentDirectory, "MatchTemplate.contourmxml");
            _MatchTemplatefilepath = newFilePath;

            newFilePath = Path.Combine(currentDirectory, "MatchRun.contourmxml");
            _MatchRunfilepath = newFilePath;

            
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

            _TemplateRoi = GetROI();
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
                    RingLightNumlabel.Text = RingInt.ToString();
                    _RingLightintensity = RingInt;

                    DirectLightBar.Value = DirectInt;
                    DirectLightNumlabel.Text = DirectInt.ToString();
                    _DirectLightintensity = DirectInt;

                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void SetVisualParam(GlobalDataDefineClsLib.MatchIdentificationParam param)
        {
            this.RingLightintensity = param.RingLightintensity;
            this.DirectLightintensity = param.DirectLightintensity;
            if (param.Score == 0)
            {
                this.Score = 0.5f;
            }
            else
            {
                this.Score = param.Score;
            }
            if (param.MaxAngle == 0)
            {
                this.AngleRange = 15;
            }
            else
            {
                this.AngleRange = param.MaxAngle;
            }
            _TemplateRoi = new RectangleF(CameraWindow.ImageWidth / 3, CameraWindow.ImageHeight / 3, CameraWindow.ImageWidth / 3, CameraWindow.ImageHeight / 3);
            _SearchRoi = new RectangleF(CameraWindow.ImageWidth / 3, CameraWindow.ImageHeight / 3, CameraWindow.ImageWidth / 3, CameraWindow.ImageHeight / 3);
            if (param.TemplateRoi == null || (param.TemplateRoi.X == 0 && param.TemplateRoi.Y == 0 && param.TemplateRoi.Width == 0 && param.TemplateRoi.Height == 0))
            {
                this.TemplateRoi = new RectangleFV() { X = _TemplateRoi.X, Y = _TemplateRoi.Y, Height = _TemplateRoi.Height, Width = _TemplateRoi.Width };
            }
            else
            {
                this.TemplateRoi = param.TemplateRoi;
            }
            if (param.SearchRoi == null || (param.SearchRoi.X == 0 && param.SearchRoi.Y == 0 && param.SearchRoi.Width == 0 && param.SearchRoi.Height == 0))
            {
                this.SearchRoi = new RectangleFV() { X = _SearchRoi.X, Y = _SearchRoi.Y, Height = _SearchRoi.Height, Width = _SearchRoi.Width };
            }
            else
            {
                this.SearchRoi = param.SearchRoi;
            }
            //this.Templateresult = param.Templateresult;
            this.MatchTemplatefilepath = param.Templatexml;
            this.MatchTemplateParampath = param.TemplateParamxml;
            if (param.TemplateParamxml == null)
            {
                this.MatchTemplateParampath = "";
            }
            else
            {
                this.MatchTemplateParampath = param.TemplateParamxml;
            }
            this.MatchRunfilepath = param.Runxml;
            if (param.Runxml == null)
            {
                this.MatchRunfilepath = "";
            }
            else
            {
                this.MatchRunfilepath = param.Runxml;
            }
        }

        public GlobalDataDefineClsLib.MatchIdentificationParam GetVisualParam()
        {
            GlobalDataDefineClsLib.MatchIdentificationParam param1 = new GlobalDataDefineClsLib.MatchIdentificationParam();

            param1.RingLightintensity = this.RingLightintensity;
            param1.DirectLightintensity = this.DirectLightintensity;
            param1.Score = this.Score;
            param1.MaxAngle = this.AngleRange;
            param1.MinAngle = -this.AngleRange;
            param1.TemplateRoi = this.TemplateRoi;
            param1.SearchRoi = this.SearchRoi;
            //param1.Templateresult = this.Templateresult;
            param1.Templatexml = this.MatchTemplatefilepath;
            param1.Runxml = this.MatchRunfilepath;

            return param1;
        }




        private void RingLightBar_Scroll(object sender, EventArgs e)
        {
            int value = RingLightBar.Value;
            if(VisualApp != null)
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

        private void AngleBar_Scroll(object sender, EventArgs e)
        {
            _MinAngle = -(AngleBar.Value);
            _MaxAngle = +(AngleBar.Value);

            AngleDeviationNumlabel.Text = _MaxAngle.ToString();
        }

        private void TemplateRoiShowBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (VisualApp != null)
            {
                CameraWindowShowInit(_TemplateRoi);
                CameraWindowShow(Graphic.rectRoi, TemplateRoiShowBtn.Checked);
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
        private void OutlineShowBtn_CheckedChanged(object sender, EventArgs e)
        {
            //if (VisualApp != null)
            //{
            //    if (_Templateresult != null)
            //    {
            //        CameraWindow.GraphicDrawInit(_Templateresult.Points, _Templateresult.Center);

            //        CameraWindow.GraphicDraw(Graphic.train, OutlineShowBtn.Checked);
            //    }
            //}
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

            if(currentTab.Text == "轮廓区域")
            {
                if (VisualApp != null)
                {
                    TemplateRoiShowBtn.Checked = true;

                    SearchAreaRoiShowBtn.Checked = false;
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
            if (currentTab.Text == "搜索区域")
            {
                if (VisualApp != null)
                {
                    TemplateRoiShowBtn.Checked = false;

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
            if (currentTab.Text == "训练参数")
            {
                
                ContourPatCreateScaleMode _ScaleMode = ContourPatCreateScaleMode.Auto;
                float _ScaleLevel = 5;
                uint _ScaleRLevel = 1;
                ContourPatCreateThresholdMode _ThresMode = ContourPatCreateThresholdMode.Auto;
                uint _ThresValue = 15;
                ContourPatCreateWeightFlag _WeightFlag = ContourPatCreateWeightFlag.False;
                ContourPatCreateChainFlag _ChainFlag = ContourPatCreateChainFlag.Auto;
                int _MinChain = 4;

                VisualApp.Algorithm.TrainLoadPara(MatchTemplateParampath);

                VisualApp.Algorithm.MatchGetTrainPara(ref _ScaleMode, ref _ScaleLevel, ref _ScaleRLevel,
            ref _ThresMode, ref _ThresValue, ref _WeightFlag,
            ref _ChainFlag, ref _MinChain);

                ContourPatternCreateParam param = new ContourPatternCreateParam()
                {
                    ScaleMode = _ScaleMode,
                    ScaleLevel = _ScaleLevel,
                    ScaleRLevel = _ScaleRLevel,
                    ThresMode = _ThresMode,
                    ThresValue = _ThresValue,
                    WeightFlag = _WeightFlag,
                    ChainFlag = _ChainFlag,
                    MinChain = _MinChain,
                };

                UpdatePatCreateParasView(param);
            }
            if(currentTab.Text == "识别参数")
            {
                float Score = 0;
                int Num = 0;
                int sortnum = 0;
                SortType sort = SortType.Score;

                VisualApp.Algorithm.MatchLoadRunPara(_MatchRunfilepath);

                VisualApp.Algorithm.MatchGetRunPara<float>(MatchParas.MinScore, ref Score);
                VisualApp.Algorithm.MatchGetRunPara<int>(MatchParas.MaxMatchNum, ref Num);
                VisualApp.Algorithm.MatchGetRunPara<int>(MatchParas.SortType, ref sortnum);
                sort = (SortType)sortnum;

                UpdateRunPatCreateParasView(Score, Num, sort);

            }
        }

        /// <summary>
        /// 更新界面显示的建模参数
        /// </summary>
        /// <param name="patternCreateParas"></param>
        private void UpdatePatCreateParasView(ContourPatternCreateParam patternCreateParas)
        {
            this.comboBoxGranFlag.SelectedIndex = Convert.ToInt32(patternCreateParas.ScaleMode);
            this.textBoxRoughValue.Value = (decimal)(float)patternCreateParas.ScaleLevel;
            this.textBoxFineValue.Value = (decimal)patternCreateParas.ScaleRLevel;
            this.comboBoxThresFlag.SelectedIndex = Convert.ToInt32(patternCreateParas.ThresMode);
            this.comboBoxWeigthMaskFlag.SelectedIndex = Convert.ToInt32(patternCreateParas.WeightFlag);
            this.comboBoxChainFlag.SelectedIndex = Convert.ToInt32(patternCreateParas.ChainFlag);
            this.textBoxMinChain.Value = (decimal)patternCreateParas.MinChain;
            this.textBoxThresValue.Value = (decimal)patternCreateParas.ThresValue;
        }

        /// <summary>
        /// 从界面配置更新至Para对象
        /// </summary>
        /// <param name="patternCreateParas"></param>
        private void UpdatePatCreateParas(ref ContourPatternCreateParam patternCreateParas)
        {
            patternCreateParas.ScaleMode = (ContourPatCreateScaleMode)Enum.Parse(typeof(ContourPatCreateScaleMode), this.comboBoxGranFlag.SelectedIndex.ToString());
            patternCreateParas.ScaleLevel = Convert.ToSingle(this.textBoxRoughValue.Text.Trim().TrimEnd('\0'));
            patternCreateParas.ScaleRLevel = Convert.ToUInt32(this.textBoxFineValue.Text.Trim().TrimEnd('\0'));
            patternCreateParas.ThresMode = (ContourPatCreateThresholdMode)Enum.Parse(typeof(ContourPatCreateThresholdMode), this.comboBoxThresFlag.SelectedIndex.ToString());
            patternCreateParas.ThresValue = Convert.ToUInt32(Convert.ToDouble(this.textBoxThresValue.Text.Trim().TrimEnd('\0')));
            patternCreateParas.WeightFlag = (ContourPatCreateWeightFlag)Enum.Parse(typeof(ContourPatCreateWeightFlag), this.comboBoxWeigthMaskFlag.SelectedIndex.ToString());
            patternCreateParas.ChainFlag = (ContourPatCreateChainFlag)Enum.Parse(typeof(ContourPatCreateChainFlag), this.comboBoxChainFlag.SelectedIndex.ToString());
            patternCreateParas.MinChain = Convert.ToInt32(Convert.ToDouble(this.textBoxMinChain.Text.Trim().TrimEnd('\0')));
        }

        private void SetTrainParam()
        {
            ContourPatternCreateParam param = new ContourPatternCreateParam()
            {
                ScaleMode = ContourPatCreateScaleMode.Auto,
                ScaleLevel = 5,
                ScaleRLevel = 1,
                ThresMode = ContourPatCreateThresholdMode.Auto,
                ThresValue = 15,
                WeightFlag = ContourPatCreateWeightFlag.False,
                ChainFlag = ContourPatCreateChainFlag.Auto,
                MinChain = 4,
            };

            UpdatePatCreateParas(ref param);

            VisualApp.Algorithm.MatchSetTrainPara(param.ScaleMode, param.ScaleLevel, param.ScaleRLevel, param.ThresMode, param.ThresValue, param.WeightFlag, param.ChainFlag, param.MinChain);

            VisualApp.Algorithm.TrainSavePara(MatchTemplateParampath);
            
        }

        /// <summary>
        /// 更新界面显示的建模参数
        /// </summary>
        /// <param name="patternCreateParas"></param>
        private void UpdateRunPatCreateParasView(float Score, int Num, SortType sort)
        {
            this.textMinScore.Value = (decimal)Score;
            this.textMaxMatchNum.Value = (decimal)Num;
            this.comboBoxSortType.SelectedIndex = Convert.ToInt32(sort)-1;
        }

        /// <summary>
        /// 从界面配置更新至Para对象
        /// </summary>
        /// <param name="patternCreateParas"></param>
        private void UpdateRunPatCreateParas(ref float Score, ref uint Num, ref SortType sort)
        {
            if(this.comboBoxSortType.SelectedIndex<1)
            {
                sort = SortType.Score;
            }
            else
            {
                sort = (SortType)Enum.Parse(typeof(SortType), (this.comboBoxSortType.SelectedIndex+1).ToString());
            }

            Score = Convert.ToSingle(this.textMinScore.Text.Trim().TrimEnd('\0'));
            Num = Convert.ToUInt32(this.textMaxMatchNum.Text.Trim().TrimEnd('\0'));
        }

        private void SetRunParam()
        {
            float Score = 0;
            uint Num = 0;
            SortType sort = SortType.Score;

            UpdateRunPatCreateParas(ref Score, ref Num, ref sort);

            VisualApp.Algorithm.MatchSetRunPara<float>( MatchParas.MinScore, Score);
            VisualApp.Algorithm.MatchSetRunPara<uint>(MatchParas.MaxMatchNum, Num);
            VisualApp.Algorithm.MatchSetRunPara<SortType>(MatchParas.SortType, sort);

            VisualApp.Algorithm.MatchSaveRunPara(_MatchRunfilepath);
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
                    _TemplateRoi = GetROI();
                }

                if (e.Modifiers == Keys.Control && TemplateResizeBtn.Checked)
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
                    _TemplateRoi = GetROI();
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

        private void TemplateBtn_ClickAsync(object sender, EventArgs e)
        {
            //Task.Factory.StartNew(new Action(() =>
            //{

            if (VisualApp != null)
            {
                VisualApp.ContinuousGetImage(false);

                VisualApp.Algorithm.TrainLoadPara(MatchTemplateParampath);

                MatchTemplateResult result = new MatchTemplateResult();

                if (Benchmark != null && Benchmark.X > 0 && Benchmark.Y > 0)
                {
                    result = VisualApp.MatchTemplateAsync(_MatchTemplatefilepath, Benchmark, _TemplateRoi);
                }
                else
                {
                    result = VisualApp.MatchTemplateAsync(_MatchTemplatefilepath, PointF.Empty, _TemplateRoi);

                    if (result != null)
                    {
                        Benchmark = result.Center;
                        BenchmarkXtextBox.Text = ((int)Benchmark.X).ToString();
                        BenchmarkYtextBox.Text = ((int)Benchmark.Y).ToString();
                    }
                }

                VisualApp.Algorithm.TrainSavePara(MatchTemplateParampath);

                //result = await VisualApp.MatchTemplateAsync(_MatchTemplatefilepath, PointF.Empty, _TemplateRoi);



                if (result != null && CameraWindow != null)
                {
                    _Templateresult = result;

                    _OutlineDeviation = result.Deviation;
                    _OutlineAngle = result.Angle;

                    CameraWindow.ClearGraphicDraw();

                    CameraWindow.GraphicDrawInit(result.Points, result.Center);



                    CameraWindow.GraphicDraw(Graphic.train, true);
                }

                VisualApp.ContinuousGetImage(true);
            }
            //}));

        }

        private void SearchBtn_Click(object sender, EventArgs e)
        {
            //Task.Factory.StartNew(new Action(() =>
            //{
            if (VisualApp != null)
            {
                List<MatchResult> results = new List<MatchResult>();

                VisualApp.ContinuousGetImage(false);

                bool Done = VisualApp.MatchFindInit(_MatchTemplatefilepath);

                Done = VisualApp.LoadMatchRunXml(_MatchRunfilepath);

                results = VisualApp.MatchFindAsync(_Score, _MinAngle, _MaxAngle, _SearchRoi);

                Done = VisualApp.SaveMatchRunXml(_MatchRunfilepath);

                if (results != null && CameraWindow != null && results.Count > 0)
                {
                    matchresult = results[0];

                    CameraWindow.ClearGraphicDraw();

                    CameraWindow.GraphicDrawInit(results);

                    CameraWindow.GraphicDraw(Graphic.match, true);
                }

                VisualApp.ContinuousGetImage(true);

            }
            //}));
        }

        private void CameraWindowClearBtn_Click(object sender, EventArgs e)
        {
            if (VisualApp != null)
            {
                CameraWindowClear();
            }
        }

        private void SetBenchmarkBtn_Click(object sender, EventArgs e)
        {
            //BenchmarkXtextBox.Text = ((int)Benchmark.X).ToString();
            //BenchmarkYtextBox.Text = ((int)Benchmark.Y).ToString();

            if(BenchmarkXtextBox.Text != null && BenchmarkYtextBox.Text != null)
            {
                float xValue = float.Parse(BenchmarkXtextBox.Text);
                float yValue = float.Parse(BenchmarkYtextBox.Text);
                if ((xValue > 0 && xValue < CameraWindow.ImageWidth) && (yValue > 0 && yValue < CameraWindow.ImageWidth))
                {
                    Benchmark.X = xValue;
                    Benchmark.Y = yValue;
                }


            }

            
        }

        private void comboBoxGranFlag_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetTrainParam();
        }

        private void textBoxRoughValue_EditValueChanged(object sender, EventArgs e)
        {
            SetTrainParam();
        }

        private void textBoxFineValue_EditValueChanged(object sender, EventArgs e)
        {
            SetTrainParam();
        }

        private void comboBoxThresFlag_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetTrainParam();
        }

        private void textBoxThresValue_EditValueChanged(object sender, EventArgs e)
        {
            SetTrainParam();
        }

        private void comboBoxWeigthMaskFlag_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetTrainParam();
        }

        private void comboBoxChainFlag_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetTrainParam();
        }

        private void textBoxMinChain_EditValueChanged(object sender, EventArgs e)
        {
            SetTrainParam();
        }

        private void textMinScore_EditValueChanged(object sender, EventArgs e)
        {
            SetRunParam();
        }

        private void textMaxMatchNum_EditValueChanged(object sender, EventArgs e)
        {
            SetRunParam();
        }

        private void comboBoxSortType_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetRunParam();
        }
    }
}
