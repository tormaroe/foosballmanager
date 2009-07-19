using System;
using System.Web.UI;
using System.Drawing;
using System.Drawing.Imaging;

namespace Fussball.Charts
{
    public partial class SparkLine : Page
    {
        private SparkLineData _dataContainer;

        protected void Page_Load(object sender, EventArgs e)
        {
            _dataContainer = Session[Request["data"]] as SparkLineData;

            if (_dataContainer == null)
                return;

            _dataContainer.SetAvg();

            Point[] points = _dataContainer.GetPoints();
            Bitmap bitmap = new Bitmap(_dataContainer.ImageWidth, _dataContainer.ImageHeight);
            Pen pen = new Pen(_dataContainer.LineColor);
            Pen avgPen = new Pen(_dataContainer.AvgLineColor);
            Graphics graphics = Graphics.FromImage(bitmap);

            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            graphics.FillRectangle(new SolidBrush(_dataContainer.BgColor), 0, 0, _dataContainer.ImageWidth, _dataContainer.ImageHeight);

            if (!Double.IsInfinity(_dataContainer.Scale))
            {

                if (_dataContainer.StdDev)
                {
                    graphics.FillRectangle(new SolidBrush(_dataContainer.StdDevColor), _dataContainer.StdDevRectangle);
                }

                //avg line
                graphics.DrawLine(avgPen,
                    _dataContainer.LeftMargin,
                    _dataContainer.MiddleY,
                    _dataContainer.ImageWidth - _dataContainer.RightMargin,
                    _dataContainer.MiddleY);

                //lines
                graphics.DrawLines(pen, points);

                DrawFinalPoint(points, graphics);
            }
            else
            {
                int middleY = _dataContainer.ImageHeight / 2;
                graphics.DrawLine(avgPen,
                    _dataContainer.LeftMargin,
                    middleY,
                    _dataContainer.ImageWidth - _dataContainer.ImageHeight,
                    middleY);
            }

            Response.ContentType = "image/jpeg";
            bitmap.Save(Response.OutputStream, ImageFormat.Jpeg);
            graphics.Dispose();
            bitmap.Dispose();
            Session[Request["data"]] = null;
        }

        private void DrawFinalPoint(Point[] points, Graphics graphics)
        {
            //final point
            Point lastPoint = points[points.Length - 1];
            Brush finalBrush = new SolidBrush(Color.Red);
            graphics.FillPie(finalBrush, lastPoint.X - 2, lastPoint.Y - 2, 4, 4, 0, 360);

            //final value
            string lastValue = _dataContainer.LastValue.ToString();
            Font drawFont = new Font("Arial", 8);
            Brush drawBrush = new SolidBrush(Color.Black);
            graphics.DrawString(lastValue, drawFont, drawBrush, lastPoint.X + 2, lastPoint.Y - 6);
        }
    }
}

