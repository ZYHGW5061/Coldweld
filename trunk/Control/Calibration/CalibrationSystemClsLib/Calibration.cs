using CameraControllerClsLib;
using CameraControllerWrapperClsLib;
using ConfigurationClsLib;
using GlobalDataDefineClsLib;
using GWVisionAlgorithmClsLib;
using HardwareManagerClsLib;
using Newtonsoft.Json;
using PositioningSystemClsLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using VisionDesigner.ContourPatMatch;
using WestDragon.Framework.UtilityHelper;

namespace CalibrationSystemClsLib
{
    /*
     * 功能描述：计算相机安装角 .
     * 
     * Pixel Size: 每像素代表的真实物理尺寸.
     */
    public class CameraAngleCalculator : AVisionFunctionHelper
    {
        public ShapeMatchConfiguration _configForShapeMatch = null;
        /// <summary>
        /// 系统配置
        /// </summary>
        private SystemConfiguration _systemConfig
        {
            get { return SystemConfiguration.Instance; }
        }
        /// <summary>
        /// 定位系统
        /// </summary>
        private PositioningSystem _positionSystem
        {
            get { return PositioningSystem.Instance; }
        }
        private CameraManager _cameraManager
        {
            get { return CameraManager.Instance; }
        }
        /// <summary>
        /// 硬件系统
        /// </summary>
        private HardwareManager _hardwareManager
        {
            get { return HardwareManager.Instance; }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="patternRecognitionEngine"></param>
        public CameraAngleCalculator(TemplateRecognitionTool patternRecognitionEngine)
            : base(patternRecognitionEngine)
        {
            _templateRecognitionEngine = patternRecognitionEngine;
        }
        public void LoadDetectConfig(ShapeMatchConfiguration config)
        {
            this._configForShapeMatch = config;
            //this._patternRecognitionEngine.PMAlignPattern = ConfigForShapeMatch.AlignPattern;

        }
        /// <summary>
        /// 计算相机安装角。
        /// <安装角>指相机与Stage X轴之间的夹角。</安装角>
        /// </summary>
        /// <returns></returns>
        public void CalculateCameraAngle(out double cameraAngle)
        {
            PointF locationRecog;
            double angle = 0;
            double score = 0;
            int recogniseCount = 0;
            bool succeed = CaptureRecognize(out locationRecog, out angle, out score, out recogniseCount).Key;
            if (!succeed)
            {
                MessageBox.Show("recognized failed!");
                cameraAngle = float.NaN;
                return;
            }
            var baseCenterX = locationRecog.X;
            var baseCenterY = locationRecog.Y;
            var firstFovCenterX = 0d;
            var firstFovCenterY = 0d;
            if (_cameraManager.CurrentCameraConfig.CameraType == EnumCameraType.LidCamera)
            {
                firstFovCenterX = _positionSystem.ReadCurrentStagePosition(EnumStageAxis.LidX);
                firstFovCenterY = _positionSystem.ReadCurrentStagePosition(EnumStageAxis.LidY);
            }
            else
            {
                firstFovCenterX = _positionSystem.ReadCurrentStagePosition(EnumStageAxis.ShellX);
                firstFovCenterY = _positionSystem.ReadCurrentStagePosition(EnumStageAxis.ShellY);
            }


            double pixelSizeStandardMag = _hardwareManager.CurrentLensConfig.StandardCalibratedMag;
            int height = _cameraManager.CurrentCameraConfig.ImageSizeWidth / 10;
            double adjustDistance = height * pixelSizeStandardMag * _cameraManager.CurrentCameraConfig.PixelSize;

            #region Search the Bottom point
            PointF locationBottom;
            PointF locat1 = new PointF(), locat2 = new PointF();
            SearchPattern(EnumSearchDirection.Right, adjustDistance, out locationBottom, ref locat1);
            #endregion

            #region Search the top point
            var curFovCenterX = 0d;
            var curFovCenterY = 0d;
            if (_cameraManager.CurrentCameraConfig.CameraType == EnumCameraType.LidCamera)
            {
                curFovCenterX = _positionSystem.ReadCurrentStagePosition(EnumStageAxis.LidX);
                _positionSystem.MoveAixsToStageCoord(EnumStageAxis.LidX, firstFovCenterX - curFovCenterX, EnumCoordSetType.Relative);
            }
            else
            {
                curFovCenterX = _positionSystem.ReadCurrentStagePosition(EnumStageAxis.ShellX);
                _positionSystem.MoveAixsToStageCoord(EnumStageAxis.ShellX, firstFovCenterX - curFovCenterX, EnumCoordSetType.Relative);
            }

            if (_cameraManager.CurrentCameraConfig.CameraType == EnumCameraType.LidCamera)
            {
                curFovCenterY = _positionSystem.ReadCurrentStagePosition(EnumStageAxis.LidY);
                _positionSystem.MoveAixsToStageCoord(EnumStageAxis.LidY, firstFovCenterY - curFovCenterY, EnumCoordSetType.Relative);
            }
            else
            {
                curFovCenterY = _positionSystem.ReadCurrentStagePosition(EnumStageAxis.ShellY);
                _positionSystem.MoveAixsToStageCoord(EnumStageAxis.ShellY, firstFovCenterY - curFovCenterY, EnumCoordSetType.Relative);
            }

            PointF locationTop;
            SearchPattern(EnumSearchDirection.Left, adjustDistance, out locationTop, ref locat2);
            #endregion


            if (locationTop.Y == locationBottom.Y)
            {
                cameraAngle = 0;
            }
            else
            {
                cameraAngle = Math.Round(CommonProcess.ConvertRadianToAngle(Math.Atan((locationTop.Y - locationBottom.Y) / Math.Abs(locationTop.X - locationBottom.X))), 4);
            }

        }

        public KeyValuePair<bool, CContourPatMatchResult> CaptureRecognize(out PointF ptResult, out double angle, out double score, out int recogniseCount)
        {
            var success = false;
            var resultJson = "";
            recogniseCount = 0;
            var capturedImage = _cameraEngine.CaptureImageOutputByte();
            //_patternRecognitionEngine.InputImage = CognexImageFormatter.GetHandler().ConvertToCogImage(capturedImage);
            var _shapeMatchEngine = _templateRecognitionEngine as GWShapeMatch;
            CContourPatMatchResult matchResult = null;
            if (_shapeMatchEngine != null)
            {
                _shapeMatchEngine.SetRunParam("MaxMatchNum", "2");
                _shapeMatchEngine.LoadTrain(_configForShapeMatch.TrainFileFullName);
                matchResult = _shapeMatchEngine.Run(capturedImage, _cameraManager.CurrentCameraConfig.ImageSizeWidth
                        , _cameraManager.CurrentCameraConfig.ImageSizeHeight, 0.5);
            }
            ptResult = new PointF();
            angle = 0;
            score = 0;
            if (matchResult != null)
            {
                //var matchResult = GWShapeMatch.Instance.Run(this.teSourceImageFilePath.Text.Trim(), @"D:\TestFolder\HIKImage\Pattern\aaa.contourmxml", 0.5);
                foreach (var item in matchResult.MatchInfoList)
                {
                    ptResult.X = item.MatchBox.CenterX;
                    ptResult.Y = item.MatchBox.CenterY;
                    angle = item.MatchBox.Angle;
                    score = item.Score;
                    success = true;
                }
                recogniseCount = matchResult.MatchInfoList.Count;
                //resultJson = JsonConvert.SerializeObject(matchResult);
            }
            if (!success)
            {
                //用于监控和定位ThetaAlignment的偏移问题，调试用
                CommonProcess.EnsureFolderExist("D:\\RecognizeFail");
                if (capturedImage == null)
                {
                    throw new EmergencyException("Image from Camera is NULL.");
                }
                Bitmap capturedImageSaved = BitmapFactory.BytesToBitmapFast(capturedImage, _cameraManager.CurrentCameraConfig.ImageSizeWidth
                        , _cameraManager.CurrentCameraConfig.ImageSizeHeight, PixelFormat.Format8bppIndexed); ;
                string failPath = string.Format("D:\\RecognizeFail\\CalibratePixelSize_{0}.bmp", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                capturedImageSaved.Save(failPath, ImageFormat.Bmp);
                capturedImageSaved.Dispose();
                capturedImageSaved = null;
            }
            return new KeyValuePair<bool, CContourPatMatchResult>(success, matchResult);
        }
        /// <summary>
        /// Search pattern algorithm.
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="initialOffset"></param>
        /// <param name="stepDistance"></param>
        /// <param name="searchResult"></param>
        private void SearchPattern(EnumSearchDirection direction, double stepDistance, out PointF searchResult, ref PointF currentStagePos)
        {
            searchResult = new PointF();
            double angle = 0;
            double score = 0;
            int recogniseCount = 0;
            PointF lastResult = new PointF();
            stepDistance = (direction == EnumSearchDirection.Right ? -Math.Abs(stepDistance) : Math.Abs(stepDistance));
            while (true)
            {

                bool succeed = CaptureRecognize(out searchResult, out angle, out score, out recogniseCount).Key;
                if (!succeed)
                {
                    searchResult = lastResult;
                    if (UserCustomAction != null)
                    {
                        //UserCustomAction(new CameraAngleActionCallback()
                        //{
                        //    ImageCaptured = _patternRecognitionEngine.InputImage,
                        //    BestResultRecognized = _patternRecognitionEngine.PMAlignBestResult
                        //});
                    }
                    break;
                }

                if (UserCustomAction != null)
                {
                    //UserCustomAction(new CameraAngleActionCallback()
                    //{
                    //    ImageCaptured = _patternRecognitionEngine.InputImage,
                    //    BestResultRecognized = _patternRecognitionEngine.PMAlignBestResult
                    //});
                }

                if (!lastResult.IsEmpty)
                {
                    //if (!CheckLegitimacyOfResults(searchResult, lastResult))
                    if (recogniseCount > 1)
                    {
                        //不合法退出校准过程
                        MessageBox.Show("Please select a unique pattern that within one field of view.");
                        break;
                    }
                }
                var curPosX = 0d;
                lastResult = searchResult;
                //轴相对位移计算的步长++++++++++++++++++++++++++++++++++++++++
                if (_cameraManager.CurrentCameraConfig.CameraType == EnumCameraType.LidCamera)
                {
                    curPosX = _positionSystem.ReadCurrentStagePosition(EnumStageAxis.LidX);
                    _positionSystem.MoveAixsToStageCoord(EnumStageAxis.LidX, stepDistance, EnumCoordSetType.Relative);
                }
                else
                {
                    curPosX = _positionSystem.ReadCurrentStagePosition(EnumStageAxis.ShellX);
                    _positionSystem.MoveAixsToStageCoord(EnumStageAxis.ShellX, stepDistance, EnumCoordSetType.Relative);
                }
                currentStagePos.X = (float)curPosX;
                //_positionSystem.MoveToSystemCoordWithoutZAxis(new XYZTCoordinate() { X = stepDistance, Y = 0, Theta = 0 }, EnumCoordSetType.Relative);//.MoveAlongStageAxis(EnumStageAxis.X, stepDistance);
                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// 检查结果合法性
        /// </summary>
        /// <param name="searchResult"></param>
        /// <param name="lastResults"></param>
        /// <returns></returns>
        //bool CheckLegitimacyOfResults(PointF searchResult, PointF lastResults)
        //{
        //    var fovCenter = _positionSystem.ReadCurrentStageCoord();
        //    var currentCoord = _positionSystem.GetStageCoordWithinFOV(new XYZTCoordinate() { X = fovCenter.X, Y = fovCenter.Y }, searchResult);

        //    var lastfovCenter = _positionSystem.ReadCurrentStageCoord();
        //    var lastCoord = _positionSystem.GetStageCoordWithinFOV(new XYZTCoordinate() { X = fovCenter.X, Y = fovCenter.Y }, lastResults);
        //    double pixelSizeMagnif = _opticalSystem.CurrentPixelSizeMagnification.PixelWidth.GetValue(EnumUnit.Millimeter);
        //    if (Math.Abs(lastCoord.X - currentCoord.X) > (_systemConfig.ImageSize.Width / 20) * pixelSizeMagnif)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}
    }

    /*
     * 功能描述：计算物镜的真实放大倍率 .
     * 
     * Pixel Size: 每像素代表的真实物理尺寸.
     */
    public class PixelSizeCalculator : AVisionFunctionHelper
    {
        public ShapeMatchConfiguration _configForShapeMatch = null;
        /// <summary>
        /// 系统配置
        /// </summary>
        private SystemConfiguration _systemConfig
        {
            get { return SystemConfiguration.Instance; }
        }
        /// <summary>
        /// 定位系统
        /// </summary>
        private PositioningSystem _positionSystem
        {
            get { return PositioningSystem.Instance; }
        }
        private CameraManager _cameraManager
        {
            get { return CameraManager.Instance; }
        }
        /// <summary>
        /// 硬件系统
        /// </summary>
        private HardwareManager _hardwareManager
        {
            get { return HardwareManager.Instance; }
        }
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="patternRecognitionEngine"></param>
        //public PixelSizeCalculator(CognexPatternRecognition patternRecognitionEngine)
        //    : base(patternRecognitionEngine)
        //{
        //    _patternRecognitionEngine = patternRecognitionEngine;
        //}
        public PixelSizeCalculator(TemplateRecognitionTool templateRecognitionEngine)
            : base(templateRecognitionEngine)
        {
            _templateRecognitionEngine = templateRecognitionEngine;
            //Params = new ThetaAlignmentParames() { SearchStep = 0.5f };
        }
        public PixelSizeCalculator(GWShapeMatch templateRecognitionEngine)
            : base(templateRecognitionEngine)
        {
            _templateRecognitionEngine = templateRecognitionEngine;
            //Params = new ThetaAlignmentParames() { SearchStep = 0.5f };
        }
        public void LoadDetectConfig(ShapeMatchConfiguration config)
        {
            this._configForShapeMatch = config;
            //this._patternRecognitionEngine.PMAlignPattern = ConfigForShapeMatch.AlignPattern;

        }
        /// <summary>
        /// calculte the angle between y axis of camera and the x axis of the stage.
        /// </summary>
        public void CalculatePixelSize(out double pixelSize)
        {
            PointF locationRecog;
            double angle = 0;
            double score = 0;
            int recogniseCount = 0;
            bool succeed = CaptureRecognize(out locationRecog, out angle,out score,out recogniseCount).Key;
            if (!succeed)
            {
                MessageBox.Show("recognized failed!");
                pixelSize = float.NaN;
                return;
            }
            var baseCenterX = locationRecog.X;
            var baseCenterY = locationRecog.Y;
            var firstFovCenterX = 0d;
            var firstFovCenterY = 0d;
            if(_cameraManager.CurrentCameraConfig.CameraType==EnumCameraType.LidCamera)
            {
                firstFovCenterX = _positionSystem.ReadCurrentStagePosition(EnumStageAxis.LidX);
                firstFovCenterY = _positionSystem.ReadCurrentStagePosition(EnumStageAxis.LidY);
            }
            else
            {
                firstFovCenterX = _positionSystem.ReadCurrentStagePosition(EnumStageAxis.ShellX);
                firstFovCenterY = _positionSystem.ReadCurrentStagePosition(EnumStageAxis.ShellY);
            }


            double pixelSizeStandardMag = _hardwareManager.CurrentLensConfig.StandardCalibratedMag;
            int height = _cameraManager.CurrentCameraConfig.ImageSizeWidth / 10;
            double adjustDistance = height * pixelSizeStandardMag* _cameraManager.CurrentCameraConfig.PixelSize;

            #region Search the Bottom point
            PointF locationBottom;
            PointF locat1 = new PointF(), locat2 = new PointF();
            SearchPattern(EnumSearchDirection.Right, adjustDistance, out locationBottom, ref locat1);
            #endregion

            #region Search the top point
            var curFovCenterX = 0d;
            var curFovCenterY = 0d;
            if (_cameraManager.CurrentCameraConfig.CameraType == EnumCameraType.LidCamera)
            {
                curFovCenterX = _positionSystem.ReadCurrentStagePosition(EnumStageAxis.LidX);
                _positionSystem.MoveAixsToStageCoord(EnumStageAxis.LidX, firstFovCenterX- curFovCenterX,EnumCoordSetType.Relative);
            }
            else
            {
                curFovCenterX = _positionSystem.ReadCurrentStagePosition(EnumStageAxis.ShellX);
                _positionSystem.MoveAixsToStageCoord(EnumStageAxis.ShellX, firstFovCenterX - curFovCenterX, EnumCoordSetType.Relative);
            }

            if (_cameraManager.CurrentCameraConfig.CameraType == EnumCameraType.LidCamera)
            {
                curFovCenterY = _positionSystem.ReadCurrentStagePosition(EnumStageAxis.LidY);
                _positionSystem.MoveAixsToStageCoord(EnumStageAxis.LidY, firstFovCenterY - curFovCenterY, EnumCoordSetType.Relative);
            }
            else
            {
                curFovCenterY = _positionSystem.ReadCurrentStagePosition(EnumStageAxis.ShellY);
                _positionSystem.MoveAixsToStageCoord(EnumStageAxis.ShellY, firstFovCenterY - curFovCenterY, EnumCoordSetType.Relative);
            }

            PointF locationTop;
            SearchPattern(EnumSearchDirection.Left, adjustDistance, out locationTop, ref locat2);
            #endregion

            //_positionSystem.MoveToSystemCoord(firstFovCenter, EnumCoordSetType.Absolute);
            if (locat1 != null && locat2 != null)
            {
                pixelSize = (float)Math.Round(Math.Abs((float)locat1.X - (float)locat2.X) / Math.Abs(locationBottom.X - locationTop.X), 6);
            }
            else
            {
                pixelSize = double.NaN;
            }
          
        }
        public KeyValuePair<bool, CContourPatMatchResult> CaptureRecognize(out PointF ptResult, out double angle, out double score,out int recogniseCount)
        {
            var success = false;
            var resultJson = "";
            recogniseCount = 0;
            var capturedImage = _cameraEngine.CaptureImageOutputByte();
            //_patternRecognitionEngine.InputImage = CognexImageFormatter.GetHandler().ConvertToCogImage(capturedImage);
            var _shapeMatchEngine = _templateRecognitionEngine as GWShapeMatch;
            CContourPatMatchResult matchResult = null;
            if (_shapeMatchEngine != null)
            {
                _shapeMatchEngine.SetRunParam("MaxMatchNum", "2");
                _shapeMatchEngine.LoadTrain(_configForShapeMatch.TrainFileFullName);
                matchResult = _shapeMatchEngine.Run(capturedImage, _cameraManager.CurrentCameraConfig.ImageSizeWidth
                        , _cameraManager.CurrentCameraConfig.ImageSizeHeight, 0.5);
            }
            ptResult = new PointF();
            angle = 0;
            score = 0;
            if (matchResult != null)
            {
                //var matchResult = GWShapeMatch.Instance.Run(this.teSourceImageFilePath.Text.Trim(), @"D:\TestFolder\HIKImage\Pattern\aaa.contourmxml", 0.5);
                foreach (var item in matchResult.MatchInfoList)
                {
                    ptResult.X = item.MatchBox.CenterX;
                    ptResult.Y = item.MatchBox.CenterY;
                    angle = item.MatchBox.Angle;
                    score = item.Score;
                    success = true;
                }
                recogniseCount = matchResult.MatchInfoList.Count;
                //resultJson = JsonConvert.SerializeObject(matchResult);
            }
            if (!success)
            {
                //用于监控和定位ThetaAlignment的偏移问题，调试用
                CommonProcess.EnsureFolderExist("D:\\RecognizeFail");
                if (capturedImage == null)
                {
                    throw new EmergencyException("Image from Camera is NULL.");
                }
                Bitmap capturedImageSaved = BitmapFactory.BytesToBitmapFast(capturedImage, _cameraManager.CurrentCameraConfig.ImageSizeWidth
                        , _cameraManager.CurrentCameraConfig.ImageSizeHeight, PixelFormat.Format8bppIndexed); ;
                string failPath = string.Format("D:\\RecognizeFail\\CalibratePixelSize_{0}.bmp", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                capturedImageSaved.Save(failPath, ImageFormat.Bmp);
                capturedImageSaved.Dispose();
                capturedImageSaved = null;
            }
            return new KeyValuePair<bool, CContourPatMatchResult>(success, matchResult);
        }
        /// <summary>
        /// Search pattern algorithm.
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="initialOffset"></param>
        /// <param name="stepDistance"></param>
        /// <param name="searchResult"></param>
        private void SearchPattern(EnumSearchDirection direction, double stepDistance, out PointF searchResult, ref PointF currentStagePos)
        {
            searchResult = new PointF();
            double angle = 0;
            double score = 0;
            int recogniseCount = 0;
            PointF lastResult = new PointF();
            stepDistance = (direction == EnumSearchDirection.Right ? -Math.Abs(stepDistance) : Math.Abs(stepDistance));
            while (true)
            {

                bool succeed = CaptureRecognize(out searchResult, out angle,out score ,out recogniseCount).Key;
                if (!succeed)
                {
                    searchResult = lastResult;
                    if (UserCustomAction != null)
                    {
                        //UserCustomAction(new CameraAngleActionCallback()
                        //{
                        //    ImageCaptured = _patternRecognitionEngine.InputImage,
                        //    BestResultRecognized = _patternRecognitionEngine.PMAlignBestResult
                        //});
                    }
                    break;
                }

                if (UserCustomAction != null)
                {
                    //UserCustomAction(new CameraAngleActionCallback()
                    //{
                    //    ImageCaptured = _patternRecognitionEngine.InputImage,
                    //    BestResultRecognized = _patternRecognitionEngine.PMAlignBestResult
                    //});
                }

                if (!lastResult.IsEmpty)
                {
                    //if (!CheckLegitimacyOfResults(searchResult, lastResult))
                    if (recogniseCount > 1)
                    {
                        //不合法退出校准过程
                        MessageBox.Show("Please select a unique pattern that within one field of view.");
                        break;
                    }
                }
                var curPosX = 0d;
                lastResult = searchResult;
                //轴相对位移计算的步长++++++++++++++++++++++++++++++++++++++++
                if (_cameraManager.CurrentCameraConfig.CameraType == EnumCameraType.LidCamera)
                {
                    curPosX = _positionSystem.ReadCurrentStagePosition(EnumStageAxis.LidX);
                    _positionSystem.MoveAixsToStageCoord(EnumStageAxis.LidX, stepDistance, EnumCoordSetType.Relative);
                }
                else
                {
                    curPosX = _positionSystem.ReadCurrentStagePosition(EnumStageAxis.ShellX);
                    _positionSystem.MoveAixsToStageCoord(EnumStageAxis.ShellX, stepDistance, EnumCoordSetType.Relative);
                }
                currentStagePos.X = (float)curPosX;
                //_positionSystem.MoveToSystemCoordWithoutZAxis(new XYZTCoordinate() { X = stepDistance, Y = 0, Theta = 0 }, EnumCoordSetType.Relative);//.MoveAlongStageAxis(EnumStageAxis.X, stepDistance);
                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// 检查结果合法性(如果在某个位置找到了多个Pattern，则认为不合理20230713)
        /// </summary>
        /// <param name="searchResult"></param>
        /// <param name="lastResults"></param>
        /// <returns></returns>
        //private bool CheckLegitimacyOfResults(PointF searchResult, PointF lastResults)
        //{
        //    var fovCenter = _positionSystem.ReadCurrentStageCoord();
        //    var currentCoord = _positionSystem.GetStageCoordWithinFOV(new XYZTCoordinate() { X = fovCenter.X, Y = fovCenter.Y }, searchResult);

        //    var lastfovCenter = _positionSystem.ReadCurrentStageCoord();
        //    var lastCoord = _positionSystem.GetStageCoordWithinFOV(new XYZTCoordinate() { X = fovCenter.X, Y = fovCenter.Y }, lastResults);
        //    double pixelSizeStandardMag = _hardwareManager.CurrentLensConfig.StandardCalibratedMag;
        //    //double pixelSizeMagnif = _opticalSystem.CurrentPixelSizeMagnification.PixelWidth.GetValue(EnumUnit.Millimeter);
        //    if (Math.Abs(lastCoord.X - currentCoord.X) > (_cameraManager.CurrentCameraConfig.ImageSizeWidth / 20) * pixelSizeStandardMag)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}
    }

    /*
     * 功能描述：Calculate the optical center (optical center's stageCoord).
     * 
     * 通过此校准，达到CHUCK中心与相机光学中心对齐的目的。
     */
    //public class OpticalCenterCalculator : ACognexVisionBasedClass
    //{
    //    /// <summary>
    //    /// constructor
    //    /// </summary>
    //    /// <param name="patternRecognitionEngine"></param>
    //    public OpticalCenterCalculator(CognexPatternRecognition patternRecognitionEngine)
    //        : base(patternRecognitionEngine)
    //    {
    //        _patternRecognitionEngine = patternRecognitionEngine;
    //    }

    //    /// <summary>
    //    /// 计算光学中心点
    //    /// </summary>
    //    /// <returns></returns>
    //    public PointF CalculateOpticalCenter()
    //    {
    //        //1 识别
    //        PointF location1;
    //        bool succeed = CaptureRecognize(out location1, EnumRefCoordSystem.Center);
    //        if (!succeed)
    //        {
    //            MessageBox.Show("Recognized Failed，Cannot find the pattern in current field of view."); return PointF.Empty;
    //        }

    //        //2 旋转180
    //        _positionSystem.MoveToSpecifiedStageCoordTheta(180, EnumCoordSetType.Relative);
    //        Thread.Sleep(1000);

    //        //3 再识别
    //        PointF location2;
    //        succeed = CaptureRotateRecognize(out location2, EnumRefCoordSystem.Center, 180);
    //        if (!succeed)
    //        {
    //            MessageBox.Show("Recognized Failed，Cannot find the pattern in current field of view after rotated 180 degrees."); return PointF.Empty;
    //        }

    //        //4 处理结果
    //        location2.X = -location2.X;
    //        location2.Y = -location2.Y;

    //        var centerX = (location1.X + location2.X) / 2;
    //        var centerY = (location1.Y + location2.Y) / 2;

    //        float xReferLeftTop = (float)(centerX);
    //        float yReferLeftTop = (float)(centerY);
    //        var opticalCenter = _positionSystem.GetStageCoordWithinFOV(_positionSystem.ReadCurrentStageCoord(), new PointF(xReferLeftTop, yReferLeftTop));

    //        return new PointF((float)opticalCenter.X, (float)opticalCenter.Y);
    //    }
    ////}

    /*
     * 功能描述： 计算激光测距仪光斑中心与CHUCK中心对齐时的STGAE读数.
     * 通过此步骤可以得到激光测距仪的STAGE坐标。
     */
    //public class LaserCenterCalculator : APositioningBasedClass
    //{
    //    /// <summary>
    //    /// 激光测距仪.
    //    /// </summary>
    //    private ILaserDisplacement _LaserDisplacement
    //    {
    //        get
    //        {
    //            return HardwareManager.GetHandler().LaserDisplacement;
    //        }
    //    }

    //    /// <summary>
    //    /// 记录开始校准时的坐标点，Keyence的光斑打在这个点上时的stage坐标。
    //    /// </summary>
    //    private ACoordinate _firstKeyenceFovPoint = null;

    //    /// <summary>
    //    /// 构造函数
    //    /// </summary>
    //    /// <param name="patternRecognitionEngine"></param>
    //    public LaserCenterCalculator()
    //        : base()
    //    {
    //    }

    //    /// <summary>
    //    /// 设置校准开始点
    //    /// </summary>
    //    /// <param name="firstKeyenceFovPoint"></param>
    //    public void SetFirstKeyenceFovPoint(ACoordinate firstKeyenceFovPoint)
    //    {
    //        _firstKeyenceFovPoint = firstKeyenceFovPoint;
    //    }

    //    /// <summary>
    //    /// 计算KeyenceCenter中心点System坐标。
    //    /// </summary>
    //    /// <returns></returns>
    //    public PointF CalculateKeyenceCenter()
    //    {/*-4.965*/
    //        //_positionSystem.MoveToSpecifiedZ(-6.087, EnumCoordSetType.Absolute);

    //        //Record the first start point.
    //        SetFirstKeyenceFovPoint(_positionSystem.ReadCurrentStageCoord());

    //        //search the right edge point.
    //        var rightEdgePoint = SearchEdgePointByDirection(EnumSearchType.Right);
    //        //search the left edge point.
    //        var leftEdgePoint = SearchEdgePointByDirection(EnumSearchType.Left);
    //        //search the top edge point.
    //        var topEdgePoint = SearchEdgePointByDirection(EnumSearchType.Top);
    //        //search the bottom edge point.
    //        var bottomEdgePoint = SearchEdgePointByDirection(EnumSearchType.Bottom);

    //        //Get the center.
    //        var center = new PointF((float)(rightEdgePoint.X + leftEdgePoint.X) / 2, (float)(topEdgePoint.Y + bottomEdgePoint.Y) / 2);
    //        //Home the start positionX.
    //        _positionSystem.MoveToStageCoord(_firstKeyenceFovPoint, EnumCoordSetType.Absolute);
    //        ACoordinate laserCenter = new XYZTCoordinate() { X = center.X, Y = center.Y, Z = _firstKeyenceFovPoint.Z, Theta = _firstKeyenceFovPoint.Theta };
    //        laserCenter = _positionSystem.CoordConverter.ConvertStageCoordToSystemCoord(laserCenter, EnumCoordSetType.Absolute);
    //        //End and return the center result;
    //        return new PointF((float)laserCenter.X, (float)laserCenter.Y);
    //    }

    //    /// <summary>
    //    /// 寻找Chuck边缘点算法
    //    /// </summary>
    //    /// <param name="searchType"></param>
    //    /// <returns></returns>
    //    private ACoordinate SearchEdgePointByDirection(EnumSearchType searchType)
    //    {
    //        double searchDirection = 1;
    //        EnumStageAxis axis = EnumStageAxis.X;
    //        switch (searchType)
    //        {
    //            case EnumSearchType.Right:
    //                searchDirection = 1;
    //                axis = EnumStageAxis.X;
    //                break;
    //            case EnumSearchType.Left:
    //                searchDirection = -1;
    //                axis = EnumStageAxis.X;
    //                break;
    //            case EnumSearchType.Top:
    //                searchDirection = -1;
    //                axis = EnumStageAxis.Y;
    //                break;
    //            case EnumSearchType.Bottom:
    //                searchDirection = 1;
    //                axis = EnumStageAxis.Y;
    //                break;
    //        }

    //        //Move to the first start point.
    //        _positionSystem.MoveToStageCoord(_firstKeyenceFovPoint, EnumCoordSetType.Absolute);

    //        //记录逐步进值遍历时的位置点,<点坐标,KeyceValue,序号>
    //        List<Tuple<ACoordinate, double, int>> pointRecordListTemp = new List<Tuple<ACoordinate, double, int>>();
    //        //步进值集合，5个精度级别
    //        double[] stepArray = new double[] { 10d, 2d, 0.2d, 0.01d, 0.001d };
    //        //步进精度级别
    //        int precisionLevel = 0;
    //        //移动的次数   对点编号
    //        int moveCount = 0, iIndex = 0;
    //        double moveDistance = 180;
    //        ACoordinate targetPoint = null;
    //        while (true)
    //        {
    //            if (moveCount * stepArray[precisionLevel] < moveDistance)
    //            {
    //                //记录当前点的坐标和激光测距仪读数
    //                var location = _positionSystem.ReadCurrentStageCoord();
    //                bool isOverRangeAtPisitiveSide = false, isOverRangeAtNegetiveSide = false;
    //                var keyenceValue = _LaserDisplacement.GetValue(ref isOverRangeAtPisitiveSide, ref isOverRangeAtNegetiveSide)[0];
    //                pointRecordListTemp.Add(Tuple.Create<ACoordinate, double, int>(location, keyenceValue, iIndex++));

    //                //开始步进
    //                _positionSystem.MoveAlongStageAxis(axis, searchDirection * stepArray[precisionLevel]);
    //                moveCount++;
    //            }
    //            else
    //            {
    //                //反序
    //                pointRecordListTemp = pointRecordListTemp.OrderByDescending(r => r.Item3).ToList();
    //                //得到第一个符合条件的值，即为边缘点的近似位置。逐精度步进即可得到相当精确的位置值。
    //                targetPoint = pointRecordListTemp.Find(delegate(Tuple<ACoordinate, double, int> record)
    //                {
    //                    return !record.Item2.Equals(double.NaN);
    //                }).Item1;

    //                //清空记录
    //                pointRecordListTemp.Clear();
    //                iIndex = 0;

    //                //移动到上步得到的结果位置。准备开始下一循环。
    //                _positionSystem.MoveToStageCoord(targetPoint, EnumCoordSetType.Absolute);
    //                Thread.Sleep(10);
    //                moveCount = 0;
    //                precisionLevel++;
    //                moveDistance = stepArray[precisionLevel - 1];
    //            }

    //            //退出
    //            if (precisionLevel == 4)
    //            {
    //                break;
    //            }
    //        }
    //        return targetPoint;
    //    }
    //}

    /*
     * 功能描述：计算WAFER中心的坐标。
     * 
     * 原理：通过寻边算法实现对Wafer中心的定位。
     */
    //public class WaferCenterCalculator : APositioningBasedClass
    //{
    //    /// <summary>
    //    /// 激光测距仪
    //    /// </summary>
    //    private ILaserDisplacement _LaserDisplacement
    //    {
    //        get
    //        {
    //            return HardwareManager.GetHandler().LaserDisplacement;
    //        }
    //    }

    //    /// <summary>
    //    /// 记录开始校准时的坐标点，Keyence的光斑打在这个点上时的stage坐标。
    //    /// </summary>
    //    private ACoordinate _firstKeyenceFovPoint = null;

    //    /// <summary>
    //    /// 晶圆的厚度.
    //    /// </summary>
    //    private float WaferThickness
    //    {
    //        get;
    //        set;
    //    }

    //    /// <summary>
    //    /// 构造函数
    //    /// </summary>
    //    /// <param name="patternRecognitionEngine"></param>
    //    public WaferCenterCalculator()
    //        : base()
    //    {
    //        WaferThickness = 0.60f;
    //    }

    //    /// <summary>
    //    /// 设置校准开始点
    //    /// </summary>
    //    /// <param name="firstKeyenceFovPoint"></param>
    //    public void SetFirstKeyenceFovPoint(ACoordinate firstKeyenceFovPoint)
    //    {
    //        _firstKeyenceFovPoint = firstKeyenceFovPoint;
    //    }

    //    /// <summary>
    //    /// 计算KeyenceCenter中心点stage坐标。
    //    /// </summary>
    //    /// <returns></returns>
    //    public PointF CalculateWaferCenter()
    //    {
    //        /**/
    //        _positionSystem.MoveToSpecifiedZ(-4.965, EnumCoordSetType.Absolute);
    //        //Record the first start point.
    //        SetFirstKeyenceFovPoint(_positionSystem.ReadCurrentSystemCoord());

    //        //search the right edge point.
    //        var rightEdgePoint = SearchEdgePointByDirection(EnumSearchType.Right);
    //        //search the left edge point.
    //        var leftEdgePoint = SearchEdgePointByDirection(EnumSearchType.Left);
    //        //search the top edge point.
    //        var topEdgePoint = SearchEdgePointByDirection(EnumSearchType.Top);
    //        //search the bottom edge point.
    //        var bottomEdgePoint = SearchEdgePointByDirection(EnumSearchType.Bottom);

    //        //Home the start positionX.
    //        _positionSystem.MoveToSystemCoord(_firstKeyenceFovPoint, EnumCoordSetType.Absolute);
    //        //End and return the center result;           
    //        return new PointF((float)(rightEdgePoint.X + leftEdgePoint.X) / 2, (float)(topEdgePoint.Y + bottomEdgePoint.Y) / 2);
    //    }

    //    /// <summary>
    //    /// 寻找Wafer边缘点算法
    //    /// </summary>
    //    /// <param name="searchType"></param>
    //    /// <returns></returns>
    //    private ACoordinate SearchEdgePointByDirection(EnumSearchType searchType)
    //    {
    //        double searchDirection = 1;
    //        EnumStageAxis axis = EnumStageAxis.X;
    //        switch (searchType)
    //        {
    //            case EnumSearchType.Right:
    //                searchDirection = 1;
    //                axis = EnumStageAxis.X;
    //                break;
    //            case EnumSearchType.Left:
    //                searchDirection = -1;
    //                axis = EnumStageAxis.X;
    //                break;
    //            case EnumSearchType.Top:
    //                searchDirection = -1;
    //                axis = EnumStageAxis.Y;
    //                break;
    //            case EnumSearchType.Bottom:
    //                searchDirection = 1;
    //                axis = EnumStageAxis.Y;
    //                break;
    //        }

    //        //Move to the first start point.
    //        _positionSystem.MoveToSystemCoord(_firstKeyenceFovPoint, EnumCoordSetType.Absolute);

    //        //记录逐步进值遍历时的位置点,<点坐标,KeyceValue,高度差,序号>
    //        List<Tuple<ACoordinate, double, double, int>> pointRecordListTemp = new List<Tuple<ACoordinate, double, double, int>>();
    //        //步进值集合，5个精度级别
    //        double[] stepArray = new double[] { 20d, 2d, 0.2d, 0.04d, 0.005d };
    //        //步进精度级别
    //        int precisionLevel = 0;
    //        //移动的次数   对点编号
    //        int moveCount = 0, iIndex = 0;
    //        double moveDistance = 150;
    //        ACoordinate targetPoint = null;
    //        while (true)
    //        {
    //            if (moveCount * stepArray[precisionLevel] <= moveDistance)
    //            {
    //                //记录当前点的坐标和激光测距仪读数
    //                var location = _positionSystem.ReadCurrentSystemCoord();
    //                bool isOverRangeAtPisitiveSide = false, isOverRangeAtNegetiveSide = false;
    //                var keyenceValue = _LaserDisplacement.GetValue(ref isOverRangeAtPisitiveSide, ref isOverRangeAtNegetiveSide)[0];
    //                var poortemp = pointRecordListTemp.Count == 0 ? keyenceValue : (pointRecordListTemp.Last().Item2);
    //                var poorValue = (float)Math.Abs(keyenceValue - poortemp);
    //                if (keyenceValue.Equals(float.NaN))
    //                {
    //                    poorValue = WaferThickness + 0.5f;
    //                }
    //                pointRecordListTemp.Add(Tuple.Create<ACoordinate, double, double, int>(location, keyenceValue, poorValue, iIndex++));

    //                //开始步进
    //                _positionSystem.MoveAlongStageAxis(axis, searchDirection * stepArray[precisionLevel]);
    //                Thread.Sleep(10);
    //                moveCount++;
    //            }
    //            else
    //            {
    //                //得到第一个符合条件的值，即为边缘点的近似位置。逐精度步进即可得到相当精确的位置值。
    //                var targetPoint0 = pointRecordListTemp.Find(delegate(Tuple<ACoordinate, double, double, int> record)
    //                {
    //                    return record.Item3 >= WaferThickness && record.Item3 <= WaferThickness + 1f;//wafer 厚度
    //                });

    //                if (targetPoint0 == null)
    //                {
    //                    targetPoint = pointRecordListTemp.Last().Item1;
    //                }
    //                else
    //                {
    //                    targetPoint = pointRecordListTemp.Find(delegate(Tuple<ACoordinate, double, double, int> record)
    //                    {
    //                        return record.Item4 == (targetPoint0.Item4 == 0 ? 0 : (targetPoint0.Item4 - 1));
    //                    }).Item1;
    //                }


    //                //清空记录
    //                pointRecordListTemp.Clear();
    //                iIndex = 0;

    //                //移动到上步得到的结果位置。准备开始下一循环。
    //                _positionSystem.MoveToSystemCoord(targetPoint, EnumCoordSetType.Absolute);
    //                Thread.Sleep(10);
    //                moveCount = 0;
    //                precisionLevel++;
    //                moveDistance = stepArray[precisionLevel - 1];
    //            }

    //            //退出
    //            if (precisionLevel == 4)
    //            {
    //                break;
    //            }
    //        }
    //        return targetPoint;
    //    }
    //}

    ///// <summary>
    ///// 负责杂光修正的功能类
    ///// </summary>
    //public sealed class HotSpotCalibration
    //{
    //    #region 变量与属性
    //    /// <summary>
    //    /// 相机控制
    //    /// </summary>
    //    private CameraControllerWrapper _cameraController;
    //    #endregion

    //    #region 初始化
    //    /// <summary>
    //    /// 构造函数
    //    /// </summary>
    //    /// <param name="cameraController"></param>
    //    public HotSpotCalibration(CameraControllerWrapper cameraController)
    //    {
    //        _cameraController = cameraController;
    //    }
    //    #endregion

    //    #region 杂光修正训练
    //    /// <summary>
    //    /// 执行杂光修正训练数据提取
    //    /// </summary>
    //    /// <param name="trainResultSavedPath">训练结果保存的路径</param>
    //    /// <param name="trainCount">训练多少次取平均</param>
    //    public void DoCalibrationTrain(string trainResultSavedPath, int trainCount)
    //    {
    //        //拍取trainCount幅灰度照片
    //        List<Bitmap> greyPictureList = new List<Bitmap>();
    //        if (_cameraController.IsCameraUsable)
    //        {
    //            for (int i = 0; i < trainCount; i++)
    //            {
    //                var pic = _cameraController.CaptureImage();
    //                if (pic != null) greyPictureList.Add(pic);
    //            }
    //        }

    //        //取平均灰度
    //        var resultBmp = Skyverse.BIRCH.BirchGlobalSettingClsLib.CommonProcess.ProcessAverageGrayImage(greyPictureList);

    //        //保存训练的结果
    //        SaveGrayBitmapDataToTxtFile(resultBmp, trainResultSavedPath);
    //    }

    //    /// <summary>
    //    ///  获取杂光修正的结果
    //    /// </summary>
    //    /// <param name="trainResultSavedPath"></param>
    //    /// <returns></returns>
    //    public List<short[]> GetTrainedData(string trainResultSavedPath)
    //    {
    //        List<short[]> data = new List<short[]>();
    //        string fileFullName = trainResultSavedPath;
    //        if (String.IsNullOrEmpty(fileFullName) || !File.Exists(fileFullName))
    //        {
    //            return data;
    //        }
    //        using (StreamReader sr = new StreamReader(fileFullName))
    //        {
    //            try
    //            {
    //                while (true)
    //                {
    //                    string strLine = sr.ReadLine();
    //                    if (strLine == null)
    //                    {
    //                        break;
    //                    }
    //                    if (String.IsNullOrEmpty(strLine))
    //                    {
    //                        continue;
    //                    }
    //                    short[] singleLineData = strLine.ToArray(new char[] { '\t' }, x => Int16.Parse(x));
    //                    if (singleLineData == null)
    //                    {
    //                        continue;
    //                    }
    //                    data.Add(singleLineData);
    //                }
    //            }
    //            finally
    //            {
    //                sr.Close();
    //            }
    //        }
    //        return data;
    //    }
    //    #endregion

    //    #region 内部方法
    //    /// <summary>
    //    /// 保存灰度图像到文本文件
    //    /// </summary>
    //    /// <param name="txtFilePath"></param>
    //    private void SaveGrayBitmapDataToTxtFile(Bitmap bitmap, string txtFilePath)
    //    {
    //        List<short[]> data = ConvertBitmapDataToList(bitmap);
    //        if (data == null || data.Count == 0)
    //        {
    //            return;
    //        }
    //        string fileFullName = txtFilePath;
    //        if (String.IsNullOrEmpty(fileFullName))
    //        {
    //            return;
    //        }
    //        Skyverse.BIRCH.GlobalSettingClsLib.CommonProcess.EnsureFileCreationFolderExist(fileFullName);
    //        using (StreamWriter sw = new StreamWriter(String.Format("{0}", fileFullName)))
    //        {
    //            try
    //            {
    //                foreach (var eachLineData in data)
    //                {
    //                    string strLine = "";
    //                    foreach (var eachData in eachLineData)
    //                    {
    //                        sw.Write(String.Format("{0}\t", eachData));
    //                    }
    //                    sw.WriteLine(strLine);
    //                }
    //            }
    //            finally
    //            {
    //                sw.Close();
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// 获取图片的灰度阵列分布值
    //    /// </summary>
    //    /// <param name="inputbmp"></param>
    //    private List<short[]> ConvertBitmapDataToList(Bitmap inputbmp)
    //    {
    //        List<short[]> data = new List<short[]>();
    //        BitmapData dataIn = inputbmp.LockBits(new Rectangle(0, 0, inputbmp.Width, inputbmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format8bppIndexed);
    //        unsafe
    //        {

    //            byte* pIn = (byte*)(dataIn.Scan0.ToPointer());    //指向源文件首地址               
    //            for (int y = 0; y < dataIn.Height; y++)  //列扫描
    //            {
    //                short[] rowData = new short[dataIn.Width];
    //                for (int x = 0; x < dataIn.Width; x++)   //行扫描
    //                {
    //                    rowData[x] = pIn[0];
    //                    pIn += 1;     //指针后移1个分量位置
    //                }
    //                pIn += dataIn.Stride - dataIn.Width * 1;
    //                data.Add(rowData);
    //            }
    //        }
    //        inputbmp.UnlockBits(dataIn);
    //        return data;
    //    }
    //    #endregion
    //}   

}
