
using CameraControllerClsLib;
using ConfigurationClsLib;
using GlobalDataDefineClsLib;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisionGUI
{
    public enum Graphic
    {
        cross,
        grid,
        train,
        match,
        line,
        circle,
        rectRoi,
        circleRoi,
        array,
    }

    public enum ImageDirection
    {
        X,
        Y,
    }

    public partial class CameraWindowGUI : UserControl
    {
        private static readonly object _lockObj = new object();
        private static volatile CameraWindowGUI _instance = null;
        public static CameraWindowGUI Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new CameraWindowGUI();
                        }
                    }
                }
                return _instance;
            }
        }

        private readonly float _aspectRatio;

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            CameraControltoolStrip.Width = this.Width;

            int width = (int)(((float)this.Width) / 1200.0f * 280.0f);
            CameraComboBox.Size = new Size(width, 60);

            int width1 = (int)(((float)this.Width) / 900.0f * 100.0f);
            CrossBtn.Size = new Size(width1, 60);
            GridBtn.Size = new Size(width1, 60);
            ClearBtn.Size = new Size(width1, 60);

            toolStripPixelCoordinates.Padding = new Padding(-1, this.Width-240, 0, 0);

            // 获取用户控件的宽高比例
            float userControlAspectRatio = (float)this.Width / this.Height;

            // 获取CameraWindow的宽高比例
            float cameraWindowAspectRatio = (float)this.CameraWindow.Width / this.CameraWindow.Height;

            if (userControlAspectRatio > cameraWindowAspectRatio)
            {
                // 用户控件更宽，CameraWindow按高来缩放
                int newHeight = this.Height - 68; // 减去顶部工具条的高度
                int newWidth = (int)(newHeight * cameraWindowAspectRatio);

                this.CameraWindow.Size = new Size(newWidth, newHeight);
            }
            else
            {
                // 用户控件更高，CameraWindow按宽来缩放
                int newWidth = this.Width;
                int newHeight = (int)(newWidth / cameraWindowAspectRatio);
                this.CameraWindow.Size = new Size(newWidth, newHeight);
            }

            // 居中显示CameraWindow
            this.CameraWindow.Location = new Point(
                (this.Width - this.CameraWindow.Width) / 2,
                68 // 距离顶部工具条下方68个像素
            );
        }


        private SystemConfiguration _systemConfig
        {
            get { return SystemConfiguration.Instance; }
        }



        private CameraWindowGUI()
        {
            InitializeComponent();

            //_systemConfig.SystemGUIType.CameraWindowWidth = 900;
            //_systemConfig.SystemGUIType.CameraWindowHeight = 680;
            //_systemConfig.SaveConfig();

            int CameraWindowWidth = _systemConfig.SystemGUIType.CameraWindowWidth;
            int CameraWindowHeight = _systemConfig.SystemGUIType.CameraWindowHeight;

            if (CameraWindowWidth < 1)
            {
                CameraWindowWidth = 900;
            }
            if (CameraWindowHeight < 1)
            {
                CameraWindowHeight = 680;
            }

            _aspectRatio = (float)CameraWindowWidth/(float)CameraWindowHeight;


            this.CameraWindow.Size = new System.Drawing.Size(CameraWindowWidth, CameraWindowHeight);
            this.CameraWindow.Location = new System.Drawing.Point(4, 50);

            this.Size = new System.Drawing.Size(CameraWindowWidth + 5, CameraWindowHeight + 135);

            CameraWindow.SizeMode = PictureBoxSizeMode.Zoom;

            ImageWidth = 3072;
            ImageHeight = 2048;

            Scale = 1;

            Scale = _systemConfig.SystemGUIType.CameraWindowImageScale;

            WindowWidth = CameraWindow.Width;
            WindowHeight = CameraWindow.Height;
            ScaleWidth = ImageWidth / WindowWidth;
            ScaleHeight = ImageHeight / WindowHeight;

            rects.Add(new RectangleF(ImageWidth / 3, ImageHeight / 3, ImageWidth / 3, ImageHeight / 3));

            circleroi = new RectangleF(ImageWidth / 3, ImageHeight / 3, ImageWidth / 3, ImageHeight / 3);

            matchdeviation = new PointF(0, 0);

            CameraComboBox.Items.Clear();

            CameraComboBox.Items.Add("搬送相机");
            CameraComboBox.Items.Add("焊台相机");

            toolStripPixelCoordinates.Text = "(X:0,Y:0)(0)";

            this.Resize += new EventHandler(this.CameraWindowGUI_Resize);

            //InitVisualControl();
        }



        public CameraWindowGUI(int WindowWidth, int WindowHeight)
        {
            InitializeComponent();

            

            this.CameraWindow.Size = new System.Drawing.Size(WindowWidth, WindowHeight);
            this.CameraWindow.Location = new System.Drawing.Point(4, 4);

            this.Size = new System.Drawing.Size(WindowWidth + 5, WindowHeight + 5);

            CameraWindow.SizeMode = PictureBoxSizeMode.Zoom;

            ImageWidth = 3072;
            ImageHeight = 2048;

            Scale = 1;

            Scale = _systemConfig.SystemGUIType.CameraWindowImageScale;

            WindowWidth = CameraWindow.Width;
            WindowHeight = CameraWindow.Height;
            ScaleWidth = ImageWidth / WindowWidth;
            ScaleHeight = ImageHeight / WindowHeight;

            rects.Add(new RectangleF(ImageWidth / 3, ImageHeight / 3, ImageWidth / 3, ImageHeight / 3));

            circleroi = new RectangleF(ImageWidth / 3, ImageHeight / 3, ImageWidth / 3, ImageHeight / 3);



            CameraComboBox.Items.Clear();

            CameraComboBox.Items.Add("搬送相机");
            CameraComboBox.Items.Add("焊台相机");

            this.Resize += new EventHandler(this.CameraWindowGUI_Resize);

            //InitVisualControl();

        }

        private void CameraWindowGUI_Resize(object sender, EventArgs e)
        {
            ResizeCameraWindow();
        }


        private void ResizeCameraWindow()
        {
            // 获取用户控件的宽高比例
            float userControlAspectRatio = (float)this.Width / this.Height;

            // 获取CameraWindow的宽高比例
            float cameraWindowAspectRatio = (float)this.CameraWindow.Width / this.CameraWindow.Height;

            if (userControlAspectRatio > cameraWindowAspectRatio)
            {
                // 用户控件更宽，CameraWindow按高来缩放
                int newHeight = this.Height - 68; // 减去顶部工具条的高度
                int newWidth = (int)(newHeight * cameraWindowAspectRatio);

                this.CameraWindow.Size = new Size(newWidth, newHeight);
            }
            else
            {
                // 用户控件更高，CameraWindow按宽来缩放
                int newWidth = this.Width;
                int newHeight = (int)(newWidth / cameraWindowAspectRatio);
                this.CameraWindow.Size = new Size(newWidth, newHeight);
            }

            // 居中显示CameraWindow
            this.CameraWindow.Location = new Point(
                (this.Width - this.CameraWindow.Width) / 2,
                (this.Height - this.CameraWindow.Height) / 2 + 68 // 距离顶部工具条下方68个像素
            );
        }


        #region 绘图


        #region Private File


        public float Scale { get; set; } = 1;
        public float ImageWidth { get; set; } = 0;
        public float ImageHeight { get; set; } = 0;
        public float WindowWidth { get; set; } = 0;
        public float WindowHeight { get; set; } = 0;
        public float ScaleWidth { get; set; } = 1;//   像素/控件
        public float ScaleHeight { get; set; } = 1;

        float Crosslength;

        //窗口字体颜色
        Font Strfont = new Font("Arial", 12, FontStyle.Bold);
        Brush Strbrush = new SolidBrush(Color.White);//字体
        Brush StrFillbrush = new SolidBrush(Color.Black);//背景
        PointF Strpoint = new PointF(10, 10);

        //窗口点颜色
        Brush Pointbrush1 = Brushes.Green;
        Brush Pointbrush2 = Brushes.Yellow;
        Brush Pointbrush3 = Brushes.Red;

        //窗口线颜色
        Pen Linepen = new Pen(Color.White, 2);

        //窗口基点十字长度
        int CrossLinelength = 10;

        //窗口圆颜色
        Pen Circlepen = new Pen(Color.White, 2);


        //PointF point = new PointF();



        List<ScorePoint> trains = new List<ScorePoint>();
        bool trainshow = false;
        PointF matchcenter = new PointF(0, 0);//模型原点
        float matchangle = 0.0f;//模型角度
        PointF matchdeviation = new PointF(0, 0);//模型偏移

        List<MatchResult> matches = new List<MatchResult>();
        bool matchshow = false;
        int matchnum = 0;


        List<LineResult> lines = new List<LineResult>();
        bool lineshow = false;

        List<CircleResult> circles = new List<CircleResult>();
        bool circleshow = false;

        bool Crossshow = false;

        bool Gridshow = false;

        List<RectangleF> rects = new List<RectangleF>();
        bool rectshow = false;

        RectangleF circleroi = new RectangleF();
        bool circleroishow = false;

        List<ScorePoint> materials = new List<ScorePoint>();
        bool materialshow = false;

        #endregion

        #region Private Method


        static (double m, double b) CalculateLineEquation(PointF p1, PointF p2)
        {
            double deltaX = p2.X - p1.X;
            double deltaY = p2.Y - p1.Y;

            if (deltaX == 0)
            {
                throw new InvalidOperationException("Line is vertical, cannot use y = mx + b form");
            }

            double m = deltaY / deltaX;
            double b = p1.Y - m * p1.X;

            return (m, b);
        }

        static PointF FindIntersection((double m1, double b1) line1, (double m2, double b2) line2)
        {
            if (line1.m1 == line2.m2)
            {
                throw new InvalidOperationException("Lines are parallel or coincide");
            }

            double x = (line2.b2 - line1.b1) / (line1.m1 - line2.m2);
            double y = line1.m1 * x + line1.b1;

            return new PointF((float)x, (float)y);
        }


        private static void CrossPoint(PointF point, float length, ref Line line1, ref Line line2)
        {
            line1.point1 = new PointF(point.X - length, point.Y);
            line1.point2 = new PointF(point.X + length, point.Y);
            line2.point1 = new PointF(point.X, point.Y - length);
            line2.point2 = new PointF(point.X, point.Y + length);
        }



        private static PointF RotatePoint(PointF point, float centerX, float centerY, double angleRadians)
        {

            double b = (angleRadians * (Math.PI)) / 180;

            float dx = point.X - centerX;
            float dy = point.Y - centerY;

            float xNew = (float)(dx * Math.Cos(b) - dy * Math.Sin(b));
            float yNew = (float)(dx * Math.Sin(b) + dy * Math.Cos(b));

            return new PointF(xNew + centerX, yNew + centerY);
        }
        private static PointF[] CalculateRotatedRectanglePoints(float centerX, float centerY, float width, float height, float angle)
        {
            //double angleRadians = angle * Math.PI / 180.0;

            float halfWidth = width / 2;
            float halfHeight = height / 2;

            PointF[] points = new PointF[4];
            points[0] = new PointF(centerX - halfWidth, centerY - halfHeight); // 左上角
            points[1] = new PointF(centerX + halfWidth, centerY - halfHeight); // 右上角
            points[2] = new PointF(centerX + halfWidth, centerY + halfHeight); // 右下角
            points[3] = new PointF(centerX - halfWidth, centerY + halfHeight); // 左下角

            for (int i = 0; i < 4; i++)
            {
                points[i] = RotatePoint(points[i], centerX, centerY, angle);
            }

            return points;
        }

        private void CameraWindow_Paint(object sender, PaintEventArgs e)
        {
            WindowWidth = CameraWindow.Width;
            WindowHeight = CameraWindow.Height;
            //if (CameraWindow.Image != null)
            //{
            //    ImageWidth = CameraWindow.Image.Width;
            //    ImageHeight = CameraWindow.Image.Height;
            //}
            ScaleWidth = ImageWidth / WindowWidth;
            ScaleHeight = ImageHeight / WindowHeight;

            Linepen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;

            //Task.Factory.StartNew(new Action(() =>
            //{
            try
            {

                if (trainshow)
                {
                    if (trains != null)
                    {
                        if (trains.Count > 0)
                        {
                            string text = "OK";

                            SizeF textSize = e.Graphics.MeasureString(text, Strfont);
                            e.Graphics.FillRectangle(Brushes.Black, new RectangleF(Strpoint, textSize));

                            e.Graphics.DrawString(text, Strfont, Strbrush, Strpoint);



                            foreach (ScorePoint pt in trains)
                            {
                                PointF point0 = RotatePoint(pt.Point, matchcenter.X, matchcenter.Y, matchangle);
                                if (pt.Score == 0)
                                {
                                    e.Graphics.FillEllipse(Pointbrush1, (point0.X + matchdeviation.X) / ScaleWidth - 1, (point0.Y + matchdeviation.Y) / ScaleHeight - 1, 2, 2);
                                }
                                if (pt.Score == 1)
                                {
                                    e.Graphics.FillEllipse(Pointbrush2, (point0.X + matchdeviation.X) / ScaleWidth - 1, (point0.Y + matchdeviation.Y) / ScaleHeight - 1, 2, 2);
                                }
                                if (pt.Score == 2)
                                {
                                    e.Graphics.FillEllipse(Pointbrush3, (point0.X + matchdeviation.X) / ScaleWidth - 1, (point0.Y + matchdeviation.Y) / ScaleHeight - 1, 2, 2);
                                }
                            }
                        }
                        else
                        {
                            string text = "NG";

                            SizeF textSize = e.Graphics.MeasureString(text, Strfont);
                            e.Graphics.FillRectangle(Brushes.Black, new RectangleF(Strpoint, textSize));

                            e.Graphics.DrawString(text, Strfont, Strbrush, Strpoint);
                        }
                    }
                    else
                    {
                        string text = "NG";
                    }

                }

                if (matchshow)
                {
                    MatchResult match = matches[matchnum];
                    if (match != null)
                    {
                        if (match.IsOk)
                        {
                            string text = "OK\n"
                           + "Center:(" + match.MatchBox.Center.X.ToString("0.00") + " , " + match.MatchBox.Center.Y.ToString("0.00") + ")\n"
                           + "Benchmark:(" + match.MatchBox.Benchmark.X.ToString("0.00") + " , " + match.MatchBox.Benchmark.Y.ToString("0.00") + ")\n"
                           + "Angle:" + match.MatchBox.Angle.ToString("0.00") + "\n"
                           + "Score:" + match.Score.ToString("0.00") + "\n";

                            SizeF textSize = e.Graphics.MeasureString(text, Strfont);
                            e.Graphics.FillRectangle(Brushes.Black, new RectangleF(Strpoint, textSize));

                            e.Graphics.DrawString(text, Strfont, Strbrush, Strpoint);

                            e.Graphics.FillEllipse(Pointbrush3, match.MatchBox.Benchmark.X / ScaleWidth - 2, match.MatchBox.Benchmark.Y / ScaleHeight - 2, 4, 4);

                            ///田字
                            PointF[] points1 = CalculateRotatedRectanglePoints(match.MatchBox.Center.X / ScaleWidth, match.MatchBox.Center.Y / ScaleHeight, CrossLinelength, CrossLinelength, match.MatchBox.Angle);
                            Line Linesz1 = new Line();
                            Line Linesz2 = new Line();
                            CrossPoint(new PointF(match.MatchBox.Benchmark.X / ScaleWidth, match.MatchBox.Benchmark.Y / ScaleHeight), CrossLinelength, ref Linesz1, ref Linesz2);
                            e.Graphics.DrawLine(Linepen, points1[0], points1[1]);
                            e.Graphics.DrawLine(Linepen, points1[1], points1[2]);
                            e.Graphics.DrawLine(Linepen, points1[2], points1[3]);
                            e.Graphics.DrawLine(Linepen, points1[3], points1[0]);
                            e.Graphics.DrawLine(Linepen, Linesz1.point1, Linesz1.point2);
                            e.Graphics.DrawLine(Linepen, Linesz2.point1, Linesz2.point2);


                            PointF[] roipoints = CalculateRotatedRectanglePoints((match.SearchBox.Shape.X + match.SearchBox.Shape.Width / 2) / ScaleWidth, (match.SearchBox.Shape.Y + match.SearchBox.Shape.Height / 2) / ScaleHeight, match.SearchBox.Shape.Width / ScaleWidth, match.SearchBox.Shape.Height / ScaleHeight, 0);
                            e.Graphics.DrawLine(Linepen, roipoints[0], roipoints[1]);
                            e.Graphics.DrawLine(Linepen, roipoints[1], roipoints[2]);
                            e.Graphics.DrawLine(Linepen, roipoints[2], roipoints[3]);
                            e.Graphics.DrawLine(Linepen, roipoints[3], roipoints[0]);


                            PointF[] points = CalculateRotatedRectanglePoints(match.MatchBox.Center.X / ScaleWidth, match.MatchBox.Center.Y / ScaleHeight, match.MatchBox.Width / ScaleWidth, match.MatchBox.Height / ScaleHeight, match.MatchBox.Angle);
                            e.Graphics.DrawLine(Linepen, points[0], points[1]);
                            e.Graphics.DrawLine(Linepen, points[1], points[2]);
                            e.Graphics.DrawLine(Linepen, points[2], points[3]);
                            e.Graphics.DrawLine(Linepen, points[3], points[0]);

                            foreach (ScorePoint pt in match.Points)
                            {
                                if (pt.Score == 0)
                                {
                                    e.Graphics.FillEllipse(Pointbrush1, pt.Point.X / ScaleWidth - 1, pt.Point.Y / ScaleHeight - 1, 2, 2);
                                }
                                if (pt.Score == 1)
                                {
                                    e.Graphics.FillEllipse(Pointbrush2, pt.Point.X / ScaleWidth - 1, pt.Point.Y / ScaleHeight - 1, 2, 2);
                                }
                                if (pt.Score == 2)
                                {
                                    e.Graphics.FillEllipse(Pointbrush3, pt.Point.X / ScaleWidth - 1, pt.Point.Y / ScaleHeight - 1, 2, 2);
                                }
                            }
                        }
                        else
                        {
                            string text = "NG\n"
                                + "Score:" + match.Score.ToString("0.00") + "\n";

                            SizeF textSize = e.Graphics.MeasureString(text, Strfont);
                            e.Graphics.FillRectangle(Brushes.Black, new RectangleF(Strpoint, textSize));

                            e.Graphics.DrawString(text, Strfont, Strbrush, Strpoint);

                            PointF[] roipoints = CalculateRotatedRectanglePoints((match.SearchBox.Shape.X + match.SearchBox.Shape.Width / 2) / ScaleWidth, (match.SearchBox.Shape.Y + match.SearchBox.Shape.Height / 2) / ScaleHeight, match.SearchBox.Shape.Width / ScaleWidth, match.SearchBox.Shape.Height / ScaleHeight, 0);
                            e.Graphics.DrawLine(Linepen, roipoints[0], roipoints[1]);
                            e.Graphics.DrawLine(Linepen, roipoints[1], roipoints[2]);
                            e.Graphics.DrawLine(Linepen, roipoints[2], roipoints[3]);
                            e.Graphics.DrawLine(Linepen, roipoints[3], roipoints[0]);

                            if (match.Points != null)
                            {
                                foreach (ScorePoint pt in match.Points)
                                {
                                    if (pt.Score == 0)
                                    {
                                        e.Graphics.FillEllipse(Pointbrush1, pt.Point.X / ScaleWidth - 1, pt.Point.Y / ScaleHeight - 1, 2, 2);
                                    }
                                    if (pt.Score == 1)
                                    {
                                        e.Graphics.FillEllipse(Pointbrush2, pt.Point.X / ScaleWidth - 1, pt.Point.Y / ScaleHeight - 1, 2, 2);
                                    }
                                    if (pt.Score == 2)
                                    {
                                        e.Graphics.FillEllipse(Pointbrush3, pt.Point.X / ScaleWidth - 1, pt.Point.Y / ScaleHeight - 1, 2, 2);
                                    }
                                }
                            }


                        }

                    }
                    else
                    {
                        string text = "NG";

                        SizeF textSize = e.Graphics.MeasureString(text, Strfont);
                        e.Graphics.FillRectangle(Brushes.Black, new RectangleF(Strpoint, textSize));

                        e.Graphics.DrawString(text, Strfont, Strbrush, Strpoint);
                    }

                }

                if (lineshow)
                {
                    if (lines.Count > 0)
                    {
                        int I = 0;
                        string text = "OK\n";

                        foreach (LineResult line in lines)
                        {
                            if (!line.IsOk)
                            {
                                text = "NG\n";
                            }
                        }

                        bool RectT = true;
                        foreach (LineResult line in lines)
                        {
                            I++;

                            if (line.pointnumber > 0 && line.Startpoint != PointF.Empty && line.Endpoint != PointF.Empty)
                            {
                                if (line.IsOk)
                                {
                                    text = text + "line[" + Convert.ToString(I) + "]" + " Start:(" + line.Startpoint.X.ToString("0.00") + " , " + line.Startpoint.Y.ToString("0.00") + ") - "
                                                                        + "End:(" + line.Endpoint.X.ToString("0.00") + " , " + line.Endpoint.Y.ToString("0.00") + ")\n "
                                                                        + "Angle:" + line.Angle.ToString("0.00") + "Straightness:" + line.Straightness.ToString("0.00") + "\n";
                                    PointF Startpoint = new PointF(line.Startpoint.X / ScaleWidth, line.Startpoint.Y / ScaleHeight);
                                    PointF Endpoint = new PointF(line.Endpoint.X / ScaleWidth, line.Endpoint.Y / ScaleHeight);
                                    e.Graphics.DrawLine(Linepen, Startpoint, Endpoint);

                                    PointF[] roipoints = CalculateRotatedRectanglePoints((line.SearchBox.Shape.X + line.SearchBox.Shape.Width / 2) / ScaleWidth, (line.SearchBox.Shape.Y + line.SearchBox.Shape.Height / 2) / ScaleHeight, line.SearchBox.Shape.Width / ScaleWidth, line.SearchBox.Shape.Height / ScaleHeight, 0);
                                    e.Graphics.DrawLine(Linepen, roipoints[0], roipoints[1]);
                                    e.Graphics.DrawLine(Linepen, roipoints[1], roipoints[2]);
                                    e.Graphics.DrawLine(Linepen, roipoints[2], roipoints[3]);
                                    e.Graphics.DrawLine(Linepen, roipoints[3], roipoints[0]);
                                }
                                else
                                {
                                    text = text + "line[" + Convert.ToString(I) + "]" + " Start:(" + line.Startpoint.X.ToString("0.00") + " , " + line.Startpoint.Y.ToString("0.00") + ") - "
                                                                        + "End:(" + line.Endpoint.X.ToString("0.00") + " , " + line.Endpoint.Y.ToString("0.00") + ")\n "
                                                                        + "Angle:" + line.Angle.ToString("0.00") + "Straightness:" + line.Straightness.ToString("0.00") + "\n";
                                    PointF Startpoint = new PointF(line.Startpoint.X / ScaleWidth, line.Startpoint.Y / ScaleHeight);
                                    PointF Endpoint = new PointF(line.Endpoint.X / ScaleWidth, line.Endpoint.Y / ScaleHeight);
                                    e.Graphics.DrawLine(Linepen, Startpoint, Endpoint);

                                    PointF[] roipoints = CalculateRotatedRectanglePoints((line.SearchBox.Shape.X + line.SearchBox.Shape.Width / 2) / ScaleWidth, (line.SearchBox.Shape.Y + line.SearchBox.Shape.Height / 2) / ScaleHeight, line.SearchBox.Shape.Width / ScaleWidth, line.SearchBox.Shape.Height / ScaleHeight, 0);
                                    e.Graphics.DrawLine(Linepen, roipoints[0], roipoints[1]);
                                    e.Graphics.DrawLine(Linepen, roipoints[1], roipoints[2]);
                                    e.Graphics.DrawLine(Linepen, roipoints[2], roipoints[3]);
                                    e.Graphics.DrawLine(Linepen, roipoints[3], roipoints[0]);
                                }

                            }
                            else
                            {
                                text = text + "line[" + Convert.ToString(I) + "]" + "null\n" + "null\n";

                                PointF[] roipoints = CalculateRotatedRectanglePoints((line.SearchBox.Shape.X + line.SearchBox.Shape.Width / 2) / ScaleWidth, (line.SearchBox.Shape.Y + line.SearchBox.Shape.Height / 2) / ScaleHeight, line.SearchBox.Shape.Width / ScaleWidth, line.SearchBox.Shape.Height / ScaleHeight, 0);
                                e.Graphics.DrawLine(Linepen, roipoints[0], roipoints[1]);
                                e.Graphics.DrawLine(Linepen, roipoints[1], roipoints[2]);
                                e.Graphics.DrawLine(Linepen, roipoints[2], roipoints[3]);
                                e.Graphics.DrawLine(Linepen, roipoints[3], roipoints[0]);
                                RectT = false;

                            }
                        }

                        if (lines.Count > 3)
                        {
                            if (RectT)
                            {
                                var line1 = CalculateLineEquation(lines[0].Startpoint, lines[0].Endpoint);
                                var line2 = CalculateLineEquation(lines[1].Startpoint, lines[1].Endpoint);
                                var line3 = CalculateLineEquation(lines[2].Startpoint, lines[2].Endpoint);
                                var line4 = CalculateLineEquation(lines[3].Startpoint, lines[3].Endpoint);

                                // 计算交点
                                var UL_intersection = FindIntersection(line1, line3);//左上交点
                                var DL_intersection = FindIntersection(line2, line3);//左下交点
                                var UR_intersection = FindIntersection(line1, line4);//右上交点
                                var DR_intersection = FindIntersection(line2, line4);//右下交点

                                RectangleFA Rect = new RectangleFA(UL_intersection, DL_intersection, UR_intersection, DR_intersection);

                                text = text + "Rect OK\n"
                                    + "Center:(" + Rect.Center.X.ToString("0.00") + " , " + Rect.Center.Y.ToString("0.00") + ")\n"
                                    + "Angle:" + Rect.Angle.ToString("0.00");

                            }
                            else
                            {
                                text = text + "Rect NG\n";
                            }
                        }

                        SizeF textSize = e.Graphics.MeasureString(text, Strfont);
                        e.Graphics.FillRectangle(Brushes.Black, new RectangleF(Strpoint, textSize));

                        e.Graphics.DrawString(text, Strfont, Strbrush, Strpoint);

                    }
                    else
                    {
                        string text = "NG";

                        SizeF textSize = e.Graphics.MeasureString(text, Strfont);
                        e.Graphics.FillRectangle(Brushes.Black, new RectangleF(Strpoint, textSize));

                        e.Graphics.DrawString(text, Strfont, Strbrush, Strpoint);
                    }
                }

                if (circleshow)
                {
                    if (circles.Count > 0)
                    {

                        string text = "OK\n";
                        foreach (CircleResult circle in circles)
                        {
                            if (circle.pointnumber > 0 && circle.CircleCenter != null)
                            {
                                if (circle.IsOk)
                                {
                                    text = "OK\n";
                                    text = text + "Center:(" + circle.CircleCenter.X.ToString("0.00") + " , " + circle.CircleCenter.Y.ToString("0.00") + ")\n"
                                    + "Radius:" + circle.CircleRadius.ToString("0.00") + "\n";

                                    Rectangle rect = new Rectangle((int)(circle.CircleCenter.X / ScaleWidth) - (int)(circle.CircleRadius / ScaleWidth),
                                        (int)(circle.CircleCenter.Y / ScaleHeight) - (int)(circle.CircleRadius / ScaleHeight),
                                        (int)(circle.CircleRadius / ScaleWidth * 2), (int)(circle.CircleRadius / ScaleHeight * 2));
                                    e.Graphics.DrawEllipse(Circlepen, rect);

                                    PointF[] roipoints = CalculateRotatedRectanglePoints((circle.SearchBox.Shape.X + circle.SearchBox.Shape.Width / 2) / ScaleWidth, (circle.SearchBox.Shape.Y + circle.SearchBox.Shape.Height / 2) / ScaleHeight, circle.SearchBox.Shape.Width / ScaleWidth, circle.SearchBox.Shape.Height / ScaleHeight, 0);
                                    e.Graphics.DrawLine(Linepen, roipoints[0], roipoints[1]);
                                    e.Graphics.DrawLine(Linepen, roipoints[1], roipoints[2]);
                                    e.Graphics.DrawLine(Linepen, roipoints[2], roipoints[3]);
                                    e.Graphics.DrawLine(Linepen, roipoints[3], roipoints[0]);
                                }
                                else
                                {
                                    text = "NG\n";
                                    text = text + "Center:(" + circle.CircleCenter.X.ToString("0.00") + " , " + circle.CircleCenter.Y.ToString("0.00") + ")\n"
                                    + "Radius:" + circle.CircleRadius.ToString("0.00") + "\n";

                                    Rectangle rect = new Rectangle((int)(circle.CircleCenter.X / ScaleWidth) - (int)(circle.CircleRadius / ScaleWidth),
                                        (int)(circle.CircleCenter.Y / ScaleHeight) - (int)(circle.CircleRadius / ScaleHeight),
                                        (int)(circle.CircleRadius / ScaleWidth * 2), (int)(circle.CircleRadius / ScaleHeight * 2));
                                    e.Graphics.DrawEllipse(Circlepen, rect);

                                    PointF[] roipoints = CalculateRotatedRectanglePoints((circle.SearchBox.Shape.X + circle.SearchBox.Shape.Width / 2) / ScaleWidth, (circle.SearchBox.Shape.Y + circle.SearchBox.Shape.Height / 2) / ScaleHeight, circle.SearchBox.Shape.Width / ScaleWidth, circle.SearchBox.Shape.Height / ScaleHeight, 0);
                                    e.Graphics.DrawLine(Linepen, roipoints[0], roipoints[1]);
                                    e.Graphics.DrawLine(Linepen, roipoints[1], roipoints[2]);
                                    e.Graphics.DrawLine(Linepen, roipoints[2], roipoints[3]);
                                    e.Graphics.DrawLine(Linepen, roipoints[3], roipoints[0]);
                                }

                            }
                            else
                            {
                                text = text + "null\n";

                                PointF[] roipoints = CalculateRotatedRectanglePoints((circle.SearchBox.Shape.X + circle.SearchBox.Shape.Width / 2) / ScaleWidth, (circle.SearchBox.Shape.Y + circle.SearchBox.Shape.Height / 2) / ScaleHeight, circle.SearchBox.Shape.Width / ScaleWidth, circle.SearchBox.Shape.Height / ScaleHeight, 0);
                            }
                        }

                        SizeF textSize = e.Graphics.MeasureString(text, Strfont);
                        e.Graphics.FillRectangle(Brushes.Black, new RectangleF(Strpoint, textSize));

                        e.Graphics.DrawString(text, Strfont, Strbrush, Strpoint);

                    }
                    else
                    {
                        string text = "NG";

                        SizeF textSize = e.Graphics.MeasureString(text, Strfont);
                        e.Graphics.FillRectangle(Brushes.Black, new RectangleF(Strpoint, textSize));

                        e.Graphics.DrawString(text, Strfont, Strbrush, Strpoint);
                    }
                }

                if (Crossshow)
                {


                    e.Graphics.DrawLine(Linepen, 0, WindowHeight / 2, WindowWidth, WindowHeight / 2);
                    e.Graphics.DrawLine(Linepen, WindowWidth / 2, 0, WindowWidth / 2, WindowHeight);

                    e.Graphics.DrawRectangle(Linepen, WindowWidth / 6, WindowHeight / 6, WindowWidth * 2 / 3, WindowHeight * 2 / 3);
                    e.Graphics.DrawRectangle(Linepen, WindowWidth / 3, WindowHeight / 3, WindowWidth * 1 / 3, WindowHeight * 1 / 3);

                }

                if (Gridshow)
                {
                    Linepen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

                    e.Graphics.DrawLine(Linepen, 0, WindowHeight * 1 / 6, WindowWidth, WindowHeight * 1 / 6);
                    e.Graphics.DrawLine(Linepen, 0, WindowHeight * 2 / 6, WindowWidth, WindowHeight * 2 / 6);
                    e.Graphics.DrawLine(Linepen, 0, WindowHeight * 3 / 6, WindowWidth, WindowHeight * 3 / 6);
                    e.Graphics.DrawLine(Linepen, 0, WindowHeight * 4 / 6, WindowWidth, WindowHeight * 4 / 6);
                    e.Graphics.DrawLine(Linepen, 0, WindowHeight * 5 / 6, WindowWidth, WindowHeight * 5 / 6);

                    e.Graphics.DrawLine(Linepen, WindowWidth * 1 / 6, 0, WindowWidth * 1 / 6, WindowHeight);
                    e.Graphics.DrawLine(Linepen, WindowWidth * 2 / 6, 0, WindowWidth * 2 / 6, WindowHeight);
                    e.Graphics.DrawLine(Linepen, WindowWidth * 3 / 6, 0, WindowWidth * 3 / 6, WindowHeight);
                    e.Graphics.DrawLine(Linepen, WindowWidth * 4 / 6, 0, WindowWidth * 4 / 6, WindowHeight);
                    e.Graphics.DrawLine(Linepen, WindowWidth * 5 / 6, 0, WindowWidth * 5 / 6, WindowHeight);
                }

                if (rectshow)
                {
                    Pen pen = new Pen(Color.Black, 2);
                    //pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

                    if (rects.Count > 0)
                    {
                        foreach (RectangleF rect in rects)
                        {
                            if (rect != null)
                            {
                                e.Graphics.DrawRectangle(Linepen, rect.X / ScaleWidth, rect.Y / ScaleHeight, rect.Width / ScaleWidth, rect.Height / ScaleHeight);
                            }
                        }
                    }
                }

                if (circleroishow)
                {

                    if (circleroi != null)
                    {
                        e.Graphics.DrawEllipse(Linepen, circleroi.X / ScaleWidth, circleroi.Y / ScaleHeight, circleroi.Width / ScaleWidth, circleroi.Height / ScaleHeight);
                    }
                }

                if (materialshow)
                {
                    if (materials != null)
                    {
                        if (materials.Count > 0)
                        {
                            string text = "OK";

                            SizeF textSize = e.Graphics.MeasureString(text, Strfont);
                            e.Graphics.FillRectangle(Brushes.Black, new RectangleF(Strpoint, textSize));

                            e.Graphics.DrawString(text, Strfont, Strbrush, Strpoint);



                            foreach (ScorePoint pt in materials)
                            {
                                if(pt.Score == 0)
                                {
                                    e.Graphics.FillEllipse(Pointbrush1, pt.Point.X / ScaleWidth - 5, pt.Point.Y / ScaleHeight - 5, 10, 10);
                                }
                                if (pt.Score == 1)
                                {
                                    e.Graphics.FillEllipse(Pointbrush2, pt.Point.X / ScaleWidth - 5, pt.Point.Y / ScaleHeight - 5, 10, 10);
                                }
                                if (pt.Score == 2)
                                {
                                    e.Graphics.FillEllipse(Pointbrush3, pt.Point.X / ScaleWidth - 5, pt.Point.Y / ScaleHeight - 5, 10, 10);
                                }
                            }
                        }
                        else
                        {
                            string text = "NG";

                            SizeF textSize = e.Graphics.MeasureString(text, Strfont);
                            e.Graphics.FillRectangle(Brushes.Black, new RectangleF(Strpoint, textSize));

                            e.Graphics.DrawString(text, Strfont, Strbrush, Strpoint);
                        }
                    }
                    else
                    {
                        string text = "NG";
                    }

                }


            }
            catch
            {

            }
            //}));


        }

        #endregion

        #region Method



        public void GraphicDrawInit(List<ScorePoint> points, PointF Center)
        {
            try
            {
                trains = points;
                matchcenter = Center;

            }
            catch
            {

            }
        }

        public void GraphicDrawInit(List<ScorePoint> points)
        {
            try
            {
                materials = points;

            }
            catch
            {

            }
        }


        public void GraphicDrawInit(List<MatchResult> matches)
        {
            try
            {
                this.matches = matches;

            }
            catch
            {

            }
        }

        public void GraphicDrawInit(MatchResult matches)
        {
            try
            {
                this.matches.Clear();
                this.matches.Add(matches);
                matchnum = 0;

            }
            catch
            {

            }
        }

        public void GraphicDrawInit(List<LineResult> lines)
        {
            try
            {
                this.lines = lines;

            }
            catch
            {

            }
        }

        public void GraphicDrawInit(LineResult lines)
        {
            try
            {
                this.lines.Clear();
                this.lines.Add(lines);

            }
            catch
            {

            }
        }

        public void GraphicDrawInit(List<CircleResult> circles)
        {
            try
            {
                this.circles = circles;

            }
            catch
            {

            }
        }

        public void GraphicDrawInit(CircleResult circles)
        {
            try
            {
                this.circles.Clear();
                this.circles.Add(circles);

            }
            catch
            {

            }
        }

        public void GraphicDrawInit(List<RectangleF> rects)
        {
            try
            {
                this.rects = rects;

            }
            catch
            {

            }
        }

        public void GraphicDrawInit(RectangleF rect)
        {
            try
            {
                this.rects.Clear();
                this.rects.Add(rect);

            }
            catch
            {

            }
        }

        public void MatchMove(float DeviationX, float DeviationY, float Angle)
        {
            try
            {
                if (trains.Count > 0)
                {
                    matchdeviation = new PointF(DeviationX, DeviationY);
                    matchangle = Angle;
                }
                CameraWindow.Invalidate();
            }
            catch
            {

            }
        }

        public void ROIReSize(ImageDirection Direct, int interval)
        {
            try
            {
                if (rects.Count > 0)
                {
                    if (rects[0] != null)
                    {
                        RectangleF rectangle = rects[0];
                        if (Direct == ImageDirection.X)
                        {
                            int Xwidth = (int)rects[0].Width + 2 * interval;
                            int CenterX = (int)rects[0].X - interval;
                            if ((CenterX > 0) && (CenterX < ImageWidth) && (Xwidth > 0) && (Xwidth < ImageWidth) && ((CenterX + Xwidth) < ImageWidth))
                            {
                                rects[0] = new RectangleF(CenterX, rectangle.Y, Xwidth, rectangle.Height);
                            }

                        }

                        if (Direct == ImageDirection.Y)
                        {
                            int Yheight = (int)rects[0].Height + 2 * interval;
                            int CenterY = (int)rects[0].Y - interval;
                            if ((CenterY > 0) && (CenterY < ImageHeight) && (Yheight > 0) && (Yheight < ImageHeight) && ((CenterY + Yheight) < ImageHeight))
                            {
                                rects[0] = new RectangleF(rectangle.X, CenterY, rectangle.Width, Yheight);
                            }
                        }
                    }
                }
                CameraWindow.Invalidate();
            }
            catch
            {

            }
        }

        public void ROIMove(ImageDirection Direct, int interval)
        {
            try
            {
                if (rects.Count > 0)
                {
                    if (rects[0] != null)
                    {
                        RectangleF rectangle = rects[0];
                        if (Direct == ImageDirection.X)
                        {
                            int CenterX = (int)rects[0].X + interval;
                            if ((CenterX > 0) && (CenterX < ImageWidth) && (rectangle.Width > 0) && (rectangle.Width < ImageWidth) && ((CenterX + rectangle.Width) < ImageWidth))
                            {
                                rects[0] = new RectangleF(CenterX, rectangle.Y, rectangle.Width, rectangle.Height);
                            }

                        }

                        if (Direct == ImageDirection.Y)
                        {
                            int CenterY = (int)rects[0].Y + interval;
                            if ((CenterY > 0) && (CenterY < ImageHeight) && (rectangle.Height > 0) && (rectangle.Height < ImageHeight) && ((CenterY + rectangle.Height) < ImageHeight))
                            {
                                rects[0] = new RectangleF(rectangle.X, CenterY, rectangle.Width, rectangle.Height);
                            }

                        }
                    }
                }
                CameraWindow.Invalidate();
            }
            catch
            {

            }
        }

        public void GraphicDrawInit(PointF Center, float Radius)
        {
            try
            {

                this.circleroi = new RectangleF(Center.X - Radius, Center.Y - Radius, Radius * 2, Radius * 2);

            }
            catch
            {

            }
        }

        public void CircleROIReSize(int Rinterval)
        {
            try
            {
                if (circleroi != null)
                {
                    RectangleF rectangle = circleroi;
                    int Xwidth = (int)circleroi.Width + 2 * Rinterval;
                    int Yheight = (int)circleroi.Height + 2 * Rinterval;
                    int CenterX = (int)circleroi.X - Rinterval;
                    int CenterY = (int)circleroi.Y - Rinterval;
                    if ((CenterX > 0) && (CenterX < ImageWidth) && (Xwidth > 0) && (Xwidth < ImageWidth) && ((CenterX + Xwidth) < ImageWidth))
                    {
                        circleroi = new RectangleF(CenterX, CenterY, Xwidth, Yheight);
                    }

                }
                CameraWindow.Invalidate();
            }
            catch
            {

            }
        }

        public void CircleROIMove(ImageDirection Direct, int interval)
        {
            try
            {
                if (circleroi != null)
                {
                    RectangleF rectangle = circleroi;
                    if (Direct == ImageDirection.X)
                    {
                        int CenterX = (int)circleroi.X + interval;
                        if ((CenterX > 0) && (CenterX < ImageWidth) && (rectangle.Width > 0) && (rectangle.Width < ImageWidth) && ((CenterX + rectangle.Width) < ImageWidth))
                        {
                            circleroi = new RectangleF(CenterX, rectangle.Y, rectangle.Width, rectangle.Height);
                        }

                    }

                    if (Direct == ImageDirection.Y)
                    {
                        int CenterY = (int)circleroi.Y + interval;
                        if ((CenterY > 0) && (CenterY < ImageHeight) && (rectangle.Height > 0) && (rectangle.Height < ImageHeight) && ((CenterY + rectangle.Height) < ImageHeight))
                        {
                            circleroi = new RectangleF(rectangle.X, CenterY, rectangle.Width, rectangle.Height);
                        }

                    }
                }
                CameraWindow.Invalidate();
            }
            catch
            {

            }
        }



        public RectangleF GetROI()
        {
            try
            {
                if (rects.Count > 0)
                {
                    return rects[0];
                }
                else
                {
                    return new RectangleF();
                }
            }
            catch
            {
                return new RectangleF();
            }
        }

        public (PointF Center, float Radius) GetCircleROI()
        {
            try
            {
                if (circleroi != null)
                {
                    return (new PointF(circleroi.X + circleroi.Width / 2, circleroi.Y + circleroi.Height / 2), circleroi.Width / 2 + circleroi.Height / 2);
                }
                else
                {
                    return (new PointF(), -1);
                }
            }
            catch
            {
                return (new PointF(), -1);
            }
        }

        public void GraphicDraw(Graphic graphic, bool Show)
        {
            try
            {
                //ROI
                if (graphic == Graphic.circleRoi)
                {
                    if (Show)
                    {
                        circleroishow = true;
                    }
                    else
                    {
                        circleroishow = false;
                    }
                }
                if (graphic == Graphic.rectRoi)
                {
                    if (Show)
                    {
                        rectshow = true;
                    }
                    else
                    {
                        rectshow = false;
                    }
                }

                //十字
                if (graphic == Graphic.cross)
                {
                    if (Gridshow)
                    {
                        Gridshow = false;
                    }
                    if (Show)
                    {
                        Crossshow = true;
                    }
                    else
                    {
                        Crossshow = false;
                    }
                }
                //网格
                if (graphic == Graphic.grid)
                {
                    if (Crossshow)
                    {
                        Crossshow = false;
                    }
                    if (Show)
                    {
                        Gridshow = true;
                    }
                    else
                    {
                        Gridshow = false;
                    }
                }
                //轮廓训练
                if (graphic == Graphic.train)
                {
                    if (Show)
                    {
                        trainshow = true;
                    }
                    else
                    {
                        trainshow = false;
                    }
                }
                //轮廓匹配结果
                if (graphic == Graphic.match)
                {
                    if (Show)
                    {
                        matchshow = true;
                    }
                    else
                    {
                        matchshow = false;
                    }
                }
                //边缘查找结果
                if (graphic == Graphic.line)
                {
                    if (Show)
                    {
                        lineshow = true;
                    }
                    else
                    {
                        lineshow = false;
                    }
                }
                //圆查找结果
                if (graphic == Graphic.circle)
                {
                    if (Show)
                    {
                        circleshow = true;
                    }
                    else
                    {
                        circleshow = false;
                    }
                }
                //物料阵列显示
                if (graphic == Graphic.array)
                {
                    if (Show)
                    {
                        materialshow = true;
                    }
                    else
                    {
                        materialshow = false;
                    }
                }

                CameraWindow.Invalidate();
            }
            catch
            {

            }
        }


        public void ClearGraphicDraw()
        {
            try
            {
                trains = new List<ScorePoint>();
                trainshow = false;

                matches = new List<MatchResult>();
                matchshow = false;
                matchnum = 0;

                lines = new List<LineResult>();
                lineshow = false;

                circles = new List<CircleResult>();
                circleshow = false;

                Crossshow = false;

                Gridshow = false;

                circleroishow = false;
                rectshow = false;

                materials = new List<ScorePoint>();
                materialshow = false;

                CameraWindow.Invalidate();

            }
            catch
            {

            }
        }


        /// <summary>
        /// 显示图像
        /// </summary>
        /// <param name="bitmap"></param>
        public void ShowImage(Bitmap bitmap, double scale = 1)
        {
            try
            {
                //if(this.InvokeRequired)
                //{
                //    this.Invoke(new Action<Bitmap>(ShowImage), bitmap);
                //    return;
                //}



                if (scale <= 0 || scale > 1) throw new ArgumentOutOfRangeException(nameof(scale), "Scale must be between 0 and 1");

                // Determine source rectangle size  
                int sourceWidth = (int)(bitmap.Width * scale);
                int sourceHeight = (int)(bitmap.Height * scale);

                // Find the centered area of the original image  
                Rectangle srcRect = new Rectangle(
                    (bitmap.Width - sourceWidth) / 2,
                    (bitmap.Height - sourceHeight) / 2,
                    sourceWidth,
                    sourceHeight);

                ImageWidth = sourceWidth;
                ImageHeight = sourceHeight;
                Scale = (float)scale;


                // Create a new bitmap with PictureBox's dimensions  
                Bitmap bmp = new Bitmap(CameraWindow.Width, CameraWindow.Height);

                using (Graphics g = Graphics.FromImage(bmp))
                {
                    // Draw the specified source rectangle of the original image onto the entire bitmap area  
                    g.DrawImage(bitmap, new Rectangle(0, 0, bmp.Width, bmp.Height), srcRect, GraphicsUnit.Pixel);
                }


                CameraWindow.Image = bmp;

                //CameraWindow.Invalidate();
            }
            catch
            {

            }

        }

        #endregion

        #endregion

        #region 相机

        #region File

        private CameraConfig _TrackCameraConfig
        {
            get { return CameraManager.Instance.GetCameraConfigByID(EnumCameraType.TrackCamera); }
        }
        private CameraConfig _WeldCameraConfig
        {
            get { return CameraManager.Instance.GetCameraConfigByID(EnumCameraType.WeldCamera); }
        }

        private CameraManager _cameraManager
        {
            get { return CameraManager.Instance; }
        }

        public ICameraController TrackCamera
        {
            get { return _cameraManager.GetCameraByID(EnumCameraType.TrackCamera); }
        }
        public ICameraController WeldCamera
        {
            get { return _cameraManager.GetCameraByID(EnumCameraType.WeldCamera); }
        }

        public int CurrentCameraNum = 0;

        bool ImageShow = false;

        bool GridBtnChecked = false;
        bool CrossBtnChecked = false;

        public bool InitVisualControled { get; set; } = false;

        #endregion

        public bool InitVisualControl()
        {
            if (TrackCamera == null || WeldCamera == null)
            {
                ImageShow = false;
                InitVisualControled = false;
                return InitVisualControled;
            }
            if (TrackCamera != null)
                TrackCamera.ImageDataAcquiredAction += TrackCameraImagecallback;
            if (WeldCamera != null)
                WeldCamera.ImageDataAcquiredAction += WeldCameraImagecallback;

            InitVisualControled = true;
            ImageShow = true;
            return InitVisualControled;
        }


        private void TrackCameraImagecallback(Bitmap bitmap)
        {
            try
            {
                if (ImageShow && CameraWindow != null)
                {
                    ShowImage(bitmap, _TrackCameraConfig.CameraWindowImageScale);
                }
            }
            catch
            {

            }
        }

        private void WeldCameraImagecallback(Bitmap bitmap)
        {
            try
            {
                if (ImageShow && CameraWindow != null)
                {
                    ShowImage(bitmap, _WeldCameraConfig.CameraWindowImageScale);
                }
            }
            catch
            {

            }
        }

        private void SetComboBoxSelectedIndex(int index)
        {
            if (this.InvokeRequired)
            {

                this.Invoke(new Action<int>(SetComboBoxSelectedIndex), index);
                return;
            }
            if (CameraComboBox.ComboBox.InvokeRequired)
            {
                CameraComboBox.ComboBox.Invoke(new Action<int>(SetComboBoxSelectedIndex), index);
            }
            else
            {
                CameraComboBox.SelectedIndex = index;
            }
        }

        /// <summary>
        /// 选择相机
        /// </summary>
        /// <param name="CameraNum">0 传送相机 1 焊接相机 </param>
        public void SelectCamera(int CameraNum)
        {
            
            CurrentCameraNum = CameraNum;



            if (CurrentCameraNum > -1 && CurrentCameraNum < 3)
            {
                this.CameraComboBox.SelectedIndexChanged -= new System.EventHandler(this.CameraComboBox_SelectedIndexChanged);

                SetComboBoxSelectedIndex(CameraNum);

                this.CameraComboBox.SelectedIndexChanged += new System.EventHandler(this.CameraComboBox_SelectedIndexChanged);
            }


            if (CurrentCameraNum == 0)
            {
                if (WeldCamera != null)
                {
                    if (WeldCamera.IsConnected() && WeldCamera.Grabbing)
                    {
                        if (WeldCamera.GrabMode == true)
                            WeldCamera.Grabbing = false;
                        WeldCamera.ContinuousGetImage(false);
                    }
                }


                if (TrackCamera != null)
                    TrackCamera.ContinuousGetImage(true);

                _cameraManager.CurrentCameraType = EnumCameraType.TrackCamera;
            }
            else if (CurrentCameraNum == 1)
            {
                if (TrackCamera != null)
                {
                    if (TrackCamera.IsConnected() && TrackCamera.Grabbing)
                    {
                        if (TrackCamera.GrabMode == true)
                            TrackCamera.ContinuousGetImage(false);
                        TrackCamera.Grabbing = false;
                    }
                }

                if (WeldCamera != null)
                    WeldCamera.ContinuousGetImage(true);

                _cameraManager.CurrentCameraType = EnumCameraType.WeldCamera;
            }

        }

        public void CameraStop()
        {
            if (TrackCamera != null)
            {
                if (TrackCamera.IsConnected() && TrackCamera.Grabbing)
                {
                    if (TrackCamera.GrabMode == true)
                        TrackCamera.ContinuousGetImage(false);
                    TrackCamera.Grabbing = false;
                }
            }
            if (WeldCamera != null)
            {
                if (WeldCamera.IsConnected() && WeldCamera.Grabbing)
                {
                    if (WeldCamera.GrabMode == true)
                        WeldCamera.Grabbing = false;
                    WeldCamera.ContinuousGetImage(false);
                }
            }
        }

        private void CameraComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectCamera(CameraComboBox.SelectedIndex);
        }

        private void CrossBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (CameraWindow != null)
            {
                if (GridBtn.Checked)
                {
                    GridBtn.Checked = false;
                }
                if (CrossBtn.Checked)
                {
                    GraphicDraw(Graphic.cross, true);
                }
                else
                {
                    GraphicDraw(Graphic.cross, false);
                }
            }
        }

        private void GridBtn_CheckedChanged(object sender, EventArgs e)
        {
            if (CameraWindow != null)
            {
                if (CrossBtn.Checked)
                {
                    CrossBtn.Checked = false;
                }
                if (GridBtn.Checked)
                {
                    GraphicDraw(Graphic.grid, true);
                }
                else
                {
                    GraphicDraw(Graphic.grid, false);
                }
            }
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            if (CameraWindow != null)
            {
                ClearGraphicDraw();
            }
        }



        #endregion

        private void CrossBtn_Click(object sender, EventArgs e)
        {
            if (CameraWindow != null)
            {
                if (GridBtnChecked == false)
                {
                    GraphicDraw(Graphic.cross, true);
                    GridBtnChecked = true;
                }
                else
                {
                    GraphicDraw(Graphic.cross, false);
                    GridBtnChecked = false;
                }
            }
        }

        private void GridBtn_Click(object sender, EventArgs e)
        {
            if (CameraWindow != null)
            {
                if (CrossBtnChecked == false)
                {
                    GraphicDraw(Graphic.grid, true);
                    CrossBtnChecked = true;
                }
                else
                {
                    GraphicDraw(Graphic.grid, false);
                    CrossBtnChecked = false;
                }
            }
        }

        private void CameraWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (CameraWindow.Image == null)
            {
                string str = $"(X:{e.Location.X},Y:{e.Location.Y})(0)";
                toolStripPixelCoordinates.Text = str;
            }
            else
            {
                double width = CameraWindow.Width;
                double height = CameraWindow.Height;
                double IMwidth = ImageWidth;
                double IMheight = ImageHeight;
                //double X1 = e.Location.X / width * IMwidth;
                //double Y1 = e.Location.Y / height * IMheight;
                double X1 = e.Location.X;
                double Y1 = e.Location.Y;
                double X2 = e.Location.X / width * IMwidth;
                double Y2 = e.Location.Y / height * IMheight;
                Bitmap bitmap = (Bitmap)CameraWindow.Image;
                string str = "";
                if (bitmap.PixelFormat == System.Drawing.Imaging.PixelFormat.Format8bppIndexed)
                {
                    if (X1 < 0 && Y1 < 0)
                    {
                        return;
                    }
                    Color srcColor = bitmap.GetPixel((int)X1, (int)Y1);
                    str = $"(X:{(int)X2},Y:{(int)Y2})({srcColor.R})";
                }
                else
                {
                    if(X1 < 0 && Y1 < 0)
                    {
                        return;
                    }
                    Color srcColor = bitmap.GetPixel((int)X1, (int)Y1);
                    str = $"(X:{(int)X2},Y:{(int)Y2})({srcColor.R},{srcColor.G},{srcColor.B})";
                }
                toolStripPixelCoordinates.Text = str;
            }
        }
    }
}
