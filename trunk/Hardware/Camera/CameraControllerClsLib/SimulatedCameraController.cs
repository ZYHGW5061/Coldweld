using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraControllerClsLib
{
    public class SimulatedCameraController : ICameraController
    {
        public Action<Bitmap> ImageDataAcquiredAction { get; set; }
        public Action<byte[]> SingleImageDataAcquiredAction { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool IsConnect { get { return true; } }

        public void Connect()
        {
        }

        public void DisConnect()
        {
        }

        /// <summary>
        /// 连接状态
        /// </summary>
        /// <returns></returns>
        public bool IsConnected()
        {
            return true;
        }

        /// <summary>
        /// 曝光时间
        /// </summary>
        public float ExposureTime
        {
            get
            {
                return -1;
            }
            set
            {

            }
        }

        /// <summary>
        /// 增益
        /// </summary>
        public float Gain
        {
            get
            {
                return -1;
            }
            set
            {
            }
        }

        /// <summary>
        /// 帧速
        /// </summary>
        public float FPS
        {
            get
            {
                return -1;
            }
            set
            {
            }

        }

        /// <summary>
        /// 采集模式 false 单采 true 连采
        /// </summary>
        public bool GrabMode
        {
            get
            {
                return false;
            }
            set
            {

            }

        }

        /// <summary>
        /// 采集状态 false 停止采集 true 开始采集
        /// </summary>
        public bool Grabbing
        {
            get
            {
                return false;
            }
            set
            {

            }

        }

        public void Set_Acqmode(int acqmode)
        {
            throw new NotImplementedException();
        }

        public void StartGrabbing()
        {
            throw new NotImplementedException();
        }

        public void StopGrabbing()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 连续采集
        /// </summary>
        /// <param name="grab"></param>
        /// <returns></returns>
        public bool ContinuousGetImage(bool grab)
        {
            return true;
        }

        /// <summary>
        /// 单张采集
        /// </summary>
        /// <returns></returns>
        public bool GetImage()
        {
            return true;
        }

        /// <summary>
        /// 单张采集
        /// </summary>
        /// <returns></returns>
        public Task<Bitmap> GetImageAsync()
        {
            return null;
        }
        public Bitmap GetImageAsync2()
        {
            return null;
        }
    }
}
