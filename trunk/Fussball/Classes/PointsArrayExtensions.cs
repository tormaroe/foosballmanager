using System;
using System.Drawing;

namespace Fussball
{
    public static class PointsArrayExtensions
    {
        public static void DrawFinalPoint(this Point[] points, Graphics graphics, string text)
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
