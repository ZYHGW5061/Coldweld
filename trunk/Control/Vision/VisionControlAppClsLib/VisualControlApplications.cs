using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using LightControllerClsLib;
using CameraControllerClsLib;
using VisionClsLib;
using GlobalDataDefineClsLib;
using System.Threading;
using System.IO;

namespace VisionControlAppClsLib
{
    /// <summary>
    /// 视觉控制
    /// </summary>
    public class VisualControlApplications
    {

        #region Private file

        ICameraController Camera;
        ILightSourceController LightCon;
        //LightControl DirectLight;
        int ring;
        int direct;

        public VisualAlgorithms Algorithm;

        public bool ImageShow = false;


        public VisualControlApplications(ICameraController Camera, ILightSourceController LightCon, int direct, int ring, VisualAlgorithms Algorithm)
        {
            Init(Camera, LightCon, direct, ring, Algorithm);

            //if (Camera != null && CameraWindow != null)
            //    Camera.ImageDataAcquiredAction += Imagecallback;

        }

        #endregion

        #region public file

        public int ImageWidth { get; set; } = 0;
        public int ImageHeight { get; set; } = 0;


        #endregion



        #region Method

        public bool Init(ICameraController Camera, ILightSourceController LightCon, int direct, int ring, VisualAlgorithms Algorithm)
        {
            try
            {
                this.Camera = Camera;
                this.LightCon = LightCon;
                this.ring = ring;
                this.direct = direct;
                this.Algorithm = Algorithm;

                return true;
            }
            catch
            {
                return false;
            }
        }

        #region 相机窗口


        public bool GetOneImage()
        {
            return Camera.GetImage();
        }

        public bool ContinuousGetImage(bool Show)
        {
            return Camera.ContinuousGetImage(Show);
        }

        public Bitmap GetBitmap()
        {
            Bitmap Image = null;
            Task task = Task.Factory.StartNew(new Action(() =>
            {
                Image = Camera.GetImageAsync2();
            }));

            task.Wait();
            
            if(Image != null)
            {
                ImageWidth = Image.Width;
                ImageHeight = Image.Height;
            }
            
            return Image;
        }
    
        
    

        #endregion

        #region 算法

        private bool MatchFindInited = false;
        private bool LineFindInited = false;
        private bool CircleFindInited = false;


        #region 高精度模板匹配

        /// <summary>
        /// 模板训练
        /// </summary>
        /// <param name="TrainImage">训练图像</param>
        /// <param name="Result">训练结果 点图</param>
        /// <param name="Benchmark">基准点</param>
        /// <param name="ROI">ROI区域</param>
        /// <param name="Sign">true 训练区域 false 掩膜</param>
        /// <returns></returns>
        public bool MatchTrain(Bitmap TrainImage, ref MatchTemplateResult Result, PointF Benchmark = new PointF(), RectangleF ROI = new RectangleF(), bool Sign = true)
        {
            ImageWidth = TrainImage.Width;
            ImageHeight = TrainImage.Height;
            return Algorithm.MatchTrain(TrainImage, ref Result, Benchmark, ROI, Sign);
        }

        /// <summary>
        /// 模板训练
        /// </summary>
        /// <param name="TrainImage">训练图像</param>
        /// <param name="Result">训练结果 点图</param>
        /// <param name="Benchmark">基准点</param>
        /// <param name="ROI">ROI区域</param>
        /// <param name="Sign">true 训练区域 false 掩膜</param>
        /// <returns></returns>
        public bool MatchTrain(Bitmap TrainImage, ref MatchTemplateResult Result, RectangleF ROI = new RectangleF(), bool Sign = true)
        {
            ImageWidth = TrainImage.Width;
            ImageHeight = TrainImage.Height;
            return Algorithm.MatchTrain(TrainImage, ref Result, ROI, Sign);
        }

