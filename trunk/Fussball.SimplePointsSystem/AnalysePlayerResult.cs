using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;

namespace Fussball.SimplePointsSystem
{
    public class AnalysePlayerResult
    {
        public AnalysePlayerResult()
        {
            Points = new List<double>();
        }
        public List<double> Points { get; set; }
    }
}
