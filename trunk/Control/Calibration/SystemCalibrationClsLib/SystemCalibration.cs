using CameraControllerClsLib;
using CommonPanelClsLib;
using ConfigurationClsLib;
using GlobalDataDefineClsLib;
using HardwareManagerClsLib;
using LaserSensorControllerClsLib;
using PositioningSystemClsLib;
using StageControllerClsLib;
using StageCtrlPanelLib;
using StageManagerClsLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisionClsLib;
using VisionControlAppClsLib;
using VisionGUI;
using WestDragon.Framework.UtilityHelper;

namespace SystemCalibrationClsLib
{
    public class SystemCalibration
    {

        #region Private File

        private static readonly object _lockObj = new object();
        private static volatile SystemCalibration _instance = null;
        public static SystemCalibration Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new SystemCalibration();
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// STAGE
        /// </summary>
        private IStageController _stageEngine
        {
            get { return StageManager.Instance.GetCurrentController(); }
        }

        private StageCore stage = StageControllerClsLib.StageCore.Instance;
        /// <summary>
        /// 定位系统
        /// </summary>
        private PositioningSystem _positioningSystem
        {
            get { return PositioningSystem.Instance; }
        }

        private CameraConfig _TrackCameraConfig
        {
            get { return CameraManager.Instance.GetCameraConfigByID(EnumCameraType.TrackCamera); }
        }
        private CameraConfig _WeldCameraConfig
        {
            get { return CameraManager.Instance.GetCameraConfigByID(EnumCameraType.WeldCamera); }
        }
       
        private SystemConfiguration _systemConfig
        {
            get { return SystemConfiguration.Instance; }
        }

        private VisionControlAppClsLib.VisualControlManager _VisualManager
        {
            get { return VisionControlAppClsLib.VisualControlManager.Instance; }
        }
        public VisualControlApplications TrackCameraVisual
        {
            get { return _VisualManager.GetCameraByID(EnumCameraType.TrackCamera); }
        }
        public VisualControlApplications WeldCameraVisual
        {
            get { return _VisualManager.GetCameraByID(EnumCameraType.WeldCamera); }
        }
        

        //private ILaserSensorController _laserSensor
        //{
        //    get { return HardwareManager.Instance.LaserSensor; }
        //}



        VisualControlForm VForm;

        #endregion

        #region Public File

        public double BondZOffset { get; set; } = 10;

        public double AutofocusRange { get; set; } = 5;

        public double AutofocusInterval { get; set; } = 0.5;


        public float Score { get; set; }
        public int MinAngle { get; set; }
        public int MaxAngle { get; set; }




        #endregion

        #region Private Mothed

        public static (double, double) ImageToXY(PointF point, PointF center, float pixelsizex, float pixelsizey)
        {
            double X = (point.X - center.X) * pixelsizex;

            double Y = (point.Y - center.Y) * pixelsizey;

            return (X, Y);

        }

        public static List<(double x, double y)> GetRectangleCoordinates(int rows, int columns, double width, double height)
        {
            List<(double x, double y)> coordinates = new List<(double x, double y)>();

            // Calculate the width and height of each subdivided rectangle
            double cellWidth = width / (columns - 1);
            double cellHeight = height / (rows - 1);

            // Calculate the starting point (top-left corner of the rectangle)
            double startX = -width / 2;
            double startY = height / 2;

            // Calculate all coordinates
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    double x = startX + col * cellWidth;
                    double y = startY - row * cellHeight;
                    coordinates.Add((x, y));
                }
            }

