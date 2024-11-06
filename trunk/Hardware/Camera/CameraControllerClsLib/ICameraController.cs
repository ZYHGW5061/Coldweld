using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CameraControllerClsLib
{
    public interface ICameraController
    {
        /// <summary>
        /// 采集图片的委托回调
        /// </summary>
        //Action<Bitmap> SingleImageDataAcquiredAction { get; set; }
        Action<byte[]> SingleImageDataAcquiredAction { get; set; }
        Action<Bitmap> ImageDataAcquiredAction { get; set; }

        void Set_Acqmode(int acqmode);
        /// <summary>
        /// 设置相机曝光时间  单位:μs 微秒 
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        //void SetExposureTime(double time);

        /// <summary>
        /// 获取相机曝光时间 单位:μs 微秒  
        /// </summary>
        /// <returns></returns>
        //long GetExposureTime();

        /// <summary>
        /// 设置色彩平衡参数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        //void SetColorBalanceRatio(float redRatio, float greenRatio, float blueRatio);

        /// <summary>
        /// 获取色彩平衡参数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        //void GetColorBalanceRatio(out float redRatio, out float greenRatio, out float blueRatio);

        /// <summary>
        /// 设置Gain
        /// </summary>
        /// <param name="value"></param>
        //void SetGain(int value);

        /// <summary>
        /// 获取Gain
        /// </summary>
        /// <param name="value"></param>
        //int GetGain();
        bool IsConnect { get; }

        bool Grabbing { get; set; }

        bool GrabMode { get; set; }

        float ExposureTime { get; set; }
        float Gain { get; set; }
        float FPS { get; set; }
        /// <summary>
        /// 打开相机
        /// </summary>
        void Connect();

        /// <summary>
        /// 关闭相机
        /// </summary>
        void DisConnect();

        /// <summary>
        /// 连接状态
        /// </summary>
        /// <returns></returns>
        bool IsConnected();

        /// <summary>
        /// 开始采集
        /// </summary>
        void StartGrabbing();

        /// <summary>
        /// 停止采集
        /// </summary>
        void StopGrabbing();

        /// <summary>
        /// 连续采集
        /// </summary>
        /// <param name="grab"></param>
        /// <returns></returns>
        bool ContinuousGetImage(bool grab);

        /// <summary>
        /// 单张采集
        /// </summary>
        /// <returns></returns>
        bool GetImage();

        /// <summary>
        /// 单张采集
        /// </summary>
        /// <returns></returns>
        Task<Bitmap> GetImageAsync();

        Bitmap GetImageAsync2();

        /// <summary>
        /// 保存配置
        /// </summary>
        //void Save();
        /// <summary>
        /// 加载配置
        /// </summary>
        //void Load();
        /// <summary>
        /// 获取行频
        /// </summary>
        //int GetLineRate();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rate"></param>
        //void SetLineRate(int rate);
    }
}
