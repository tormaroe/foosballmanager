using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Drawing.Imaging;

namespace Fussball.Charts
{
    public partial class SparkLineDuo : System.Web.UI.Page
    {
        
        private SparkLineDuoData _dataContainer;

        protected void Page_Load(object sender, EventArgs e)
        {
            _dataContainer = Session[Request["data"]] as SparkLineDuoData;

            if (_dataContainer == null)
                return;

            _dataContainer.SetAvg();

            Point[] pointsSet1 = _dataContainer.GetPoints(1);
            Point[] pointsSet2 = _dataContainer.GetPoints(2);
            Bitmap bitmap = new Bitmap(_dataContainer.ImageWidth, _dataContainer.ImageHeight);
            Pen penSet1 = new Pen(_dataContainer.LineColorSet1);
            Pen penSet2 = new Pen(_dataContainer.LineColorSet2);
            Graphics graphics = Graphics.FromImage(bitmap);

            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            graphics.FillRectangle(new SolidBrush(_dataContainer.BgColor), 0, 0, _dataContainer.ImageWidth, _dataContainer.ImageHeight);

            if (!Double.IsInfinity(_dataContainer.Scale))
            {
                //lines
                graphics.DrawLines(penSet1, pointsSet1);
                graphics.DrawLines(penSet2, pointsSet2);

                DrawFinalPoint(pointsSet1, graphics, _dataContainer.Set1Text);
                DrawFinalPoint(pointsSet2, graphics, _dataContainer.Set2Text);

            }
            else
            {

            }

            Response.ContentType = "image/jpeg";
            bitmap.Save(Response.OutputStream, ImageFormat.Jpeg);
            graphics.Dispose();
            bitmap.Dispose();
            Session[Request["data"]] = null;
        }

        private void DrawFinalPoint(Point[] points, Graphics graphics, string text)
        {
            //final point
            Point lastPoint = points[points.Length - 1];
            Brush finalBrush = new SolidBrush(Color.Red);
            graphics.FillPie(finalBrush, lastPoint.X - 2, lastPoint.Y - 2, 4, 4, 0, 360);

            //final value
            string lastValue = text;
            Font drawFont = new Font("Arial", 8);
            Brush drawBrush = new SolidBrush(Color.Black);
            graphics.DrawString(lastValue, drawFont, drawBrush, lastPoint.X + 2, lastPoint.Y - 6);
        }
    }
}

