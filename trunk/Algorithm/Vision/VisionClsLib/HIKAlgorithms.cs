using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
//using System.Drawing.Common;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Forms;


using VisionDesigner;
using VisionDesigner.ContourPatMatch;
using System.Xml;
using System.IO;
using VisionDesigner.LineFind;
using VisionDesigner.CircleFind;

using OpenCvSharp;
using OpenCvSharp.Extensions;
using VisionClsLib;
using GlobalDataDefineClsLib;

namespace VisionClsLib
{

    class HIKAlgorithms
    {
        #region Private File

        ContourPatMatch match = new ContourPatMatch();

        LineFind line = new LineFind();

        CircleFind circle = new CircleFind();

        #endregion

        #region 高精度模板匹配

        /// <summary>
        /// 初始化模板匹配算子
        /// </summary>
        /// <returns></returns>
        public bool MatchInit()
        {
            try
            {
                return match.Init();
            }
            catch
            {
                return false;
            }
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
        public bool MatchTrain(Bitmap TrainImage, ref MatchTemplateResult Result, PointF Benchmark = new PointF(), RectangleF ROI = new RectangleF(), bool Sign = true)
        {
            MatchTemplateResult Templateresult = new MatchTemplateResult();
            List<ScorePoint> result = new List<ScorePoint>();
            try
            {
                if (match.Inited)
                {
                    if (TrainImage != null)
                    {
                        match.TrainLoadImage(TrainImage);
                        match.TrainClearROI();
                        if (Benchmark != null)
                        {
                            match.TrainSetFixPoint(Benchmark.X, Benchmark.Y);
                        }
                        else
                        {
                            
                        }
                        if (ROI.Width != 0 && ROI.Height !=0 )
                        {
                            match.TrainAddROI(ROI, Sign);
                            
                        }
                        else
                        {
                            match.TrainClearROI();
                        }
                        Templateresult = match.Train();
                        Result = Templateresult;
                    }
                    
                    if (result.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                    
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
            MatchTemplateResult Templateresult = new MatchTemplateResult();
            List<ScorePoint> result = new List<ScorePoint>();
            try
            {
                if (match.Inited)
                {
                    if (TrainImage != null)
                    {
                        match.TrainLoadImage(TrainImage);
                        match.TrainClearROI();
                        if (ROI.Width != 0 && ROI.Height != 0)
                        {
                            match.TrainAddROI(ROI, Sign);

                        }
                        else
                        {
                            match.TrainClearROI();
                        }
                        Templateresult = match.Train();
                        Result = Templateresult;
                    }

                    if (result.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }


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
        /// 保存模板
        /// </summary>
        /// <param name="TrainXmlFilepath">模板文件完整路径 后缀.contourmxml</param>
        /// <returns></returns>
        public bool MatchSaveTrain(string TrainXmlFilepath)
        {
            try
            {
                if (match.Inited)
                {
                    if (TrainXmlFilepath != null)
                    {
                        return match.TrainSaveMatch(TrainXmlFilepath);
                    }
                    else
                    {
                        return false;
                    }
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
        /// 读取模板
        /// </summary>
        /// <param name="TrainXmlFilepath">模板文件完整路径 后缀.contourmxml</param>
        /// <returns></returns>
        public bool MatchLoadTrain(string TrainXmlFilepath)
        {
            try
            {
                if (match.Inited)
                {
                    if (TrainXmlFilepath != null)
                    {
                        return match.TrainLoadMatch(TrainXmlFilepath);
                    }
                    else
                    {
                        return false;
                    }
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
        /// 保存训练参数
        /// </summary>
        /// <param name="TrainXmlFilepath">模板文件完整路径 后缀.contourmxml</param>
        /// <returns></returns>
        public bool TrainSavePara(string TrainXmlFilepath)
        {
            try
            {
                if (match.Inited)
                {
                    if (TrainXmlFilepath != null)
                    {
                        match.TrainSavePara(TrainXmlFilepath);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
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
        /// 读取训练参数
        /// </summary>
        /// <param name="TrainXmlFilepath">模板文件完整路径 后缀.contourmxml</param>
        /// <returns></returns>
        public bool TrainLoadPara(string TrainXmlFilepath)
        {
            try
            {
                if (match.Inited)
                {
                    if (TrainXmlFilepath != null)
                    {
                        match.TrainLoadPara(TrainXmlFilepath);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
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
            try
            {
                if (match.Inited)
                {
                    return match.TrainLoadPara(ScaleMode, ScaleLevel, ScaleRLevel, ThresMode, ThresValue, WeightFlag, ChainFlag, MinChain);
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
            try
            {
                if (match.Inited)
                {
                    return match.TrainSavePara(ref ScaleMode, ref ScaleLevel,ref ScaleRLevel,ref ThresMode,ref ThresValue,ref WeightFlag,ref ChainFlag,ref MinChain);
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
        /// 设置识别参数
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="para">参数名</param>
        /// <param name="Val">值</param>
        /// <returns></returns>
        public bool MatchSetRunPara<T>(MatchParas para, T Val)
        {
            try
            {
                if (match.Inited)
                {
                    match.RunSetPara<T>(para, Val);
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
        /// 获取识别参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="para"></param>
        /// <param name="Val"></param>
        /// <returns></returns>
        public bool MatchGetRunPara<T>(MatchParas para, ref T Val)
        {
            try
            {
                if (match.Inited)
                {
                    Val = match.RunGetPara<T>(para);

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
        /// 加载识别参数
        /// </summary>
        /// <param name="fileDlg">识别参数.xml路径</param>
        /// <returns></returns>
        public bool MatchLoadRunPara(string fileDlg)
        {
            try
            {
                if(match.Inited)
                {
                    match.RunLoadPara(fileDlg);

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
        /// 保存识别参数
        /// </summary>
        /// <param name="fileDlg">识别参数.xml路径</param>
        /// <returns></returns>
        public bool MatchSaveRunPara(string fileDlg)
        {
            try
            {
                if (match.Inited)
                {
                    match.RunSavePara(fileDlg);

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
        /// 模板识别
        /// </summary>
        /// <param name="RunImage">识别图像</param>
        /// <param name="Result">识别结果</param>
        /// <param name="ROI">识别区域</param>
        /// <returns></returns>
        public bool MatchRun(Bitmap RunImage,float Socre, ref List<MatchResult> Result, RectangleF ROI = new RectangleF())
        {
            List<MatchResult> result = new List<MatchResult>();
            try
            {
                if (match.Inited)
                {
                    if (RunImage != null)
                    {
                        match.RunLoadImage(RunImage);
                        match.RunClearROI();
                        if (ROI.Width != 0 && ROI.Height != 0)
                        {
                            match.RunAddROI(ROI);

                        }
                        else
                        {
                            match.RunClearROI();
                        }
                        result = match.Run();

                        if(result != null)
                        {
                            foreach(MatchResult match in result)
                            {
                                if(match.Score > Socre)
                                {
                                    match.IsOk = true;
                                }
                            }
                        }

                        Result = result;
                    }

                    if(result == null)
                    {
                        return false;
                    }
                    if (result.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
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

        #endregion

        #region 边缘查找

        /// <summary>
        /// 初始化边缘查找算子
        /// </summary>
        /// <returns></returns>
        public bool LineFindInit()
        {
            try
            {
                return line.Init();
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 设置边缘查找参数
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="para">参数名</param>
        /// <param name="Val">值</param>
        /// <returns></returns>
        public bool LineFindSetRunPara<T>(LineFindParas para, T Val)
        {
            try
            {
                if (line.Inited)
                {
                    line.SetPara<T>(para, Val);
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
        /// 获取边缘查找参数
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="para">参数名</param>
        /// <param name="Val">返回值</param>
        /// <returns></returns>
        public bool LineFindGetRunPara<T>(LineFindParas para, ref T Val)
        {
            try
            {
                if (line.Inited)
                {
                    Val = line.GetPara<T>(para);

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
        /// 加载边缘查找参数
        /// </summary>
        /// <param name="fileDlg">边缘查找参数.xml路径</param>
        /// <returns></returns>
        public bool LineFindLoadRunPara(string fileDlg)
        {
            try
            {
                if (line.Inited)
                {
                    line.LoadPara(fileDlg);

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
        /// 保存边缘查找参数
        /// </summary>
        /// <param name="fileDlg">边缘查找参数.xml路径</param>
        /// <returns></returns>
        public bool LineFindSaveRunPara(string fileDlg)
        {
            try
            {
                if (line.Inited)
                {
                    line.SavePara(fileDlg);

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
        /// 边缘查找识别
        /// </summary>
        /// <param name="RunImage">识别图像</param>
        /// <param name="Result">识别结果</param>
        /// <param name="ROI">识别区域</param>
        /// <param name="ScanDirection">搜索方向 false 从左到右 true 从上到下</param>
        /// <returns></returns>
        public bool LineFindRun(Bitmap RunImage, int Score, ref LineResult Result, RectangleF ROI = new RectangleF(), bool ScanDirection = false)
        {
            LineResult result = new LineResult();
            try
            {
                if (line.Inited)
                {
                    if (RunImage != null)
                    {
                        line.LoadImage(RunImage);
                        if(ScanDirection == false)
                        {
                            line.SetPara<string>(LineFindParas.FindOrient, "LeftToRight"); 
                        }
                        else
                        {
                            line.SetPara<string>(LineFindParas.FindOrient, "UpToDown");
                        }
                        line.ClearROI();
                        if (ROI.Width != 0 && ROI.Height != 0)
                        {
                            line.AddROI(ROI);

                        }
                        else
                        {
                            line.ClearROI();
                        }
                        result = line.Run();
                        if(result.Straightness > Score)
                        {
                            result.IsOk = true;
                        }
                        else
                        {
                            result.IsOk = false;
                        }
                        Result = result;
                    }

                    if (result != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
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

        #endregion

        #region 圆查找

        /// <summary>
        /// 初始化圆查找算子
        /// </summary>
        /// <returns></returns>
        public bool CircleFindInit()
        {
            try
            {
                return circle.Init();
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 设置圆查找参数
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="para">参数名</param>
        /// <param name="Val">值</param>
        /// <returns></returns>
        public bool CircleFindSetRunPara<T>(CircleFindParas para, T Val)
        {
            try
            {
                if (circle.Inited)
                {
                    circle.SetPara<T>(para, Val);
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
        /// 获取圆查找参数
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="para">参数名</param>
        /// <param name="Val">返回值</param>
        /// <returns></returns>
        public bool CircleFindGetRunPara<T>(CircleFindParas para, ref T Val)
        {
            try
            {
                if (circle.Inited)
                {
                    Val = circle.GetPara<T>(para);

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
        /// 加载圆查找参数
        /// </summary>
        /// <param name="fileDlg">圆查找参数.xml路径</param>
        /// <returns></returns>
        public bool CircleFindLoadRunPara(string fileDlg)
        {
            try
            {
                if (circle.Inited)
                {
                    circle.LoadPara(fileDlg);

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
        /// 保存圆查找参数
        /// </summary>
        /// <param name="fileDlg">圆查找参数.xml路径</param>
        /// <returns></returns>
        public bool CircleFindSaveRunPara(string fileDlg)
        {
            try
            {
                if (circle.Inited)
                {
                    circle.SavePara(fileDlg);

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
            CircleResult result = new CircleResult();
            try
            {
                if (circle.Inited)
                {
                    if (RunImage != null)
                    {
                        circle.LoadImage(RunImage);
                        if(MinRadius > 0)
                        {
                            circle.SetPara<int>(CircleFindParas.MinRadius, MinRadius);
                        }
                        if(MaxRadius > 0)
                        {
                            circle.SetPara<int>(CircleFindParas.MaxRadius, MaxRadius);
                        }
                        if (ROI.Width != 0 && ROI.Height != 0)
                        {
                            circle.AddROI(ROI);

                        }
                        else
                        {
                            circle.ClearROI();
                        }
                        result = circle.Run();

                        if(result.Circleness > Score)
                        {
                            result.IsOk = true;
                        }
                        else
                        {
                            result.IsOk = false;
                        }    

                        Result = result;
                    }

                    if (result != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
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

        #endregion


    }

    public class ImageBasicAlgorithms
    {

        #region Format Conversion Method


        public static System.Drawing.Bitmap MattoBitmap(OpenCvSharp.Mat mat)
        {
            try
            {
                System.Drawing.Bitmap bitmap;

                bitmap = mat.ToBitmap();

                return bitmap;
            }
            catch(Exception)
            {
                return null;
            }
        }

        public static OpenCvSharp.Mat BitmaptoMat(System.Drawing.Bitmap bitmap)
        {
            try
            {
                Mat mat = BitmapConverter.ToMat(bitmap);
                return mat;
            }
            catch(Exception)
            {
                return null;
            }
        }

        public static CMvdImage BitmapToCMvdImage(System.Drawing.Bitmap bitmap)
        {
            try
            {
                CMvdImage cMvdImage = new CMvdImage();
                System.Drawing.Imaging.PixelFormat bitPixelFormat = bitmap.PixelFormat;
                BitmapData bmData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitPixelFormat);

                if (bitPixelFormat == System.Drawing.Imaging.PixelFormat.Format8bppIndexed)
                {
                    Int32 bitmapDataSize = bmData.Stride * bmData.Height;
                    int offset = bmData.Stride - bmData.Width;
                    Int32 ImageBaseDataSize = bmData.Width * bmData.Height;
                    byte[] _BitImageBufferBytes = new byte[bitmapDataSize];
                    byte[] _ImageBaseDataBufferBytes = new byte[ImageBaseDataSize];
                    Marshal.Copy(bmData.Scan0, _BitImageBufferBytes, 0, bitmapDataSize);
                    int bitmapIndex = 0;
                    int ImageBaseDataIndex = 0;
                    for (int i = 0; i < bmData.Height; i++)
                    {
                        for (int j = 0; j < bmData.Width; j++)
                        {
                            _ImageBaseDataBufferBytes[ImageBaseDataIndex++] = _BitImageBufferBytes[bitmapIndex++];
                        }
                        bitmapIndex += offset;
                    }
                    MVD_IMAGE_DATA_INFO stImageData = new MVD_IMAGE_DATA_INFO();
                    stImageData.stDataChannel[0].nRowStep = (uint)bmData.Width;
                    stImageData.stDataChannel[0].nLen = (uint)ImageBaseDataSize;
                    stImageData.stDataChannel[0].nSize = (uint)ImageBaseDataSize;
                    stImageData.stDataChannel[0].arrDataBytes = _ImageBaseDataBufferBytes;
                    cMvdImage.InitImage((uint)bmData.Width, (uint)bmData.Height, MVD_PIXEL_FORMAT.MVD_PIXEL_MONO_08, stImageData);
                }
                else if (bitPixelFormat == System.Drawing.Imaging.PixelFormat.Format24bppRgb)
                {
                    Int32 bitmapDataSize = bmData.Stride * bmData.Height;//bitmap图像缓存长度
                    int offset = bmData.Stride - bmData.Width * 3;
                    Int32 ImageBaseDataSize = bmData.Width * bmData.Height * 3;
                    byte[] _BitImageBufferBytes = new byte[bitmapDataSize];
                    byte[] _ImageBaseDataBufferBytes = new byte[ImageBaseDataSize];
                    Marshal.Copy(bmData.Scan0, _BitImageBufferBytes, 0, bitmapDataSize);
                    int bitmapIndex = 0;
                    int ImageBaseDataIndex = 0;
                    for (int i = 0; i < bmData.Height; i++)
                    {
                        for (int j = 0; j < bmData.Width; j++)
                        {
                            _ImageBaseDataBufferBytes[ImageBaseDataIndex++] = _BitImageBufferBytes[bitmapIndex + 2];
                            _ImageBaseDataBufferBytes[ImageBaseDataIndex++] = _BitImageBufferBytes[bitmapIndex + 1];
                            _ImageBaseDataBufferBytes[ImageBaseDataIndex++] = _BitImageBufferBytes[bitmapIndex];
                            bitmapIndex += 3;
                        }
                        bitmapIndex += offset;
                    }
                    MVD_IMAGE_DATA_INFO stImageData = new MVD_IMAGE_DATA_INFO();
                    stImageData.stDataChannel[0].nRowStep = (uint)bmData.Width * 3;
                    stImageData.stDataChannel[0].nLen = (uint)ImageBaseDataSize;
                    stImageData.stDataChannel[0].nSize = (uint)ImageBaseDataSize;
                    stImageData.stDataChannel[0].arrDataBytes = _ImageBaseDataBufferBytes;
                    cMvdImage.InitImage((uint)bmData.Width, (uint)bmData.Height, MVD_PIXEL_FORMAT.MVD_PIXEL_RGB_RGB24_C3, stImageData);
                }
                bitmap.UnlockBits(bmData);  // 解除锁定
                return cMvdImage;

            }
            catch(Exception)
            {
                return null;
            }
        }


        #endregion

        #region Common methods

        /// <summary>
        /// 计算图像清晰度
        /// </summary>
        /// <param name="bitmap">输入的Bitmap图像</param>
        /// <param name="method">选择的算子，0为Laplacian，1为Sobel，2为Scharr</param>
        /// <returns>图像清晰度的度量值</returns>
        public static double CalculateSharpness(Bitmap bitmap, int method = 0)
        {
            Mat image = BitmapConverter.ToMat(bitmap);
            Cv2.CvtColor(image, image, ColorConversionCodes.BGR2GRAY);

            Mat edgeImage = new Mat();
            switch (method)
            {
                case 0: // Laplacian算子
                    Cv2.Laplacian(image, edgeImage, MatType.CV_64F);
                    break;
                case 1: // Sobel算子
                    Cv2.Sobel(image, edgeImage, MatType.CV_64F, 1, 1);
                    break;
                case 2: // Scharr算子
                    Cv2.Scharr(image, edgeImage, MatType.CV_64F, 1, 0);
                    break;
                default:
                    throw new ArgumentException("无效的方法选择");
            }

            Mat mean = new Mat();
            Mat stddev = new Mat();
            Cv2.MeanStdDev(edgeImage, mean, stddev);

            double sharpness = stddev.At<double>(0) * stddev.At<double>(0);
            return sharpness;
        }



        #endregion


    }


    #region ContourPatMatch


    #region ContourPatMatch Private Filed

    public enum SortType
    {
        None = 1,
        Score = 2,
        Angle = 3,
        X = 4,
        Y = 5,
        XY = 6,
        YX = 7,
    }

    public enum ContourPatCreateScaleMode
    {
        Manual = 0,
        Auto = 1
    }

    public enum ContourPatCreateThresholdMode
    {
        Manual = 0,
        Auto = 1
    }
    public enum ContourPatCreateWeightFlag
    {
        False = 0,
        True = 1
    }
    public enum ContourPatCreateChainFlag
    {
        Manual = 0,
        Auto = 1
    }

    public class ContourPatternCreateParam
    {
        public ContourPatternCreateParam()
        {
            ScaleMode = ContourPatCreateScaleMode.Auto;
            ThresMode = ContourPatCreateThresholdMode.Auto;
            WeightFlag = ContourPatCreateWeightFlag.False;
            ChainFlag = ContourPatCreateChainFlag.Auto;
        }

        public ContourPatCreateScaleMode ScaleMode { get; set; }

        public float ScaleLevel { get; set; }

        public UInt32 ScaleRLevel { get; set; }

        public ContourPatCreateThresholdMode ThresMode { get; set; }

        public UInt32 ThresValue { get; set; }

        public ContourPatCreateWeightFlag WeightFlag { get; set; }

        public ContourPatCreateChainFlag ChainFlag { get; set; }

        public Int32 MinChain { get; set; }

    }

    public class CMvdNodeBase
    {
        string m_strName = string.Empty;
        string m_strDescription = string.Empty;
        string m_strDisplayName = string.Empty;
        string m_strVisibility = string.Empty;
        string m_strAccessMode = string.Empty;
        int m_nAlgorithmIndex = 0;

        public string Name
        {
            get
            {
                return m_strName;
            }
            set
            {
                m_strName = value;
            }
        }

        public string Description
        {
            get
            {
                return m_strDescription;
            }
            set
            {
                m_strDescription = value;
            }
        }

        public string DisplayName
        {
            get
            {
                return m_strDisplayName;
            }
            set
            {
                m_strDisplayName = value;
            }
        }

        public string Visibility
        {
            get
            {
                return m_strVisibility;
            }
            set
            {
                m_strVisibility = value;
            }
        }

        public string AccessMode
        {
            get
            {
                return m_strAccessMode;
            }
            set
            {
                m_strAccessMode = value;
            }
        }

        public int AlgorithmIndex
        {
            get
            {
                return m_nAlgorithmIndex;
            }
            set
            {
                m_nAlgorithmIndex = value;
            }
        }
    }

    public class CMvdNodeInteger : CMvdNodeBase
    {
        int m_nCurValue = 0;
        int m_nDefaultValue = 0;
        int m_nMinValue = 0;
        int m_nMaxValue = 0;
        int m_nIncValue = 0;

        public int CurValue
        {
            get
            {
                return m_nCurValue;
            }
            set
            {
                m_nCurValue = value;
            }
        }

        public int DefaultValue
        {
            get
            {
                return m_nDefaultValue;
            }
            set
            {
                m_nDefaultValue = value;
            }
        }

        public int MinValue
        {
            get
            {
                return m_nMinValue;
            }
            set
            {
                m_nMinValue = value;
            }
        }

        public int MaxValue
        {
            get
            {
                return m_nMaxValue;
            }
            set
            {
                m_nMaxValue = value;
            }
        }

        public int IncValue
        {
            get
            {
                return m_nIncValue;
            }
            set
            {
                m_nIncValue = value;
            }
        }
    }

    public class CMvdNodeEnumEntry
    {
        string m_strName = string.Empty;
        string m_strDescription = string.Empty;
        string m_strDisplayName = string.Empty;
        bool m_bIsImplemented = false;
        int m_nValue = 0;

        public string Name
        {
            get
            {
                return m_strName;
            }
            set
            {
                m_strName = value;
            }
        }

        public string Description
        {
            get
            {
                return m_strDescription;
            }
            set
            {
                m_strDescription = value;
            }
        }

        public string DisplayName
        {
            get
            {
                return m_strDisplayName;
            }
            set
            {
                m_strDisplayName = value;
            }
        }

        public bool IsImplemented
        {
            get
            {
                return m_bIsImplemented;
            }
            set
            {
                m_bIsImplemented = value;
            }
        }

        public int Value
        {
            get
            {
                return m_nValue;
            }
            set
            {
                m_nValue = value;
            }
        }
    }

    public class CMvdNodeFloat : CMvdNodeBase
    {
        float m_fCurValue = 0;
        float m_fDefaultValue = 0;
        float m_fMinValue = 0;
        float m_fMaxValue = 0;
        float m_fIncValue = 0;

        public float CurValue
        {
            get
            {
                return m_fCurValue;
            }
            set
            {
                m_fCurValue = value;
            }
        }

        public float DefaultValue
        {
            get
            {
                return m_fDefaultValue;
            }
            set
            {
                m_fDefaultValue = value;
            }
        }

        public float MinValue
        {
            get
            {
                return m_fMinValue;
            }
            set
            {
                m_fMinValue = value;
            }
        }

        public float MaxValue
        {
            get
            {
                return m_fMaxValue;
            }
            set
            {
                m_fMaxValue = value;
            }
        }

        public float IncValue
        {
            get
            {
                return m_fIncValue;
            }
            set
            {
                m_fIncValue = value;
            }
        }
    }

    public class CMvdNodeBoolean : CMvdNodeBase
    {
        bool m_bDefaultValue = false;
        bool m_bCurValue = false;

        public bool CurValue
        {
            get
            {
                return m_bCurValue;
            }
            set
            {
                m_bCurValue = value;
            }
        }

        public bool DefaultValue
        {
            get
            {
                return m_bDefaultValue;
            }
            set
            {
                m_bDefaultValue = value;
            }
        }
    }

    public class CMvdNodeEnumeration : CMvdNodeBase
    {
        CMvdNodeEnumEntry m_eCurValue = new CMvdNodeEnumEntry();
        CMvdNodeEnumEntry m_eDefaultValue = new CMvdNodeEnumEntry();
        List<CMvdNodeEnumEntry> m_listEnumEntry = new List<CMvdNodeEnumEntry>();

        public CMvdNodeEnumEntry CurValue
        {
            get
            {
                return m_eCurValue;
            }
            set
            {
                m_eCurValue = value;
            }
        }

        public CMvdNodeEnumEntry DefaultValue
        {
            get
            {
                return m_eDefaultValue;
            }
            set
            {
                m_eDefaultValue = value;
            }
        }

        public List<CMvdNodeEnumEntry> EnumRange
        {
            get
            {
                return m_listEnumEntry;
            }
            set
            {
                m_listEnumEntry = value;
            }
        }
    }

    public class CMvdXmlParseTool
    {
        private List<CMvdNodeInteger> m_listIntValue = null;
        private List<CMvdNodeEnumeration> m_listEnumValue = null;
        private List<CMvdNodeFloat> m_listFloatValue = null;
        private List<CMvdNodeBoolean> m_listBooleanValue = null;

        public CMvdXmlParseTool(Byte[] bufXml, uint nXmlLen)
        {
            m_listIntValue = new List<CMvdNodeInteger>();
            m_listEnumValue = new List<CMvdNodeEnumeration>();
            m_listFloatValue = new List<CMvdNodeFloat>();
            m_listBooleanValue = new List<CMvdNodeBoolean>();
            UpdateXmlBuf(bufXml, nXmlLen);
        }

        public List<CMvdNodeInteger> IntValueList
        {
            get
            {
                return m_listIntValue;
            }
        }

        public List<CMvdNodeEnumeration> EnumValueList
        {
            get
            {
                return m_listEnumValue;
            }
        }

        public List<CMvdNodeFloat> FloatValueList
        {
            get
            {
                return m_listFloatValue;
            }
        }

        public List<CMvdNodeBoolean> BooleanValueList
        {
            get
            {
                return m_listBooleanValue;
            }
        }

        public void UpdateXmlBuf(Byte[] bufXml, uint nXmlLen)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;    //忽略文档里面的注释
            XmlReader reader = XmlReader.Create(new MemoryStream(bufXml, 0, (int)nXmlLen), settings);
            xmlDoc.Load(reader);
            reader.Close();
            m_listIntValue.Clear();
            m_listEnumValue.Clear();
            m_listFloatValue.Clear();
            m_listBooleanValue.Clear();
            XmlNode xnCategory = xmlDoc.SelectSingleNode("AlgorithmRoot").SelectSingleNode("Category");
            foreach (XmlNode xn in xnCategory)
            {
                switch (xn.Name)
                {
                    case "Integer":
                        {
                            CMvdNodeInteger NodeInt = new CMvdNodeInteger();
                            NodeInt.Name = ((XmlElement)xn).GetAttribute("Name");
                            NodeInt.Description = xn.SelectSingleNode("Description").InnerText;
                            NodeInt.DisplayName = xn.SelectSingleNode("DisplayName").InnerText;
                            NodeInt.Visibility = xn.SelectSingleNode("Visibility").InnerText;
                            NodeInt.AccessMode = xn.SelectSingleNode("AccessMode").InnerText;
                            NodeInt.AlgorithmIndex = IntStringToInt(xn.SelectSingleNode("AlgorithmIndex").InnerText);
                            NodeInt.CurValue = IntStringToInt(xn.SelectSingleNode("CurValue").InnerText);
                            NodeInt.DefaultValue = IntStringToInt(xn.SelectSingleNode("DefaultValue").InnerText);
                            NodeInt.MinValue = IntStringToInt(xn.SelectSingleNode("MinValue").InnerText);
                            NodeInt.MaxValue = IntStringToInt(xn.SelectSingleNode("MaxValue").InnerText);
                            NodeInt.IncValue = IntStringToInt(xn.SelectSingleNode("IncValue").InnerText);
                            m_listIntValue.Add(NodeInt);
                        }
                        break;
                    case "Enumeration":
                        {
                            CMvdNodeEnumeration NodeEnum = new CMvdNodeEnumeration();
                            NodeEnum.Name = ((XmlElement)xn).GetAttribute("Name");
                            NodeEnum.Description = xn.SelectSingleNode("Description").InnerText;
                            NodeEnum.DisplayName = xn.SelectSingleNode("DisplayName").InnerText;
                            NodeEnum.Visibility = xn.SelectSingleNode("Visibility").InnerText;
                            NodeEnum.AccessMode = xn.SelectSingleNode("AccessMode").InnerText;
                            NodeEnum.AlgorithmIndex = IntStringToInt(xn.SelectSingleNode("AlgorithmIndex").InnerText);
                            int nCurValue = IntStringToInt(xn.SelectSingleNode("CurValue").InnerText);
                            int nDefaultValue = IntStringToInt(xn.SelectSingleNode("DefaultValue").InnerText);
                            XmlNodeList xnlEnumEntry = xn.SelectNodes("EnumEntry");
                            List<CMvdNodeEnumEntry> clistNodeEnumEntry = new List<CMvdNodeEnumEntry>();
                            foreach (XmlNode xnEnumEntry in xnlEnumEntry)
                            {
                                CMvdNodeEnumEntry cNodeEnumEntry = new CMvdNodeEnumEntry();
                                cNodeEnumEntry.Name = ((XmlElement)xnEnumEntry).GetAttribute("Name");
                                cNodeEnumEntry.Description = xnEnumEntry.SelectSingleNode("Description").InnerText;
                                cNodeEnumEntry.DisplayName = xnEnumEntry.SelectSingleNode("DisplayName").InnerText;
                                cNodeEnumEntry.Value = IntStringToInt(xnEnumEntry.SelectSingleNode("Value").InnerText);
                                clistNodeEnumEntry.Add(cNodeEnumEntry);
                                if (nCurValue == cNodeEnumEntry.Value)
                                {
                                    NodeEnum.CurValue = cNodeEnumEntry;
                                }
                                if (nDefaultValue == cNodeEnumEntry.Value)
                                {
                                    NodeEnum.DefaultValue = cNodeEnumEntry;
                                }
                            }
                            NodeEnum.EnumRange = clistNodeEnumEntry;
                            m_listEnumValue.Add(NodeEnum);
                        }
                        break;
                    case "Float":
                        {
                            CMvdNodeFloat NodeFloat = new CMvdNodeFloat();
                            NodeFloat.Name = ((XmlElement)xn).GetAttribute("Name");
                            NodeFloat.Description = xn.SelectSingleNode("Description").InnerText;
                            NodeFloat.DisplayName = xn.SelectSingleNode("DisplayName").InnerText;
                            NodeFloat.Visibility = xn.SelectSingleNode("Visibility").InnerText;
                            NodeFloat.AccessMode = xn.SelectSingleNode("AccessMode").InnerText;
                            NodeFloat.AlgorithmIndex = IntStringToInt(xn.SelectSingleNode("AlgorithmIndex").InnerText);
                            NodeFloat.CurValue = System.Convert.ToSingle(xn.SelectSingleNode("CurValue").InnerText);
                            NodeFloat.DefaultValue = System.Convert.ToSingle(xn.SelectSingleNode("DefaultValue").InnerText);
                            NodeFloat.MinValue = System.Convert.ToSingle(xn.SelectSingleNode("MinValue").InnerText);
                            NodeFloat.MaxValue = System.Convert.ToSingle(xn.SelectSingleNode("MaxValue").InnerText);
                            NodeFloat.IncValue = System.Convert.ToSingle(xn.SelectSingleNode("IncValue").InnerText);
                            m_listFloatValue.Add(NodeFloat);
                        }
                        break;
                    case "Boolean":
                        {
                            CMvdNodeBoolean NodeBoolean = new CMvdNodeBoolean();
                            NodeBoolean.Name = ((XmlElement)xn).GetAttribute("Name");
                            NodeBoolean.Description = xn.SelectSingleNode("Description").InnerText;
                            NodeBoolean.DisplayName = xn.SelectSingleNode("DisplayName").InnerText;
                            NodeBoolean.Visibility = xn.SelectSingleNode("Visibility").InnerText;
                            NodeBoolean.AccessMode = xn.SelectSingleNode("AccessMode").InnerText;
                            NodeBoolean.AlgorithmIndex = IntStringToInt(xn.SelectSingleNode("AlgorithmIndex").InnerText);
                            NodeBoolean.CurValue = xn.SelectSingleNode("CurValue").InnerText.Equals("true", StringComparison.OrdinalIgnoreCase) == true ? true : false;
                            NodeBoolean.DefaultValue = xn.SelectSingleNode("DefaultValue").InnerText.Equals("true", StringComparison.OrdinalIgnoreCase) == true ? true : false;
                            m_listBooleanValue.Add(NodeBoolean);
                        }
                        break;
                    default:
                        {
                            throw new VisionDesigner.MvdException(VisionDesigner.MVD_MODULE_TYPE.MVD_MODUL_APP
                                                                , VisionDesigner.MVD_ERROR_CODE.MVD_E_SUPPORT
                                                                , "Algorithm type not support!");
                        }
                }
            }
        }

        public void ClearXmlBuf()
        {
            m_listIntValue.Clear();
            m_listEnumValue.Clear();
            m_listFloatValue.Clear();
            m_listBooleanValue.Clear();
        }

        private int IntStringToInt(string strIntString)
        {
            if (strIntString.Contains("0x") || strIntString.Contains("0X"))
            {
                return Convert.ToInt32(strIntString, 16);
            }
            else
            {
                return Convert.ToInt32(strIntString, 10);
            }
        }
    }

    #endregion

    class ContourPatMatch
    {
        #region TrainFiled



        #region Private Filed

        /// <summary>
        /// 计时器
        /// </summary>
        private Stopwatch _StopWatch = new Stopwatch();

        /// <summary>
        /// 训练图像
        /// </summary>
        private CMvdImage _TrainImage = null;

        /// <summary>
        /// 建模配置参数
        /// </summary>
        private ContourPatternCreateParam _PatCreateParam = new ContourPatternCreateParam();

        /// <summary>
        /// 绘制Region区域
        /// </summary>
        private List<CPatMatchRegion> _DrawRegionList = new List<CPatMatchRegion>();

        /// <summary>
        /// 基准点
        /// </summary>
        private CMvdPointSetF _DrawFixPoint = new CMvdPointSetF();


        #endregion

        #region Property

        /// <summary>
        /// 模型
        /// </summary>
        internal CContourPattern ContourPattern { get; set; }

        #endregion



        #endregion

        #region RunFiled

        public bool Inited = false;
        public bool matchtrainexist = false;

        #region Private Filed

        /// <summary>
        /// Xml解析工具
        /// </summary>
        private CMvdXmlParseTool _XmlParseTool = null;

        /// <summary>
        /// 高精度模板算子
        /// </summary>
        private CContourPattern _ContourPattern = null;

        /// <summary>
        /// 高精度匹配算子
        /// </summary>
        private CContourPatMatchTool _ContourPatMatchTool = null;

        /// <summary>
        /// 输入图像
        /// </summary>
        private CMvdImage _InputImage = null;

        /// <summary>
        /// ROI
        /// </summary>
        private CMvdShape _ROIShape = null;

        /// <summary>
        /// 掩膜图形列表
        /// </summary>
        private List<CMvdShape> _MaskShapeList = new List<CMvdShape>();

        /// <summary>
        /// 匹配框列表
        /// </summary>
        private List<CMvdRectangleF> _MatchBoxList = new List<CMvdRectangleF>();

        /// <summary>
        /// 匹配轮廓列表
        /// </summary>
        private List<CMvdPointSetF> _MatchOutlineList = new List<CMvdPointSetF>();


        #endregion


        #endregion

        #region Private method


        private void UpdateTrainParaView()
        {
            string algoParaValue = String.Empty;
            ContourPattern.GetRunParam("PyramidScaleFlag", ref algoParaValue);
            _PatCreateParam.ScaleMode = (ContourPatCreateScaleMode)Enum.Parse(typeof(ContourPatCreateScaleMode), algoParaValue);
            ContourPattern.GetRunParam("EdgeThresholdFlag", ref algoParaValue);
            _PatCreateParam.ThresMode = (ContourPatCreateThresholdMode)Enum.Parse(typeof(ContourPatCreateThresholdMode), algoParaValue);
            ContourPattern.GetRunParam("PyramidScaleLevel", ref algoParaValue);
            _PatCreateParam.ScaleLevel = Convert.ToSingle(algoParaValue);
            ContourPattern.GetRunParam("PyramidScaleRLevel", ref algoParaValue);
            _PatCreateParam.ScaleRLevel = Convert.ToUInt32(algoParaValue);
            ContourPattern.GetRunParam("EdgeThreshold", ref algoParaValue);
            _PatCreateParam.ThresValue = Convert.ToUInt32(algoParaValue);

            ContourPattern.GetRunParam("WeightMaskFlag", ref algoParaValue);
            _PatCreateParam.WeightFlag = (ContourPatCreateWeightFlag)Enum.Parse(typeof(ContourPatCreateWeightFlag), algoParaValue);
            ContourPattern.GetRunParam("ChainFlag", ref algoParaValue);
            _PatCreateParam.ChainFlag = (ContourPatCreateChainFlag)Enum.Parse(typeof(ContourPatCreateChainFlag), algoParaValue);
            ContourPattern.GetRunParam("MinChainLen", ref algoParaValue);
            _PatCreateParam.MinChain = Convert.ToInt32(algoParaValue);
        }

        private void UpdatePatCreateParasView(ref ContourPatCreateScaleMode ScaleMode, ref float ScaleLevel, ref uint ScaleRLevel,
            ref ContourPatCreateThresholdMode ThresMode, ref uint ThresValue, ref ContourPatCreateWeightFlag WeightFlag,
            ref ContourPatCreateChainFlag ChainFlag, ref int MinChain)
        {
            ScaleMode = _PatCreateParam.ScaleMode;
            ScaleLevel = _PatCreateParam.ScaleLevel;
            ScaleRLevel = _PatCreateParam.ScaleRLevel;
            ThresMode = _PatCreateParam.ThresMode;
            ThresValue = _PatCreateParam.ThresValue;
            WeightFlag = _PatCreateParam.WeightFlag;
            ChainFlag = _PatCreateParam.ChainFlag;
            MinChain = _PatCreateParam.MinChain;
        }

        private void CreateFixPointShape(float centerX, float centerY, ref CMvdPointSetF fixPoint)
        {
            fixPoint = new CMvdPointSetF();

            for (int j = 0; j < 9; j++)
            {
                float fX = centerX - 4 + j;
                fixPoint.AddPoint(fX, centerY);
                float fY = centerY - 4 + j;
                fixPoint.AddPoint(centerX, fY);
            }
            fixPoint.BorderColor = new MVD_COLOR(255, 0, 0, 255);
        }

        private T GetParam<T>(string paraName)
        {
            if (null == _XmlParseTool)
            {
                return default(T);
            }
            else
            {
                var value = default(T);
                Type targetType = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);
                if (typeof(T) == typeof(int))
                {
                    for (int i = 0; i < _XmlParseTool.IntValueList.Count; ++i)
                    {
                        if(_XmlParseTool.IntValueList[i].Name == paraName)
                        {
                            value=(T)Convert.ChangeType(_XmlParseTool.IntValueList[i].CurValue, targetType);
                        }
                       
                    }
                }

                if(typeof(T) == typeof(string))
                {
                    for (int i = 0; i < _XmlParseTool.EnumValueList.Count; ++i)
                    {
                        if (_XmlParseTool.EnumValueList[i].Name == paraName)
                        {
                            value = (T)Convert.ChangeType(_XmlParseTool.EnumValueList[i].CurValue.Name, targetType);
                        }
                    }
                }

                if(typeof(T) == typeof(float))
                {
                    for (int i = 0; i < _XmlParseTool.FloatValueList.Count; ++i)
                    {
                        if (_XmlParseTool.FloatValueList[i].Name == paraName)
                        {
                            value = (T)Convert.ChangeType(_XmlParseTool.FloatValueList[i].CurValue, targetType);
                        }
                    }
                }

                if (typeof(T) == typeof(bool))
                {
                    for (int i = 0; i < _XmlParseTool.BooleanValueList.Count; ++i)
                    {
                        if(_XmlParseTool.BooleanValueList[i].Name == paraName)
                        {
                            value = (T)Convert.ChangeType(_XmlParseTool.BooleanValueList[i].CurValue, targetType);
                        }
                    }
                }

                for (int i = 0; i < _XmlParseTool.EnumValueList.Count; ++i)
                {
                    if (_XmlParseTool.EnumValueList[i].Name == paraName)
                    {
                        value = (T)Convert.ChangeType(_XmlParseTool.EnumValueList[i].CurValue.Value, targetType);
                    }
                }


                try
                {
                    return value;
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException($"Failed to convert to {typeof(T).Name}.", ex);
                }
            }
        }

        /// <summary>
        /// Update paramters
        /// </summary>
        /// <param name="bufXml"></param>
        /// <param name="nXmlLen"></param>
        private void UpdateParamList(Byte[] paramXmlBufferBytes, uint paramXmlBufferLength)
        {
            if (null == _XmlParseTool)
            {
                _XmlParseTool = new CMvdXmlParseTool(paramXmlBufferBytes, paramXmlBufferLength);
            }
            else
            {
                _XmlParseTool.UpdateXmlBuf(paramXmlBufferBytes, paramXmlBufferLength);
            }
            //dataGridView1.Rows.Clear();
            for (int i = 0; i < _XmlParseTool.IntValueList.Count; ++i)
            {
                string vname = _XmlParseTool.IntValueList[i].Name;
                int vvalue = _XmlParseTool.IntValueList[i].CurValue;
            }

            for (int i = 0; i < _XmlParseTool.EnumValueList.Count; ++i)
            {
                string vname = _XmlParseTool.EnumValueList[i].Name;
                int vvalue = _XmlParseTool.IntValueList[i].CurValue;
            }

            for (int i = 0; i < _XmlParseTool.FloatValueList.Count; ++i)
            {
                string vname = _XmlParseTool.FloatValueList[i].Name;
                float vvalue = _XmlParseTool.FloatValueList[i].CurValue;
            }

            for (int i = 0; i < _XmlParseTool.BooleanValueList.Count; ++i)
            {
                string vname = _XmlParseTool.BooleanValueList[i].Name;
                bool vvalue = _XmlParseTool.BooleanValueList[i].CurValue;
            }
        }

        private void UpdateTrainParaView(ContourPatCreateScaleMode ScaleMode, float ScaleLevel, uint ScaleRLevel,
            ContourPatCreateThresholdMode ThresMode, uint ThresValue, ContourPatCreateWeightFlag WeightFlag,
            ContourPatCreateChainFlag ChainFlag, int MinChain)
        {
            _PatCreateParam.ScaleMode = ScaleMode;
            _PatCreateParam.ScaleLevel = ScaleLevel;
            _PatCreateParam.ScaleRLevel = ScaleRLevel;
            _PatCreateParam.ThresMode = ThresMode;
            _PatCreateParam.ThresValue = ThresValue;
            _PatCreateParam.WeightFlag = WeightFlag;
            _PatCreateParam.ChainFlag = ChainFlag;
            _PatCreateParam.MinChain = MinChain;

            ContourPattern.SetRunParam("PyramidScaleFlag", Convert.ToInt32(_PatCreateParam.ScaleMode).ToString());
            if (ContourPatCreateScaleMode.Manual == _PatCreateParam.ScaleMode)
            {
                ContourPattern.SetRunParam("PyramidScaleLevel", _PatCreateParam.ScaleLevel.ToString());
                ContourPattern.SetRunParam("PyramidScaleRLevel", _PatCreateParam.ScaleRLevel.ToString());
            }
            ContourPattern.SetRunParam("EdgeThresholdFlag", Convert.ToInt32(_PatCreateParam.ThresMode).ToString());
            if (ContourPatCreateThresholdMode.Manual == _PatCreateParam.ThresMode)
            {
                ContourPattern.SetRunParam("EdgeThreshold", _PatCreateParam.ThresValue.ToString());
            }
            ContourPattern.SetRunParam("WeightMaskFlag", Convert.ToInt32(_PatCreateParam.WeightFlag).ToString());
            ContourPattern.SetRunParam("ChainFlag", Convert.ToInt32(_PatCreateParam.ChainFlag).ToString());
            if (ContourPatCreateChainFlag.Manual == _PatCreateParam.ChainFlag)
            {
                ContourPattern.SetRunParam("MinChainLen", _PatCreateParam.MinChain.ToString());
            }
        }

        #endregion


        #region Train method



        public void TrainLoadImage(string fileDlg)
        {

            try
            {
                if (fileDlg != null)
                {
                    if (null == _TrainImage)
                    {
                        _TrainImage = new CMvdImage();
                    }

                    _TrainImage.InitImage(fileDlg);

                    if (MVD_PIXEL_FORMAT.MVD_PIXEL_RGB_RGB24_C3 == _TrainImage.PixelFormat
                         || MVD_PIXEL_FORMAT.MVD_PIXEL_RGB_RGB24_P3 == _TrainImage.PixelFormat)
                    {
                        _TrainImage.ConvertImagePixelFormat(MVD_PIXEL_FORMAT.MVD_PIXEL_MONO_08);
                    }

                    ContourPattern.InputImage = _TrainImage;
                    ContourPattern.MaskImage = null;
                    ContourPattern.RegionList.Clear();

                    _DrawRegionList.Clear();
                    _DrawFixPoint = null;

                    if ((ContourPattern.BasicParam.FixPoint.fX >= 0)
                     && (ContourPattern.BasicParam.FixPoint.fY >= 0)
                     && (ContourPattern.BasicParam.FixPoint.fX <= _TrainImage.Width)
                     && (ContourPattern.BasicParam.FixPoint.fY <= _TrainImage.Height))
                    {
                        CreateFixPointShape(ContourPattern.BasicParam.FixPoint.fX
                                          , ContourPattern.BasicParam.FixPoint.fY
                                          , ref _DrawFixPoint);
                    }
                }
            }
            catch (MvdException)
            {
                ContourPattern.InputImage = null;
            }
            catch (System.Exception)
            {
                ContourPattern.InputImage = null;
            }
        }

        public void TrainLoadImage(Bitmap bitmap)
        {

            try
            {
                if (bitmap != null)
                {
                    if (null == _TrainImage)
                    {
                        _TrainImage = new CMvdImage();
                    }

                    _TrainImage = ImageBasicAlgorithms.BitmapToCMvdImage(bitmap);

                    if (MVD_PIXEL_FORMAT.MVD_PIXEL_RGB_RGB24_C3 == _TrainImage.PixelFormat
                         || MVD_PIXEL_FORMAT.MVD_PIXEL_RGB_RGB24_P3 == _TrainImage.PixelFormat)
                    {
                        _TrainImage.ConvertImagePixelFormat(MVD_PIXEL_FORMAT.MVD_PIXEL_MONO_08);
                    }

                    ContourPattern.InputImage = _TrainImage;
                    ContourPattern.MaskImage = null;
                    ContourPattern.RegionList.Clear();

                    _DrawRegionList.Clear();
                    _DrawFixPoint = null;

                    if ((ContourPattern.BasicParam.FixPoint.fX >= 0)
                     && (ContourPattern.BasicParam.FixPoint.fY >= 0)
                     && (ContourPattern.BasicParam.FixPoint.fX <= _TrainImage.Width)
                     && (ContourPattern.BasicParam.FixPoint.fY <= _TrainImage.Height))
                    {
                        CreateFixPointShape(ContourPattern.BasicParam.FixPoint.fX
                                          , ContourPattern.BasicParam.FixPoint.fY
                                          , ref _DrawFixPoint);
                    }
                }
            }
            catch (MvdException)
            {
                ContourPattern.InputImage = null;
            }
            catch (System.Exception)
            {
                ContourPattern.InputImage = null;
            }
        }

        public void TrainLoadPara(string fileDlg)
        {
            FileStream stream = null;
            try
            {
                
                if (fileDlg != null)
                {
                    if (File.Exists(fileDlg))
                    {
                        stream = new FileStream(fileDlg, FileMode.Open, FileAccess.Read);
                        Int64 fileLength = stream.Length;
                        byte[] fileBytes = new byte[fileLength];
                        uint readLength = Convert.ToUInt32(stream.Read(fileBytes, 0, fileBytes.Length));
                        ContourPattern.LoadConfiguration(fileBytes, readLength);
                        UpdateTrainParaView();
                    }
                    else
                    {
                        TrainSavePara(fileDlg);
                    }

                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (null != stream)
                {
                    stream.Close();
                    stream.Dispose();
                }
            }
        }

        public void TrainSavePara(string fileDlg)
        {
            FileStream stream = null;
            try
            {
                if (fileDlg != null)
                {
                    string filePath = fileDlg;
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }

                    byte[] fileBytes = new byte[256];
                    uint configDataSize = 256;
                    uint configDataLen = 0;
                    try
                    {
                        ContourPattern.SaveConfiguration(fileBytes, configDataSize, ref configDataLen);
                    }
                    catch (MvdException ex)
                    {
                        if (MVD_ERROR_CODE.MVD_E_NOENOUGH_BUF == ex.ErrorCode)
                        {
                            fileBytes = new byte[configDataLen];
                            configDataSize = configDataLen;
                            ContourPattern.SaveConfiguration(fileBytes, configDataSize, ref configDataLen);
                        }
                        else
                        {
                            throw ex;
                        }
                    }

                    
                    stream = new FileStream(filePath, FileMode.CreateNew);
                    stream.Write(fileBytes, 0, Convert.ToInt32(configDataLen));
                    stream.Flush();

                }
            }
            catch (Exception)
            {

            }
            finally
            {
                if (null != stream)
                {
                    stream.Close();
                    stream.Dispose();
                }
            }
        }

        public bool TrainLoadPara(ContourPatCreateScaleMode ScaleMode, float ScaleLevel, uint ScaleRLevel,
            ContourPatCreateThresholdMode ThresMode, uint ThresValue, ContourPatCreateWeightFlag WeightFlag,
            ContourPatCreateChainFlag ChainFlag, int MinChain)
        {
            try
            {
                UpdateTrainParaView(ScaleMode, ScaleLevel, ScaleRLevel, ThresMode, ThresValue, WeightFlag, ChainFlag, MinChain);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool TrainSavePara(ref ContourPatCreateScaleMode ScaleMode, ref float ScaleLevel, ref uint ScaleRLevel,
            ref ContourPatCreateThresholdMode ThresMode, ref uint ThresValue, ref ContourPatCreateWeightFlag WeightFlag,
            ref ContourPatCreateChainFlag ChainFlag, ref int MinChain)
        {
            try
            {
                UpdatePatCreateParasView(ref ScaleMode, ref ScaleLevel, ref ScaleRLevel, ref ThresMode, ref ThresValue, ref WeightFlag, ref ChainFlag, ref MinChain);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public void TrainSetFixPoint(float X, float Y)
        {
            try
            {
                if (null == ContourPattern.InputImage)
                {
                    return;
                }
                float fixPointX = X;
                float fixPointY = Y;

                if (((fixPointX < 0) || (fixPointX > ContourPattern.InputImage.Width))
                 || ((fixPointY < 0) || (fixPointY > ContourPattern.InputImage.Height)))
                {
                    _DrawFixPoint = null;
                }
                else
                {
                    CreateFixPointShape(fixPointX, fixPointY, ref _DrawFixPoint);

                    ContourPattern.BasicParam.FixPoint = new MVD_POINT_F(fixPointX, fixPointY);

                }

            }
            catch (Exception)
            {
                _DrawFixPoint = null;
            }
        }

        public void TrainAddROI(RectangleF Rect, bool Sign)
        {
            try
            {
                var region = new CPatMatchRegion();
                region.Shape = new CMvdRectangleF(Rect.X + Rect.Width / 2, Rect.Y + Rect.Height / 2, Rect.Width, Rect.Height);
                region.Sign = Sign;

                _DrawRegionList.Add(region);
            }
            catch (Exception)
            {

            }
        }

        public void TrainClearROI()
        {
            try
            {
                _DrawRegionList.Clear();
                //foreach (var item in _DrawRegionList)
                //{
                //    if (cShapeObj == item.Shape)
                //    {
                //        _DrawRegionList.Remove(item);
                //        break;
                //    }
                //}

                //if (cShapeObj == _DrawFixPoint)
                //{
                //    _DrawFixPoint = null;
                //}
                //return;
            }
            catch (Exception)
            {

            }
        }

        public MatchTemplateResult Train()
        {

            try
            {

                MatchTemplateResult TrainResults = new MatchTemplateResult();
                List<ScorePoint> points = new List<ScorePoint>();
                if ((null == ContourPattern) || (null == ContourPattern.InputImage))
                {
                    throw new MvdException(MVD_MODULE_TYPE.MVD_MODUL_APP, MVD_ERROR_CODE.MVD_E_CALLORDER);
                }

                if (0 == (int)_PatCreateParam.ChainFlag)
                {
                    double RoughScale = _PatCreateParam.ScaleLevel;
                    double fineScale = _PatCreateParam.ScaleRLevel;
                    if (fineScale > RoughScale)
                    {
                        throw new MvdException(MVD_MODULE_TYPE.MVD_MODUL_APP
                                             , MVD_ERROR_CODE.MVD_E_PARAMETER_ILLEGAL
                                             , "Fine scale must lower than Rough scale.");
                    }
                }

                float LUX = 0;
                float LUY = 0;

                ContourPattern.RegionList.Clear();
                foreach (var item in _DrawRegionList)
                {
                    var region = new CPatMatchRegion();
                    switch (item.Shape.ShapeType)
                    {
                        case MVD_SHAPE_TYPE.MvdShapeRectangle:
                            {
                                var regionShape = item.Shape as CMvdRectangleF;
                                regionShape.Interaction = true;
                                region.Shape = regionShape.Clone() as CMvdRectangleF;
                                LUX = regionShape.CenterX - regionShape.Width / 2;
                                LUY = regionShape.CenterY - regionShape.Height / 2;
                            }
                            break;
                        case MVD_SHAPE_TYPE.MvdShapeAnnularSector:
                            {
                                var regionShape = item.Shape as CMvdAnnularSectorF;
                                regionShape.Interaction = true;
                                region.Shape = regionShape.Clone() as CMvdAnnularSectorF;
                            }
                            break;
                        case MVD_SHAPE_TYPE.MvdShapePolygon:
                            {
                                var regionShape = item.Shape as CMvdPolygonF;
                                regionShape.Interaction = true;
                                region.Shape = regionShape.Clone() as CMvdPolygonF;
                            }
                            break;
                        default:
                            {
                                throw new MvdException(MVD_MODULE_TYPE.MVD_MODUL_APP, MVD_ERROR_CODE.MVD_E_PARAMETER_ILLEGAL);
                            }
                    }
                    region.Sign = item.Sign;

                    ContourPattern.RegionList.Add(region);
                }

                ContourPattern.SetRunParam("PyramidScaleFlag", Convert.ToInt32(_PatCreateParam.ScaleMode).ToString());
                if (ContourPatCreateScaleMode.Manual == _PatCreateParam.ScaleMode)
                {
                    ContourPattern.SetRunParam("PyramidScaleLevel", _PatCreateParam.ScaleLevel.ToString());
                    ContourPattern.SetRunParam("PyramidScaleRLevel", _PatCreateParam.ScaleRLevel.ToString());
                }
                ContourPattern.SetRunParam("EdgeThresholdFlag", Convert.ToInt32(_PatCreateParam.ThresMode).ToString());
                if (ContourPatCreateThresholdMode.Manual == _PatCreateParam.ThresMode)
                {
                    ContourPattern.SetRunParam("EdgeThreshold", _PatCreateParam.ThresValue.ToString());
                }
                ContourPattern.SetRunParam("WeightMaskFlag", Convert.ToInt32(_PatCreateParam.WeightFlag).ToString());
                ContourPattern.SetRunParam("ChainFlag", Convert.ToInt32(_PatCreateParam.ChainFlag).ToString());
                if (ContourPatCreateChainFlag.Manual == _PatCreateParam.ChainFlag)
                {
                    ContourPattern.SetRunParam("MinChainLen", _PatCreateParam.MinChain.ToString());
                }


                _StopWatch.Restart();
                ContourPattern.Train();
                _StopWatch.Stop();

                var patternData = ContourPattern.Result.Data;
                _PatCreateParam.ScaleLevel = patternData.PyramidScaleLevel;
                _PatCreateParam.ScaleRLevel = Convert.ToUInt32(patternData.PyramidScaleReturnLevel);
                _PatCreateParam.ThresValue = patternData.LowThreshold;
                _PatCreateParam.MinChain = patternData.MinChainLen;

                CMvdImage trainedImage = null;
                List<CPatMatchOutline> patmatchOutline = null;

                if (null == ContourPattern.Result)
                {
                    matchtrainexist = false;
                    return null;
                    
                }
                else
                {
                    trainedImage = ContourPattern.Result.Data.TrainedImage;
                    patmatchOutline = ContourPattern.Result.OutlineList;


                    if (null != trainedImage)
                    {

                        if (null != patmatchOutline)
                        {
                            
                            foreach (var item in patmatchOutline)
                            {
                                foreach (var point in item.EdgePointList)
                                {
                                    if (0 == point.Score)
                                    {
                                        PointF point1 = new PointF(LUX + point.Position.fX, LUY + point.Position.fY);
                                        ScorePoint spoint = new ScorePoint(point1, 0);
                                        points.Add(spoint);
                                    }
                                    else if (1 == point.Score)
                                    {
                                        PointF point1 = new PointF(LUX + point.Position.fX, LUY + point.Position.fY);
                                        ScorePoint spoint = new ScorePoint(point1, 1);
                                        points.Add(spoint);
                                    }
                                    else if (2 == point.Score)
                                    {
                                        PointF point1 = new PointF(LUX + point.Position.fX, LUY + point.Position.fY);
                                        ScorePoint spoint = new ScorePoint(point1, 2);
                                        points.Add(spoint);
                                    }
                                }
                            }

                            TrainResults = new MatchTemplateResult(new PointF(LUX + ContourPattern.Result.Data.FixPoint.fX, LUY + ContourPattern.Result.Data.FixPoint.fY), 0, new PointF(0, 0), points);
                        }
                    }

                    matchtrainexist = true;
                    return TrainResults;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool TrainLoadMatch(string fileDig)
        {
            try
            {
                if (fileDig != null && File.Exists(fileDig))
                {
                    ContourPattern.ImportPattern(fileDig);
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool TrainSaveMatch(string fileDig)
        {
            try
            {
                if (fileDig != null)
                {
                    string filePath = fileDig;
                    ContourPattern.ExportPattern(filePath);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }



        #endregion

        #region Run method

        public bool Init()
        {
            try
            {
                ContourPattern = new CContourPattern();
                _ContourPattern = new CContourPattern();
                _ContourPatMatchTool = new CContourPatMatchTool();
                byte[] fileBytes = new byte[256];
                uint nConfigDataSize = 256;
                uint nConfigDataLen = 0;
                try
                {
                    _ContourPatMatchTool.SaveConfiguration(fileBytes, nConfigDataSize, ref nConfigDataLen);
                }
                catch (MvdException ex)
                {
                    if (MVD_ERROR_CODE.MVD_E_NOENOUGH_BUF == ex.ErrorCode)
                    {
                        fileBytes = new byte[nConfigDataLen];
                        nConfigDataSize = nConfigDataLen;
                        _ContourPatMatchTool.SaveConfiguration(fileBytes, nConfigDataSize, ref nConfigDataLen);
                    }
                    else
                    {
                        throw ex;
                    }
                }
                UpdateParamList(fileBytes, nConfigDataLen);

                if (null != ContourPattern.InputImage)
                {
                    _TrainImage = ContourPattern.InputImage.Clone();

                    if ((ContourPattern.BasicParam.FixPoint.fX >= 0)
                     && (ContourPattern.BasicParam.FixPoint.fY >= 0)
                     && (ContourPattern.BasicParam.FixPoint.fX <= _TrainImage.Width)
                     && (ContourPattern.BasicParam.FixPoint.fY <= _TrainImage.Height))
                    {
                        CreateFixPointShape(ContourPattern.BasicParam.FixPoint.fX
                                          , ContourPattern.BasicParam.FixPoint.fY
                                          , ref _DrawFixPoint);
                    }
                }
                Inited = true;
                return true;
            }
            catch (MvdException ex)
            {
                //MessageBox.Show("TrainFrom Load Failed! Error code : 0x" + ex.ErrorCode.ToString("X"));
                Inited = false;
                return false;
            }
            catch (System.Exception ex)
            {
                //MessageBox.Show("TrainFrom Load Failed! Exception : " + ex.Message);
                Inited = false;
                return false;
            }
            finally
            {

            }
        }

        public void RunLoadImage(string fileDlg)
        {
            try
            {
                if (fileDlg != null)
                {
                    if (null == _InputImage)
                    {
                        _InputImage = new CMvdImage();
                    }
                    _InputImage.InitImage(fileDlg);

                    if (MVD_PIXEL_FORMAT.MVD_PIXEL_RGB_RGB24_C3 == _InputImage.PixelFormat
                         || MVD_PIXEL_FORMAT.MVD_PIXEL_RGB_RGB24_P3 == _InputImage.PixelFormat)
                    {
                        _InputImage.ConvertImagePixelFormat(MVD_PIXEL_FORMAT.MVD_PIXEL_MONO_08);
                    }

                    _ROIShape = null;
                    _MaskShapeList.Clear();
                    _MatchBoxList.Clear();
                    _MatchOutlineList.Clear();
                }
            }
            catch (Exception)
            {

            }
        }

        public void RunLoadImage(Bitmap bitmap)
        {
            try
            {
                if (null == _InputImage)
                {
                    _InputImage = new CMvdImage();
                }
                _InputImage = ImageBasicAlgorithms.BitmapToCMvdImage(bitmap);

                if (MVD_PIXEL_FORMAT.MVD_PIXEL_RGB_RGB24_C3 == _InputImage.PixelFormat
                         || MVD_PIXEL_FORMAT.MVD_PIXEL_RGB_RGB24_P3 == _InputImage.PixelFormat)
                {
                    _InputImage.ConvertImagePixelFormat(MVD_PIXEL_FORMAT.MVD_PIXEL_MONO_08);
                }

                _ROIShape = null;
                _MaskShapeList.Clear();
                _MatchBoxList.Clear();
                _MatchOutlineList.Clear();
            }
            catch (Exception)
            {

            }
        }

        public void RunLoadPara(string fileDlg)
        {
            FileStream fileStr = null;
            try
            {

                if (fileDlg != null)
                {
                    if (File.Exists(fileDlg))
                    {
                        fileStr = new FileStream(fileDlg, FileMode.Open, FileAccess.Read);
                        Int64 nFileLen = fileStr.Length;
                        byte[] fileBytes = new byte[nFileLen];
                        uint nReadLen = Convert.ToUInt32(fileStr.Read(fileBytes, 0, fileBytes.Length));
                        fileStr.Close();
                        fileStr.Dispose();
                        _ContourPatMatchTool.LoadConfiguration(fileBytes, nReadLen);
                        UpdateParamList(fileBytes, nReadLen);
                    }
                    else
                    {
                        RunSavePara(fileDlg);
                    }
                }
                
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (null != fileStr)
                {
                    fileStr.Close();
                    fileStr.Dispose();
                }
            }
        }

        public void RunSavePara(string fileDlg)
        {
            FileStream stream = null;
            try
            {
                if (fileDlg != null)
                {
                    string filePath = fileDlg;
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }

                    byte[] fileBytes = new byte[256];
                    uint configDataSize = 256;
                    uint configDataLen = 0;
                    try
                    {
                        _ContourPatMatchTool.SaveConfiguration(fileBytes, configDataSize, ref configDataLen);
                    }
                    catch (MvdException ex)
                    {
                        if (MVD_ERROR_CODE.MVD_E_NOENOUGH_BUF == ex.ErrorCode)
                        {
                            fileBytes = new byte[configDataLen];
                            configDataSize = configDataLen;
                            _ContourPatMatchTool.SaveConfiguration(fileBytes, configDataSize, ref configDataLen);
                        }
                        else
                        {
                            throw ex;
                        }
                    }

                    
                    stream = new FileStream(filePath, FileMode.CreateNew);
                    stream.Write(fileBytes, 0, Convert.ToInt32(configDataLen));
                    stream.Flush();
                    stream.Close();
                    stream.Dispose();
                }
            }
            catch (Exception)
            {

            }
        }

        public void RunSetPara<T>(MatchParas para, T Val)
        {
            try
            {
                String paramName = Enum.GetName(typeof(MatchParas), para);
                string paramValue = null;
                for (int i = 0; i < _XmlParseTool.IntValueList.Count; ++i)
                {
                    if (paramName == _XmlParseTool.IntValueList[i].Name)
                    {
                        paramValue = Val.ToString();
                    }
                }
                for (int i = 0; i < _XmlParseTool.FloatValueList.Count; ++i)
                {
                    if (paramName == _XmlParseTool.FloatValueList[i].Name)
                    {
                        paramValue = Val.ToString();
                    }
                }
                for (int i = 0; i < _XmlParseTool.EnumValueList.Count; ++i)
                {
                    if (paramName == _XmlParseTool.EnumValueList[i].Name)
                    {
                        String strEnumEntryName = Val.ToString();
                        paramValue = strEnumEntryName;
                    }
                }
                for (int i = 0; i < _XmlParseTool.BooleanValueList.Count; ++i)
                {
                    if (paramName == _XmlParseTool.BooleanValueList[i].Name)
                    {
                        String strBooleanValueName = Val.ToString();
                        paramValue = strBooleanValueName.Equals("True", StringComparison.OrdinalIgnoreCase) == true ? "1" : "0";
                    }
                }
                _StopWatch.Restart();
                _ContourPatMatchTool.SetRunParam(paramName, paramValue);
                _StopWatch.Stop();

            }
            catch (Exception)
            {

            }
        }

        public T RunGetPara<T>(MatchParas para)
        {
            T value = default(T);
            try
            {
                String paramName = Enum.GetName(typeof(MatchParas), para);
                value = GetParam<T>(paramName);

                return value;
            }
            catch
            {
                return default(T);
            }
        }

        public void RunClearROI()
        {
            try
            {
                _ROIShape = null;

                _MatchBoxList.Clear();


            }
            catch (Exception)
            {

            }
        }

        public void RunAddROI(RectangleF Rect)
        {
            try
            {
                _ROIShape = new CMvdRectangleF(Rect.X + Rect.Width / 2, Rect.Y + Rect.Height / 2, Rect.Width, Rect.Height);
            }
            catch (Exception)
            {

            }
        }

        public List<MatchResult> Run()
        {
            try
            {
                List<MatchResult> Results = new List<MatchResult>();

                if ((null == _ContourPatMatchTool) || (null == _ContourPattern) || (null == _InputImage))
                {
                    throw new MvdException(MVD_MODULE_TYPE.MVD_MODUL_APP, MVD_ERROR_CODE.MVD_E_CALLORDER);
                }

                _ContourPatMatchTool.BasicParam.ShowOutlineStatus = true;

                _MatchOutlineList.Clear();

                _ContourPattern = ContourPattern;

                _ContourPatMatchTool.InputImage = _InputImage;
                _ContourPatMatchTool.Pattern = _ContourPattern;
                _ContourPatMatchTool.ROI = _ROIShape;

                _ContourPatMatchTool.ClearMasks();
                foreach (var item in _MaskShapeList)
                {
                    _ContourPatMatchTool.AddMask(item);
                }

                _StopWatch.Restart();
                _ContourPatMatchTool.Run();
                _StopWatch.Stop();

                var matchResult = _ContourPatMatchTool.Result;

                CMvdRectangleF Shape = _ROIShape as CMvdRectangleF;
                ROI rect = new ROI(new RectangleF(Shape.LeftTopX, Shape.LeftTopY, Shape.Width, Shape.Height), true);


                int i = 0;
                if (matchResult.MatchInfoList.Count() > 0)
                {
                    foreach (var item in matchResult.MatchInfoList)
                    {
                        var matchBox = new RectangleFA(new PointF(item.MatchPoint.fX, item.MatchPoint.fY), new PointF(item.MatchBox.CenterX, item.MatchBox.CenterY), item.MatchBox.Width, item.MatchBox.Height, item.MatchBox.Angle);

                        List<ScorePoint> Points = new List<ScorePoint>();
                        foreach (var point in matchResult.OutlineList[i].EdgePointList)
                        {
                            if (0 == point.Score)
                            {
                                Points.Add(new ScorePoint(new PointF(point.Position.fX, point.Position.fY), 0));
                            }
                            else if (1 == point.Score)
                            {
                                Points.Add(new ScorePoint(new PointF(point.Position.fX, point.Position.fY), 1));
                            }
                            else if (2 == point.Score)
                            {
                                Points.Add(new ScorePoint(new PointF(point.Position.fX, point.Position.fY), 2));
                            }
                        }

                        Results.Add(new MatchResult(matchBox, rect, item.Score, item.Scale, item.ScaleX, item.ScaleY, Points));
                        i++;
                    }
                }
                else
                {
                    Results.Add(new MatchResult(null, rect, 0, 0, 0, 0, null));
                }

                return Results;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

    }

    #endregion

    #region LineFind

    #region LineFind Private Filed



    #endregion

    class LineFind
    {
        #region Private File

        private CLineFindTool m_stLineFindToolObj = null;
        private CMvdImage m_stInputImage = null;
        private CMvdShape m_stROIShape = null;
        List<VisionDesigner.CMvdShape> m_lMaskShapes = new List<VisionDesigner.CMvdShape>();
        private VisionDesigner.CMvdLineSegmentF m_stResLineShape = null;
        private CMvdXmlParseTool m_stXmlParseToolObj = null;
        List<CMvdLineSegmentF> _DrawOutlineList = new List<CMvdLineSegmentF>();     // 绘制轮廓
        List<CMvdRectangleF> _DrawCaliperBoxList = new List<CMvdRectangleF>();     // 绘制卡尺框

        public bool Inited = false;

        #endregion

        #region Private Method

        /// <summary>
        /// Update paramters
        /// </summary>
        /// <param name="bufXml"></param>
        /// <param name="nXmlLen"></param>
        private void UpdateParamList(Byte[] bufXml, uint nXmlLen)
        {
            if (null == m_stXmlParseToolObj)
            {
                m_stXmlParseToolObj = new CMvdXmlParseTool(bufXml, nXmlLen);
            }
            else
            {
                m_stXmlParseToolObj.UpdateXmlBuf(bufXml, nXmlLen);
            }
        }

        private double GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return ts.TotalMilliseconds;
        }

        private T GetParam<T>(string paraName)
        {
            if (null == m_stXmlParseToolObj)
            {
                return default(T);
            }
            else
            {
                var value = default(T);
                Type targetType = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);
                if (typeof(T) == typeof(int))
                {
                    for (int i = 0; i < m_stXmlParseToolObj.IntValueList.Count; ++i)
                    {
                        if (m_stXmlParseToolObj.IntValueList[i].Name == paraName)
                        {
                            value = (T)Convert.ChangeType(m_stXmlParseToolObj.IntValueList[i].CurValue, targetType);
                        }

                    }
                }

                if (typeof(T) == typeof(string))
                {
                    for (int i = 0; i < m_stXmlParseToolObj.EnumValueList.Count; ++i)
                    {
                        if (m_stXmlParseToolObj.EnumValueList[i].Name == paraName)
                        {
                            value = (T)Convert.ChangeType(m_stXmlParseToolObj.EnumValueList[i].CurValue.Name, targetType);
                        }
                    }
                }

                if (typeof(T) == typeof(float))
                {
                    for (int i = 0; i < m_stXmlParseToolObj.FloatValueList.Count; ++i)
                    {
                        if (m_stXmlParseToolObj.FloatValueList[i].Name == paraName)
                        {
                            value = (T)Convert.ChangeType(m_stXmlParseToolObj.FloatValueList[i].CurValue, targetType);
                        }
                    }
                }

                if (typeof(T) == typeof(bool))
                {
                    for (int i = 0; i < m_stXmlParseToolObj.BooleanValueList.Count; ++i)
                    {
                        if (m_stXmlParseToolObj.BooleanValueList[i].Name == paraName)
                        {
                            value = (T)Convert.ChangeType(m_stXmlParseToolObj.BooleanValueList[i].CurValue, targetType);
                        }
                    }
                }


                try
                {
                    return value;
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException($"Failed to convert to {typeof(T).Name}.", ex);
                }
            }
        }


        #endregion

        #region Method

        public bool Init()
        {
            try
            {
                m_stLineFindToolObj = new CLineFindTool();
                byte[] fileBytes = new byte[256];
                uint nConfigDataSize = 256;
                uint nConfigDataLen = 0;
                try
                {
                    m_stLineFindToolObj.SaveConfiguration(fileBytes, nConfigDataSize, ref nConfigDataLen);
                }
                catch (MvdException ex)
                {
                    if (MVD_ERROR_CODE.MVD_E_NOENOUGH_BUF == ex.ErrorCode)
                    {
                        fileBytes = new byte[nConfigDataLen];
                        nConfigDataSize = nConfigDataLen;
                        m_stLineFindToolObj.SaveConfiguration(fileBytes, nConfigDataSize, ref nConfigDataLen);
                    }
                    else
                    {
                        throw ex;
                    }
                }
                UpdateParamList(fileBytes, nConfigDataLen);
                Inited = true;
                return true;
            }
            catch(Exception)
            {
                Inited = false;
                return false;
            }
        }

        public void LoadImage(string fileDlg)
        {
            try
            {
                if (fileDlg != null)
                {
                    if (null == m_stInputImage)
                    {
                        m_stInputImage = new CMvdImage();
                    }
                    m_stInputImage.InitImage(fileDlg);

                    if (MVD_PIXEL_FORMAT.MVD_PIXEL_RGB_RGB24_C3 == m_stInputImage.PixelFormat
                         || MVD_PIXEL_FORMAT.MVD_PIXEL_RGB_RGB24_P3 == m_stInputImage.PixelFormat)
                    {
                        m_stInputImage.ConvertImagePixelFormat(MVD_PIXEL_FORMAT.MVD_PIXEL_MONO_08);
                    }

                    m_lMaskShapes.Clear();
                    m_stROIShape = null;
                    m_stResLineShape = null;
                    _DrawOutlineList.Clear();
                    _DrawCaliperBoxList.Clear();
                }
            }
            catch (Exception)
            {

            }
        }

        public void LoadImage(Bitmap bitmap)
        {
            try
            {
                if (null == m_stInputImage)
                {
                    m_stInputImage = new CMvdImage();
                }
                m_stInputImage = ImageBasicAlgorithms.BitmapToCMvdImage(bitmap);

                if (MVD_PIXEL_FORMAT.MVD_PIXEL_RGB_RGB24_C3 == m_stInputImage.PixelFormat
                         || MVD_PIXEL_FORMAT.MVD_PIXEL_RGB_RGB24_P3 == m_stInputImage.PixelFormat)
                {
                    m_stInputImage.ConvertImagePixelFormat(MVD_PIXEL_FORMAT.MVD_PIXEL_MONO_08);
                }

                m_lMaskShapes.Clear();
                m_stROIShape = null;
                m_stResLineShape = null;
                _DrawOutlineList.Clear();
                _DrawCaliperBoxList.Clear();
            }
            catch (Exception)
            {

            }
        }

        public void LoadPara(string fileDlg)
        {
            FileStream fileStr = null;
            try
            {

                if (fileDlg != null)
                {
                    string filePath = fileDlg;
                    if (File.Exists(filePath))
                    {
                        fileStr = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                        Int64 nFileLen = fileStr.Length;
                        byte[] fileBytes = new byte[nFileLen];
                        uint nReadLen = Convert.ToUInt32(fileStr.Read(fileBytes, 0, fileBytes.Length));
                        fileStr.Close();
                        fileStr.Dispose();
                        m_stLineFindToolObj.LoadConfiguration(fileBytes, nReadLen);
                        UpdateParamList(fileBytes, nReadLen);
                    }

                }
            }
            catch (Exception)
            {

            }
            finally
            {
                if (null != fileStr)
                {
                    fileStr.Close();
                    fileStr.Dispose();
                }
            }
        }

        public void SavePara(string fileDlg)
        {
            FileStream fileStr = null;
            try
            {
                string filePath = fileDlg;
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                fileStr = new FileStream(filePath, FileMode.Create);

                /* Save parameters in local file as XML. */
                byte[] fileBytes = new byte[256];
                uint nConfigDataSize = 256;
                uint nConfigDataLen = 0;
                try
                {
                    m_stLineFindToolObj.SaveConfiguration(fileBytes, nConfigDataSize, ref nConfigDataLen);
                }
                catch (MvdException ex)
                {
                    if (MVD_ERROR_CODE.MVD_E_NOENOUGH_BUF == ex.ErrorCode)
                    {
                        fileBytes = new byte[nConfigDataLen];
                        nConfigDataSize = nConfigDataLen;
                        m_stLineFindToolObj.SaveConfiguration(fileBytes, nConfigDataSize, ref nConfigDataLen);
                    }
                    else
                    {
                        throw ex;
                    }
                }

                fileStr.Write(fileBytes, 0, Convert.ToInt32(nConfigDataLen));
                fileStr.Flush();
                fileStr.Close();
                fileStr.Dispose();
            }
            catch (Exception)
            {

            }
        }

        public void SetPara<T>(LineFindParas para, T Val)
        {
            try
            {
                String strName = Enum.GetName(typeof(LineFindParas), para);
                string strCurParamVal = null;
                for (int i = 0; i < m_stXmlParseToolObj.IntValueList.Count; ++i)
                {
                    if (strName == m_stXmlParseToolObj.IntValueList[i].Name)
                    {
                        strCurParamVal = Val.ToString();
                    }
                }
                for (int i = 0; i < m_stXmlParseToolObj.EnumValueList.Count; ++i)
                {
                    if (strName == m_stXmlParseToolObj.EnumValueList[i].Name)
                    {
                        String strEnumEntryName = Val.ToString();
                        strCurParamVal = strEnumEntryName;
                    }
                }

                double fStartTime = GetTimeStamp();
                m_stLineFindToolObj.SetRunParam(strName, strCurParamVal);
                double fCostTime = GetTimeStamp() - fStartTime;

            }
            catch (Exception)
            {

            }
        }


        public T GetPara<T>(LineFindParas para)
        {
            T value = default(T);
            try
            {
                String paramName = Enum.GetName(typeof(LineFindParas), para);
                GetParam<T>(paramName);

                return value;
            }
            catch
            {
                return default(T);
            }
        }


        public void AddROI(RectangleF Rect)
        {
            try
            {
                m_stROIShape = new CMvdRectangleF(Rect.X + Rect.Width / 2, Rect.Y + Rect.Height / 2, Rect.Width, Rect.Height);


            }
            catch (Exception)
            {

            }
        }

        public void ClearROI()
        {
            try
            {
                m_stROIShape = null;

                m_stResLineShape = null;

                _DrawOutlineList.Clear();
            }
            catch (Exception)
            {

            }
        }

        public LineResult Run()
        {
            try
            {
                LineResult result = new LineResult();

                if ((null == m_stLineFindToolObj) || (null == m_stInputImage))
                {
                    throw new MvdException(MVD_MODULE_TYPE.MVD_MODUL_TOOL, MVD_ERROR_CODE.MVD_E_CALLORDER);
                }

                if (null != m_stResLineShape)
                {
                    m_stResLineShape = null;
                }
                _DrawCaliperBoxList.Clear();
                _DrawOutlineList.Clear();

                m_stLineFindToolObj.InputImage = m_stInputImage;
                if (null == m_stROIShape)
                {
                    m_stLineFindToolObj.ROI = new VisionDesigner.CMvdRectangleF(m_stInputImage.Width / 2, m_stInputImage.Height / 2, m_stInputImage.Width, m_stInputImage.Height);
                }
                else
                {
                    m_stLineFindToolObj.ROI = m_stROIShape;
                }

                m_stLineFindToolObj.ClearMasks();
                foreach (var item in m_lMaskShapes)
                {
                    m_stLineFindToolObj.AddMask(item);
                }
                double fStartTime = GetTimeStamp();
                m_stLineFindToolObj.Run();
                double fCostTime = GetTimeStamp() - fStartTime;

                CLineFindResult stLineFindRes = m_stLineFindToolObj.Result;

                CMvdRectangleF Shape = m_stROIShape as CMvdRectangleF;
                ROI rect = new ROI(new RectangleF(Shape.LeftTopX, Shape.LeftTopY, Shape.Width, Shape.Height), true);


                if (1 == stLineFindRes.Status)
                {
                    PointF Startpoint = new PointF(stLineFindRes.LineStartPoint.fX, stLineFindRes.LineStartPoint.fY);
                    PointF Endpoint = new PointF(stLineFindRes.LineEndPoint.fX, stLineFindRes.LineEndPoint.fY);
                    float Angle = stLineFindRes.LineAngle;
                    float Straightness = stLineFindRes.LineStraightness;
                    int PointNum = stLineFindRes.EdgePointInfo.Count;

                    List<ScorePoint> Points = new List<ScorePoint>();
                    foreach (var point in stLineFindRes.EdgePointInfo)
                    {
                        if (0 == point.Score)
                        {
                            Points.Add(new ScorePoint(new PointF(point.EdgePoint.fX, point.EdgePoint.fY), 0));
                        }
                        else if (1 == point.Score)
                        {
                            Points.Add(new ScorePoint(new PointF(point.EdgePoint.fX, point.EdgePoint.fY), 1));
                        }
                        else if (2 == point.Score)
                        {
                            Points.Add(new ScorePoint(new PointF(point.EdgePoint.fX, point.EdgePoint.fY), 2));
                        }
                    }

                    result = new LineResult(Startpoint, Endpoint, rect, Angle, Straightness, PointNum, Points);

                }
                else
                {
                    result = new LineResult(PointF.Empty, PointF.Empty, rect, 0, 0, 0, null);
                }

                return result;
            }
            catch(Exception)
            {
                return null;
            }
        }


        #endregion

    }


    #endregion


    #region CircleFind

    #region CircleFind Private Filed

    #endregion

    class CircleFind
    {
        #region Private File

        Stopwatch _StopWatch = null;     // 计时工具
        CCircleFindTool _CircleFindTool = null;     // 圆查找工具
        CMvdXmlParseTool _XmlParseTool = null;     // 运行参数解析工具
        CMvdImage _InputImage = null;     // 输入图像
        CMvdShape _ROI = null;     // ROI图形
        List<CMvdShape> _MaskShapeList = null;     // 屏蔽区图形列表
        CMvdAnnularSectorF _DrawArc = null;     // 绘制圆弧
        List<CMvdLineSegmentF> _DrawOutlineList = null;     // 绘制轮廓
        List<CMvdRectangleF> _DrawCaliperBoxList = null;     // 绘制卡尺框
        bool _ImageLoaded = false;    // 图片正常加载

        public bool Inited = false;

        #endregion

        #region Private Method

        /// <summary>
        /// Update paramters
        /// </summary>
        /// <param name="bufXml"></param>
        /// <param name="nXmlLen"></param>
        private void UpdateParamList(Byte[] bufXml, uint nXmlLen)
        {
            if (null == _XmlParseTool)
            {
                _XmlParseTool = new CMvdXmlParseTool(bufXml, nXmlLen);
            }
            else
            {
                _XmlParseTool.UpdateXmlBuf(bufXml, nXmlLen);
            }
        }

        private double GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return ts.TotalMilliseconds;
        }

        private T GetParam<T>(string paraName)
        {
            if (null == _XmlParseTool)
            {
                return default(T);
            }
            else
            {
                var value = default(T);
                Type targetType = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);
                if (typeof(T) == typeof(int))
                {
                    for (int i = 0; i < _XmlParseTool.IntValueList.Count; ++i)
                    {
                        if (_XmlParseTool.IntValueList[i].Name == paraName)
                        {
                            value = (T)Convert.ChangeType(_XmlParseTool.IntValueList[i].CurValue, targetType);
                        }

                    }
                }

                if (typeof(T) == typeof(string))
                {
                    for (int i = 0; i < _XmlParseTool.EnumValueList.Count; ++i)
                    {
                        if (_XmlParseTool.EnumValueList[i].Name == paraName)
                        {
                            value = (T)Convert.ChangeType(_XmlParseTool.EnumValueList[i].CurValue.Name, targetType);
                        }
                    }
                }

                if (typeof(T) == typeof(float))
                {
                    for (int i = 0; i < _XmlParseTool.FloatValueList.Count; ++i)
                    {
                        if (_XmlParseTool.FloatValueList[i].Name == paraName)
                        {
                            value = (T)Convert.ChangeType(_XmlParseTool.FloatValueList[i].CurValue, targetType);
                        }
                    }
                }

                if (typeof(T) == typeof(bool))
                {
                    for (int i = 0; i < _XmlParseTool.BooleanValueList.Count; ++i)
                    {
                        if (_XmlParseTool.BooleanValueList[i].Name == paraName)
                        {
                            value = (T)Convert.ChangeType(_XmlParseTool.BooleanValueList[i].CurValue, targetType);
                        }
                    }
                }


                try
                {
                    return value;
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException($"Failed to convert to {typeof(T).Name}.", ex);
                }
            }
        }


        #endregion

        #region Method

        public bool Init()
        {
            try
            {
                this._StopWatch = new Stopwatch();
                this._MaskShapeList = new List<CMvdShape>();
                this._DrawOutlineList = new List<CMvdLineSegmentF>();
                this._DrawCaliperBoxList = new List<CMvdRectangleF>();

                _CircleFindTool = new CCircleFindTool();
                byte[] runParamConfigBytes = new byte[256];
                uint runParamConfigDataSize = 256;
                uint runParamConfigDataLength = 0;

                try
                {
                    _CircleFindTool.SaveConfiguration(runParamConfigBytes, runParamConfigDataSize, ref runParamConfigDataLength);
                }
                catch (MvdException ex)
                {
                    if (MVD_ERROR_CODE.MVD_E_NOENOUGH_BUF == ex.ErrorCode)
                    {
                        runParamConfigBytes = new byte[runParamConfigDataLength];
                        runParamConfigDataSize = runParamConfigDataLength;
                        _CircleFindTool.SaveConfiguration(runParamConfigBytes, runParamConfigDataSize, ref runParamConfigDataLength);
                    }
                    else
                    {
                        throw ex;
                    }
                }
                UpdateParamList(runParamConfigBytes, runParamConfigDataLength);
                Inited = true;
                return true;
            }
            catch (Exception)
            {
                Inited = false;
                return false;
            }
        }

        public void LoadImage(string fileDlg)
        {
            try
            {
                _ImageLoaded = false;
                if (fileDlg != null)
                {
                    if (null == _InputImage)
                    {
                        _InputImage = new CMvdImage();
                    }
                    _InputImage.InitImage(fileDlg);
                    if (MVD_PIXEL_FORMAT.MVD_PIXEL_RGB_RGB24_C3 == _InputImage.PixelFormat
                         || MVD_PIXEL_FORMAT.MVD_PIXEL_RGB_RGB24_P3 == _InputImage.PixelFormat)
                    {
                        _InputImage.ConvertImagePixelFormat(MVD_PIXEL_FORMAT.MVD_PIXEL_MONO_08);
                    }

                    _ROI = null;
                    _MaskShapeList.Clear();
                    _DrawArc = null;
                    _DrawOutlineList.Clear();
                    _DrawCaliperBoxList.Clear();


                    _ImageLoaded = true;
                }

                
            }
            catch (Exception)
            {

            }
        }

        public void LoadImage(Bitmap bitmap)
        {
            try
            {
                _ImageLoaded = false;
                if (null == _InputImage)
                {
                    _InputImage = new CMvdImage();
                }
                _InputImage = ImageBasicAlgorithms.BitmapToCMvdImage(bitmap);

                if (MVD_PIXEL_FORMAT.MVD_PIXEL_RGB_RGB24_C3 == _InputImage.PixelFormat
                         || MVD_PIXEL_FORMAT.MVD_PIXEL_RGB_RGB24_P3 == _InputImage.PixelFormat)
                {
                    _InputImage.ConvertImagePixelFormat(MVD_PIXEL_FORMAT.MVD_PIXEL_MONO_08);
                }

                _ROI = null;
                _MaskShapeList.Clear();
                _DrawArc = null;
                _DrawOutlineList.Clear();
                _DrawCaliperBoxList.Clear();


                _ImageLoaded = true;
            }
            catch (Exception)
            {

            }
        }

        public void LoadPara(string fileDlg)
        {
            FileStream fileStr = null;
            try
            {

                if (fileDlg != null)
                {
                    string filePath = fileDlg;
                    if (File.Exists(filePath))
                    {
                        byte[] runParamConfigBytes = null;
                        uint runParamConfigDataLength = 0;

                        using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                        {
                            runParamConfigBytes = new byte[fileStream.Length];
                            runParamConfigDataLength = Convert.ToUInt32(fileStream.Read(runParamConfigBytes, 0, runParamConfigBytes.Length));
                        }

                        _CircleFindTool.LoadConfiguration(runParamConfigBytes, runParamConfigDataLength);
                        UpdateParamList(runParamConfigBytes, runParamConfigDataLength);
                    }

                }
            }
            catch (Exception)
            {

            }
            finally
            {
                if (null != fileStr)
                {
                    fileStr.Close();
                    fileStr.Dispose();
                }
            }
        }

        public void SavePara(string fileDlg)
        {
            FileStream fileStream = null;
            try
            {
                string filePath = fileDlg;
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                fileStream = new FileStream(filePath, FileMode.Create);

                byte[] runParamConfigBytes = new byte[256];
                uint runParamConfigDataSize = 256;
                uint runParamConfigDataLength = 0;
                try
                {
                    _CircleFindTool.SaveConfiguration(runParamConfigBytes, runParamConfigDataSize, ref runParamConfigDataLength);
                }
                catch (MvdException ex)
                {
                    if (MVD_ERROR_CODE.MVD_E_NOENOUGH_BUF == ex.ErrorCode)
                    {
                        runParamConfigBytes = new byte[runParamConfigDataLength];
                        runParamConfigDataSize = runParamConfigDataLength;
                        _CircleFindTool.SaveConfiguration(runParamConfigBytes, runParamConfigDataSize, ref runParamConfigDataLength);
                    }
                    else
                    {
                        throw ex;
                    }
                }

                fileStream.Write(runParamConfigBytes, 0, Convert.ToInt32(runParamConfigDataLength));
                fileStream.Flush();

                fileStream.Close();
                fileStream.Dispose();
            }
            catch (Exception)
            {

            }
        }

        public void SetPara<T>(CircleFindParas para, T Val)
        {
            try
            {
                String paramName = Enum.GetName(typeof(CircleFindParas), para);
                String paramValue = String.Empty;

                for (int i = 0; i < _XmlParseTool.IntValueList.Count; ++i)
                {
                    if (paramName == _XmlParseTool.IntValueList[i].Name)
                    {
                        paramValue = Val.ToString();
                    }
                }
                for (int i = 0; i < _XmlParseTool.EnumValueList.Count; ++i)
                {
                    if (paramName == _XmlParseTool.EnumValueList[i].Name)
                    {
                        paramValue = Val.ToString();
                    }
                }

                _StopWatch.Restart();
                _CircleFindTool.SetRunParam(paramName, paramValue);
                _StopWatch.Stop();

            }
            catch (Exception)
            {

            }
        }

        public T GetPara<T>(CircleFindParas para)
        {
            T value = default(T);
            try
            {
                String paramName = Enum.GetName(typeof(LineFindParas), para);
                GetParam<T>(paramName);

                return value;
            }
            catch
            {
                return default(T);
            }
        }


        public void AddROI(RectangleF Rect)
        {
            try
            {
                _ROI = new CMvdRectangleF(Rect.X + Rect.Width / 2, Rect.Y + Rect.Height / 2, Rect.Width, Rect.Height);


            }
            catch (Exception)
            {

            }
        }

        public void ClearROI()
        {
            try
            {
                _ROI = null;

                _DrawOutlineList.Clear();
            }
            catch (Exception)
            {

            }
        }

        public CircleResult Run()
        {
            try
            {
                CircleResult result = new CircleResult();

                if ((null == _CircleFindTool) || (null == _InputImage))
                {
                    throw new MvdException(MVD_MODULE_TYPE.MVD_MODUL_APP, MVD_ERROR_CODE.MVD_E_CALLORDER);
                }

                _DrawCaliperBoxList.Clear();
                _DrawOutlineList.Clear();

                _CircleFindTool.InputImage = _InputImage;
                _CircleFindTool.ROI = _ROI;

                _CircleFindTool.ClearMasks();
                foreach (var item in _MaskShapeList)
                {
                    _CircleFindTool.AddMask(item);
                }

                _StopWatch.Restart();
                _CircleFindTool.Run();
                _StopWatch.Stop();

                CCircleFindResult circleFindResult = _CircleFindTool.Result;

                CMvdRectangleF Shape = _ROI as CMvdRectangleF;
                ROI rect = new ROI(new RectangleF(Shape.LeftTopX, Shape.LeftTopY, Shape.Width, Shape.Height), true);


                if (1 == circleFindResult.Status)
                {
                    PointF CircleCenter = new PointF(circleFindResult.Circle.Center.fX, circleFindResult.Circle.Center.fY);
                    float CircleRadius = circleFindResult.Circle.Radius;

                    PointF ArcCenter = new PointF(circleFindResult.Arc.Center.fX, circleFindResult.Arc.Center.fY);
                    float ArcOuterRadius = circleFindResult.Arc.OuterRadius;
                    float ArcStartAngle = circleFindResult.Arc.StartAngle;
                    float ArcAngleRange = circleFindResult.Arc.AngleRange;

                    float Circleness = circleFindResult.Circleness;

                    int PointNum = circleFindResult.EdgePointInfo.Count;

                    List<ScorePoint> Points = new List<ScorePoint>();
                    foreach (var point in circleFindResult.EdgePointInfo)
                    {
                        if (0 == point.Score)
                        {
                            Points.Add(new ScorePoint(new PointF(point.EdgePoint.fX, point.EdgePoint.fY), 0));
                        }
                        else if (1 == point.Score)
                        {
                            Points.Add(new ScorePoint(new PointF(point.EdgePoint.fX, point.EdgePoint.fY), 1));
                        }
                        else if (2 == point.Score)
                        {
                            Points.Add(new ScorePoint(new PointF(point.EdgePoint.fX, point.EdgePoint.fY), 2));
                        }
                    }

                    result = new CircleResult(CircleCenter, CircleRadius, ArcCenter, ArcOuterRadius, ArcStartAngle,ArcAngleRange,Circleness,rect, PointNum, Points);

                }
                else
                {
                    result = new CircleResult(PointF.Empty, 0, PointF.Empty, 0, 0, 0, 0, rect, 0, null);
                }

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }


        #endregion

    }


    #endregion

}
