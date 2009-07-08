using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;

/// <summary>
/// Summary description for SparkLineData
/// </summary>
public class SparkLineData
{
    private Color _bgColor;
    private Color _avgLineColor;
    private Color _lineColor;
    private Color _stdDevColor;
    private bool _stdDev;
    private double[] _data;
    private int _imageWidth;
    private int _imageHeight;
    private int _topMargin;
    private int _bottomMargin;
    private int _leftMargin;
    private int _rightMargin;
    private double _max;
    private double _min;
    private double _avg;
    private double _sum;
    private double _stdDevValue;

    public SparkLineData()
    {
        SetDefaultValues();
    }

    public double Max
    {
        get
        {
            return _max;
        }
    }

    public double Min
    {
        get
        {
            return _min;
        }
    }

    public double Avg
    {
        get
        {
            return _avg;
        }
    }

    public double LastValue
    {
        get
        {
            return _data[_data.Length - 1];
        }
    }

    #region derived properties
    public double Scale
    {
        get
        {
            return (_imageHeight - (_topMargin + _bottomMargin)) / Math.Abs(_max - _min);
        }
    }

    public double StepWidth
    {
        get
        {
            return (_imageWidth - (_leftMargin + _rightMargin)) / (_data.Length - 1);
        }
    }

    public int MiddleY
    {
        get
        {
            int middleY = _imageHeight;
            middleY -= (int)(Math.Abs(_avg - _min) * Scale);
            middleY -= _bottomMargin;
            return middleY;
        }
    }

    public Rectangle StdDevRectangle
    {
        get
        {
            Rectangle rect = new Rectangle();
            rect.Width = _imageWidth - (_rightMargin + _leftMargin);
            rect.Height = (int) (_stdDevValue * Scale);
            rect.X = _leftMargin;
            rect.Y = MiddleY - (rect.Height / 2);
            return rect;
        }
    }
    #endregion

    public Point[] GetPoints()
    {
        Point[] points = new Point[_data.Length];

        if (!Double.IsInfinity(this.Scale))
        {
            double stepWidth = StepWidth;
            double scale = Scale;

            for (int i = 0; i < _data.Length; i++)
            {
                points[i] = new Point(
                    ((int)(i * stepWidth)) + _leftMargin,
                    _imageHeight - ((int)(Math.Abs(_data[i] - _min) * scale)) - _bottomMargin
                    );
            }
        }

        return points;
    }

    public double[] Data
    {
        get
        {
            return _data;
        }
        set
        {
            _data = value;
        }
    }

    private bool _setAvgRun;
    public void SetAvg()
    {
        if (!_setAvgRun)
        {
            for (int i = 0; i < _data.Length; i++)
            {
                _sum += _data[i];

                if (i == 0)
                {
                    _max = _data[i];
                    _min = _data[i];
                }
                else
                {
                    if (_max < _data[i]) _max = _data[i];
                    if (_min > _data[i]) _min = _data[i];
                }
            }

            _avg = _sum / _data.Length;

            if (_stdDev)
            {
                double var = 0;

                for (int i = 0; i < _data.Length; i++)
                {
                    var += Math.Pow(_data[i] - _avg, 2);
                }

                _stdDevValue = Math.Sqrt(var / _data.Length);
            }
            _setAvgRun = true;
        }
    }

    private void SetDefaultValues()
    {
        _bgColor = Color.White;
        _avgLineColor = Color.Gray;
        _lineColor = Color.Black;
        _stdDevColor = Color.FromArgb(240, 240, 240);

        _stdDev = true;

        _imageWidth = 200;
        _imageHeight = 60;

        _topMargin = 5;
        _bottomMargin = 5;
        _leftMargin = 5;
        _rightMargin = 35;
    }

    public int ImageWidth
    {
        get
        {
            return _imageWidth;
        }
        set
        {
            _imageWidth = value;
        }
    }

    public int ImageHeight
    {
        get
        {
            return _imageHeight;
        }
        set
        {
            _imageHeight = value;
        }
    }

    public Color LineColor
    {
        get
        {
            return _lineColor;
        }
        set
        {
            _lineColor = value;
        }
    }

    public Color AvgLineColor
    {
        get
        {
            return _avgLineColor;
        }
        set
        {
            _avgLineColor = value;
        }
    }

    public Color BgColor
    {
        get
        {
            return _bgColor;
        }
        set
        {
            _bgColor = value;
        }
    }

    public Color StdDevColor
    {
        get
        {
            return _stdDevColor;
        }
        set
        {
            _stdDevColor = value;
        }
    }

    public int LeftMargin
    {
        get
        {
            return _leftMargin;
        }
        set
        {
            _leftMargin = value;
        }
    }
    public int RightMargin
    {
        get
        {
            return _rightMargin;
        }
        set
        {
            _rightMargin = value;
        }
    }

    public bool StdDev
    {
        get
        {
            return _stdDev;
        }
        set
        {
            _stdDev = value;
        }
    }
}
