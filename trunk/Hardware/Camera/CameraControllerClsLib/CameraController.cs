using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MvCamCtrl.NET;
using MvCamCtrl.NET.CameraParams;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Net;
using System.Threading;

namespace CameraControllerClsLib
{
    public class CameraController: ICameraController
    {
        #region Camera File

        public string CameraId = String.Empty;
        private bool isconnected = false;
        private readonly object lockObject = new object();
        private float exposuretime = 0;
        private float gain = 0;
        private float fps = 0;
        private bool grabmode = false;//false 单采模式 true 连续采集
        private bool grabbing = false;//false 停止采集 true 正在采集
        private int callbackmode = 0;//false 返回方式采集单张图像 true 回调方式采集单张图像
        /// <summary>
        /// 单张图像回调委托
        /// </summary>
        public Action<Bitmap> ImageDataAcquiredAction { get; set; }//单张图像回调委托

        HIKCameraController Camera = new HIKCameraController();

        private TaskCompletionSource<Bitmap> imageCompletionSource;

        private ManualResetEvent imageReadyEvent = new ManualResetEvent(false);
        private Bitmap acquiredBitmap = null;

        public bool IsConnect
        {
            get { return isconnected; }
        }

        #endregion

        #region Private Method

        public CameraController()
        {
            SubscriptionImage();
        }

        public CameraController(string cameraId)
        {
            SetCameraID(cameraId);
            SubscriptionImage();
        }

        ~CameraController()
        {
            if (Camera.ConnectSte)
            {
                Camera.DisConnect();
            }
        }

        private void SubscriptionImage()
        {
            try
            {
                Camera.SingleImageDataAcquiredAction += ImageCallback;
            }
            catch
            {

            }
        }

        private void ImageCallback(Bitmap bitmap)
        {
            try
            {
                if (GrabMode)
                {
                    if (ImageDataAcquiredAction != null)
                    {
                        ImageDataAcquiredAction(bitmap);
                    }
                }
                else
                {
                    if (callbackmode == 0)
                    {
                        if (ImageDataAcquiredAction != null)
                        {
                            ImageDataAcquiredAction(bitmap);
                        }
                    }
                    else if (callbackmode == 1)
                    {
                        imageCompletionSource.SetResult(bitmap);
                    }
                    else if (callbackmode == 2)
                    {
                        acquiredBitmap = bitmap;
                        imageReadyEvent.Set();
                    }

                }

            }
            catch
            {

            }
        }

        #endregion

        #region Camera Method