            return coordinates;
        }

        public static (double pixelsizeX, double pixelsizeY, double angle) CalculateCameraParameters(
        List<(double xs, double ys)> XYPosition,
        List<(double pixelx, double pixely)> XYPixelx,
        int Row, int Col)
        {
            if (XYPosition.Count < 2 || XYPixelx.Count < 2)
            {
                throw new ArgumentException("There must be at least two points in each list.");
            }

            int NanX = 0;
            int NanY = 0;

            int count = XYPosition.Count;
            double totalPixelSizeX = 0;
            double totalPixelSizeY = 0;
            double totalAngle = 0;

            double deltaXActual = XYPosition[0].xs - XYPosition[(Row - 1) + 0 * Row].xs;
            double deltaYActual = XYPosition[0].ys - XYPosition[(Row - 1) + 0 * Row].ys;

            double deltaXPixel = XYPixelx[0].pixelx - XYPixelx[(Row - 1) + 0 * Row].pixelx;
            double deltaYPixel = XYPixelx[0].pixely - XYPixelx[(Row - 1) + 0 * Row].pixely;

            double angleActual = Math.Atan2(Math.Abs(deltaYActual), Math.Abs(deltaXActual));
            double anglePixel = Math.Atan2(Math.Abs(deltaYPixel), Math.Abs(deltaXPixel));
            double angle = anglePixel - angleActual;

            double deltaXActual1 = XYPosition[(Row - 1) + 0 * Row].xs - XYPosition[(Row - 1) + (Col - 1) * Row].xs;
            double deltaYActual1 = XYPosition[(Row - 1) + 0 * Row].ys - XYPosition[(Row - 1) + (Col - 1) * Row].ys;

            double deltaXPixel1 = XYPixelx[(Row - 1) + 0 * Row].pixelx - XYPixelx[(Row - 1) + (Col - 1) * Row].pixelx;
            double deltaYPixel1 = XYPixelx[(Row - 1) + 0 * Row].pixely - XYPixelx[(Row - 1) + (Col - 1) * Row].pixely;


            //for (int i = 0; i < count - 1; i++)
            //{
            //    // Calculate the differences in actual coordinates and pixel coordinates
            //    double deltaXActual = XYPosition[i + 1].xs - XYPosition[i].xs;
            //    double deltaYActual = XYPosition[i + 1].ys - XYPosition[i].ys;
            //    double deltaXPixel = XYPixelx[i + 1].pixelx - XYPixelx[i].pixelx;
            //    double deltaYPixel = XYPixelx[i + 1].pixely - XYPixelx[i].pixely;

            //    // Calculate pixelsizeX and pixelsizeY
            //    double pixelsizeX = (Math.Abs(deltaXActual / deltaXPixel));
            //    double pixelsizeY = (Math.Abs(deltaYActual / deltaYPixel));

            //    // Calculate the angle between the actual coordinates and the pixel coordinates
            //    double angleActual = Math.Atan2(deltaYActual, deltaXActual);
            //    double anglePixel = Math.Atan2(deltaYPixel, deltaXPixel);
            //    double angle = anglePixel - angleActual;

            //    // Convert angle to degrees
            //    angle = angle * (180 / Math.PI);

            //    if (double.IsNaN(pixelsizeX))
            //    {
            //        pixelsizeX = 0;
            //        NanX++;
            //    }
            //    if (double.IsNaN(pixelsizeY))
            //    {
            //        pixelsizeY = 0;
            //        NanY++;
            //    }

            //    totalPixelSizeX += pixelsizeX;
            //    totalPixelSizeY += pixelsizeY;
            //    totalAngle += angle;
            //}

            //double averagePixelSizeX = totalPixelSizeX / (count - NanX - 1);
            //double averagePixelSizeY = totalPixelSizeY / (count - NanY - 1);
            //double averageAngle = totalAngle / (count - 1);

            double averagePixelSizeX = Math.Abs(deltaXActual / deltaXPixel);
            double averagePixelSizeY = Math.Abs(deltaYActual1 / deltaYPixel1);
            double averageAngle = angle;

            return (averagePixelSizeX, averagePixelSizeY, averageAngle);
        }

        public int ShowVisualForm(UserControl visualControlGUI, string Name, string title)
        {
            int result = -1;
            var formReadyEvent = new ManualResetEvent(false);

            var tcs = new TaskCompletionSource<int>();
            VisualControlForm VForm = new VisualControlForm();

            VForm.OnButtonClicked += (buttonResult) =>
            {
                result = buttonResult == "confirm" ? 1 : 0;
                formReadyEvent.Set();
            };



            VForm.InitializeGui(visualControlGUI);

            VForm.Location = new Point(1550, 150);

            VForm.FormShow(Name, title, true, tcs.SetResult, new Point(1550, 150));

            while (!formReadyEvent.WaitOne(100))
            {
                Application.DoEvents();
            }


            return result;

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

        /// <summary>
        /// 阻塞消息弹窗
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public int ShowMessage(string title, string content, string type)
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

        private int ShowMessage2(string title, string content, string type)
        {
            int result = -1;
            var formReadyEvent = new ManualResetEvent(false);
            //WarningBox1.FormShow(title, content, type);
            MessageBox1 myMessageBox1 = new MessageBox1();
            myMessageBox1.OnButtonClicked += (buttonResult) =>
            {
                result = buttonResult == "confirm" ? 1 : 0;
                formReadyEvent.Set();
            };
            myMessageBox1.showMessage(title, content, type);

            while (!formReadyEvent.WaitOne(100))
            {
                Application.DoEvents();
            }

            return result;
        }

        /// <summary>
        /// 不阻塞消息弹窗
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public int ShowMessageAsync(string title, string content, string type)
        {


            return ShowMessage2(title, content, type);




        }

        private void ShowStage()
        {

            FrmStageControl form = (Application.OpenForms["FrmStageControl"]) as FrmStageControl;
            if (form == null)
            {
                form = new FrmStageControl();
                form.Location = new Point(1550, 200);
                //form.Location = this.PointToScreen(new Point(1550, 150));
                //form.Owner = this.FindForm();
                //lightform.StartPosition = FormStartPosition.CenterScreen;
                form.Show();
            }
            else
            {
                form.Activate();
            }
        }

        private void CloseStage()
        {
            try
            {
                FrmStageControl form = Application.OpenForms.OfType<FrmStageControl>().FirstOrDefault();
                if (form != null)
                {
                    form.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("关闭轴点动窗口时发生错误: " + ex.Message);
            }
        }

        private void ShowStageAxisMove()
        {
            FrmStageAxisMoveControl form = (Application.OpenForms["FrmStageAxisMoveControl"]) as FrmStageAxisMoveControl;
            if (form == null)
            {
                form = new FrmStageAxisMoveControl();
                form.Location = new Point(1550, 550);
                form.ShowLocation(new Point(1550, 600));
                //form.Location = this.PointToScreen(new Point(1550, 150));
                //form.Owner = this.FindForm();
                //lightform.StartPosition = FormStartPosition.CenterScreen;
                form.Show();
            }
            else
            {
                form.Activate();
            }
        }

        private void CloseStageAxisMove()
        {
            try
            {
                FrmStageAxisMoveControl form = Application.OpenForms.OfType<FrmStageAxisMoveControl>().FirstOrDefault();
                if (form != null)
                {
                    form.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("关闭轴移动窗口时发生错误: " + ex.Message);
            }
        }

        private (double, double, double, bool) ShowCameraParamForm(string text3 = "Bond相机参数设置", double ImageWidthPixelsize = 1, double ImageHeightPixelsize = 1, double Angle = 0)
        {
            return CameraParamBox.FormShow(text3, ImageWidthPixelsize, ImageHeightPixelsize, Angle);
        }


        /// <summary>
        /// 料盒钩爪移动
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="Z"></param>
        private void MaterialboxHookXYZAbsoluteMove(double X, double Y, double Z)
        {
            //stage.AbloluteMoveSync(new EnumStageAxis[3] { EnumStageAxis.BondX, EnumStageAxis.BondY, EnumStageAxis.BondZ }, new double[3] { X, Y, Z });
            stage.ClrAlarm(EnumStageAxis.MaterialboxZ);
            stage.AbloluteMoveSync(EnumStageAxis.MaterialboxZ, Z);

            //stage.ClrAlarm(EnumStageAxis.BondX);
            //stage.AbloluteMoveSync(EnumStageAxis.BondX, X);

            //stage.ClrAlarm(EnumStageAxis.BondY);
            //stage.AbloluteMoveSync(EnumStageAxis.BondY, Y);


            EnumStageAxis[] multiAxis = new EnumStageAxis[2];
            multiAxis[0] = EnumStageAxis.MaterialboxX;
            multiAxis[1] = EnumStageAxis.MaterialboxY;

            double[] target1 = new double[2];

            target1[0] = X;
            target1[1] = Y;

            _positioningSystem.MoveAixsToStageCoord(multiAxis, target1, EnumCoordSetType.Absolute);
        }

        /// <summary>
        /// 物料钩爪移动
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="Z"></param>
        private void MaterialHookXYZAbsoluteMove(double X, double Y, double Z)
        {
            //stage.AbloluteMoveSync(new EnumStageAxis[3] { EnumStageAxis.BondX, EnumStageAxis.BondY, EnumStageAxis.BondZ }, new double[3] { X, Y, Z });
            stage.ClrAlarm(EnumStageAxis.MaterialZ);
            stage.AbloluteMoveSync(EnumStageAxis.MaterialZ, Z);

            //stage.ClrAlarm(EnumStageAxis.BondX);
            //stage.AbloluteMoveSync(EnumStageAxis.BondX, X);

            //stage.ClrAlarm(EnumStageAxis.BondY);
            //stage.AbloluteMoveSync(EnumStageAxis.BondY, Y);


            EnumStageAxis[] multiAxis = new EnumStageAxis[2];
            multiAxis[0] = EnumStageAxis.MaterialX;
            multiAxis[1] = EnumStageAxis.MaterialY;

            double[] target1 = new double[2];

            target1[0] = X;
            target1[1] = Y;

            _positioningSystem.MoveAixsToStageCoord(multiAxis, target1, EnumCoordSetType.Absolute);
        }


        private bool AxisAbsoluteMove(EnumStageAxis axis, double target)
        {
            stage.ClrAlarm(axis);
            stage.AbloluteMoveSync(axis, target);

            double position = stage.GetCurrentPosition(axis);
            int T = 0;
            if (Math.Abs(position - target) > 0.5 || T < 900)
            {
                while (Math.Abs(position - target) > 0.5)
                {
                    position = stage.GetCurrentPosition(axis);
                    if (Math.Abs(position - target) < 0.5)
                    {
                        break;
                    }
                    T++;
                    Thread.Sleep(100);
                }
            }
            if(Math.Abs(position - target) > 0.5)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void AxisRelativeMove(EnumStageAxis axis, double target)
        {
            stage.RelativeMoveSync(axis, (float)target);
        }

        private double ReadCurrentAxisposition(EnumStageAxis axis)
        {

            double position = stage.GetCurrentPosition(axis);
            //double position = 2;
            return position;
        }

        private bool AxisHome(EnumStageAxis axis)
        {
            return _stageEngine[axis].Home();
        }

        private void AxisClrAlarm(EnumStageAxis axis)
        {
            _stageEngine[axis].ClrAlarm();
        }

        private void AxisEnable(EnumStageAxis axis)
        {
            _stageEngine[axis].Enable();
        }



        //流程


        public (double, double) ImageOffsetToXY(PointF point)
        {
            double MaterialboxX = ReadCurrentAxisposition(EnumStageAxis.MaterialboxX);
            double MaterialboxY = ReadCurrentAxisposition(EnumStageAxis.MaterialboxY);
            (MaterialboxX, MaterialboxY) = ImageToXY(point, new PointF(_TrackCameraConfig.ImageSizeWidth / 2, _TrackCameraConfig.ImageSizeHeight / 2), _TrackCameraConfig.WidthPixelSize, _TrackCameraConfig.HeightPixelSize);

            return (MaterialboxX, MaterialboxY);
        }

        /// <summary>
        /// 识别中心
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public MatchResult IdentificationAsync(EnumCameraType camera, MatchIdentificationParam param)
        {
            //Task.Factory.StartNew(new Action(() =>
            //{
            if (camera == EnumCameraType.TrackCamera)
            {
                TrackCameraVisual.SetDirectLightintensity(param.DirectLightintensity);
                TrackCameraVisual.SetRingLightintensity(param.RingLightintensity);

                bool Done = TrackCameraVisual.LoadMatchTrainXml(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, param.Templatexml));

                Done = TrackCameraVisual.LoadMatchRunXml(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, param.Runxml));

                //var result =  BondCameraVisual.MatchFindAsync(param.Score, param.MinAngle, param.MaxAngle, param.SearchRoi);

                Bitmap bitmap = TrackCameraVisual.GetBitmap();
                Bitmap Showbitmap = new Bitmap(bitmap);

                bool En1 = TrackCameraVisual.MatchSetRunPara<float>(MatchParas.MinScore, 0.2f);
                bool En2 = TrackCameraVisual.MatchSetRunPara<int>(MatchParas.AngleStart, param.MinAngle);
                bool En3 = TrackCameraVisual.MatchSetRunPara<int>(MatchParas.AngleEnd, param.MaxAngle);

                if (!(En1 && En2 && En3))
                {
                    return null;
                }

                List<MatchResult> results = new List<MatchResult>();

                Done = TrackCameraVisual.MatchRun(bitmap, param.Score, ref results, param.SearchRoi);

                if (results != null && results.Count > 0)
                {
                    Task.Factory.StartNew(new Action(() =>
                    {
                        CameraWindowGUI.Instance.ShowImage(Showbitmap, _TrackCameraConfig.CameraWindowImageScale);
                        CameraWindowGUI.Instance.ClearGraphicDraw();
                        CameraWindowGUI.Instance.GraphicDrawInit(results);
                        CameraWindowGUI.Instance.GraphicDraw(Graphic.match, true);
                    }));
                    if (results[0].IsOk)
                    {
                        {

                            return results[0];
                        }
                    }



                }
            }

            if (camera == EnumCameraType.WeldCamera)
            {
                WeldCameraVisual.SetDirectLightintensity(param.DirectLightintensity);
                WeldCameraVisual.SetRingLightintensity(param.RingLightintensity);

                bool Done = WeldCameraVisual.LoadMatchTrainXml(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, param.Templatexml));

                Done = WeldCameraVisual.LoadMatchRunXml(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, param.Runxml));

                //var result =  WaferCameraVisual.MatchFindAsync(param.Score, param.MinAngle, param.MaxAngle, param.SearchRoi);

                Bitmap bitmap = WeldCameraVisual.GetBitmap();
                Bitmap Showbitmap = new Bitmap(bitmap);

                bool En1 = WeldCameraVisual.MatchSetRunPara<float>(MatchParas.MinScore, 0.2f);
                bool En2 = WeldCameraVisual.MatchSetRunPara<int>(MatchParas.AngleStart, param.MinAngle);
                bool En3 = WeldCameraVisual.MatchSetRunPara<int>(MatchParas.AngleEnd, param.MaxAngle);

                if (!(En1 && En2 && En3))
                {
                    return null;
                }

                List<MatchResult> results = new List<MatchResult>();

                Done = WeldCameraVisual.MatchRun(bitmap, param.Score, ref results, param.SearchRoi);

                if (results != null && results.Count > 0)
                {
                    Task.Factory.StartNew(new Action(() =>
                    {
                        CameraWindowGUI.Instance.ShowImage(Showbitmap, _WeldCameraConfig.CameraWindowImageScale);
                        CameraWindowGUI.Instance.ClearGraphicDraw();
                        CameraWindowGUI.Instance.GraphicDrawInit(results);
                        CameraWindowGUI.Instance.GraphicDraw(Graphic.match, true);
                    }));

                    if (results[0].IsOk)
                    {
                        {
                            return results[0];

                        }
                    }



                }
            }

            //}));
            return null;
        }

        /// <summary>
        /// 识别并相对于相机中心的偏移坐标和角度
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public XYZTCoordinateConfig IdentificationAsync2(EnumCameraType camera, MatchIdentificationParam param)
        {
            XYZTCoordinateConfig offset = new XYZTCoordinateConfig();

            if (camera == EnumCameraType.TrackCamera)
            {
                TrackCameraVisual.SetDirectLightintensity(param.DirectLightintensity);
                TrackCameraVisual.SetRingLightintensity(param.RingLightintensity);

                bool Done = TrackCameraVisual.LoadMatchTrainXml(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, param.Templatexml));

                Done = TrackCameraVisual.LoadMatchRunXml(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, param.Runxml));

                //var result =  BondCameraVisual.MatchFindAsync(param.Score, param.MinAngle, param.MaxAngle, param.SearchRoi);

                Bitmap bitmap = TrackCameraVisual.GetBitmap();
                Bitmap Showbitmap = new Bitmap(bitmap);

                bool En1 = TrackCameraVisual.MatchSetRunPara<float>(MatchParas.MinScore, 0.2f);
                bool En2 = TrackCameraVisual.MatchSetRunPara<int>(MatchParas.AngleStart, param.MinAngle);
                bool En3 = TrackCameraVisual.MatchSetRunPara<int>(MatchParas.AngleEnd, param.MaxAngle);

                if (!(En1 && En2 && En3))
                {
                    return null;
                }

                List<MatchResult> results = new List<MatchResult>();

                Done = TrackCameraVisual.MatchRun(bitmap, param.Score, ref results, param.SearchRoi);


                double MaterialboxX = ReadCurrentAxisposition(EnumStageAxis.MaterialboxX);
                double MaterialboxY = ReadCurrentAxisposition(EnumStageAxis.MaterialboxY);
                double MaterialboxZ = ReadCurrentAxisposition(EnumStageAxis.MaterialboxZ);

                if (results != null && results.Count > 0)
                {
                    Task.Factory.StartNew(new Action(() =>
                    {
                        CameraWindowGUI.Instance.ShowImage(Showbitmap, _TrackCameraConfig.CameraWindowImageScale);
                        CameraWindowGUI.Instance.ClearGraphicDraw();
                        CameraWindowGUI.Instance.GraphicDrawInit(results);
                        CameraWindowGUI.Instance.GraphicDraw(Graphic.match, true);
                    }));
                    if (results[0].IsOk)
                    {
                        {
                            (MaterialboxX, MaterialboxY) = ImageToXY(results[0].MatchBox.Benchmark, new PointF(bitmap.Width / 2, bitmap.Height / 2), _TrackCameraConfig.WidthPixelSize, _TrackCameraConfig.HeightPixelSize);

                            offset = new XYZTCoordinateConfig();
                            offset.X = -MaterialboxX;
                            offset.Y = -MaterialboxY;
                            offset.Z = 0;
                            offset.Theta = results[0].MatchBox.Angle;

                            return offset;
                        }
                    }
                    else
                    {
                        return null;
                    }



                }
                else
                {
                    return null;
                }
            }

            return null;
        }

        /// <summary>
        /// 料盒钩爪移动到安全位置
        /// </summary>
        /// <param name="Mode"></param>
        /// <returns></returns>
        private bool MaterialBoxHookReturnSafeLocationAction(int Mode = 0)
        {
            bool Done = false;
            if (Mode == 0)
            {
                //if()
                //{

                //}

                double MaterialBoxhookX = _systemConfig.PositioningConfig.MaterialBoxhookSafeLocation.X;
                double MaterialBoxhookY = _systemConfig.PositioningConfig.MaterialBoxhookSafeLocation.Y;
                double MaterialBoxhookZ = _systemConfig.PositioningConfig.MaterialBoxhookSafeLocation.Z;
                double MaterialBoxhookT = _systemConfig.PositioningConfig.MaterialBoxhookSafeLocation.Theta;

                double MaterialBoxhookH = _systemConfig.PositioningConfig.MaterialBoxhookHookSafeLocation;

                AxisAbsoluteMove(EnumStageAxis.MaterialboxZ, MaterialBoxhookZ);

                MaterialboxHookXYZAbsoluteMove(MaterialBoxhookX, MaterialBoxhookY, MaterialBoxhookZ);

                AxisAbsoluteMove(EnumStageAxis.MaterialboxT, MaterialBoxhookT);

                AxisAbsoluteMove(EnumStageAxis.MaterialboxHook, MaterialBoxhookH);


                Done = true;
            }
            else if (Mode == 1)
            {
                double MaterialBoxhookX = _systemConfig.PositioningConfig.MaterialBoxhookSafeLocation.X;
                double MaterialBoxhookY = _systemConfig.PositioningConfig.MaterialBoxhookSafeLocation.Y;
                double MaterialBoxhookZ = _systemConfig.PositioningConfig.MaterialBoxhookSafeLocation.Z;
                double MaterialBoxhookT = _systemConfig.PositioningConfig.MaterialBoxhookSafeLocation.Theta;

                double MaterialBoxhookH = _systemConfig.PositioningConfig.MaterialBoxhookHookSafeLocation;

                AxisAbsoluteMove(EnumStageAxis.MaterialboxZ, MaterialBoxhookZ);

                MaterialboxHookXYZAbsoluteMove(MaterialBoxhookX, MaterialBoxhookY, MaterialBoxhookZ);

                AxisAbsoluteMove(EnumStageAxis.MaterialboxT, MaterialBoxhookT);

                AxisAbsoluteMove(EnumStageAxis.MaterialboxHook, MaterialBoxhookH);

                Done = true;
            }
            else if (Mode == 2)
            {
                ShowStage();

                int result1 = ShowMessageAsync("动作确认", "料盘钩爪是否移动到安全位置", "提示");
                if (result1 == 1)
                {
                    double MaterialBoxhookX = ReadCurrentAxisposition(EnumStageAxis.MaterialboxX);
                    double MaterialBoxhookY = ReadCurrentAxisposition(EnumStageAxis.MaterialboxY);
                    double MaterialBoxhookZ = ReadCurrentAxisposition(EnumStageAxis.MaterialboxZ);
                    double MaterialBoxhookT = ReadCurrentAxisposition(EnumStageAxis.MaterialboxT);
                    double MaterialBoxhookHook = ReadCurrentAxisposition(EnumStageAxis.MaterialboxHook);


                    _systemConfig.PositioningConfig.MaterialBoxhookSafeLocation.X = MaterialBoxhookX;
                    _systemConfig.PositioningConfig.MaterialBoxhookSafeLocation.Y = MaterialBoxhookY;
                    _systemConfig.PositioningConfig.MaterialBoxhookSafeLocation.Z = MaterialBoxhookZ;
                    _systemConfig.PositioningConfig.MaterialBoxhookSafeLocation.Theta = MaterialBoxhookT;

                    _systemConfig.PositioningConfig.MaterialBoxhookHookSafeLocation = (float)MaterialBoxhookHook;


                    Done = true;
                }
                else
                {
                    return Done;
                }


            }
            return Done;

        }

        /// <summary>
        /// 物料钩爪移动到安全位置
        /// </summary>
        /// <param name="Mode"></param>
        /// <returns></returns>
        private bool MaterialHookReturnSafeLocationAction(int Mode = 0)
        {
            bool Done = false;
            if (Mode == 0)
            {
                double MaterialhookX = _systemConfig.PositioningConfig.MaterialhookSafeLocation.X;
                double MaterialhookY = _systemConfig.PositioningConfig.MaterialhookSafeLocation.Y;
                double MaterialhookZ = _systemConfig.PositioningConfig.MaterialhookSafeLocation.Z;

                double MaterialhookH = _systemConfig.PositioningConfig.MaterialhookHookSafeLocation;

                AxisAbsoluteMove(EnumStageAxis.MaterialZ, MaterialhookZ);

                MaterialHookXYZAbsoluteMove(MaterialhookX, MaterialhookY, MaterialhookZ);


                AxisAbsoluteMove(EnumStageAxis.MaterialHook, MaterialhookH);


                Done = true;
            }
            else if (Mode == 1)
            {
                double MaterialhookX = _systemConfig.PositioningConfig.MaterialhookSafeLocation.X;
                double MaterialhookY = _systemConfig.PositioningConfig.MaterialhookSafeLocation.Y;
                double MaterialhookZ = _systemConfig.PositioningConfig.MaterialhookSafeLocation.Z;

                double MaterialhookH = _systemConfig.PositioningConfig.MaterialhookHookSafeLocation;

                AxisAbsoluteMove(EnumStageAxis.MaterialZ, MaterialhookZ);

                MaterialHookXYZAbsoluteMove(MaterialhookX, MaterialhookY, MaterialhookZ);


                AxisAbsoluteMove(EnumStageAxis.MaterialHook, MaterialhookH);

                Done = true;
            }
            else if (Mode == 2)
            {
                ShowStage();

                int result1 = ShowMessageAsync("动作确认", "物料钩爪是否移动到安全位置", "提示");
                if (result1 == 1)
                {
                    double MaterialhookX = ReadCurrentAxisposition(EnumStageAxis.MaterialX);
                    double MaterialhookY = ReadCurrentAxisposition(EnumStageAxis.MaterialY);
                    double MaterialhookZ = ReadCurrentAxisposition(EnumStageAxis.MaterialZ);
                    double MaterialhookHook = ReadCurrentAxisposition(EnumStageAxis.MaterialHook);


                    _systemConfig.PositioningConfig.MaterialhookSafeLocation.X = MaterialhookX;
                    _systemConfig.PositioningConfig.MaterialhookSafeLocation.Y = MaterialhookY;
                    _systemConfig.PositioningConfig.MaterialhookSafeLocation.Z = MaterialhookZ;

                    _systemConfig.PositioningConfig.MaterialhookHookSafeLocation = (float)MaterialhookHook;


                    Done = true;
                }
                else
                {
                    return Done;
                }


            }
            return Done;

        }

        /// <summary>
        /// 轴复位
        /// </summary>
        /// <returns></returns>
        private bool ReturnHomeAction()
        {
            bool Done = false;

            bool Axis5 = false, Axis4 = false;

            int result1 = ShowMessageAsync("动作确认", "升降轴是否回原点", "提示");
            if (result1 == 1)
            {
                Done = false;
 
                while(!Done)
                {
                    Done = AxisHome(EnumStageAxis.Presslifting);
                    if(Done)
                    {
                        break;
                    }
                    else
                    {
                        result1 = ShowMessageAsync("动作确认", "升降轴回原点失败，升降轴是否重新回原点", "提示");
                        if(result1 == 1)
                        {
                            AxisClrAlarm(EnumStageAxis.Presslifting);

                            AxisEnable(EnumStageAxis.Presslifting);


                        }
                        else
                        {
                            return false;
                        }
                    }
                }


                AxisAbsoluteMove(EnumStageAxis.Presslifting, 1);

            }
            else
            {

            }

            result1 = ShowMessageAsync("动作确认", "料盒5轴是否回原点", "提示");
            if (result1 == 1)
            {

                Done = false;

                while (!Done)
                {
                    Done = AxisHome(EnumStageAxis.MaterialboxZ);
                    if (Done)
                    {
                        break;
                    }
                    else
                    {
                        result1 = ShowMessageAsync("动作确认", "料盒5轴Z轴回原点失败，请重新初始化轴，重新回原点", "提示");
                        if (result1 == 1)
                        {
                            AxisClrAlarm(EnumStageAxis.MaterialboxZ);

                            AxisEnable(EnumStageAxis.MaterialboxZ);

                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                Thread.Sleep(1000);
                Done = false;

                while (!Done)
                {
                    Done = AxisHome(EnumStageAxis.MaterialboxT);
                    if (Done)
                    {
                        break;
                    }
                    else
                    {
                        result1 = ShowMessageAsync("动作确认", "料盒5轴T轴回原点失败，请重新初始化轴，重新回原点", "提示");
                        if (result1 == 1)
                        {
                            AxisClrAlarm(EnumStageAxis.MaterialboxT);

                            AxisEnable(EnumStageAxis.MaterialboxT);

                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                Thread.Sleep(1000);

                Done = false;

                while (!Done)
                {
                    Done = AxisHome(EnumStageAxis.MaterialboxHook);
                    if (Done)
                    {
                        break;
                    }
                    else
                    {
                        result1 = ShowMessageAsync("动作确认", "料盒5轴夹爪轴回原点失败，请重新初始化轴，重新回原点", "提示");
                        if (result1 == 1)
                        {
                            AxisClrAlarm(EnumStageAxis.MaterialboxHook);

                            AxisEnable(EnumStageAxis.MaterialboxHook);

                        }
                        else
                        {
                            return false;
                        }
                    }
                }

                Thread.Sleep(1000);

                Done = false;

                while (!Done)
                {
                    Done = AxisHome(EnumStageAxis.MaterialboxY);
                    if (Done)
                    {
                        break;
                    }
                    else
                    {
                        result1 = ShowMessageAsync("动作确认", "料盒5轴Y轴回原点失败，请重新初始化轴，重新回原点", "提示");
                        if (result1 == 1)
                        {
                            AxisClrAlarm(EnumStageAxis.MaterialboxY);

                            AxisEnable(EnumStageAxis.MaterialboxY);

                        }
                        else
                        {
                            return false;
                        }
                    }
                }

                Thread.Sleep(5000);

                Done = AxisAbsoluteMove(EnumStageAxis.MaterialboxY, SystemConfiguration.Instance.PositioningConfig.MaterialBoxhookSafeLocation.Y);
                if(!Done)
                {
                    ShowMessageAsync("移动失败", "料盒5轴Y轴移动到空闲位置失败", "警告");
                    return false;
                }

                Thread.Sleep(5000);
                Done = AxisAbsoluteMove(EnumStageAxis.MaterialboxT, SystemConfiguration.Instance.PositioningConfig.MaterialBoxhookSafeLocation.Theta);
                if (!Done)
                {
                    ShowMessageAsync("移动失败", "料盒5轴T轴移动到空闲位置失败", "警告");
                    return false;
                }

                Done = false;

                while (!Done)
                {
                    Done = AxisHome(EnumStageAxis.MaterialboxX);
                    if (Done)
                    {
                        break;
                    }
                    else
                    {
                        result1 = ShowMessageAsync("动作确认", "料盒5轴X轴回原点失败，请重新初始化轴，重新回原点", "提示");
                        if (result1 == 1)
                        {
                            AxisClrAlarm(EnumStageAxis.MaterialboxX);

                            AxisEnable(EnumStageAxis.MaterialboxX);

                        }
                        else
                        {
                            return false;
                        }
                    }
                }


                Axis5 = true;

            }
            else
            {
                
            }

            if(Axis5)
            {
                result1 = ShowMessageAsync("动作确认", "物料4轴是否回原点", "提示");
                if (result1 == 1)
                {
                    Done = false;

                    while (!Done)
                    {
                        Done = AxisHome(EnumStageAxis.MaterialY);
                        if (Done)
                        {
                            break;
                        }
                        else
                        {
                            result1 = ShowMessageAsync("动作确认", "物料4轴Y轴回原点失败，请重新初始化轴，重新回原点", "提示");
                            if (result1 == 1)
                            {
                                AxisClrAlarm(EnumStageAxis.MaterialY);

                                AxisEnable(EnumStageAxis.MaterialY);

                            }
                            else
                            {
                                return false;
                            }
                        }
                    }

                    Done = false;

                    while (!Done)
                    {
                        Done = AxisHome(EnumStageAxis.MaterialX);
                        if (Done)
                        {
                            break;
                        }
                        else
                        {
                            result1 = ShowMessageAsync("动作确认", "物料4轴X轴回原点失败，请重新初始化轴，重新回原点", "提示");
                            if (result1 == 1)
                            {
                                AxisClrAlarm(EnumStageAxis.MaterialX);

                                AxisEnable(EnumStageAxis.MaterialX);

                            }
                            else
                            {
                                return false;
                            }
                        }
                    }

                    Done = false;

                    while (!Done)
                    {
                        Done = AxisHome(EnumStageAxis.MaterialZ);
                        if (Done)
                        {
                            break;
                        }
                        else
                        {
                            result1 = ShowMessageAsync("动作确认", "物料4轴Z轴回原点失败，请重新初始化轴，重新回原点", "提示");
                            if (result1 == 1)
                            {
                                AxisClrAlarm(EnumStageAxis.MaterialZ);

                                AxisEnable(EnumStageAxis.MaterialZ);

                            }
                            else
                            {
                                return false;
                            }
                        }
                    }


                    Done = false;

                    while (!Done)
                    {
                        Done = AxisHome(EnumStageAxis.MaterialHook);
                        if (Done)
                        {
                            break;
                        }
                        else
                        {
                            result1 = ShowMessageAsync("动作确认", "物料4轴夹爪轴回原点失败，请重新初始化轴，重新回原点", "提示");
                            if (result1 == 1)
                            {
                                AxisClrAlarm(EnumStageAxis.MaterialHook);

                                AxisEnable(EnumStageAxis.MaterialHook);

                            }
                            else
                            {
                                return false;
                            }
                        }
                    }

                    Axis4 = true;
                }
                else
                {

                }
            }
           

            if(Axis4)
            {
                result1 = ShowMessageAsync("动作确认", "物料4轴是否回空闲位置", "提示");
                if (result1 == 1)
                {
                    Done = AxisAbsoluteMove(EnumStageAxis.MaterialZ, SystemConfiguration.Instance.PositioningConfig.MaterialhookSafeLocation.Z);
                    if (!Done)
                    {
                        ShowMessageAsync("移动失败", "物料4轴Z轴移动到空闲位置失败", "警告");
                        return false;
                    }

                    Thread.Sleep(5000);

                    Done = AxisAbsoluteMove(EnumStageAxis.MaterialX, SystemConfiguration.Instance.PositioningConfig.MaterialhookSafeLocation.X);
                    if (!Done)
                    {
                        ShowMessageAsync("移动失败", "物料4轴X轴移动到空闲位置失败", "警告");
                        return false;
                    }

                    Thread.Sleep(5000);

                    Done = AxisAbsoluteMove(EnumStageAxis.MaterialY, SystemConfiguration.Instance.PositioningConfig.MaterialhookSafeLocation.Y);
                    if (!Done)
                    {
                        ShowMessageAsync("移动失败", "物料4轴Y轴移动到空闲位置失败", "警告");
                        return false;
                    }

                    Thread.Sleep(5000);

                    Done = AxisAbsoluteMove(EnumStageAxis.MaterialHook, SystemConfiguration.Instance.PositioningConfig.MaterialhookHookSafeLocation);
                    if (!Done)
                    {
                        ShowMessageAsync("移动失败", "物料4轴夹爪轴移动到空闲位置失败", "警告");
                        return false;
                    }
                }


            }

            if(Axis5)
            {
                result1 = ShowMessageAsync("动作确认", "料盒5轴是否回空闲位置", "提示");
                if (result1 == 1)
                {
                    Done = AxisAbsoluteMove(EnumStageAxis.MaterialboxX, SystemConfiguration.Instance.PositioningConfig.MaterialBoxhookSafeLocation.X);
                    if (!Done)
                    {
                        ShowMessageAsync("移动失败", "料盒5轴X轴移动到空闲位置失败", "警告");
                        return false;
                    }

                    Done = AxisAbsoluteMove(EnumStageAxis.MaterialboxHook, SystemConfiguration.Instance.PositioningConfig.MaterialBoxhookHookSafeLocation);
                    if (!Done)
                    {
                        ShowMessageAsync("移动失败", "料盒5轴夹爪轴移动到空闲位置失败", "警告");
                        return false;
                    }
                }
            }
            

            return Done;
        }

        /// <summary>
        /// 物料钩爪校准到搬送相机中心
        /// </summary>
        /// <param name="Mode"></param>
        /// <returns></returns>
        private bool TrackCameraIdentifyCenterMaterialHook(int Mode = 0)
        {
            bool Done = false;
            if (Mode == 0)
            {
                double MaterialhookX = _systemConfig.PositioningConfig.TrackCameraCenterMaterialHook.X;
                double MaterialhookY = _systemConfig.PositioningConfig.TrackCameraCenterMaterialHook.Y;
                double MaterialhookZ = _systemConfig.PositioningConfig.TrackCameraCenterMaterialHook.Z;

                AxisAbsoluteMove(EnumStageAxis.MaterialZ, MaterialhookZ);

                MaterialHookXYZAbsoluteMove(MaterialhookX, MaterialhookY, MaterialhookZ);


                Done = true;
            }
            else if (Mode == 1)
            {
                double MaterialhookX = _systemConfig.PositioningConfig.TrackCameraCenterMaterialHook.X;
                double MaterialhookY = _systemConfig.PositioningConfig.TrackCameraCenterMaterialHook.Y;
                double MaterialhookZ = _systemConfig.PositioningConfig.TrackCameraCenterMaterialHook.Z;

                AxisAbsoluteMove(EnumStageAxis.MaterialZ, MaterialhookZ);

                MaterialHookXYZAbsoluteMove(MaterialhookX, MaterialhookY, MaterialhookZ);

                Done = true;
            }
            else if (Mode == 2)
            {
                ShowStage();

                int result1 = ShowMessageAsync("动作确认", "物料钩爪是否移动到搬送相机中心", "提示");
                if (result1 == 1)
                {
                    double MaterialhookX = ReadCurrentAxisposition(EnumStageAxis.MaterialX);
                    double MaterialhookY = ReadCurrentAxisposition(EnumStageAxis.MaterialY);
                    double MaterialhookZ = ReadCurrentAxisposition(EnumStageAxis.MaterialZ);
                    double MaterialhookHook = ReadCurrentAxisposition(EnumStageAxis.MaterialHook);


                    _systemConfig.PositioningConfig.TrackCameraCenterMaterialHook.X = MaterialhookX;
                    _systemConfig.PositioningConfig.TrackCameraCenterMaterialHook.Y = MaterialhookY;
                    _systemConfig.PositioningConfig.TrackCameraCenterMaterialHook.Z = MaterialhookZ;

                    Done = true;
                }
                else
                {
                    return Done;
                }


            }
            return Done;
        }

        /// <summary>
        /// 料盒钩爪校准到搬送相机中心
        /// </summary>
        /// <param name="Mode"></param>
        /// <returns></returns>
        private bool TrackCameraIdentifyCenterMaterialBoxHook(int Mode = 0)
        {
            bool Done = false;
            if (Mode == 0)
            {
                double MaterialboxhookX = _systemConfig.PositioningConfig.TrackCameraCenterMaterialBoxHook.X;
                double MaterialboxhookY = _systemConfig.PositioningConfig.TrackCameraCenterMaterialBoxHook.Y;
                double MaterialboxhookZ = _systemConfig.PositioningConfig.TrackCameraCenterMaterialBoxHook.Z;

                AxisAbsoluteMove(EnumStageAxis.MaterialboxZ, MaterialboxhookZ);

                MaterialboxHookXYZAbsoluteMove(MaterialboxhookX, MaterialboxhookY, MaterialboxhookZ);


                Done = true;
            }
            else if (Mode == 1)
            {
                double MaterialboxhookX = _systemConfig.PositioningConfig.TrackCameraCenterMaterialBoxHook.X;
                double MaterialboxhookY = _systemConfig.PositioningConfig.TrackCameraCenterMaterialBoxHook.Y;
                double MaterialboxhookZ = _systemConfig.PositioningConfig.TrackCameraCenterMaterialBoxHook.Z;

                AxisAbsoluteMove(EnumStageAxis.MaterialboxZ, MaterialboxhookZ);

                MaterialboxHookXYZAbsoluteMove(MaterialboxhookX, MaterialboxhookY, MaterialboxhookZ);


                Done = true;
            }
            else if (Mode == 2)
            {
                ShowStage();

                int result1 = ShowMessageAsync("动作确认", "料盒钩爪是否移动到搬送相机中心", "提示");
                if (result1 == 1)
                {
                    double MaterialhookX = ReadCurrentAxisposition(EnumStageAxis.MaterialboxX);
                    double MaterialhookY = ReadCurrentAxisposition(EnumStageAxis.MaterialboxY);
                    double MaterialhookZ = ReadCurrentAxisposition(EnumStageAxis.MaterialboxZ);


                    _systemConfig.PositioningConfig.TrackCameraCenterMaterialBoxHook.X = MaterialhookX;
                    _systemConfig.PositioningConfig.TrackCameraCenterMaterialBoxHook.Y = MaterialhookY;
                    _systemConfig.PositioningConfig.TrackCameraCenterMaterialBoxHook.Z = MaterialhookZ;

                    Done = true;
                }
                else
                {
                    return Done;
                }


            }
            return Done;
        }



        #endregion




        #region Public Mothed

        /// <summary>
        /// 自动校准
        /// </summary>
        public void AutoRun()
        {

        }

        /// <summary>
        /// 手动校准
        /// </summary>
        /// <param name="Mode"> 1 半自动 2 全手动</param>
        public void ManualRun()
        {
            try
            {
                if (CameraWindowGUI.Instance != null)
                {
                    CameraWindowGUI.Instance.SelectCamera(0);
                }
                if (!(CameraWindowForm.Instance.IsHandleCreated && CameraWindowForm.Instance.Visible))
                {
                    CameraWindowForm.Instance.ShowLocation(new Point(200, 200));
                    CameraWindowForm.Instance.Show();
                }

                Task.Factory.StartNew(new Action(() =>
                {
                    int result1 = ShowMessageAsync("动作确认", "是否校准料盒钩爪与搬送相机的关系", "提示");
                    if (result1 == 1)
                    {
                        TrackCameraIdentifyCenterMaterialBoxHook(2);

                    }

                    //result1 = ShowMessageAsync("动作确认", "是否校准料盒钩爪安全位置", "提示");
                    //if (result1 == 1)
                    //{
                    //    MaterialBoxHookReturnSafeLocationAction(2);

                    //}

                    result1 = ShowMessageAsync("动作确认", "是否校准物料钩爪与搬送相机的关系", "提示");
                    if (result1 == 1)
                    {
                        TrackCameraIdentifyCenterMaterialHook(2);

                    }

                    //result1 = ShowMessageAsync("动作确认", "是否校准料盒钩爪安全位置", "提示");
                    //if (result1 == 1)
                    //{
                    //    MaterialHookReturnSafeLocationAction(2);

                    //}


                    

                    

                    
                }
                ));
            }
            catch(Exception ex)
            {

            }
            

        }

        /// <summary>
        /// 料盒钩爪移动到安全位置
        /// </summary>
        public void MaterialBoxHookReturnSafeLocation(int mode = 0)
        {
            try
            {
                //提示安装CCU工具
                int result1 = ShowMessageAsync("动作确认", "是否将料盒钩爪移动到安全位置", "提示");
                if (result1 == 1)
                {
                    Task.Factory.StartNew(new Action(() =>
                    {
                        MaterialBoxHookReturnSafeLocationAction(mode);
                    }));
                    
                }
                else
                {
                    return;
                }
            }
            catch(Exception ex)
            {

            }
        }

        /// <summary>
        /// 物料钩爪移动到安全位置
        /// </summary>
        public void MaterialHookReturnSafeLocation(int mode = 0)
        {
            try
            {
                //提示安装CCU工具
                int result1 = ShowMessageAsync("动作确认", "是否将物料钩爪移动到安全位置", "提示");
                if (result1 == 1)
                {
                    Task.Factory.StartNew(new Action(() =>
                    {
                        MaterialHookReturnSafeLocationAction(mode);
                    }));
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {

            }
        }



        public void ReturnHome()
        {
            try
            {
                //提示安装CCU工具
                int result1 = ShowMessageAsync("动作确认", "是否回原点", "提示");
                if (result1 == 1)
                {
                    Task.Factory.StartNew(new Action(() =>
                    {
                        ReturnHomeAction();
                    }));
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {

            }
        }


        #endregion

    }
}