        /// <summary>
        /// 保存模板
        /// </summary>
        /// <param name="TrainXmlFilepath">模板文件完整路径 后缀.contourmxml</param>
        /// <returns></returns>
        public bool MatchSaveTrain(string TrainXmlFilepath)
        {
            return Algorithm.MatchSaveTrain(TrainXmlFilepath);
        }

        /// <summary>
        /// 读取模板
        /// </summary>
        /// <param name="TrainXmlFilepath">模板文件完整路径 后缀.contourmxml</param>
        /// <returns></returns>
        public bool MatchLoadTrain(string TrainXmlFilepath)
        {
            return Algorithm.MatchLoadTrain(TrainXmlFilepath);
        }

        /// <summary>
        /// 设置训练参数
        /// </summary>
        /// <param name="ScaleMode"></param>
        /// <param name="ScaleLevel"></param>
        /// <param name="ScaleRLevel"></param>
        /// <param name="ThresMode"></param>
        /// <param name="ThresValue"></param>
        /// <param name="WeightFlag"></param>
        /// <param name="ChainFlag"></param>
        /// <param name="MinChain"></param>
        /// <returns></returns>
        public bool MatchSetTrainPara(ContourPatCreateScaleMode ScaleMode, float ScaleLevel, uint ScaleRLevel,
            ContourPatCreateThresholdMode ThresMode, uint ThresValue, ContourPatCreateWeightFlag WeightFlag,
            ContourPatCreateChainFlag ChainFlag, int MinChain)
        {
            return Algorithm.MatchSetTrainPara(ScaleMode, ScaleLevel, ScaleRLevel, ThresMode, ThresValue, WeightFlag, ChainFlag, MinChain);

        }

        /// <summary>
        /// 获取训练参数
        /// </summary>
        /// <param name="ScaleMode"></param>
        /// <param name="ScaleLevel"></param>
        /// <param name="ScaleRLevel"></param>
        /// <param name="ThresMode"></param>
        /// <param name="ThresValue"></param>
        /// <param name="WeightFlag"></param>
        /// <param name="ChainFlag"></param>
        /// <param name="MinChain"></param>
        /// <returns></returns>
        public bool MatchGetTrainPara(ref ContourPatCreateScaleMode ScaleMode, ref float ScaleLevel, ref uint ScaleRLevel,
            ref ContourPatCreateThresholdMode ThresMode, ref uint ThresValue, ref ContourPatCreateWeightFlag WeightFlag,
            ref ContourPatCreateChainFlag ChainFlag, ref int MinChain)
        {
            return Algorithm.MatchGetTrainPara(ref ScaleMode, ref ScaleLevel, ref ScaleRLevel, ref ThresMode, ref ThresValue, ref WeightFlag, ref ChainFlag, ref MinChain);
        }

        /// <summary>
        /// 设置识别参数
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="para">参数名</param>
        /// <param name="Val">值</param>
        /// <returns></returns>
        public bool MatchSetRunPara<T>(MatchParas para, T Val)
        {
            return Algorithm.MatchSetRunPara<T>(para, Val);
        }

        /// <summary>
        /// 获取识别参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="para"></param>
        /// <param name="Val"></param>
        /// <returns></returns>
        public bool MatchGetRunPara<T>(MatchParas para, ref T Val)
        {
            return Algorithm.MatchGetRunPara<T>(para, ref Val);
        }

        /// <summary>
        /// 加载识别参数
        /// </summary>
        /// <param name="fileDlg">识别参数.xml路径</param>
        /// <returns></returns>
        public bool MatchLoadRunPara(string fileDlg)
        {
            return Algorithm.MatchLoadRunPara(fileDlg);
        }

        /// <summary>
        /// 保存识别参数
        /// </summary>
        /// <param name="fileDlg">识别参数.xml路径</param>
        /// <returns></returns>
        public bool MatchSaveRunPara(string fileDlg)
        {
            return Algorithm.MatchSaveRunPara(fileDlg);
        }

