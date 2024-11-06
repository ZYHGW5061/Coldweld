using GlobalDataDefineClsLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisionControlAppClsLib;

namespace VisionGUI
{
    public class VisualControlGuiManger
    {
        private static readonly object _lockObj = new object();
        private static volatile VisualControlGuiManger _instance = null;
        public static VisualControlGuiManger Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new VisualControlGuiManger();
                        }
                    }
                }
                return _instance;
            }
        }
        private VisualControlGuiManger()
        {
            Initialize();
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

        VisualMatchControlGUI BondMatchGUI = new VisualMatchControlGUI();
        VisualLineFindControlGUI BondLineFindGUI = new VisualLineFindControlGUI();
        VisualCircleFindControlGUI BondCircleFindGUI = new VisualCircleFindControlGUI();


        public void Initialize()
        {
            BondMatchGUI.InitVisualControl(CameraWindowGUI.Instance, TrackCameraVisual);
            BondLineFindGUI.InitVisualControl(CameraWindowGUI.Instance, TrackCameraVisual);
            BondCircleFindGUI.InitVisualControl(CameraWindowGUI.Instance, TrackCameraVisual);


            CameraWindowForm.Instance.InitializeWindow(CameraWindowGUI.Instance);

            CameraWindowForm.Instance.Size = new System.Drawing.Size(950, 800);
        }


    }
}
