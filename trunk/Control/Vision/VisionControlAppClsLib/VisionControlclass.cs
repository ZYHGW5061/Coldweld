using CameraControllerClsLib;
using CommonPanelClsLib;
using ConfigurationClsLib;
using GlobalDataDefineClsLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisionClsLib;
using VisionGUI;

namespace VisionControlAppClsLib
{
    public class VisionControlclass
    {
        private static readonly object _lockObj = new object();
        private static volatile VisionControlclass _instance = null;
        public static VisionControlclass Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new VisionControlclass();
                        }
                    }
                }
                return _instance;
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

        public static (double, double) ImageToXY(PointF point, PointF center, float pixelsizex, float pixelsizey)
        {
            double X = (point.X - center.X) * pixelsizex;

            double Y = (point.Y - center.Y) * pixelsizey;

            return (X, Y);

        }


        /// <summary>
        /// 阻塞消息弹窗
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public int ShowMessage(string title, string content, string type)
        {
            if (WarningBox.FormShow(title, content, type) == 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        private int ShowMessage2(string title, string content, string type)
        {
            int result = -1;
            var formReadyEvent = new ManualResetEvent(false);
            //WarningBox1.FormShow(title, content, type);
            MessageBox1 myMessageBox1 = new MessageBox1();
            myMessageBox1.OnButtonClicked += (buttonResult) =>
            {
                result = buttonResult == "confirm" ? 1 : 0;
                formReadyEvent.Set();
            };
            myMessageBox1.showMessage(title, content, type);

            while (!formReadyEvent.WaitOne(100))
            {
                Application.DoEvents();
            }

            return result;
        }

        /// <summary>
        /// 不阻塞消息弹窗
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public int ShowMessageAsync(string title, string content, string type)
        {


            return ShowMessage2(title, content, type);




        }



        /// <summary>
        /// 识别中心
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public MatchResult IdentificationAsync(EnumCameraType camera, MatchIdentificationParam param)
        {
            //Task.Factory.StartNew(new Action(() =>
            //{
            if (camera == EnumCameraType.TrackCamera)
            {
                TrackCameraVisual.SetDirectLightintensity(param.DirectLightintensity);
                TrackCameraVisual.SetRingLightintensity(param.RingLightintensity);

                bool Done = TrackCameraVisual.LoadMatchTrainXml(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, param.Templatexml));

                Done = TrackCameraVisual.LoadMatchRunXml(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, param.Runxml));

                //var result =  BondCameraVisual.MatchFindAsync(param.Score, param.MinAngle, param.MaxAngle, param.SearchRoi);

                Bitmap bitmap = TrackCameraVisual.GetBitmap();
                Bitmap Showbitmap = new Bitmap(bitmap);

                bool En1 = TrackCameraVisual.MatchSetRunPara<float>(MatchParas.MinScore, 0.2f);
                bool En2 = TrackCameraVisual.MatchSetRunPara<int>(MatchParas.AngleStart, param.MinAngle);
                bool En3 = TrackCameraVisual.MatchSetRunPara<int>(MatchParas.AngleEnd, param.MaxAngle);

                if (!(En1 && En2 && En3))
                {
                    return null;
                }

                List<MatchResult> results = new List<MatchResult>();

                Done = TrackCameraVisual.MatchRun(bitmap, param.Score, ref results, param.SearchRoi);

                if (results != null && results.Count > 0)
                {
                    Task.Factory.StartNew(new Action(() =>
                    {
                        CameraWindowGUI.Instance.ShowImage(Showbitmap, _TrackCameraConfig.CameraWindowImageScale);
                        CameraWindowGUI.Instance.ClearGraphicDraw();
                        CameraWindowGUI.Instance.GraphicDrawInit(results);
                        CameraWindowGUI.Instance.GraphicDraw(Graphic.match, true);
                    }));
                    if (results[0].IsOk)
                    {

                        return results[0];
                    }



                }
            }

            if (camera == EnumCameraType.WeldCamera)
            {
                WeldCameraVisual.SetDirectLightintensity(param.DirectLightintensity);
                WeldCameraVisual.SetRingLightintensity(param.RingLightintensity);

                bool Done = WeldCameraVisual.LoadMatchTrainXml(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, param.Templatexml));

                Done = WeldCameraVisual.LoadMatchRunXml(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, param.Runxml));

                //var result =  WaferCameraVisual.MatchFindAsync(param.Score, param.MinAngle, param.MaxAngle, param.SearchRoi);

                Bitmap bitmap = WeldCameraVisual.GetBitmap();
                Bitmap Showbitmap = new Bitmap(bitmap);

                bool En1 = WeldCameraVisual.MatchSetRunPara<float>(MatchParas.MinScore, 0.2f);
                bool En2 = WeldCameraVisual.MatchSetRunPara<int>(MatchParas.AngleStart, param.MinAngle);
                bool En3 = WeldCameraVisual.MatchSetRunPara<int>(MatchParas.AngleEnd, param.MaxAngle);

                if (!(En1 && En2 && En3))
                {
                    return null;
                }

                List<MatchResult> results = new List<MatchResult>();

                Done = WeldCameraVisual.MatchRun(bitmap, param.Score, ref results, param.SearchRoi);

                if (results != null && results.Count > 0)
                {
                    Task.Factory.StartNew(new Action(() =>
                    {
                        CameraWindowGUI.Instance.ShowImage(Showbitmap, _WeldCameraConfig.CameraWindowImageScale);
                        CameraWindowGUI.Instance.ClearGraphicDraw();
                        CameraWindowGUI.Instance.GraphicDrawInit(results);
                        CameraWindowGUI.Instance.GraphicDraw(Graphic.match, true);
                    }));
                    if (results[0].IsOk)
                    {

                        return results[0];
                    }



                }
            }

            //}));
            return null;
        }

        /// <summary>
        /// 识别并相对于相机中心的偏移坐标和角度
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public XYZTCoordinateConfig IdentificationAsync2(EnumCameraType camera, MatchIdentificationParam param)
        {
            XYZTCoordinateConfig offset = new XYZTCoordinateConfig();

            if (camera == EnumCameraType.TrackCamera)
            {
                TrackCameraVisual.SetDirectLightintensity(param.DirectLightintensity);
                TrackCameraVisual.SetRingLightintensity(param.RingLightintensity);

                bool Done = TrackCameraVisual.LoadMatchTrainXml(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, param.Templatexml));

                Done = TrackCameraVisual.LoadMatchRunXml(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, param.Runxml));

                //var result =  BondCameraVisual.MatchFindAsync(param.Score, param.MinAngle, param.MaxAngle, param.SearchRoi);

                Bitmap bitmap = TrackCameraVisual.GetBitmap();
                Bitmap Showbitmap = new Bitmap(bitmap);

                bool En1 = TrackCameraVisual.MatchSetRunPara<float>(MatchParas.MinScore, 0.2f);
                bool En2 = TrackCameraVisual.MatchSetRunPara<int>(MatchParas.AngleStart, param.MinAngle);
                bool En3 = TrackCameraVisual.MatchSetRunPara<int>(MatchParas.AngleEnd, param.MaxAngle);

                if (!(En1 && En2 && En3))
                {
                    return null;
                }

                List<MatchResult> results = new List<MatchResult>();

                Done = TrackCameraVisual.MatchRun(bitmap, param.Score, ref results, param.SearchRoi);


                double MaterialboxX = 0;
                double MaterialboxY = 0;
                double MaterialboxZ = 0;

                if (results != null && results.Count > 0)
                {
                    if (results[0].IsOk)
                    {
                        Task.Factory.StartNew(new Action(() =>
                        {
                            CameraWindowGUI.Instance.ShowImage(Showbitmap, _TrackCameraConfig.CameraWindowImageScale);
                            CameraWindowGUI.Instance.ClearGraphicDraw();
                            CameraWindowGUI.Instance.GraphicDrawInit(results);
                            CameraWindowGUI.Instance.GraphicDraw(Graphic.match, true);
                        }));
                        {
                            (MaterialboxX, MaterialboxY) = ImageToXY(results[0].MatchBox.Benchmark, new PointF(bitmap.Width / 2, bitmap.Height / 2), _TrackCameraConfig.WidthPixelSize, _TrackCameraConfig.HeightPixelSize);

                            offset = new XYZTCoordinateConfig();
                            offset.X = -MaterialboxX;
                            offset.Y = -MaterialboxY;
                            offset.Z = 0;
                            offset.Theta = results[0].MatchBox.Angle;

                            return offset;
                        }
                    }
                    else
                    {
                        return null;
                    }



                }
                else
                {
                    return null;
                }
            }

            return null;
        }

        public void ShowArray(List<PointF> array, PointF center)
        {
            List<ScorePoint> array0 = new List<ScorePoint>();

            PointF centerpixel = new PointF()
            {
                X = _TrackCameraConfig.ImageSizeWidth / 2,
                Y = _TrackCameraConfig.ImageSizeHeight / 2,

            };

            PointF pixelSize = new PointF()
            {
                X = _TrackCameraConfig.WidthPixelSize,
                Y = _TrackCameraConfig.HeightPixelSize,
            };


            foreach (var point in array)
            {
                // 计算像素坐标  
                float pixelX = (point.X - center.X) / pixelSize.X + centerpixel.X;
                float pixelY = (point.Y - center.Y) / pixelSize.Y + centerpixel.Y;

                array0.Add(new ScorePoint(new PointF(pixelX, pixelY), 0));
            }

            Task.Factory.StartNew(new Action(() =>
            {
                CameraWindowGUI.Instance.ClearGraphicDraw();
                CameraWindowGUI.Instance.GraphicDrawInit(array0);
                CameraWindowGUI.Instance.GraphicDraw(Graphic.array, true);
            }));

        }


    }
}
