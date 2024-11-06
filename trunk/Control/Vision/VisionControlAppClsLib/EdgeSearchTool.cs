using CameraControllerClsLib;
using GlobalDataDefineClsLib;
using GWVisionAlgorithmClsLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilityToolsClsLib;
using VisionDesigner.ContourPatMatch;
using WelderTerminal.HardwareManagerClsLib;
using WestDragon.Framework.BaseLoggerClsLib;
using WestDragon.Framework.UtilityHelper;

namespace VisionHelperClsLib
{
    public class EdgeSearchTool
    {
        /// 特征识别器
        /// </summary>
        public TemplateRecognitionTool TemplateRecognizer
        { get { return _templateRecognitionEngine; } }

        /// <summary>
        /// 参数
        /// </summary>
        public ThetaAlignmentParames Params { get; set; }

        /// <summary>
        /// Train data for ThetaAlignment. Fov center\pattern center\pattern.
        /// </summary>
        public ShapeMatchConfiguration _configForShapeMatch = null;

        /// <summary>
        /// 硬件系统
        /// </summary>
        protected HardwareManager _hardWareManager
        {
            get { return HardwareManager.Instance; }
        }
        private CameraManager _cameraManager
        {
            get { return CameraManager.Instance; }
        }

        public EdgeSearchTool(GWShapeMatch templateRecognitionEngine)
            : base(templateRecognitionEngine)
        {
            _templateRecognitionEngine = templateRecognitionEngine;
            Params = new ThetaAlignmentParames() { SearchStep = 0.5f };
        }
        /// <summary>
        /// 加载训练数据
        /// </summary>
        public void LoadDetectConfig(ShapeMatchConfiguration config)
        {
            this._configForShapeMatch = config;
            //this._patternRecognitionEngine.PMAlignPattern = ConfigForShapeMatch.AlignPattern;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ptResult"></param>
        /// <param name="angle"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public new KeyValuePair<bool, CContourPatMatchResult> CaptureRecognize(out PointF ptResult, out float angle,out float score,out int recogniseCount,float acceptScore=0.5f)
        {
            var success = false;
            var resultJson = "";
            var capturedImage = _cameraEngine.CaptureImageOutputByte();
            //_patternRecognitionEngine.InputImage = CognexImageFormatter.GetHandler().ConvertToCogImage(capturedImage);
            var _shapeMatchEngine = _templateRecognitionEngine as GWShapeMatch;
            _shapeMatchEngine.SetRunParam("MinScore", acceptScore.ToString());
            CContourPatMatchResult matchResult = null;
            if (_shapeMatchEngine!=null)
            {
                _shapeMatchEngine.LoadTrain(_configForShapeMatch.TrainFileFullName);
                matchResult = _shapeMatchEngine.Run(capturedImage, _cameraManager.CurrentCameraConfig.ImageSizeWidth
                        , _cameraManager.CurrentCameraConfig.ImageSizeHeight, acceptScore, _configForShapeMatch.ROILeftUpperPoint.X+ _configForShapeMatch.ROIWidth/2
                        , _configForShapeMatch.ROILeftUpperPoint.Y + _configForShapeMatch.ROIHeight / 2, _configForShapeMatch.ROIWidth , _configForShapeMatch.ROIHeight);
            }
            ptResult = new PointF();
            angle = 0;
            score = 0;
            recogniseCount = 0;
            if (matchResult != null)
            {
                LogRecorder.RecordLog(EnumLogContentType.Debug, $"CaptureRecognize,MatchResultCount:{matchResult.MatchInfoList.Count}.");
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
            if(success)
            {
                if (_systemConfig.JobConfig.RecogniseResulSaveOption == EnumRecogniseResulSaveOption.SaveOK
                    || _systemConfig.JobConfig.RecogniseResulSaveOption == EnumRecogniseResulSaveOption.AllSave)
                {
                    CommonProcess.EnsureFolderExist("D:\\RecognizeSuccess");
                    if (capturedImage == null)
                    {
                        throw new EmergencyException("Image from Camera is NULL.");
                    }
                    Bitmap capturedImageSaved = BitmapFactory.BytesToBitmapFast(capturedImage, _cameraManager.CurrentCameraConfig.ImageSizeWidth
                            , _cameraManager.CurrentCameraConfig.ImageSizeHeight, PixelFormat.Format8bppIndexed); ;
                    string failPath = string.Format("D:\\RecognizeSuccess\\Detect_{0}.bmp", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                    capturedImageSaved.Save(failPath, ImageFormat.Bmp);
                    capturedImageSaved.Dispose();
                    capturedImageSaved = null;
                }
            }
            if (!success)
            {
                if (_systemConfig.JobConfig.RecogniseResulSaveOption == EnumRecogniseResulSaveOption.SaveNG
                    || _systemConfig.JobConfig.RecogniseResulSaveOption == EnumRecogniseResulSaveOption.AllSave)
                {
                    LogRecorder.RecordLog(WestDragon.Framework.BaseLoggerClsLib.EnumLogContentType.Error, "Recognize Fail.");
                    //用于监控和定位ThetaAlignment的偏移问题，调试用
                    CommonProcess.EnsureFolderExist("D:\\RecognizeFail");
                    if (capturedImage == null)
                    {
                        throw new EmergencyException("Image from Camera is NULL.");
                    }
                    Bitmap capturedImageSaved = BitmapFactory.BytesToBitmapFast(capturedImage, _cameraManager.CurrentCameraConfig.ImageSizeWidth
                            , _cameraManager.CurrentCameraConfig.ImageSizeHeight, PixelFormat.Format8bppIndexed); ;
                    string failPath = string.Format("D:\\RecognizeFail\\Detect_{0}.bmp", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                    capturedImageSaved.Save(failPath, ImageFormat.Bmp);
                    capturedImageSaved.Dispose();
                    capturedImageSaved = null;
                }
            }
            return new KeyValuePair<bool, CContourPatMatchResult>(success, matchResult);
        }

        
    }
}