        /// <summary>
        /// 查询相机
        /// </summary>
        /// <returns>相机Id/Ip</returns>
        public List<string> ScanConnectDevices()
        {
            try
            {
                List<string> CameraIDs = Camera.CameraID();
                return CameraIDs;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 设置相机ID
        /// </summary>
        /// <param name="CameraId">相机IP或者ID</param>
        /// <returns></returns>
        public bool SetCameraID(string CameraId)
        {
            try
            {
                bool Done = Camera.HIKCameraAcq(CameraId);
                if (Done)
                {
                    this.CameraId = CameraId;
                }
                return Done;
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// 连接相机
        /// </summary>
        /// <returns></returns>
        public bool Connect()
        {
            try
            {
                if (this.CameraId == null || this.CameraId == "")
                {
                    return false;
                }

                int Done = Camera.Connect();
                if (Done == 0)
                {
                    grabbing = false;
                    Thread.Sleep(100);
                    GrabMode = false;
                    Thread.Sleep(100);
                    grabbing = true;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 连接相机
        /// </summary>
        /// <param name="CameraId">相机IP或者ID</param>
        /// <returns></returns>
        public bool Connect(string CameraId)
        {
            try
            {
                if (CameraId == null || CameraId == "")
                {
                    return false;
                }

                bool Done = Camera.HIKCameraAcq(CameraId);
                if (Done)
                {
                    this.CameraId = CameraId;
                }
                int Done1 = Camera.Connect();

                isconnected = IsConnected();

                if (Done1 == 0)
                {

                    grabbing = false;
                    Thread.Sleep(100);
                    GrabMode = false;
                    Thread.Sleep(100);
                    //grabbing = true;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 断开相机
        /// </summary>
        /// <returns></returns>
        public bool DisConnect()
        {
            try
            {
                Camera.DisConnect();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 连接状态
        /// </summary>
        /// <returns></returns>
        public bool IsConnected()
        {
            lock (lockObject)
            {
                try
                {
                    isconnected = Camera.ConnectSte;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    isconnected = false;
                }
                return isconnected;
            }
        }

        /// <summary>
        /// 曝光时间
        /// </summary>
        public float ExposureTime
        {
            get
            {
                try
                {
                    if (IsConnected())
                    {
                        exposuretime = Camera.GetExposureTime();
                        return exposuretime;
                    }
                    else
                    {
                        return -1;
                    }
                }
                catch
                {
                    return -1;
                }
            }
            set
            {
                exposuretime = value;
                try
                {
                    if (IsConnected())
                    {
                        Camera.SetExposureTime(exposuretime);
                    }
                }
                catch
                {

                }
            }
        }

        /// <summary>
        /// 增益
        /// </summary>
        public float Gain
        {
            get
            {
                try
                {
                    if (IsConnected())
                    {
                        gain = Camera.GetGain();
                        return gain;
                    }
                    else
                    {
                        return -1;
                    }
                }
                catch
                {
                    return -1;
                }
            }
            set
            {
                gain = value;
                try
                {
                    if (IsConnected())
                    {
                        Camera.SetGain(gain);
                    }
                }
                catch
                {

                }
            }
        }

        /// <summary>
        /// 帧速
        /// </summary>
        public float FPS
        {
            get
            {
                try
                {
                    if (IsConnected())
                    {
                        fps = Camera.GetFPS();
                        return fps;
                    }
                    else
                    {
                        return -1;
                    }
                }
                catch
                {
                    return -1;
                }
            }
            set
            {
                fps = value;
                try
                {
                    if (IsConnected())
                    {
                        Camera.SetFPS(fps);
                    }
                }
                catch
                {

                }
            }
        }

        /// <summary>
        /// 采集模式 false 单采 true 连采
        /// </summary>
        public bool GrabMode
        {
            get
            {
                try
                {
                    if (IsConnected())
                    {
                        int Done = Camera.bnTriggerMode;
                        if (Done == 0)
                        {
                            grabmode = true;
                        }
                        else if (Done == 1)
                        {
                            grabmode = false;
                        }
                        return grabmode;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                grabmode = value;
                try
                {
                    if (IsConnected())
                    {
                        if (grabmode)
                        {
                            Camera.Set_Acqmode(0);
                        }
                        else
                        {
                            Camera.Set_Acqmode(1);
                        }
                    }
                }
                catch
                {

                }
            }

        }

        /// <summary>
        /// 采集状态 false 停止采集 true 开始采集
        /// </summary>
        public bool Grabbing
        {
            get
            {
                try
                {
                    if (IsConnected())
                    {
                        grabbing = Camera.m_bGrabbing;
                        return grabbing;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                grabbing = value;
                try
                {
                    if (IsConnected())
                    {
                        if (grabbing)
                        {
                            Camera.StartGrabbing();
                        }
                        else
                        {
                            Camera.StopGrabbing();
                        }
                    }
                }
                catch
                {

                }
            }

        }

        public Action<byte[]> SingleImageDataAcquiredAction { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        //public bool IsConnect => throw new NotImplementedException();

        /// <summary>
        /// 连续采集模式获取图像
        /// </summary>
        /// <param name="grab">true 开启连采 false 关闭连采</param>
        /// <returns></returns>
        public bool ContinuousGetImage(bool grab)
        {
            try
            {
                if (IsConnected())
                {

                    if (grabmode == false)
                    {
                        if (grabbing)
                        {
                            Grabbing = false;
                        }

                        Thread.Sleep(100);

                        GrabMode = true;

                        Thread.Sleep(100);
                    }

                    if (grab)
                    {
                        if (grabbing == false)
                        {
                            Grabbing = true;
                        }
                    }
                    else
                    {
                        if (grabbing)
                        {
                            Grabbing = false;
                        }
                    }

                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 单采模式获取图像触发
        /// </summary>
        /// <returns></returns>
        public bool GetImage()
        {
            try
            {
                if (IsConnected())
                {
                    if (grabmode)
                    {
                        if (grabbing)
                        {
                            Grabbing = false;
                        }

                        Thread.Sleep(100);

                        GrabMode = false;

                        Thread.Sleep(100);

                        if (grabbing == false)
                        {
                            Grabbing = true;
                        }
                    }
                    if (grabbing == false)
                    {
                        Grabbing = true;
                    }

                    callbackmode = 0;

                    Camera.GetImage();

                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 单采模式获取图像
        /// </summary>
        /// <returns></returns>
        public async Task<Bitmap> GetImageAsync()
        {
            Bitmap bitmap = null;
            try
            {
                if (IsConnected())
                {
                    if (grabmode)
                    {
                        if (grabbing)
                        {
                            Grabbing = false;
                        }

                        Thread.Sleep(100);

                        GrabMode = false;

                        Thread.Sleep(100);

                        if (grabbing == false)
                        {
                            Grabbing = true;
                        }
                    }
                    if (grabbing == false)
                    {
                        Grabbing = true;
                    }

                    callbackmode = 1;

                    imageCompletionSource = new TaskCompletionSource<Bitmap>();

                    Camera.GetImage();

                    bitmap = await imageCompletionSource.Task;
                }

                return bitmap;
            }
            catch
            {
                return bitmap;
            }
        }

        /// <summary>
        /// 单采模式获取图像
        /// </summary>
        /// <returns></returns>
        public Bitmap GetImageAsync2()
        {
            acquiredBitmap = null;
            imageReadyEvent.Reset();
            try
            {
                if (IsConnected())
                {
                    if (grabmode)
                    {
                        if (grabbing)
                        {
                            Grabbing = false;
                        }

                        Thread.Sleep(100);

                        GrabMode = false;

                        Thread.Sleep(100);

                        if (grabbing == false)
                        {
                            Grabbing = true;
                        }
                    }
                    if (grabbing == false)
                    {
                        Grabbing = true;
                    }

                    callbackmode = 2;

                    Camera.GetImage();

                    imageReadyEvent.WaitOne(10000);
                }

                return acquiredBitmap;
            }
            catch
            {
                return acquiredBitmap;
            }
        }

        public void Set_Acqmode(int acqmode)
        {
            throw new NotImplementedException();
        }

        void ICameraController.Connect()
        {
            Connect();
            //throw new NotImplementedException();
        }

        void ICameraController.DisConnect()
        {
            DisConnect();
            //throw new NotImplementedException();
        }

        public void StartGrabbing()
        {
            throw new NotImplementedException();
        }

        public void StopGrabbing()
        {
            throw new NotImplementedException();
        }


        #endregion

    }


    public class HIKCameraController
    {
        //private static readonly HIKCameraController instance = new HIKCameraController();
        //private HIKCameraController()
        //{
        //}
        //public static HIKCameraController Instance
        //{
        //    get { return instance; }
        //}

        public HIKCameraController()
        {

        }

        ~HIKCameraController()
        {
            if (ConnectSte == true)
            {
                DisConnect();
            }
        }

        #region File

        public bool ConnectSte = false;


        private CCamera m_MyCamera = new CCamera();
        List<CCameraInfo> m_ltDeviceList = new List<CCameraInfo>();
        List<string> Deviceliststr = new List<string>();
        int Deviceitem = -1;
        public int bnTriggerMode = 0;//采集模式（0连续，1单张）
        public bool m_bGrabbing = false;//开始采集的状态
        private static Object BufForDriverLock = new Object();// ch:用于从驱动获取图像的缓存 | en:Buffer for getting image from driver
        Bitmap m_pcBitmap = null;// ch:图像数据 | en:Bitmap
        PixelFormat m_enBitmapPixelFormat = PixelFormat.DontCare;

        //public Action<byte[]> SingleImageDataAcquiredAction { get; set; }
        public Action<Bitmap> SingleImageDataAcquiredAction { get; set; }

        private cbOutputExdelegate ImageCallback;

        #endregion

        public void ImageCallbackFunc(IntPtr pData, ref MV_FRAME_OUT_INFO_EX pFrameInfo, IntPtr pUser)
        {
            //Console.WriteLine("Get one frame: Width[" + Convert.ToString(pFrameInfo.nWidth) + "] , Height[" + Convert.ToString(pFrameInfo.nHeight)
            //                    + "] , FrameNum[" + Convert.ToString(pFrameInfo.nFrameNum) + "]");

            try
            {
                CPixelConvertParam pcConvertParam = new CPixelConvertParam();

                pcConvertParam.InImage.ImageAddr = pData;
                pcConvertParam.InImage.FrameLen = pFrameInfo.nFrameLen;
                pcConvertParam.InImage.Width = pFrameInfo.nWidth;
                pcConvertParam.InImage.Height = pFrameInfo.nHeight;
                pcConvertParam.InImage.PixelType = pFrameInfo.enPixelType;

                if (PixelFormat.Format8bppIndexed == m_pcBitmap.PixelFormat)
                {
                    pcConvertParam.OutImage.PixelType = MvGvspPixelType.PixelType_Gvsp_Mono8;
                    m_MyCamera.ConvertPixelType(ref pcConvertParam);
                }
                else
                {
                    pcConvertParam.OutImage.PixelType = MvGvspPixelType.PixelType_Gvsp_BGR8_Packed;
                    m_MyCamera.ConvertPixelType(ref pcConvertParam);
                }



                Bitmap dst = new Bitmap(pcConvertParam.InImage.Width, pcConvertParam.InImage.Height, m_pcBitmap.PixelFormat);

                // ch:保存Bitmap数据 | en:Save Bitmap Data
                BitmapData m_pcBitmapData = dst.LockBits(new Rectangle(0, 0, pcConvertParam.InImage.Width, pcConvertParam.InImage.Height), ImageLockMode.ReadWrite, dst.PixelFormat);

                Marshal.Copy(pcConvertParam.OutImage.ImageData, 0, m_pcBitmapData.Scan0, (Int32)pcConvertParam.OutImage.ImageData.Length);
                if (PixelFormat.Format8bppIndexed == m_enBitmapPixelFormat)
                {
                    ColorPalette palette = dst.Palette;
                    for (int i = 0; i < palette.Entries.Length; i++)
                    {
                        palette.Entries[i] = Color.FromArgb(i, i, i);
                    }
                    dst.Palette = palette;
                }
                dst.UnlockBits(m_pcBitmapData);

                Bitmap clonedBitmap = (Bitmap)dst.Clone();
                if (SingleImageDataAcquiredAction != null)
                {
                    SingleImageDataAcquiredAction(clonedBitmap);
                }
            }
            catch (Exception ex)
            {

            }



            //m_MyCamera.FreeImageBuffer(ref pcFrameInfo);


            //return m_pcBitmap;
        }

        /// <summary>
        /// 在构造函数中实例化设备列表对象
        /// </summary>
        public bool HIKCameraAcq(string cameraId)
        {
            try
            {
                return DeviceListAcq(cameraId);
            }
            catch
            {
                return false;
            }

            //Control.CheckForIllegalCrossThreadCalls = false;
        }

        /// <summary>
        /// ch:显示错误信息 | en:Show error message
        /// </summary>
        /// <param name="csMessage"></param>
        /// <param name="nErrorNum"></param>
        private void ShowErrorMsg(string csMessage, int nErrorNum)
        {
            try
            {
                string errorMsg;
                if (nErrorNum == 0)
                {
                    errorMsg = csMessage;
                }
                else
                {
                    errorMsg = csMessage + ": Error =" + String.Format("{0:X}", nErrorNum);
                }

                switch (nErrorNum)
                {
                    case CErrorDefine.MV_E_HANDLE: errorMsg += " Error or invalid handle "; break;
                    case CErrorDefine.MV_E_SUPPORT: errorMsg += " Not supported function "; break;
                    case CErrorDefine.MV_E_BUFOVER: errorMsg += " Cache is full "; break;
                    case CErrorDefine.MV_E_CALLORDER: errorMsg += " Function calling order error "; break;
                    case CErrorDefine.MV_E_PARAMETER: errorMsg += " Incorrect parameter "; break;
                    case CErrorDefine.MV_E_RESOURCE: errorMsg += " Applying resource failed "; break;
                    case CErrorDefine.MV_E_NODATA: errorMsg += " No data "; break;
                    case CErrorDefine.MV_E_PRECONDITION: errorMsg += " Precondition error, or running environment changed "; break;
                    case CErrorDefine.MV_E_VERSION: errorMsg += " Version mismatches "; break;
                    case CErrorDefine.MV_E_NOENOUGH_BUF: errorMsg += " Insufficient memory "; break;
                    case CErrorDefine.MV_E_UNKNOW: errorMsg += " Unknown error "; break;
                    case CErrorDefine.MV_E_GC_GENERIC: errorMsg += " General error "; break;
                    case CErrorDefine.MV_E_GC_ACCESS: errorMsg += " Node accessing condition error "; break;
                    case CErrorDefine.MV_E_ACCESS_DENIED: errorMsg += " No permission "; break;
                    case CErrorDefine.MV_E_BUSY: errorMsg += " Device is busy, or network disconnected "; break;
                    case CErrorDefine.MV_E_NETER: errorMsg += " Network error "; break;
                }

                //MessageBox.Show(errorMsg, "PROMPT");
            }
            catch
            { }

        }

        public static string Int2IP(UInt32 ipCode)
        {
            try
            {
                byte a = (byte)((ipCode & 0xFF000000) >> 0x18);
                byte b = (byte)((ipCode & 0x00FF0000) >> 0x10);
                byte c = (byte)((ipCode & 0x0000FF00) >> 0x8);
                byte d = (byte)(ipCode & 0x000000FF);
                string ipStr = String.Format("{0}.{1}.{2}.{3}", a, b, c, d);
                return ipStr;
            }
            catch
            { return null; }

        }

        /// <summary>
        /// 查询相机列表
        /// </summary>
        /// <returns>m_ltDeviceList.Count</returns>
        private List<string> DeviceListAcq()
        {
            List<string> CameraID = new List<string>();
            try
            {
                // ch:创建设备列表 | en:Create Device List
                System.GC.Collect();
                m_ltDeviceList.Clear();
                int nRet = CSystem.EnumDevices(CSystem.MV_GIGE_DEVICE | CSystem.MV_USB_DEVICE, ref m_ltDeviceList);
                if (0 != nRet)
                {
                    ShowErrorMsg("Enumerate devices fail!", 0);
                    Deviceliststr = CameraID;
                    return Deviceliststr;
                }



                //Deviceliststr = new string[m_ltDeviceList.Count];
                // ch:在窗体列表中显示设备名 | en:Display device name in the form list
                for (int i = 0; i < m_ltDeviceList.Count; i++)
                {
                    Deviceliststr[i] = System.String.Empty;
                    if (m_ltDeviceList[i].nTLayerType == CSystem.MV_GIGE_DEVICE)
                    {
                        CGigECameraInfo gigeInfo = (CGigECameraInfo)m_ltDeviceList[i];

                        if (Convert.ToString(gigeInfo.nCurrentIp) != "")
                        {
                            CameraID.Add(gigeInfo.UserDefinedName);
                            //if (Deviceliststr[i].Length > 0)
                            //    Deviceliststr[i] = Deviceliststr[i].Remove(0, Deviceliststr[i].Length);
                            //Deviceliststr[i] = Deviceliststr[i] + gigeInfo.UserDefinedName;
                            ////cbDeviceList.Items.Add("GEV: " + gigeInfo.UserDefinedName + " (" + gigeInfo.chSerialNumber + ")");
                        }
                        else
                        {
                            CameraID.Add(Int2IP(gigeInfo.nCurrentIp));
                            //Deviceliststr[i] = Deviceliststr[i] + "IP: " + Int2IP(gigeInfo.nCurrentIp) + " " + gigeInfo.chModelName + " (" + gigeInfo.chSerialNumber + ")";
                            ////cbDeviceList.Items.Add("GEV: " + gigeInfo.chManufacturerName + " " + gigeInfo.chModelName + " (" + gigeInfo.chSerialNumber + ")");
                        }
                    }
                    else if (m_ltDeviceList[i].nTLayerType == CSystem.MV_USB_DEVICE)
                    {
                        CUSBCameraInfo usbInfo = (CUSBCameraInfo)m_ltDeviceList[i];
                        if (usbInfo.UserDefinedName != "")
                        {
                            CameraID.Add(usbInfo.UserDefinedName);
                            //Deviceliststr[i] = Deviceliststr[i] + "U3V: " + usbInfo.UserDefinedName + " (" + usbInfo.chSerialNumber + ")";
                            ////cbDeviceList.Items.Add("U3V: " + usbInfo.UserDefinedName + " (" + usbInfo.chSerialNumber + ")");
                        }
                        else
                        {
                            CameraID.Add(usbInfo.chManufacturerName);
                            //Deviceliststr[i] = Deviceliststr[i] + "U3V: " + usbInfo.chManufacturerName + " " + usbInfo.chModelName + " (" + usbInfo.chSerialNumber + ")";
                            ////cbDeviceList.Items.Add("U3V: " + usbInfo.chManufacturerName + " " + usbInfo.chModelName + " (" + usbInfo.chSerialNumber + ")");
                        }
                    }
                }

                // ch:选择第一项 | en:Select the first item
                if (m_ltDeviceList.Count != 0)
                {
                    Deviceitem = 0;
                }
                Deviceliststr = CameraID;
                return Deviceliststr;
            }
            catch
            { return null; }

        }
        /// <summary>
        /// 设置相机ID
        /// </summary>
        /// <returns></returns>
        private bool DeviceListAcq(string cameraId)
        {
            //List<string> CameraID = new List<string>();
            try
            {
                // ch:创建设备列表 | en:Create Device List
                System.GC.Collect();
                m_ltDeviceList.Clear();
                int nRet = CSystem.EnumDevices(CSystem.MV_GIGE_DEVICE | CSystem.MV_USB_DEVICE, ref m_ltDeviceList);

                if (0 != nRet)
                {
                    ShowErrorMsg("Enumerate devices fail!", 0);
                    //Deviceliststr = CameraID;
                    return false;
                }

                Deviceliststr = new List<string>();
                //Deviceliststr = new string[m_ltDeviceList.Count];
                // ch:在窗体列表中显示设备名 | en:Display device name in the form list
                for (int i = 0; i < m_ltDeviceList.Count; i++)
                {
                    //Deviceliststr[i] = System.String.Empty;
                    if (m_ltDeviceList[i].nTLayerType == CSystem.MV_GIGE_DEVICE)
                    {
                        CGigECameraInfo gigeInfo = (CGigECameraInfo)m_ltDeviceList[i];


                        string ipAddress0 = Encoding.Default.GetString(gigeInfo.chUserDefinedName).TrimEnd('\0');

                        IPAddress ipAddress1 = IPAddress.Parse(gigeInfo.nCurrentIp.ToString());
                        //byte[] ipBytes = ipAddress1.GetAddressBytes();
                        //Array.Reverse(ipBytes);
                        //IPAddress correctedIpAddress = new IPAddress(ipBytes);
                        string ipAddress = ipAddress1.ToString();

                        if (gigeInfo.UserDefinedName == cameraId)
                        {
                            Deviceitem = i;
                            return true;
                        }
                        if (ipAddress == cameraId)
                        {
                            Deviceitem = i;
                            return true;
                        }

                    }
                    else if (m_ltDeviceList[i].nTLayerType == CSystem.MV_USB_DEVICE)
                    {
                    }
                }
                //Deviceliststr = CameraID;
                return false;
            }
            catch(Exception ex)
            { return false; }

        }
        /// <summary>
        /// ch:像素类型是否为Mono格式 | en:If Pixel Type is Mono 
        /// </summary>
        /// <param name="enPixelType"></param>
        /// <returns>true/false</returns>
        private Boolean IsMono(MvGvspPixelType enPixelType)
        {
            try
            {
                switch (enPixelType)
                {
                    case MvGvspPixelType.PixelType_Gvsp_Mono1p:
                    case MvGvspPixelType.PixelType_Gvsp_Mono2p:
                    case MvGvspPixelType.PixelType_Gvsp_Mono4p:
                    case MvGvspPixelType.PixelType_Gvsp_Mono8:
                    case MvGvspPixelType.PixelType_Gvsp_Mono8_Signed:
                    case MvGvspPixelType.PixelType_Gvsp_Mono10:
                    case MvGvspPixelType.PixelType_Gvsp_Mono10_Packed:
                    case MvGvspPixelType.PixelType_Gvsp_Mono12:
                    case MvGvspPixelType.PixelType_Gvsp_Mono12_Packed:
                    case MvGvspPixelType.PixelType_Gvsp_Mono14:
                    case MvGvspPixelType.PixelType_Gvsp_Mono16:
                        return true;
                    default:
                        return false;
                }
            }
            catch
            { return false; }

        }

        /// <summary>
        ///  ch:取图前的必要操作步骤 | en:Necessary operation before grab
        /// </summary>
        /// <returns>CErrorDefine.MV_OK</returns>
        private Int32 NecessaryOperBeforeGrab()
        {
            try
            {
                // ch:取图像宽 | en:Get Iamge Width
                CIntValue pcWidth = new CIntValue();
                int nRet = m_MyCamera.GetIntValue("Width", ref pcWidth);
                if (CErrorDefine.MV_OK != nRet)
                {
                    ShowErrorMsg("Get Width Info Fail!", nRet);
                    return nRet;
                }
                // ch:取图像高 | en:Get Iamge Height
                CIntValue pcHeight = new CIntValue();
                nRet = m_MyCamera.GetIntValue("Height", ref pcHeight);
                if (CErrorDefine.MV_OK != nRet)
                {
                    ShowErrorMsg("Get Height Info Fail!", nRet);
                    return nRet;
                }
                // ch:取像素格式 | en:Get Pixel Format
                CEnumValue pcPixelFormat = new CEnumValue();
                nRet = m_MyCamera.GetEnumValue("PixelFormat", ref pcPixelFormat);
                if (CErrorDefine.MV_OK != nRet)
                {
                    ShowErrorMsg("Get Pixel Format Fail!", nRet);
                    return nRet;
                }

                // ch:设置bitmap像素格式
                if ((Int32)MvGvspPixelType.PixelType_Gvsp_Undefined == (Int32)pcPixelFormat.CurValue)
                {
                    ShowErrorMsg("Unknown Pixel Format!", CErrorDefine.MV_E_UNKNOW);
                    return CErrorDefine.MV_E_UNKNOW;
                }
                else if (IsMono((MvGvspPixelType)pcPixelFormat.CurValue))
                {
                    m_enBitmapPixelFormat = PixelFormat.Format8bppIndexed;
                }
                else
                {
                    m_enBitmapPixelFormat = PixelFormat.Format24bppRgb;
                }

                if (null != m_pcBitmap)
                {
                    m_pcBitmap.Dispose();
                    m_pcBitmap = null;
                }
                m_pcBitmap = new Bitmap((Int32)pcWidth.CurValue, (Int32)pcHeight.CurValue, m_enBitmapPixelFormat);

                // ch:Mono8格式，设置为标准调色板 | en:Set Standard Palette in Mono8 Format
                if (PixelFormat.Format8bppIndexed == m_enBitmapPixelFormat)
                {
                    ColorPalette palette = m_pcBitmap.Palette;
                    for (int i = 0; i < palette.Entries.Length; i++)
                    {
                        palette.Entries[i] = Color.FromArgb(i, i, i);
                    }
                    m_pcBitmap.Palette = palette;
                }

                return CErrorDefine.MV_OK;
            }
            catch
            { return CErrorDefine.MV_ALG_ERR; }

        }

        ////相机功能函数////

        /// <summary>
        /// 查找相机
        /// </summary>
        /// <returns>Camera_Num相机个数</returns>
        public List<string> CameraID()
        {
            List<string> Camera_ID = DeviceListAcq();
            return Camera_ID;
        }


        /// <summary>
        /// 打开相机
        /// </summary>
        /// <param name="Camera_Num_Select"></param>
        public int Connect(int Camera_Num_Select)
        {
            Deviceitem = Camera_Num_Select;
            if (m_ltDeviceList.Count == 0 || Deviceitem == -1)
            {
                ShowErrorMsg("No device, please select", 0);
                return -1;
            }

            // ch:获取选择的设备信息 | en:Get selected device information
            CCameraInfo device = m_ltDeviceList[Deviceitem];

            // ch:打开设备 | en:Open device
            if (null == m_MyCamera)
            {
                m_MyCamera = new CCamera();
                if (null == m_MyCamera)
                {
                    return -1;
                }
            }

            int nRet = m_MyCamera.CreateHandle(ref device);
            if (CErrorDefine.MV_OK != nRet)
            {
                return -1;
            }

            nRet = m_MyCamera.OpenDevice();
            if (CErrorDefine.MV_OK != nRet)
            {
                m_MyCamera.DestroyHandle();
                ShowErrorMsg("Device open fail!", nRet);
                return -1;
            }

            // ch:探测网络最佳包大小(只对GigE相机有效) | en:Detection network optimal package size(It only works for the GigE camera)
            if (device.nTLayerType == CSystem.MV_GIGE_DEVICE)
            {
                int nPacketSize = m_MyCamera.GIGE_GetOptimalPacketSize();
                if (nPacketSize > 0)
                {
                    nRet = m_MyCamera.SetIntValue("GevSCPSPacketSize", (uint)nPacketSize);
                    if (nRet != CErrorDefine.MV_OK)
                    {
                        ShowErrorMsg("Set Packet Size failed!", nRet);
                    }
                }
                else
                {
                    ShowErrorMsg("Get Packet Size failed!", nPacketSize);
                }
            }

            // ch:设置采集连续模式 | en:Set Continues Aquisition Mode
            //m_MyCamera.SetEnumValue("AcquisitionMode", (uint)MV_CAM_ACQUISITION_MODE.MV_ACQ_MODE_CONTINUOUS);
            //m_MyCamera.SetEnumValue("TriggerMode", (uint)MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_OFF);

            ConnectSte = true;
            return 0;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int Connect()
        {
            try
            {
                if (m_ltDeviceList.Count == 0 || Deviceitem == -1)
                {
                    ShowErrorMsg("No device, please select", 0);
                    return -1;
                }

                // ch:获取选择的设备信息 | en:Get selected device information
                CCameraInfo device = m_ltDeviceList[Deviceitem];

                // ch:打开设备 | en:Open device
                if (null == m_MyCamera)
                {
                    m_MyCamera = new CCamera();
                    if (null == m_MyCamera)
                    {
                        return -1;
                    }
                }

                int nRet = m_MyCamera.CreateHandle(ref device);
                if (CErrorDefine.MV_OK != nRet)
                {
                    return -1;
                }

                nRet = m_MyCamera.OpenDevice();
                if (CErrorDefine.MV_OK != nRet)
                {
                    m_MyCamera.DestroyHandle();
                    ShowErrorMsg("Device open fail!", nRet);
                    return -1;
                }

                // ch:探测网络最佳包大小(只对GigE相机有效) | en:Detection network optimal package size(It only works for the GigE camera)
                if (device.nTLayerType == CSystem.MV_GIGE_DEVICE)
                {
                    int nPacketSize = m_MyCamera.GIGE_GetOptimalPacketSize();
                    if (nPacketSize > 0)
                    {
                        nRet = m_MyCamera.SetIntValue("GevSCPSPacketSize", (uint)nPacketSize);
                        if (nRet != CErrorDefine.MV_OK)
                        {
                            ShowErrorMsg("Set Packet Size failed!", nRet);
                        }
                    }
                    else
                    {
                        ShowErrorMsg("Get Packet Size failed!", nPacketSize);
                    }
                }

                // ch:设置采集连续模式 | en:Set Continues Aquisition Mode
                //m_MyCamera.SetEnumValue("AcquisitionMode", (uint)MV_CAM_ACQUISITION_MODE.MV_ACQ_MODE_CONTINUOUS);
                //m_MyCamera.SetEnumValue("TriggerMode", (uint)MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_OFF);
                Set_Acqmode(1);
                // ch:前置配置 | en:pre-operation
                nRet = NecessaryOperBeforeGrab();
                if (CErrorDefine.MV_OK != nRet)
                {
                    return -1;
                }

                // ch:注册回调函数 | en:Register image callback
                ImageCallback = new cbOutputExdelegate(ImageCallbackFunc);
                nRet = m_MyCamera.RegisterImageCallBackEx(ImageCallback, IntPtr.Zero);
                if (CErrorDefine.MV_OK != nRet)
                {
                    Console.WriteLine("Register image callback failed!");
                    return -1;
                }

                // ch:标志位置位true | en:Set position bit true
                m_bGrabbing = true;

                // ch:开始采集 | en:Start Grabbing
                nRet = m_MyCamera.StartGrabbing();
                if (CErrorDefine.MV_OK != nRet)
                {
                    m_bGrabbing = false;
                    ShowErrorMsg("Start Grabbing Fail!", nRet);
                    return -1;
                }
                ConnectSte = true;
                return 0;
            }
            catch
            { return -1; }

        }
        /// <summary>
        /// 关闭相机
        /// </summary>
        public void DisConnect()
        {
            try
            {
                StopGrabbing();
                // ch:取流标志位清零 | en:Reset flow flag bit
                if (m_bGrabbing == true)
                {
                    m_bGrabbing = false;
                }

                // ch:关闭设备 | en:Close Device
                m_MyCamera.CloseDevice();
                m_MyCamera.DestroyHandle();
                ConnectSte = false;
            }
            catch
            { }


        }

        /// <summary>
        /// 设置采集模式
        /// </summary>
        /// <param name="acqmode"></param>
        public void Set_Acqmode(int acqmode)
        {
            try
            {
                if (acqmode == 0)
                {
                    bnTriggerMode = 0;
                    //设置连续采集
                    //m_MyCamera.SetEnumValue("AcquisitionMode", (uint)MV_CAM_ACQUISITION_MODE.MV_ACQ_MODE_CONTINUOUS);
                    m_MyCamera.SetEnumValue("TriggerMode", (uint)MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_OFF);
                }
                else
                {
                    bnTriggerMode = 1;
                    // ch:打开触发模式 | en:Open Trigger Mode
                    if (bnTriggerMode == 1)
                    {
                        //m_MyCamera.SetEnumValue("AcquisitionMode", (uint)MV_CAM_ACQUISITION_MODE.MV_ACQ_MODE_SINGLE);
                        m_MyCamera.SetEnumValue("TriggerMode", (uint)MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_ON);

                        // ch:触发源选择:0 - Line0; | en:Trigger source select:0 - Line0;
                        //           1 - Line1;
                        //           2 - Line2;
                        //           3 - Line3;
                        //           4 - Counter;
                        //           7 - Software;
                        if (bnTriggerMode == 1)
                        {
                            m_MyCamera.SetEnumValue("TriggerSource", (uint)MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_SOFTWARE);
                            if (m_bGrabbing)
                            {
                                //bnTriggerExec.Enabled = true;
                            }
                        }
                        else
                        {
                            m_MyCamera.SetEnumValue("TriggerSource", (uint)MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_LINE0);
                        }
                        //cbSoftTrigger.Enabled = true;
                    }
                }
            }
            catch
            { }

        }

        /// <summary>
        /// 开始采集
        /// </summary>
        public void StartGrabbing()
        {
            try
            {
                //int nRetGet = CErrorDefine.MV_OK;
                //if (bnTriggerMode == 1)
                //{
                //    // ch:触发命令 | en:Trigger command
                //    nRetGet = m_MyCamera.SetCommandValue("TriggerSoftware");
                //    if (CErrorDefine.MV_OK != nRetGet)
                //    {
                //        ShowErrorMsg("Trigger Software Fail!", nRetGet);
                //    }
                //}
                int nRet = 0;
                // ch:标志位置位true | en:Set position bit true
                m_bGrabbing = true;

                // ch:开始采集 | en:Start Grabbing
                nRet = m_MyCamera.StartGrabbing();
                if (CErrorDefine.MV_OK != nRet)
                {
                    //m_bGrabbing = false;
                    ShowErrorMsg("Start Grabbing Fail!", nRet);
                    return;
                }
            }
            catch
            { }

        }

        /// <summary>
        /// 停止采集
        /// </summary>
        public void StopGrabbing()
        {
            try
            {
                // ch:标志位设为false | en:Set flag bit false
                m_bGrabbing = false;

                // ch:停止采集 | en:Stop Grabbing
                int nRet = m_MyCamera.StopGrabbing();

                if (nRet != CErrorDefine.MV_OK)
                {
                    ShowErrorMsg("Stop Grabbing Fail!", nRet);
                }
            }
            catch
            { }

        }

        /// <summary>
        /// 获取图像
        /// </summary>
        public void GetImage()
        {
            try
            {
                int nRet = CErrorDefine.MV_OK;
                if (m_bGrabbing == true)
                {
                    if (bnTriggerMode == 1)
                    {
                        // ch:触发命令 | en:Trigger command
                        nRet = m_MyCamera.SetCommandValue("TriggerSoftware");
                        if (CErrorDefine.MV_OK != nRet)
                        {
                            ShowErrorMsg("Trigger Software Fail!", nRet);
                        }
                    }
                }
                //else
                //{
                //    StartGrabbing();
                //    //m_bGrabbing = true;
                //    if (bnTriggerMode == 1)
                //    {
                //        // ch:触发命令 | en:Trigger command
                //        nRet = m_MyCamera.SetCommandValue("TriggerSoftware");
                //        if (CErrorDefine.MV_OK != nRet)
                //        {
                //            ShowErrorMsg("Trigger Software Fail!", nRet);
                //        }
                //    }
                //    //Thread.Sleep(100);
                //    //StopGrabbing();
                //    //m_bGrabbing = false;
                //}
            }
            catch
            { }



        }

        /// <summary>
        /// 设置曝光时间
        /// </summary>
        /// <param name="tbExposure"></param>
        public void SetExposureTime(float tbExposure)
        {
            try
            {
                m_MyCamera.SetEnumValue("ExposureAuto", 0);
                int nRet = m_MyCamera.SetFloatValue("ExposureTime", tbExposure);
                if (nRet != CErrorDefine.MV_OK)
                {
                    ShowErrorMsg("Set Exposure Time Fail!", nRet);
                }
            }
            catch
            { }

        }

        /// <summary>
        /// 获取曝光时间
        /// </summary>
        /// <returns></returns>
        public float GetExposureTime()
        {
            try
            {
                CFloatValue pcFloatValue = new CFloatValue();
                int nRet = m_MyCamera.GetFloatValue("ExposureTime", ref pcFloatValue);
                if (CErrorDefine.MV_OK != nRet)
                {
                    return -1;
                }
                return pcFloatValue.CurValue;
            }
            catch
            { return -1; }

        }

        /// <summary>
        /// 设置Gain
        /// </summary>
        /// <param name="Gain"></param>
        public void SetGain(float Gain)
        {
            m_MyCamera.SetEnumValue("GainAuto", 0);
            int nRet = m_MyCamera.SetFloatValue("Gain", Gain);
            if (nRet != CErrorDefine.MV_OK)
            {
                ShowErrorMsg("Set Gain Fail!", nRet);
            }
        }

        /// <summary>
        /// 获取Gain
        /// </summary>
        /// <param name="Gain"></param>
        /// <returns></returns>
        public float GetGain()
        {
            CFloatValue pcFloatValue = new CFloatValue();
            int nRet = m_MyCamera.GetFloatValue("Gain", ref pcFloatValue);
            if (CErrorDefine.MV_OK == nRet)
            {
                return -1;
            }
            return pcFloatValue.CurValue;
        }

        /// <summary>
        /// 设置帧频
        /// </summary>
        /// <param name="tbFrameRate"></param>
        public void SetFPS(float tbFrameRate)
        {
            int nRet = m_MyCamera.SetFloatValue("AcquisitionFrameRate", tbFrameRate);
            if (nRet != CErrorDefine.MV_OK)
            {
                ShowErrorMsg("Set Frame Rate Fail!", nRet);
            }
        }

        /// <summary>
        /// 获取帧频
        /// </summary>
        /// <returns></returns>
        public float GetFPS()
        {
            CFloatValue pcFloatValue = new CFloatValue();
            int nRet = m_MyCamera.GetFloatValue("ResultingFrameRate", ref pcFloatValue);
            if (CErrorDefine.MV_OK == nRet)
            {
                return -1;
            }
            return pcFloatValue.CurValue;
        }

        //public static Bitmap ConvertToBitmap(byte[] imageData, int imageWidth, int imageHeight, int imageChannels)
        //{
        //    // 转换为灰度图像
        //    Mat yuvImage = new Mat(imageHeight, imageWidth, MatType.CV_8UC3, imageData);
        //    Mat grayImage = new Mat();
        //    Cv2.CvtColor(yuvImage, grayImage, ColorConversionCodes.YUV2GRAY_I420);

        //    // 设置为Mono8格式和标准调色板
        //    Mat monoImage = new Mat();
        //    grayImage.ConvertTo(monoImage, MatType.CV_8UC1);
        //    Cv2.ApplyColorMap(monoImage, monoImage, ColormapTypes.Jet);

        //    // 转换为Bitmap
        //    Bitmap bitmap = new Bitmap(imageWidth, imageHeight, PixelFormat.Format24bppRgb);
        //    BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, imageWidth, imageHeight), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

        //    try
        //    {
        //        unsafe
        //        {
        //            byte* dstPtr = (byte*)bitmapData.Scan0;
        //            int srcStride = monoImage.Step();

        //            for (int y = 0; y < imageHeight; y++)
        //            {
        //                byte* srcPtr = monoImage.Ptr(y);
        //                byte* dstRowPtr = dstPtr + y * bitmapData.Stride;

        //                for (int x = 0; x < imageWidth; x++)
        //                {
        //                    dstRowPtr[x * 3] = srcPtr[x];
        //                    dstRowPtr[x * 3 + 1] = srcPtr[x];
        //                    dstRowPtr[x * 3 + 2] = srcPtr[x];
        //                }
        //            }
        //        }
        //    }
        //    finally
        //    {
        //        bitmap.UnlockBits(bitmapData);
        //    }

        //    return bitmap;
        //}

        //public unsafe Bitmap BytesToBitmap(byte[] buffer)
        //{
        //    switch (m_pcBitmap.PixelFormat)
        //    {
        //        case PixelFormat.Format24bppRgb:
        //            {
        //                Bitmap newBitmap = new Bitmap(m_pcBitmap.Width, m_pcBitmap.Height, PixelFormat.Format24bppRgb);
        //                //将Bitmap锁定到系统内存中  
        //                //rect是指源图像中需要锁定那一块矩形区域进行处理  
        //                //ImageLockMode.ReadWrite是指对图像出操作的权限，枚举有只读，只写，用户输入缓冲区，还是读写  
        //                BitmapData srcBmpData = newBitmap.LockBits(new Rectangle(0, 0, m_pcBitmap.Width, m_pcBitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
        //                //位图中第一个像素数据的地址。它也可以看成是位图中的第一个扫描行  
        //                IntPtr srcPtr = srcBmpData.Scan0;
        //                byte* ptr = (byte*)srcPtr;
        //                for (int i = 0; i < m_pcBitmap.Height; i++)
        //                {
        //                    for (int j = 0; j < m_pcBitmap.Width; j++)
        //                    {
        //                        int num1 = (i * m_pcBitmap.Width + j) * 3;
        //                        ptr[0] = buffer[num1];
        //                        ptr[1] = buffer[num1 + 1];
        //                        ptr[2] = buffer[num1 + 2];
        //                        ptr += 3;
        //                    }
        //                }
        //                newBitmap.UnlockBits(srcBmpData);
        //                return newBitmap;
        //            }
        //        case PixelFormat.Format8bppIndexed:
        //            {
        //                Bitmap img = new Bitmap(m_pcBitmap.Width, m_pcBitmap.Height, PixelFormat.Format8bppIndexed);
        //                BitmapData data = img.LockBits(new Rectangle(0, 0, m_pcBitmap.Width, m_pcBitmap.Height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);
        //                System.Runtime.InteropServices.Marshal.Copy(buffer, 0, data.Scan0, buffer.Length);
        //                if (img.PixelFormat == PixelFormat.Format8bppIndexed)
        //                {
        //                    ColorPalette colorPalette = img.Palette;
        //                    for (int i = 0; i < 256; i++)
        //                    {
        //                        colorPalette.Entries[i] = Color.FromArgb(i, i, i);
        //                    }
        //                    img.Palette = colorPalette;
        //                }
        //                img.UnlockBits(data);
        //                return img;
        //            }
        //        default:
        //            return null;
        //    }

        //}
    }

}
