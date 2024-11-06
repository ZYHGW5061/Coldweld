using ConfigurationClsLib;
using GlobalDataDefineClsLib;
using PositioningSystemClsLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using WestDragon.Framework.UtilityHelper;

namespace CalibrationSystemClsLib
{
    /// <summary>
    /// 校准系统，用来执行BIRCH系统的各项校准工作。
    /// </summary>
    public class CalibrationSystem  
    {
        private static volatile CalibrationSystem _instance = new CalibrationSystem();
        private static readonly object _lockObj = new object();
        /// <summary>
        /// 获取单例对象
        /// </summary>
        /// <returns></returns>
        public static CalibrationSystem Instance
        {
            get
            {
                if(_instance==null)
                {
                    lock(_lockObj)
                    {
                        if(_instance==null)
                        {
                            _instance = new CalibrationSystem();
                        }
                    }
                }
                return _instance;
            }
        }

        #region Properties


        /// <summary>
        /// 系统配置
        /// </summary>
        private SystemConfiguration _systemConfig
        {
            get { return SystemConfiguration.Instance; }
        }

        /// <summary>
        /// 执行模式识别的功能类
        /// </summary>
        //public CognexPatternRecognition PatternRecognizer
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// 负责校准相机选准角度和偏移校准的功能类
        /// </summary>
        //private CameraAngleCalculator _cameraAngleCalibrator
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// 负责校准相机镜头实际放大倍率的功能类
        /// </summary>
        private PixelSizeCalculator _pixelSizeCalibrator
        {
            get;
            set;
        }
        CameraAngleCalculator _cameraAngleCalibrator
        {
            get;
            set;
        }
        /// <summary>
        ///  负责校准光学中心位置的功能类
        /// </summary>
        //private OpticalCenterCalculator _opticalCenterCalibrator
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// 负责校准Keyence激光测距仪中心位置的功能类
        /// </summary>
        //private LaserCenterCalculator _laserCenterCalibrator
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// 负责校准Keyence激光测距仪中心位置的功能类
        /// </summary>
        //private WaferCenterCalculator _waferCenterCalibrator
        //{
        //    get;
        //    set;
        //}        
        #endregion

        #region Initialize
        /// <summary>
        /// Init ，校准系统
        /// </summary>
        private CalibrationSystem() 
        {
            InitCalibrationEngines();
        }


        /// <summary>
        /// Init  calibration engines.
        /// </summary>
        private void InitCalibrationEngines() 
        {
            //var shapeMatcher = new GWShapeMatch();
            _pixelSizeCalibrator = new PixelSizeCalculator(shapeMatcher);
            _cameraAngleCalibrator = new CameraAngleCalculator(shapeMatcher);
        }
        public void LoadDetectConfig(ShapeMatchConfiguration config)
        {
            _pixelSizeCalibrator.LoadDetectConfig(config);
            _cameraAngleCalibrator.LoadDetectConfig(config);
        }
        #endregion

        #region Calibration
        /// <summary>
        /// Do calculate camera angle.
        /// </summary>
        /// <returns></returns>
        public double DoCalculateCameraAngle(Action<ACustomActionBaseClass> action)
        {
            if (_cameraAngleCalibrator != null)
            {
                if (!_cameraAngleCalibrator.UserCustomAction.CheckDelegateRegistered(action))
                {
                    _cameraAngleCalibrator.UserCustomAction = action;
                }
                double angle = double.NaN;
                _cameraAngleCalibrator.CalculateCameraAngle(out angle);
                return angle;
            }
            else
            {
                return double.NaN;
            }
        }

        /// <summary>
        /// Do calculate Pixel Size.
        /// </summary>
        /// <returns></returns>
        public double DoCalculatePixelSize(Action<ACustomActionBaseClass> action)
        {
            if (_pixelSizeCalibrator != null)
            {
                if (!_pixelSizeCalibrator.UserCustomAction.CheckDelegateRegistered(action))
                {
                    _pixelSizeCalibrator.UserCustomAction = action;
                }
                double pixelSize = double.NaN;
                _pixelSizeCalibrator.CalculatePixelSize(out pixelSize);
                return pixelSize;
            }
            else
            {
                return double.NaN;
            }
        }

        /// <summary>
        /// Do calculate camera angle.
        /// </summary>
        /// <returns></returns>
        //public PointF DoCalculateOpticalCenter()
        //{
        //    if (_opticalCenterCalibrator != null)
        //    {
        //        return _opticalCenterCalibrator.CalculateOpticalCenter();
        //    }
        //    else
        //    {
        //        return PointF.Empty;
        //    }
        //}

        /// <summary>
        /// Do calculate keyence center.
        /// </summary>
        /// <returns></returns>
        //public PointF DoCalculateKeyenceCenter()
        //{
        //    if (_laserCenterCalibrator != null)
        //    {
        //        return _laserCenterCalibrator.CalculateKeyenceCenter();
        //    }
        //    else
        //    {
        //        return PointF.Empty;
        //    }
        //}

        /// <summary>
        /// Do calculate wafer center.
        /// </summary>
        /// <returns></returns>
        //public PointF DoCalculateWaferCenter()
        //{
        //    if (_waferCenterCalibrator != null)
        //    {
        //        return _waferCenterCalibrator.CalculateWaferCenter();
        //    }
        //    else
        //    {
        //        return PointF.Empty;
        //    }
        //}        
        #endregion

        public void Do3dmapping()
        {

        }
    }
}
