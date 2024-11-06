using CameraControllerClsLib;
using LightControllerClsLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisionClsLib;

namespace VisionControlAppClsLib
{
    public class VisualModuleClass
    {
        #region Private File

        private static VisualModuleClass instance = null;
        private static readonly object lockObject = new object();
        private VisualModuleClass()
        {
        }

        public static VisualModuleClass Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObject)
                    {
                        if (instance == null)
                        {
                            instance = new VisualModuleClass();
                        }
                    }
                }
                return instance;
            }
        }

        #endregion

        #region Public File

        private float ImageWidth = 0;
        private float ImageHeight = 0;

        private RectangleF ROI = new RectangleF();

        public CameraController SubstrateCameraApp = new CameraController();

        public CameraController WaferCameraApp = new CameraController();

        public CameraController UplookCameraApp = new CameraController();

        public LightController light = LightController.Instance;

        //VisualControlApplications

        public VisualAlgorithms Substratevisual = new VisualAlgorithms();

        public VisualAlgorithms Wafervisual = new VisualAlgorithms();

        public VisualAlgorithms Uplookvisual = new VisualAlgorithms();

        public VisualControlApplications SubstrateVCApp;

        public VisualControlApplications WaferVCApp;

        public VisualControlApplications UplookVCApp;


        #endregion

        #region Public Mothed




        #endregion

    }
}