        /// <summary>
        /// 模板识别
        /// </summary>
        /// <param name="RunImage">识别图像</param>
        /// <param name="Result">识别结果</param>
        /// <param name="ROI">识别区域</param>
        /// <returns></returns>
        public bool MatchRun(Bitmap RunImage, float Score, ref List<MatchResult> Result, RectangleFV ROI)
        {
            ImageWidth = RunImage.Width;
            ImageHeight = RunImage.Height;
            RectangleF rectangle = new RectangleF(ROI.X, ROI.Y, ROI.Width, ROI.Height);
            return Algorithm.MatchRun(RunImage, Score, ref Result, rectangle);
        }

        #endregion

        #region 边缘查找

        /// <summary>
        /// 设置边缘查找参数
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="para">参数名</param>
        /// <param name="Val">值</param>
        /// <returns></returns>
        public bool LineFindSetRunPara<T>(LineFindParas para, T Val)
        {
            return Algorithm.LineFindSetRunPara<T>(para, Val);
        }

        /// <summary>
        /// 获取边缘查找参数
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="para">参数名</param>
        /// <param name="Val">返回值</param>
        /// <returns></returns>
        public bool LineFindGetRunPara<T>(LineFindParas para, ref T Val)
        {
            return Algorithm.LineFindGetRunPara<T>(para, ref Val);
        }

        /// <summary>
        /// 加载边缘查找参数
        /// </summary>
        /// <param name="fileDlg">边缘查找参数.xml路径</param>
        /// <returns></returns>
        public bool LineFindLoadRunPara(string fileDlg)
        {
            return Algorithm.LineFindLoadRunPara(fileDlg);
        }

        /// <summary>
        /// 保存边缘查找参数
        /// </summary>
        /// <param name="fileDlg">边缘查找参数.xml路径</param>
        /// <returns></returns>
        public bool LineFindSaveRunPara(string fileDlg)
        {
            return Algorithm.LineFindSaveRunPara(fileDlg);
        }

        /// <summary>
        /// 边缘查找识别
        /// </summary>
        /// <param name="RunImage">识别图像</param>
        /// <param name="Result">识别结果</param>
        /// <param name="ROI">识别区域</param>
        /// <param name="ScanDirection">搜索方向 false 从左到右 true 从上到下</param>
        /// <returns></returns>
        public bool LineFindRun(Bitmap RunImage, int Score, ref LineResult Result, RectangleF ROI = new RectangleF(), bool ScanDirection = false)
        {
            ImageWidth = RunImage.Width;
            ImageHeight = RunImage.Height;
            return Algorithm.LineFindRun(RunImage, Score, ref Result, ROI, ScanDirection);
        }


        #endregion

        #region 圆查找


        /// <summary>
        /// 设置圆查找参数
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="para">参数名</param>
        /// <param name="Val">值</param>
        /// <returns></returns>
        public bool CircleFindSetRunPara<T>(CircleFindParas para, T Val)
        {
            return Algorithm.CircleFindSetRunPara<T>(para, Val);
        }

        /// <summary>
        /// 获取圆查找参数
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="para">参数名</param>
        /// <param name="Val">返回值</param>
        /// <returns></returns>
        public bool CircleFindGetRunPara<T>(CircleFindParas para, ref T Val)
        {
            return Algorithm.CircleFindGetRunPara<T>(para, ref Val);
        }

        /// <summary>
        /// 加载圆查找参数
        /// </summary>
        /// <param name="fileDlg">圆查找参数.xml路径</param>
        /// <returns></returns>
        public bool CircleFindLoadRunPara(string fileDlg)
        {
            return Algorithm.CircleFindLoadRunPara(fileDlg);
        }

        /// <summary>
        /// 保存圆查找参数
        /// </summary>
        /// <param name="fileDlg">圆查找参数.xml路径</param>
        /// <returns></returns>
        public bool CircleFindSaveRunPara(string fileDlg)
        {
            return Algorithm.CircleFindSaveRunPara(fileDlg);
        }

