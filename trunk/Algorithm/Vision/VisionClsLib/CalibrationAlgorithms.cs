using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisionClsLib
{
    /*
     * PPThera 逆时针旋转角度为正
     *  
     * 
     * */


    public struct PPXYDeviationPara
    {
        public PointF Center;
        public float Radius;
        public float InitialAngle;

        public PPXYDeviationPara(PointF Center, float Radius, float InitialAngle)
        {
            this.Center = Center;
            this.Radius = Radius;
            this.InitialAngle = InitialAngle;
        }
    }

    public class CalibrationAlgorithms
    {
        public CalibrationAlgorithms()
        {

        }


        #region Private Method

        static (double, double) RotatePoint(double x, double y, double thetaDegrees)
        {
            double thetaRadians = thetaDegrees * Math.PI / 180.0;

            double xRotated = x * Math.Cos(thetaRadians) - y * Math.Sin(thetaRadians);
            double yRotated = x * Math.Sin(thetaRadians) + y * Math.Cos(thetaRadians);

            return (xRotated, yRotated);
        }
        static float Distance(PointF point1, PointF point2)
        {
            return (float)Math.Sqrt(Math.Pow(point1.X - point2.X, 2) + Math.Pow(point1.Y - point2.Y, 2));
        }

        PointF CirclePoint(float angleInDegrees, PointF center, float radius)
        {
            try
            {
                double angleInRadians = angleInDegrees * (Math.PI / 180);

                float x = center.X + (float)(radius * Math.Cos(angleInRadians));
                float y = center.Y + (float)(radius * Math.Sin(angleInRadians));

                return new PointF(x, y);
            }
            catch
            {
                return new PointF();
            }
        }

        #endregion

        #region 吸嘴校准

        #region file

        public PointF Center = new PointF();
        public float Radius = 0;
        public float InitialAngle = 0;


        #endregion



        #region Method

        /// <summary>
        /// 吸嘴校准参数初始化
        /// </summary>
        /// <param name="Center"></param>
        /// <param name="Radius"></param>
        /// <param name="InitialAngle"></param>
        /// <returns></returns>
        public bool PPRotateXYDeviationParamInit(PointF Center, float Radius, float InitialAngle)
        {
            try
            {
                this.Center = Center;
                this.Radius = Radius;
                this.InitialAngle = InitialAngle;

                return true;
            }
            catch
            {
                return false;
            }
            
        }

        /// <summary>
        /// 吸嘴旋转XY补偿参数计算
        /// </summary>
        /// <param name="Point1">角度0对应坐标</param>
        /// <param name="Point2">角度180对应坐标</param>
        /// <param name="Angle1">0度</param>
        /// <param name="Angle2">180度</param>
        /// <returns>吸嘴校准参数</returns>
        public PPXYDeviationPara PPRotateXYDeviationParamCal(PointF Point1, PointF Point2, float Angle1=0, float Angle2=180)
        {
            PPXYDeviationPara Para = new PPXYDeviationPara();
            try
            {
                if ((Angle2 - Angle1) == 180)
                {
                    PointF center = new PointF((Point1.X + Point2.X) / 2, (Point1.Y + Point2.Y) / 2);
                    float radius = Distance(Point1, center);
                    float angle = (float)(Math.Atan2(Point2.Y - Point1.Y, Point2.X - Point1.X) * (180 / Math.PI)) + 180;
                    if (angle > 180)
                    {
                        angle -= 360;
                    }
                    else if (angle < -180)
                    {
                        angle += 360;
                    }

                    angle = angle - Angle1;

                    Para = new PPXYDeviationPara(center, radius, angle);

                    Center = center;
                    Radius = radius;
                    InitialAngle = angle;
                }

                return Para;
            }
            catch
            {
                return new PPXYDeviationPara();
            }
        }

        /// <summary>
        /// 吸嘴旋转补偿
        /// </summary>
        /// <param name="BeforeAngle">旋转前角度</param>
        /// <param name="TargetAngle">旋转后角度</param>
        /// <returns>PointF.X 向右偏移 PointF.Y 向上偏移</returns>
        public PointF PPXYDeviationCal(float BeforeAngle, float TargetAngle)
        {
            PointF Compensationvalue = new PointF();
            try
            {
                BeforeAngle = BeforeAngle + InitialAngle;
                TargetAngle = TargetAngle + InitialAngle;
                PointF point1 = CirclePoint(BeforeAngle, Center, Radius);
                PointF point2 = CirclePoint(TargetAngle, Center, Radius);

                Compensationvalue = new PointF(-point2.X + point1.X, point2.Y - point1.Y);

                return Compensationvalue;
            }
            catch
            {
                return new PointF();
            }
        }

        #endregion

        #endregion

    }
}
