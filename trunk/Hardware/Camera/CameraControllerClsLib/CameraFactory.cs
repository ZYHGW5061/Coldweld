using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConfigurationClsLib;
using GlobalDataDefineClsLib;

namespace CameraControllerClsLib
{
    public class CameraFactory
    {
        public static ICameraController CreateCamera(CameraConfig cameraConfig)
        {
            //return new BaslerCamera(camerainfo);
            if (cameraConfig.RunningType == EnumRunningType.Actual)
            {
                var productor = cameraConfig.CameraProducer;
                switch (productor)
                {
                    case EnumCameraProducer.HIK:
                        return new CameraController(cameraConfig.CameraName);
                    //case EnumCameraProductor.ViewWorksLine:
                    //    return new ViewWorksLineCamera(camerainfo);
                    //case EnumCameraProductor.JAI5100C:
                    //    return new JAI5100CAreaCamera();
                    default:
                        return new CameraController(cameraConfig.CameraName);
                }
            }
            else
            {
                return new SimulatedCameraController();
            }
        }
    }
}
