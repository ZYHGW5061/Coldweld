using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathHelperClsLib
{
    public class CircleComputer
    {
        /// <summary>
        /// 根据三个圆边缘点坐标计算圆心和圆直径
        /// </summary>
        /// <param name="firstPoint"></param>
        /// <param name="secondPoint"></param>
        /// <param name="thirdPoint"></param>
        /// <param name="center">中心坐标</param>
        /// <param name="diameter">直径</param>
        public static void GetCircleCenterByThreeEdgePointsCoordinate(PointF firstPoint, PointF secondPoint, PointF thirdPoint, out PointF center, out float diameter)
        {

            diameter = 0f;
            var abEdgeLength = Math.Sqrt(Math.Pow(secondPoint.X - firstPoint.X, 2) + Math.Pow(secondPoint.Y - firstPoint.Y, 2));
            var bcEdgeLength = Math.Sqrt(Math.Pow(thirdPoint.X - secondPoint.X, 2) + Math.Pow(thirdPoint.Y - secondPoint.Y, 2));
            var acEdgeLength = Math.Sqrt(Math.Pow(firstPoint.X - thirdPoint.X, 2) + Math.Pow(firstPoint.Y - thirdPoint.Y, 2));
            //海伦公式
            var s = (abEdgeLength + bcEdgeLength + acEdgeLength) / 2;
            var S = Math.Sqrt(s * (s - abEdgeLength) * (s - bcEdgeLength) * (s - acEdgeLength));

            diameter = (float)(abEdgeLength * bcEdgeLength * acEdgeLength / (4 * S));
            //三角形的垂心坐标公式
            var A = Math.Pow(bcEdgeLength, 2) * (Math.Pow(acEdgeLength, 2) + Math.Pow(abEdgeLength, 2) - Math.Pow(bcEdgeLength, 2));
            var B = Math.Pow(acEdgeLength, 2) * (Math.Pow(bcEdgeLength, 2) + Math.Pow(abEdgeLength, 2) - Math.Pow(acEdgeLength, 2));
            var C = Math.Pow(abEdgeLength, 2) * (Math.Pow(acEdgeLength, 2) + Math.Pow(abEdgeLength, 2) - Math.Pow(abEdgeLength, 2));

            var x = (A * firstPoint.X + B * secondPoint.X + C * thirdPoint.X) / (A + B + C);
            var y = (A * firstPoint.Y + B * secondPoint.Y + C * thirdPoint.Y) / (A + B + C);
            center = new PointF((float)x, (float)y);
        }
    }
}
