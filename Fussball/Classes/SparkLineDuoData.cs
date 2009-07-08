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
/// Summary description for SparkLineDuoData
/// </summary>
public class SparkLineDuoData
{
    private Color _bgColor;
    private Color _lineColorSet1;
    private Color _lineColorSet2;
    private double[] _dataSet1;
    private double[] _dataSet2;
    private int _imageWidth;
    private int _imageHeight;
    private int _topMargin;
    private int _bottomMargin;
    private int _leftMargin;
    private int _rightMargin;
    private double _max;
    private double _min;

    private string _set1Text;

    public string Set1Text
    {
        get
        {
            return _set1Text;
        }
        set
        {
            _set1Text = value;
        }
    }

    private string _set2Text;

    public string Set2Text
    {
        get
        {
            return _set2Text;
        }
        set
        {
            _set2Text = value;
        }
    }


    public SparkLineDuoData()
    {
        SetDefaultValues();
    }

    public double LastValueSet1
    {
        get
        {
            return _dataSet1[_dataSet1.Length - 1];
        }
    }
    public double LastValueSet2
    {
        get
        {
            return _dataSet2[_dataSet2.Length - 1];
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

    public double StepWidthSet1
    {
        get
        {
            return (_imageWidth - (_leftMargin + _rightMargin)) / (_dataSet1.Length - 1);
        }
    }

    public double StepWidthSet2
    {
        get
        {
            return (_imageWidth - (_leftMargin + _rightMargin)) / (_dataSet2.Length - 1);
        }
    }

    #endregion

    public Point[] GetPoints(int setNumber)
    {
        double[] set = (setNumber == 1 ? _dataSet1 : _dataSet2);

        Point[] points = new Point[set.Length];

        if (!Double.IsInfinity(this.Scale))
        {
            double stepWidth = (setNumber == 1 ? StepWidthSet1 : StepWidthSet2);
            double scale = Scale;

            for (int i = 0; i < set.Length; i++)
            {
                points[i] = new Point(
                    ((int)(i * stepWidth)) + _leftMargin,
                    _imageHeight - ((int)(Math.Abs(set[i] - _min) * scale)) - _bottomMargin
                    );
            }
        }

        return points;
    }

    public double[] DataSet1
    {
        get
        {
            return _dataSet1;
        }
        set
        {
            _dataSet1 = value;
        }
    }
    public double[] DataSet2
    {
        get
        {
            return _dataSet2;
        }
        set
        {
            _dataSet2 = value;
        }
    }

    private bool _setAvgRun;
    public void SetAvg()
    {
        if (!_setAvgRun)
        {
            FindMinAndMax(_dataSet1);
            FindMinAndMax(_dataSet2);

            _setAvgRun = true;
        }
    }

    private void FindMinAndMax(double[] set)
    {
        for (int i = 0; i < set.Length; i++)
        {
            if (i == 0 && _max == 0 && _min == 0)
            {
                _max = set[i];
                _min = set[i];
            }
            else
            {
                if (_max < set[i]) _max = set[i];
                if (_min > set[i]) _min = set[i];
            }
        }
    }

    private void SetDefaultValues()
    {
        _bgColor = Color.White;
        _lineColorSet1 = Color.Blue;
        _lineColorSet2 = Color.Green;
        
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

    public Color LineColorSet1
    {
        get
        {
            return _lineColorSet1;
        }
        set
        {
            _lineColorSet1 = value;
        }
    }
    
    public Color LineColorSet2
    {
        get
        {
            return _lineColorSet2;
        }
        set
        {
            _lineColorSet2 = value;
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
}
