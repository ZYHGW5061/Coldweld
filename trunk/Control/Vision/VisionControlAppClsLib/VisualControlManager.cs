using CameraControllerClsLib;
using CameraControllerWrapperClsLib;
using ConfigurationClsLib;
using GlobalDataDefineClsLib;
using LightControllerClsLib;
using LightControllerManagerClsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisionClsLib;
using VisionControlAppClsLib;

namespace VisionControlAppClsLib
{
    public class VisualControlManager
    {
        private static readonly object _lockObj = new object();
        private static volatile VisualControlManager _instance = null;
        public static VisualControlManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new VisualControlManager();
                        }
                    }
                }
                return _instance;
            }
        }
        private VisualControlManager()
        {
            AllVisualControl = new Dictionary<EnumCameraType, VisualControlApplications>();
            //Initialize();
        }




        public Dictionary<EnumCameraType, VisualControlApplications> AllVisualControl { get; private set; }
        public EnumCameraType CurrentCameraType { get; set; }
        //public ICameraController CurrentCamera { get; set; }



        /// <summary>
        /// 硬件配置
        /// </summary>
        private HardwareConfiguration _hardwareConfig
        {
            get { return HardwareConfiguration.Instance; }
        }
        private CameraControllerClsLib.CameraManager _cameraManager
        {
            get { return CameraControllerClsLib.CameraManager.Instance; }
        }
        private LightControllerManager _LightControllerManager
        {
            get { return LightControllerManager.Instance; }
        }
        public ICameraController TrackCamera
        {
            get { return _cameraManager.GetCameraByID(EnumCameraType.TrackCamera); }
        }
        public ICameraController WeldCamera
        {
            get { return _cameraManager.GetCameraByID(EnumCameraType.WeldCamera); }
        }

        public ILightSourceController TrackRingLightController
        {
            get
            {
                return _LightControllerManager.GetLightController(EnumLightSourceType.TrackRingField);
            }
        }
        public ILightSourceController WeldRingLightController
        {
            get
            {
                return _LightControllerManager.GetLightController(EnumLightSourceType.WeldRingField);
            }
        }
        
        private CameraConfig _TrackCameraConfig
        {
            get { return CameraManager.Instance.GetCameraConfigByID(EnumCameraType.TrackCamera); }
        }
        private CameraConfig _WeldCameraConfig
        {
            get { return CameraManager.Instance.GetCameraConfigByID(EnumCameraType.WeldCamera); }
        }

        VisualAlgorithms Trackvisual = new VisualAlgorithms();

        VisualAlgorithms Weldvisual = new VisualAlgorithms();



        public bool InitializeVisualControls()
        {
            

            bool S = Trackvisual.Init();
            bool W = Weldvisual.Init();
            VisualControlApplications App;
            var configs = HardwareConfiguration.Instance.CameraConfigList;
            foreach (var item in configs)
            {
                var camera = CameraControllerClsLib.CameraFactory.CreateCamera(item);

               


                var cameraIndexName = (EnumCameraType)Enum.Parse(typeof(EnumCameraType), item.CameraName);
                if (cameraIndexName == EnumCameraType.TrackCamera)
                {
                    App = new VisualControlApplications(TrackCamera, TrackRingLightController, -1, _hardwareConfig.TrackRingLightConfig.ChannelNumber, Trackvisual);
                    App.ImageWidth = _TrackCameraConfig.ImageSizeWidth;
                    App.ImageHeight = _TrackCameraConfig.ImageSizeHeight;
                    //Add camera to camera dic.
                    AllVisualControl.Add(item.CameraType, App);
                    S = true;
                }
                if (cameraIndexName == EnumCameraType.WeldCamera)
                {
                    App = new VisualControlApplications(WeldCamera, WeldRingLightController, -1, _hardwareConfig.WeldRingLightConfig.ChannelNumber, Weldvisual);
                    App.ImageWidth = _WeldCameraConfig.ImageSizeWidth;
                    App.ImageHeight = _WeldCameraConfig.ImageSizeHeight;
                    //Add camera to camera dic.
                    AllVisualControl.Add(item.CameraType, App);
                    W = true;
                }


            }




            return S & W;
        }

        public VisualControlApplications GetCameraByID(EnumCameraType cameraIndex)
        {
            VisualControlApplications ret = null;
            if (AllVisualControl.ContainsKey(cameraIndex))
            {
                ret = AllVisualControl[cameraIndex];
            }
            return ret;
        }



    }
}
