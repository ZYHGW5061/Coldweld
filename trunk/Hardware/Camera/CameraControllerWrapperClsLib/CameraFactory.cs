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
    public class CameraFactory
    {
        public static ICameraController CreateCamera(CameraConfig cameraConfig)
        {
            if (cameraConfig.RunningType == EnumRunningType.Actual)
            {
                var productor = cameraConfig.CameraProducer;
                switch (productor)
                {
                    case EnumCameraProducer.HIK:
                        return new SimulatedCameraController();
                    default:
                        return new SimulatedCameraController();
                }
            }
            else
            {
                return new SimulatedCameraController();
            }
        }
    }
}
