using CameraControllerClsLib;
using CommonPanelClsLib;
using ConfigurationClsLib;
using GlobalDataDefineClsLib;
using StageControllerClsLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisionClsLib;
using VisionControlAppClsLib;
using VisionGUI;

namespace SecondaryCalibrationClsLib
{
    public class UplookCameraSecondaryCalibration
    {


        #region private file

        private static readonly object _lockObj = new object();
        private static volatile UplookCameraSecondaryCalibration _instance = null;
        public static UplookCameraSecondaryCalibration Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new UplookCameraSecondaryCalibration();
                        }
                    }
                }
                return _instance;
            }
        }



        private StageCore stage = StageControllerClsLib.StageCore.Instance;
        private SystemConfiguration _systemConfig
        {
            get { return SystemConfiguration.Instance; }
        }
        private VisionControlAppClsLib.VisualControlManager _VisualManager
        {
            get { return VisionControlAppClsLib.VisualControlManager.Instance; }
        }
        public VisualControlApplications BondCameraVisual
        {
            get { return _VisualManager.GetCameraByID(EnumCameraType.BondCamera); }
        }
        public VisualControlApplications UplookingCameraVisual
        {
            get { return _VisualManager.GetCameraByID(EnumCameraType.UplookingCamera); }
        }
        public VisualControlApplications WaferCameraVisual
        {
            get { return _VisualManager.GetCameraByID(EnumCameraType.WaferCamera); }
        }
        private CameraConfig _BondcameraConfig
        {
            get { return CameraManager.Instance.GetCameraConfigByID(EnumCameraType.BondCamera); }
        }
        private CameraConfig _UplookingcameraConfig
        {
            get { return CameraManager.Instance.GetCameraConfigByID(EnumCameraType.UplookingCamera); }
        }
        private CameraConfig _WafercameraConfig
        {
            get { return CameraManager.Instance.GetCameraConfigByID(EnumCameraType.WaferCamera); }
        }


        #endregion


        #region public file

        public double BondZOffset { get; set; } = 5;

        public double AutofocusRange { get; set; } = 5;

        public double AutofocusInterval { get; set; } = 0.5;

        /// <summary>
        /// 校准坐标 一般是移动到仰视相机中心的坐标
        /// </summary>
        public XYZTCoordinateConfig CalibrationLocation { get; set; }

        /// <summary>
        /// XYZThera偏移量
        /// </summary>
        public XYZTOffsetConfig XYZToffset { get; set; }

        #endregion

        #region private method

        private static (double, double) ImageToXY(PointF point, PointF center, float pixelsizex, float pixelsizey)
        {
            double X = (point.X - center.X) * pixelsizex;

            double Y = (point.Y - center.Y) * pixelsizey;

            return (X, Y);

        }


        private int ShowVisualForm(UserControl visualControlGUI, string Name, string title)
        {
            try
            {
                int REF = -1;
                using (VisualControlForm VForm = new VisualControlForm())
                {
                    VForm.InitializeGui(visualControlGUI);

                    //string hh = VForm.showMessage(Name, title, true);
                    //if (hh == "next")
                    //{
                    //    REF = 1;
                    //}
                    //else
                    //{
                    //    REF = 0;
                    //}
                }
                return REF;
            }
            catch
            {
                return -1;
            }
        }

        private void ShowCameraForm(bool show)
        {
            if (show)
            {
                CameraWindowForm.Instance.Show();
            }
            else
            {
                CameraWindowForm.Instance.Hide();
            }
        }

        private int ShowMessage(string title, string content, string type)
        {
            if (WarningBox.FormShow(title, content, type) == 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        private (double, double, double, bool) ShowCameraParamForm(string text3 = "Bond相机参数设置", double ImageWidthPixelsize = 1, double ImageHeightPixelsize = 1, double Angle = 0)
        {
            return CameraParamBox.FormShow(text3, ImageWidthPixelsize, ImageHeightPixelsize, Angle);
        }


        /// <summary>
        /// 榜头移动
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="Z"></param>
        private void BondXYZAbsoluteMove(double X, double Y, double Z)
        {
            stage.AbloluteMoveSync(new EnumStageAxis[3] { EnumStageAxis.BondX, EnumStageAxis.BondY, EnumStageAxis.BondZ }, new double[3] { X, Y, Z });

        }

        private void AxisAbsoluteMove(EnumStageAxis axis, double target)
        {
            stage.AbloluteMoveSync(axis, target);
        }

        private void AxisRelativeMove(EnumStageAxis axis, double target)
        {
            stage.RelativeMoveSync(axis, (float)target);
        }

        private double ReadCurrentAxisposition(EnumStageAxis axis)
        {
            double position = stage.GetCurrentPosition(axis);
            return position;
        }

        private void AxisHome(EnumStageAxis axis)
        {
            stage.Home(axis);
        }


        /// <summary>
        /// 识别中心
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        private MatchResult IdentificationAsync(MatchIdentificationParam param)
        {
            if (!(CameraWindowForm.Instance.IsHandleCreated && CameraWindowForm.Instance.Visible))
            {
                CameraWindowGUI.Instance.SelectCamera(1);
                CameraWindowForm.Instance.Show();
            }

            UplookingCameraVisual.SetDirectLightintensity(param.DirectLightintensity);
            UplookingCameraVisual.SetRingLightintensity(param.RingLightintensity);

            bool Done = UplookingCameraVisual.LoadMatchTrainXml(param.Templatexml);

            Done = UplookingCameraVisual.LoadMatchRunXml(param.Runxml);

            //var result = await UplookingCameraVisual.MatchFindAsync(param.Score, param.MinAngle, param.MaxAngle, param.SearchRoi);

            Bitmap bitmap = UplookingCameraVisual.GetBitmap();

            bool En1 = UplookingCameraVisual.MatchSetRunPara<float>(MatchParas.MinScore, 0.2f);
            bool En2 = UplookingCameraVisual.MatchSetRunPara<int>(MatchParas.AngleStart, param.MinAngle);
            bool En3 = UplookingCameraVisual.MatchSetRunPara<int>(MatchParas.AngleEnd, param.MaxAngle);

            if (!(En1 && En2 && En3))
            {
                return null;
            }

            List<MatchResult> results = new List<MatchResult>();

            Done = UplookingCameraVisual.MatchRun(bitmap, param.Score, ref results, param.SearchRoi);



            if (results != null && results.Count > 0)
            {
                if(CameraWindowGUI.Instance != null)
                {
                    CameraWindowGUI.Instance.ClearGraphicDraw();

                    CameraWindowGUI.Instance.GraphicDrawInit(results);

                    CameraWindowGUI.Instance.GraphicDraw(Graphic.match, true);
                    

                }
                if (results[0].IsOk)
                {
                    return results[0];

                }



            }


            return null;
        }

        private List<LineResult> IdentificationAsync(LineFindIdentificationParam param)
        {
            if (!(CameraWindowForm.Instance.IsHandleCreated && CameraWindowForm.Instance.Visible))
            {
                CameraWindowGUI.Instance.SelectCamera(1);
                CameraWindowForm.Instance.Show();
            }

            UplookingCameraVisual.SetDirectLightintensity(param.DirectLightintensity);
            UplookingCameraVisual.SetRingLightintensity(param.RingLightintensity);

            List<int> Scores = new List<int>();

            List<string> LinesFile = new List<string>();

            List<RectangleF> ROIs = new List<RectangleF>();
            List<bool> Scans = new List<bool>();


            LinesFile.Add(param.UpEdgefilepath);
            ROIs.Add(new RectangleF(param.UpEdgeRoi.X, param.UpEdgeRoi.Y, param.UpEdgeRoi.Width, param.UpEdgeRoi.Height));
            Scans.Add(true);
            Scores.Add(param.UpEdgeScore);

            LinesFile.Add(param.DownEdgefilepath);
            ROIs.Add(new RectangleF(param.DownEdgeRoi.X, param.DownEdgeRoi.Y, param.DownEdgeRoi.Width, param.DownEdgeRoi.Height));
            Scans.Add(true);
            Scores.Add(param.DownEdgeScore);

            LinesFile.Add(param.LeftEdgefilepath);
            ROIs.Add(new RectangleF(param.LeftEdgeRoi.X, param.LeftEdgeRoi.Y, param.LeftEdgeRoi.Width, param.LeftEdgeRoi.Height));
            Scans.Add(false);
            Scores.Add(param.LeftEdgeScore);

            LinesFile.Add(param.RightEdgefilepath);
            ROIs.Add(new RectangleF(param.RightEdgeRoi.X, param.RightEdgeRoi.Y, param.RightEdgeRoi.Width, param.RightEdgeRoi.Height));
            Scans.Add(false);
            Scores.Add(param.RightEdgeScore);

            UplookingCameraVisual.ContinuousGetImage(false);

            List<LineResult> Lines = new List<LineResult>();



            Lines = UplookingCameraVisual.LineFindAsync(LinesFile, Scores, ROIs, Scans);

            if (Lines != null && Lines.Count > 0)
            {
                if (CameraWindowGUI.Instance != null)
                {
                    CameraWindowGUI.Instance.ClearGraphicDraw();

                    CameraWindowGUI.Instance.GraphicDrawInit(Lines);

                    CameraWindowGUI.Instance.GraphicDraw(Graphic.line, true);


                }
                return Lines;
            }


            return null;
        }


        private XYZTOffsetConfig UplookingCameraIdentificationAsync(MatchIdentificationParam param)
        {
            double BondX = ReadCurrentAxisposition(EnumStageAxis.BondX);
            double BondY = ReadCurrentAxisposition(EnumStageAxis.BondY);
            double BondZ = ReadCurrentAxisposition(EnumStageAxis.BondZ);

            BondX = CalibrationLocation.X;
            BondY = CalibrationLocation.Y;
            BondZ = CalibrationLocation.Z;


            AxisAbsoluteMove(EnumStageAxis.BondZ, BondZ + BondZOffset);

            BondXYZAbsoluteMove(BondX, BondY, BondZ + BondZOffset);

            AxisAbsoluteMove(EnumStageAxis.BondZ, BondZ);

            MatchResult result = new MatchResult();

            result = IdentificationAsync(param);

            double Xoffset, Yoffset, Theraoffset;
            (Xoffset, Yoffset) = ImageToXY(new PointF(result.MatchBox.Benchmark.X, result.MatchBox.Benchmark.Y), new PointF(_UplookingcameraConfig.ImageSizeWidth / 2, _UplookingcameraConfig.ImageSizeHeight / 2), _UplookingcameraConfig.WidthPixelSize, _UplookingcameraConfig.HeightPixelSize);
            Theraoffset = result.MatchBox.Angle;


            return new XYZTOffsetConfig() { X = Xoffset, Y = Yoffset, Theta = Theraoffset };

        }

        private XYZTOffsetConfig UplookingCameraIdentificationAsync(LineFindIdentificationParam param)
        {
            double BondX = ReadCurrentAxisposition(EnumStageAxis.BondX);
            double BondY = ReadCurrentAxisposition(EnumStageAxis.BondY);
            double BondZ = ReadCurrentAxisposition(EnumStageAxis.BondZ);

            BondX = CalibrationLocation.X;
            BondY = CalibrationLocation.Y;
            BondZ = CalibrationLocation.Z;


            AxisAbsoluteMove(EnumStageAxis.BondZ, BondZ + BondZOffset);

            BondXYZAbsoluteMove(BondX, BondY, BondZ + BondZOffset);

            AxisAbsoluteMove(EnumStageAxis.BondZ, BondZ);

            List<LineResult> result = new List<LineResult>();

            result = IdentificationAsync(param);

            RectangleFA rectangle = UplookingCameraVisual.RectEdgeCalculate(result[0], result[1], result[2], result[3]);

            double Xoffset, Yoffset, Theraoffset;
            (Xoffset, Yoffset) = ImageToXY(new PointF(rectangle.Center.X, rectangle.Center.Y), new PointF(_UplookingcameraConfig.ImageSizeWidth / 2, _UplookingcameraConfig.ImageSizeHeight / 2), _UplookingcameraConfig.WidthPixelSize, _UplookingcameraConfig.HeightPixelSize);
            Theraoffset = rectangle.Angle;


            return new XYZTOffsetConfig() { X = Xoffset, Y = Yoffset, Theta = Theraoffset };
        }

        /// <summary>
        /// 仰视相机创建识别
        /// </summary>
        private MatchIdentificationParam UplookingCameraIdentifymarkpointsManual(MatchIdentificationParam param)
        {

            double BondX = ReadCurrentAxisposition(EnumStageAxis.BondX);
            double BondY = ReadCurrentAxisposition(EnumStageAxis.BondY);
            double BondZ = ReadCurrentAxisposition(EnumStageAxis.BondZ);

            BondX = CalibrationLocation.X;
            BondY = CalibrationLocation.Y;
            BondZ = CalibrationLocation.Z;

            string name = "仰视相机识别";
            string title = "";
            VisualMatchControlGUI visualMatch = new VisualMatchControlGUI();
            visualMatch.InitVisualControl(CameraWindowGUI.Instance, UplookingCameraVisual);

            visualMatch.SetVisualParam(param);


            AxisAbsoluteMove(EnumStageAxis.BondZ, BondZ + BondZOffset);

            BondXYZAbsoluteMove(BondX, BondY, BondZ + BondZOffset);

            AxisAbsoluteMove(EnumStageAxis.BondZ, BondZ);

            //if (!(VisualControlForm.Instance.IsHandleCreated && VisualControlForm.Instance.Visible))
            //{
            //    int Done = ShowVisualForm(visualMatch, name, title);

            //    if (Done == 0)
            //    {
            //        return null;
            //    }
            //    else
            //    {
            //        param = visualMatch.GetVisualParam();
            //    }
            //}

            return param;

        }

        /// <summary>
        /// 仰视相机创建识别
        /// </summary>
        private LineFindIdentificationParam UplookingCameraIdentifymarkpointsManual(LineFindIdentificationParam param)
        {

            double BondX = ReadCurrentAxisposition(EnumStageAxis.BondX);
            double BondY = ReadCurrentAxisposition(EnumStageAxis.BondY);
            double BondZ = ReadCurrentAxisposition(EnumStageAxis.BondZ);

            BondX = CalibrationLocation.X;
            BondY = CalibrationLocation.Y;
            BondZ = CalibrationLocation.Z;

            string name = "仰视相机识别";
            string title = "";
            VisualLineFindControlGUI visualMatch = new VisualLineFindControlGUI();
            visualMatch.InitVisualControl(CameraWindowGUI.Instance, UplookingCameraVisual);

            visualMatch.SetVisualParam(param);


            AxisAbsoluteMove(EnumStageAxis.BondZ, BondZ + BondZOffset);

            BondXYZAbsoluteMove(BondX, BondY, BondZ + BondZOffset);

            AxisAbsoluteMove(EnumStageAxis.BondZ, BondZ);

            //if (!(VisualControlForm.Instance.IsHandleCreated && VisualControlForm.Instance.Visible))
            //{
            //    int Done = ShowVisualForm(visualMatch, name, title);

            //    if (Done == 0)
            //    {
            //        return null;
            //    }
            //    else
            //    {
            //        param = visualMatch.GetVisualParam();
            //    }
            //}

            return param;

        }

        #endregion


        #region public method

        /// <summary>
        /// 仰视相机校准 轮廓识别
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public XYZTOffsetConfig RunAsync(MatchIdentificationParam param)
        {
            try
            {
                Task.Factory.StartNew(new Action(async () =>
                {
                    XYZToffset = UplookingCameraIdentificationAsync(param);
                }));

            }
            catch
            {

            }
            
            return XYZToffset;
        }
        /// <summary>
        /// 仰视相机校准 边缘识别
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public XYZTOffsetConfig RunAsync(LineFindIdentificationParam param)
        {
            try
            {
                Task.Factory.StartNew(new Action(async () =>
                {
                    XYZToffset = UplookingCameraIdentificationAsync(param);
                }));

            }
            catch
            {

            }
            return XYZToffset;
        }

        /// <summary>
        /// 仰视相机创建识别 轮廓
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public MatchIdentificationParam CreationMatchProcess(MatchIdentificationParam param)
        {
            try
            {
                Task.Factory.StartNew(new Action(async () =>
                {
                    param = UplookingCameraIdentifymarkpointsManual(param);
                }));

            }
            catch
            {

            }

            return param;
        }

        /// <summary>
        /// 仰视相机创建识别 边缘
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public LineFindIdentificationParam CreationMatchProcess(LineFindIdentificationParam param)
        {
            try
            {
                Task.Factory.StartNew(new Action(async () =>
                {
                    param = UplookingCameraIdentifymarkpointsManual(param);
                }));

            }
            catch
            {

            }

            return param;
        }

        #endregion




    }
}