        /// <summary>
        /// 圆查找识别
        /// </summary>
        /// <param name="RunImage">识别图像</param>
        /// <param name="Result">识别结果</param>
        /// <param name="ROI">识别区域</param>
        /// <param name="MinRadius">最小半径</param>
        /// <param name="MaxRadius">最大半径</param>
        /// <returns></returns>
        public bool CircleFindRun(Bitmap RunImage, int Score, ref CircleResult Result, RectangleF ROI = new RectangleF(), int MinRadius = -1, int MaxRadius = -1)
        {
            ImageWidth = RunImage.Width;
            ImageHeight = RunImage.Height;
            return Algorithm.CircleFindRun(RunImage, Score, ref Result, ROI, MinRadius, MaxRadius);
        }

        #endregion

        


        /// <summary>
        /// 加载轮廓训练xml 模板
        /// </summary>
        /// <param name="Templatexml"></param>
        /// <returns></returns>
        public bool LoadMatchTrainXml(string Templatexml)
        {
            if (Templatexml != null)
            {
                return Algorithm.MatchLoadTrain(Templatexml);
            }
            return false;
        }
        /// <summary>
        /// 保存轮廓训练xml 模板
        /// </summary>
        /// <param name="Templatexml"></param>
        /// <returns></returns>
        public bool SaveMatchTrainXml(string Templatexml)
        {
            if (Templatexml != null)
            {
                return Algorithm.MatchSaveTrain(Templatexml);
            }
            return false;
        }

        /// <summary>
        /// 加载识别参数
        /// </summary>
        /// <param name="Templatexml"></param>
        /// <returns></returns>
        public bool LoadMatchRunXml(string Runxml)
        {
            if (Runxml != null)
            {
                return Algorithm.MatchLoadRunPara(Runxml);
            }
            return false;
        }
        /// <summary>
        /// 保存识别参数
        /// </summary>
        /// <param name="Templatexml"></param>
        /// <returns></returns>
        public bool SaveMatchRunXml(string Runxml)
        {
            if (Runxml != null)
            {
                return Algorithm.MatchSaveRunPara(Runxml);
            }
            return false;
        }

        public bool MatchFindInit(string Templatexml)
        {
            if (Templatexml != null)
            {
                MatchFindInited = Algorithm.MatchLoadTrain(Templatexml);
            }
            return MatchFindInited;
        }

        public List<MatchResult> MatchFindAsync(float Score, int Minangle, int Maxangle, RectangleF ROI = new RectangleF())
        {

            List<MatchResult> results = new List<MatchResult>();

            if (MatchFindInited)
            {
                Bitmap SeachImage = Camera.GetImageAsync2();

                if(SeachImage == null)
                {
                    return null;
                }

                ImageWidth = SeachImage.Width;
                ImageHeight = SeachImage.Height;

                bool En1 = Algorithm.MatchSetRunPara<float>(MatchParas.MinScore, 0.2f);
                bool En2 = Algorithm.MatchSetRunPara<int>(MatchParas.AngleStart, Minangle);
                bool En3 = Algorithm.MatchSetRunPara<int>(MatchParas.AngleEnd, Maxangle);

                if(!(En1&& En2&& En3))
                {
                    return null;
                }

                bool Done = Algorithm.MatchRun(SeachImage, Score, ref results, ROI);


            }

            return results;
        }


        public MatchTemplateResult MatchTemplateAsync(string Templatexml, PointF Benchmark = new PointF(), RectangleF ROI = new RectangleF())
        {
            bool Done=false;

            MatchTemplateResult Result = new MatchTemplateResult();

            Bitmap TemplateImage = Camera.GetImageAsync2();

            if(TemplateImage == null)
            {
                return null;
            }
            ImageWidth = TemplateImage.Width;
            ImageHeight = TemplateImage.Height;

            List<ScorePoint> points = new List<ScorePoint>();

            if(Benchmark == PointF.Empty)
            {
                if(ROI != RectangleF.Empty)
                {
                    Benchmark = new PointF(ROI.X + ROI.Width / 2, ROI.Y + ROI.Height / 2);
                }
                
            }

            Done = Algorithm.MatchTrain(TemplateImage, ref Result, Benchmark,ROI);

            if(Templatexml != null)
            {
                Algorithm.MatchSaveTrain(Templatexml);

                if (TemplateImage != null)
                {
                    string directoryPath = Path.GetDirectoryName(Templatexml);
                    string imagePath = Path.Combine(directoryPath, "TemplateImage.png");
                    TemplateImage.Save(imagePath, System.Drawing.Imaging.ImageFormat.Png);
                }
            }

            



            return Result;
        }

