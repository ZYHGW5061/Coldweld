using CameraControllerClsLib;
using ConfigurationClsLib;
using GlobalDataDefineClsLib;
using GlobalToolClsLib;
using HardwareManagerClsLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WestDragon.Framework.BaseLoggerClsLib;
using WestDragon.Framework.UtilityHelper;

namespace CameraControllerWrapperClsLib
{
    public class CameraControllerWrapper
    {
        private static readonly object _lockObj = new object();
        private static volatile CameraControllerWrapper _instance;
        public static CameraControllerWrapper Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new CameraControllerWrapper();
                        }
                    }
                }
                return _instance;
            }
        }
        /// <summary>
        /// 获取相机类型
        /// </summary>
        public EnumCameraType CameraType
        {
            get
            {
                return _cameraManager.CurrentCameraType;
            }
        }

        /// <summary>
        /// 硬件系统
        /// </summary>
        private HardwareManager _hardwareManager
        {
            get { return HardwareManager.Instance; }
        }
        private CameraManager _cameraManager
        {
            get { return CameraManager.Instance; }
        }
        /// <summary>
        /// 系统配置
        /// </summary>
        private SystemConfiguration _systemConfig
        {
            get { return SystemConfiguration.Instance; }
        }


        /// <summary>
        /// 记录相机拍照得到的图像
        /// </summary>
        private Bitmap _capturedImage = null;
        private byte[] _capturedImageByte = null;
        /// <summary>
        /// 当前相机
        /// </summary>
        private ICameraController _currentCamera
        {
            get
            {
                return _cameraManager.CurrentCamera;
            }
        }

        /// <summary>
        /// 获取相机是否可用
        /// </summary>
        public bool IsCameraUsable
        {
            get
            {
                return _currentCamera == null ? false : (_currentCamera.IsConnect);
            }
        }

        /// <summary>
        /// 流程控制
        /// </summary>
        private RunStatusController StatusController
        {
            get;
            set;
        }

        /// <summary>
        /// 初始化，相机控制的二次封装
        /// </summary>
        private CameraControllerWrapper()
        {
            StatusController = new RunStatusController();
        }


        /// <summary>
        /// 注册相机处理事件方法
        /// </summary>
        private void RegisterCameraHandler()
        {
            if (_currentCamera != null && !_currentCamera.SingleImageDataAcquiredAction.CheckDelegateRegistered((Action<byte[]>)ProcessSingleImageAcquired))
            {
                _currentCamera.SingleImageDataAcquiredAction += new Action<byte[]>(ProcessSingleImageAcquired);
            }
        }

        /// <summary>
        /// 反注册相机处理事件方法
        /// </summary>
        private void UnregisterCameraHandler()
        {
            if (_currentCamera != null && _currentCamera.SingleImageDataAcquiredAction.CheckDelegateRegistered((Action<byte[]>)ProcessSingleImageAcquired))
            {
                _currentCamera.SingleImageDataAcquiredAction -= new Action<byte[]>(ProcessSingleImageAcquired);
            }
        }


        /// <summary>
        /// 拍照
        /// </summary>
        /// <returns></returns>
        public Bitmap CaptureImage()
        {
            if (!_currentCamera.IsConnect)
            {
                _currentCamera.Connect();
            }

            var retryOperator = new RetryMechanismOperation();
            retryOperator.MaxRetryCount = 3;
            retryOperator.ProcessFunc = StartCapture;
            retryOperator.Run();
           
            return _capturedImage;
        }
        public byte[] CaptureImageOutputByte()
        {
            if (!_currentCamera.IsConnect)
            {
                _currentCamera.Connect();
            }

            var retryOperator = new RetryMechanismOperation();
            retryOperator.MaxRetryCount = 3;
            retryOperator.ProcessFunc = StartCapture;
            retryOperator.Run();        
            return _capturedImageByte;
        }

        /// <summary>
        /// 带超时重试机制的拍照
        /// </summary>
        /// <returns></returns>
        private bool StartCapture()
        {
            try
            {
                if (IsCameraUsable)
                {
                    if (!_currentCamera.IsConnect)
                    {
                        _currentCamera.Connect();
                    }
                    if (_capturedImage != null)
                    {
                        _capturedImage.Dispose();
                        _capturedImage = null;
                    }
                    LogRecorder.RecordLog(EnumLogContentType.Debug, $"CameraControllerWrapper:StartCapture.CutrrentCamera:{_cameraManager.CurrentCameraType}.");
                    StatusController.Start();
                    StatusController.Pause();
                    RegisterCameraHandler();

                    _currentCamera.StartGrabbing();
                    EnumStatus status;
                    if (!StatusController.CheckStatus(out status, 20000))
                    {
                        if (status == EnumStatus.Aborted)
                        {
                            LogRecorder.RecordLog(EnumLogContentType.Info, "CameraControllerWrapper:EnumStatus.Aborted.");
                            _capturedImage = null;
                            return true;
                        }
                        else if (status == EnumStatus.Timeout)
                        {
                            LogRecorder.RecordLog(EnumLogContentType.Info, "CameraControllerWrapper:EnumStatus.Timeout.");
                            _capturedImage = null;
                            return false;
                        }
                    }
                    return true;
                }
                else
                {
                    throw new Exception("camera is not usable,please check it");
                }
            }
            finally
            {
                UnregisterCameraHandler();
            }
        }
        

        /// <summary>
        /// 获取拍摄的图像
        /// </summary>
        /// <param name="imageContent"></param>
        private void ProcessSingleImageAcquired(byte[] imageContent)
        {
            try
            {
                if (imageContent != null)
                {
                    LogRecorder.RecordLog(EnumLogContentType.Info, "ProcessSingleImageAcquired.Start.");
                    if (_cameraManager.CurrentCameraConfig.ImageType == EnumImageData.Grey)
                    {
                        _capturedImage = BitmapFactory.BytesToBitmapFast(imageContent, _cameraManager.CurrentCameraConfig.ImageSizeWidth
                            , _cameraManager.CurrentCameraConfig.ImageSizeHeight, PixelFormat.Format8bppIndexed);
                    }
                    else
                    {
                        _capturedImage = BitmapFactory.BytesToBitmapFast(imageContent, _cameraManager.CurrentCameraConfig.ImageSizeWidth
                            , _cameraManager.CurrentCameraConfig.ImageSizeHeight, PixelFormat.Format24bppRgb);
                    }
                    _capturedImageByte = imageContent;
                    UnregisterCameraHandler();
                    LogRecorder.RecordLog(EnumLogContentType.Info, "ProcessSingleImageAcquired.End.");
                }
                else
                {
                    throw new NotImplementedException("data from camera is null.");
                }
            }
            finally
            {
                LogRecorder.RecordLog(EnumLogContentType.Info, "CameraControllerWrapper:StatusController.Resume.");
                StatusController.Resume();
            }
        }
    }  
}
