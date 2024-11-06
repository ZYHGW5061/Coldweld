using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathHelperClsLib
{
    public class RectangleComputer
    {
        /// <summary>
        /// 根据角坐标获取中心坐标
        /// </summary>
        /// <param name="leftUpper"></param>
        /// <param name="rightUpper"></param>
        /// <param name="rightLower"></param>
        /// <param name="leftLower"></param>
        /// <param name="center"></param>
        /// <param name="size"></param>
        /// <param name="angle"></param>
        public static void GetRectangleCenterByCornerCoordinates(PointF leftUpper, PointF rightUpper, PointF rightLower, PointF leftLower, out PointF center, out PointF size, out float angle)
        {

            var centerX = (leftUpper.X + rightUpper.X + rightLower.X + leftLower.X) / 4; // 计算中心点的 x 坐标
            var centerY = (leftUpper.Y + rightUpper.Y + rightLower.Y + leftLower.Y) / 4; // 计算中心点的 y 坐标
            center = new PointF(centerX, centerY);

            double sideWidth = Math.Sqrt(Math.Pow(rightUpper.X - leftUpper.X, 2) + Math.Pow(rightUpper.Y - leftUpper.Y, 2)); 
            double sideHeight = Math.Sqrt(Math.Pow(rightLower.X - rightUpper.X, 2) + Math.Pow(rightLower.Y - rightUpper.Y, 2));
            size = new PointF((float)sideWidth, (float)sideHeight);
            // 计算旋转角度
            double dx1 = rightUpper.X - leftUpper.X;
            double dy1 = rightUpper.Y - leftUpper.Y;
            double dx2 = rightLower.X - rightUpper.X;
            double dy2 = rightLower.Y - rightUpper.Y;
            double angle1 = Math.Atan2(dy1, dx1);
            double angle2 = Math.Atan2(dy2, dx2);
            //angle = (float)(angle2 - angle1);
            angle = (float)angle1;
        }
        /// <summary>
        /// 根据另外三个角坐标获取左下角坐标
        /// </summary>
        /// <param name="leftUpper"></param>
        /// <param name="rightUpper"></param>
        /// <param name="rightLower"></param>
        /// <param name="leftLower"></param>
        public static void GetLeftLowerCoorByOtherCornerCoordinates(PointF leftUpper, PointF rightUpper, PointF rightLower, out PointF leftLower)
        {

            // 计算第四个角的坐标
            double centerX = (leftUpper.X + rightUpper.X + rightLower.X) / 3;
            double centerY = (leftUpper.Y + rightUpper.Y + rightLower.Y) / 3;

            // 计算旋转角度
            double dx1 = rightUpper.X - leftUpper.X;
            double dy1 = rightUpper.Y - leftUpper.Y;
            double dx2 = rightLower.X - rightUpper.X;
            double dy2 = rightLower.Y - rightUpper.Y;
            double angle1 = Math.Atan2(dy1, dx1);
            double angle2 = Math.Atan2(dy2, dx2);
            var rotationAngle = (float)(angle2 - angle1);

            double cosTheta = Math.Cos(rotationAngle);
            double sinTheta = Math.Sin(rotationAngle);

            var x4 = centerX + cosTheta * (rightLower.X - centerX) - sinTheta * (rightLower.Y - centerY);
            var y4 = centerY + sinTheta * (rightLower.X - centerX) + cosTheta * (rightLower.Y - centerY);
            leftLower = new PointF((float)x4, (float)y4);
        }
        /// <summary>
        /// 根据边上的点获取矩形中心坐标
        /// </summary>
        /// <param name="leftPoint1"></param>
        /// <param name="leftPoint2"></param>
        /// <param name="upPoint1"></param>
        /// <param name="upPoint2"></param>
        /// <param name="rightPoint1"></param>
        /// <param name="rightPoint2"></param>
        /// <param name="downPoint1"></param>
        /// <param name="downPoint2"></param>
        /// <param name="center"></param>
        /// <param name="size"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static void GetRectangleCenterByEdgePoints(PointF leftPoint1, PointF leftPoint2, PointF upPoint1, PointF upPoint2, PointF rightPoint1, PointF rightPoint2
            , PointF downPoint1,PointF downPoint2, out PointF center, out PointF size, out float angle)
        {
            //计算每条边的直线方程，再计算四个交点，再用上面的公式计算
            center = new PointF();

            size = new PointF();

            angle = 0f;

        }
    }
}