        public List<MatchResult> MatchFindAsync(string Templatexml, RectangleF ROI = new RectangleF())
        {
            List<MatchResult> results = new List<MatchResult>();

            if(Templatexml!=null)
            {
                MatchFindInited = Algorithm.MatchLoadTrain(Templatexml);

                if(MatchFindInited)
                {
                    Bitmap SeachImage = Camera.GetImageAsync2();

                    ImageWidth = SeachImage.Width;
                    ImageHeight = SeachImage.Height;

                    //bool En1 = Algorithm.MatchSetRunPara<float>(MatchParas.MinScore, Score);
                    //bool En2 = Algorithm.MatchSetRunPara<int>(MatchParas.AngleStart, Minangle);
                    //bool En3 = Algorithm.MatchSetRunPara<int>(MatchParas.AngleEnd, Maxangle);

                    //if (!(En1 && En2 && En3))
                    //{
                    //    return null;
                    //}

                    bool Done = Algorithm.MatchRun(SeachImage, 0.2f, ref results, ROI);

                }
                
            }

            return results;
        }

        public bool LineFindInit(string LineFindxml)
        {
            if (LineFindxml != null)
            {
                LineFindInited = Algorithm.LineFindLoadRunPara(LineFindxml);

            }
            return LineFindInited;
        }

        public bool LineFindSave(string LineFindxml, int Score, bool ScanDirection)
        {
            if (LineFindxml != null)
            {
                //bool En1 = Algorithm.LineFindSetRunPara<int>(LineFindParas.LineRate, Score);



                if (ScanDirection == false)
                {
                    Algorithm.LineFindSetRunPara<string>(LineFindParas.FindOrient, "LeftToRight");
                }
                else
                {
                    Algorithm.LineFindSetRunPara<string>(LineFindParas.FindOrient, "UpToDown");
                }

            }
            return Algorithm.LineFindSaveRunPara(LineFindxml);
        }

        public List<LineResult> LineFindAsync(List<string> LineFindxmls,List<int> Scores, List<RectangleF> ROIs, List<bool> Scans)
        {
            List<LineResult> results = new List<LineResult>();

            Bitmap SeachImage = Camera.GetImageAsync2();

            ImageWidth = SeachImage.Width;
            ImageHeight = SeachImage.Height;

            if (LineFindxmls.Count>0)
            {
                int i = 0;
                foreach(string LineFindxml in LineFindxmls)
                {
                    if (LineFindxml != null)
                    {
                        LineFindInited = Algorithm.LineFindLoadRunPara(LineFindxml);

                        if (LineFindInited)
                        {
                            

                            

                            LineResult result = new LineResult();
                            bool Done = Algorithm.LineFindRun(SeachImage, Scores[i], ref result, ROIs[i], Scans[i]);
                            results.Add(result);

                            
                        }

                        i++;
                    }
                }


            }

            return results;
        }

        public List<LineResult> LineFindAsync(List<bool> ScanDirections, List<int> Scores, List<RectangleF> ROIs)
        {
            List<LineResult> results = new List<LineResult>();

            Bitmap SeachImage = Camera.GetImageAsync2();

            ImageWidth = SeachImage.Width;
            ImageHeight = SeachImage.Height;

            if (LineFindInited)
            {
                if (ROIs.Count > 0)
                {
                    int i = 0;
                    foreach (RectangleF ROI in ROIs)
                    {
                        

                        LineResult result = new LineResult();
                        bool Done = Algorithm.LineFindRun(SeachImage, Scores[i], ref result, ROIs[i], ScanDirections[i]);
                        results.Add(result);

                        i++;
                    }


                }
            }
            

            return results;
        }

