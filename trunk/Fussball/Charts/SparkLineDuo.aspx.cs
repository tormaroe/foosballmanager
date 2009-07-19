using System;
using System.Web.UI;
using System.Drawing;
using System.Drawing.Imaging;

namespace Fussball.Charts
{
    public partial class SparkLineDuo : Page
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

                pointsSet1.DrawFinalPoint(graphics, _dataContainer.Set1Text);
                pointsSet2.DrawFinalPoint(graphics, _dataContainer.Set2Text);

            }            

            Response.ContentType = "image/jpeg";
            bitmap.Save(Response.OutputStream, ImageFormat.Jpeg);
            graphics.Dispose();
            bitmap.Dispose();
            Session[Request["data"]] = null;
        }

        
    }
}

