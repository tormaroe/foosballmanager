using System;
using System.Web.UI;
using System.Drawing;
using System.Drawing.Imaging;

namespace Fussball.Charts
{
    public partial class SparkLine : Page
    {
        private const string SPARKLINEDATA_REQUESTKEY = "data";
        private const string RESPONSE_CONTENT_TYPE = "image/jpeg";
        
        private SparkLineData _dataContainer;
        private Bitmap _bitmap;
        private Graphics _graphics;
        private Pen _avgPen;

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadData();
            if (_dataContainer == null)
                return;

            _dataContainer.SetAvg();

            InitializeGraphics();
            DrawChart();
            WriteChartToResponse();
            CleanupObjects();
        }

        private void LoadData()
        {
            _dataContainer = Session[Request[SPARKLINEDATA_REQUESTKEY]] as SparkLineData;
        }

        private void InitializeGraphics()
        {
            _bitmap = new Bitmap(_dataContainer.ImageWidth, _dataContainer.ImageHeight);
            _graphics = Graphics.FromImage(_bitmap);
            _graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            _graphics.FillRectangle(new SolidBrush(_dataContainer.BgColor), 0, 0, _dataContainer.ImageWidth, _dataContainer.ImageHeight);
            _avgPen = new Pen(_dataContainer.AvgLineColor);
        }

        private void DrawChart()
        {
            if (!Double.IsInfinity(_dataContainer.Scale))
            {
                DrawStandardDeviation();
                DrapNormalAvarageLine();
                DrapPoints();
            }
            else
            {
                DrawAvarageLineForInfinityCase();
            }
        }

        private void DrawStandardDeviation()
        {
            if (_dataContainer.StdDev)
            {
                _graphics.FillRectangle(new SolidBrush(_dataContainer.StdDevColor), _dataContainer.StdDevRectangle);
            }
        }

        private void DrapNormalAvarageLine()
        {
            _graphics.DrawLine(_avgPen,
                                _dataContainer.LeftMargin,
                                _dataContainer.MiddleY,
                                _dataContainer.ImageWidth - _dataContainer.RightMargin,
                                _dataContainer.MiddleY);
        }

        private void DrawAvarageLineForInfinityCase()
        {
            int middleY = _dataContainer.ImageHeight / 2;
            _graphics.DrawLine(_avgPen,
                _dataContainer.LeftMargin,
                middleY,
                _dataContainer.ImageWidth - _dataContainer.ImageHeight,
                middleY);
        }

        private void DrapPoints()
        {
            Point[] points = _dataContainer.GetPoints();
            _graphics.DrawLines(new Pen(_dataContainer.LineColor), points);            
            points.DrawFinalPoint(_graphics, _dataContainer.LastValue.ToString());
        }

        private void WriteChartToResponse()
        {
            Response.ContentType = RESPONSE_CONTENT_TYPE;
            _bitmap.Save(Response.OutputStream, ImageFormat.Jpeg);            
        }

        private void CleanupObjects()
        {
            _graphics.Dispose();
            _bitmap.Dispose();
            Session[Request[SPARKLINEDATA_REQUESTKEY]] = null;
        }
    }
}

