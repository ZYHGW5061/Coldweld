using GlobalDataDefineClsLib;
using PositioningSystemClsLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalibrationSystemClsLib
{
    /// <summary>
    /// 校准相机角度过程中的自定义回调
    /// </summary>
    public class CameraAngleActionCallback : ACustomActionBaseClass
    {
        ///// <summary>
        /////  拍照得到的图像
        ///// </summary>
        //public ICogImage ImageCaptured { get; set; }

        ///// <summary>
        ///// 识别到的最好结果
        ///// </summary>
        //public CogPMAlignResult BestResultRecognized { get; set; }
        public Bitmap ImageCaptured { get; set; }
    }
}
