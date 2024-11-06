using CameraControllerClsLib;
using ConfigurationClsLib;
using GlobalDataDefineClsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraControllerWrapperClsLib
{
    public class CameraManager
    {
        private static readonly object _lockObj = new object();
        private static volatile CameraManager _instance = null;
        public static CameraManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new CameraManager();
                        }
                    }
                }
                return _instance;
            }
        }
        private CameraManager()
        {
            AllCamera = new Dictionary<EnumCameraType, ICameraController>();
            //Initialize();
        }
        public Dictionary<EnumCameraType, ICameraController> AllCamera { get; private set; }
        public EnumCameraType CurrentCameraType { get; set; }
        //public ICameraController CurrentCamera { get; set; }
        public bool InitializeCameras()
        {
            var configs = HardwareConfiguration.Instance.CameraConfigList;
            foreach (var item in configs)
            {
                var camera = CameraFactory.CreateCamera(item);
                //var cameraIndexName = (EnumCameraType)Enum.Parse(typeof(EnumCameraType), item.CameraName);
                //Initialize camera
                camera.Connect();
                //Add camera to camera dic.
                AllCamera.Add(item.CameraType, camera);
            }
            return true;
        }
        public bool Shutdown()
        {
            foreach (var cam in AllCamera)
            {
                cam.Value.DisConnect();
            }
            return true;
        }
        public ICameraController GetCameraByID(EnumCameraType cameraIndex)
        {
            ICameraController ret = null;
            if (AllCamera.ContainsKey(cameraIndex))
            {
                ret = AllCamera[cameraIndex];
            }
            return ret;
        }
        public ICameraController CurrentCamera
        {
            get
            {
                ICameraController ret = null;
                if (AllCamera.ContainsKey(CurrentCameraType))
                {
                    ret = AllCamera[CurrentCameraType];
                }
                return ret;
            }
        }
        public CameraConfig CurrentCameraConfig
        {
            get
            {
                CameraConfig ret = null;
                if (HardwareConfiguration.Instance.CameraConfigList.Any(i => i.CameraType == CurrentCameraType))
                {
                    ret = HardwareConfiguration.Instance.CameraConfigList.FirstOrDefault(i => i.CameraType == CurrentCameraType);
                }
                return ret;
            }
        }
    }
}