        public List<LineResult> LineFindAsync(int Score, bool ScanDirection, RectangleF ROI)
        {
            List<LineResult> results = new List<LineResult>();


            bool En1 = Algorithm.LineFindSetRunPara<int>(LineFindParas.LineRate, Score);

            if (!En1)
            {
                return null;
            }

            Bitmap SeachImage = Camera.GetImageAsync2();

            ImageWidth = SeachImage.Width;
            ImageHeight = SeachImage.Height;

            LineResult result = new LineResult();
            bool Done = Algorithm.LineFindRun(SeachImage, Score, ref result, ROI, ScanDirection);
            results.Add(result);


            return results;
        }

        public RectangleFA RectEdgeCalculate(LineResult Upline, LineResult Downline, LineResult Leftline, LineResult Rightline)
        {
            return Algorithm.RectEdgeCalculate(Upline, Downline, Leftline, Rightline);
        }


        public bool CircleFindSave(string CircleFindxml)
        {
            return Algorithm.CircleFindSaveRunPara(CircleFindxml);
        }

        public bool CircleFindInit(string CircleFindxml)
        {
            if (CircleFindxml != null)
            {
                CircleFindInited = Algorithm.LineFindLoadRunPara(CircleFindxml);

            }
            return CircleFindInited;
        }

        public CircleResult CircleFindAsync(int Score, RectangleF ROI, int MinRadius = -1, int MaxRadius = -1)
        {
            CircleResult results = new CircleResult();

            if (CircleFindInited)
            {
                Bitmap SeachImage = Camera.GetImageAsync2();

                ImageWidth = SeachImage.Width;
                ImageHeight = SeachImage.Height;

                bool En1 = Algorithm.CircleFindSetRunPara<int>(CircleFindParas.CircleRate, Score);

                //if (!En1)
                //{
                //    return null;
                //}

                bool Done = Algorithm.CircleFindRun(SeachImage, Score, ref results, ROI, MinRadius, MaxRadius);
            }


            return results;
        }


        public CircleResult CircleFindAsync(string CircleFindxml, int Score, RectangleF ROI, int MinRadius = -1, int MaxRadius = -1)
        {
            CircleResult results = new CircleResult();

            if(CircleFindxml != null)
            {
                

                CircleFindInited = Algorithm.LineFindLoadRunPara(CircleFindxml);

                if (CircleFindInited)
                {
                    Bitmap SeachImage = Camera.GetImageAsync2();

                    ImageWidth = SeachImage.Width;
                    ImageHeight = SeachImage.Height;

                    bool Done = Algorithm.CircleFindRun(SeachImage, Score, ref results, ROI, MinRadius, MaxRadius);
                }

                    
            }
            

            return results;
        }


        #endregion

        #region 光源

        public int GetRingLightintensity()
        {
            if (LightCon != null && (int)ring > -1)
            {
                return (int)LightCon.GetIntensity((int)ring);
            }
            else
            {
                return -1;
            }
        }

        public int GetDirectLightintensity()
        {
            if(LightCon != null && (int)direct > -1)
            {
                return (int)LightCon.GetIntensity((int)direct);
            }
            else
            {
                return -1;
            }
        }

        public bool SetRingLightintensity(int Intensity)
        {
            if (LightCon != null && (int)ring > -1)
            {
                LightCon.SetIntensity(Intensity, (int)ring);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SetDirectLightintensity(int Intensity)
        {
            if (LightCon != null && (int)direct > -1)
            {
                LightCon.SetIntensity(Intensity, (int)direct);
                return true;
            }
            else
            {
                return false;
            }
        }


        #endregion

        #endregion


    }
}
