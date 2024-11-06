using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisionGUI
{
    public static class CameraParamBox
    {

        public static (double, double, double, bool) FormShow(string text3 = "Bond相机参数设置", double ImageWidthPixelsize = 1, double ImageHeightPixelsize = 1, double Angle = 0)
        {
            try
            {
                double imageWidthPixelsize = ImageWidthPixelsize;
                double imageHeightPixelsize = ImageHeightPixelsize;
                double angle = Angle;
                bool confirm = true;
                using (CameraParamForm myMessageBox1 = new CameraParamForm())
                {
                    (imageWidthPixelsize, imageHeightPixelsize, angle, confirm) = myMessageBox1.showMessage(text3, ImageWidthPixelsize, ImageHeightPixelsize, Angle);
                }
                return (imageWidthPixelsize, imageHeightPixelsize, angle, confirm);
            }
            catch { return (1, 1, 0, false); }

        }
    }
}
