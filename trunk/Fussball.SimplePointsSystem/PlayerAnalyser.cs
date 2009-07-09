using System;

namespace Fussball.SimplePointsSystem
{
    internal class PlayerAnalyser
    {
        private AuditTrail _auditTrail;
        private AnalysePlayerResult _result;
        private string _playerScoreChangedText;

        public PlayerAnalyser(AuditTrail auditTrail)
        {
            _auditTrail = auditTrail;
        }

        public AnalysePlayerResult Analyse(string playerName)
        {
            _playerScoreChangedText = string.Format("{0}'s score changed to ", playerName);

            _result = new AnalysePlayerResult();
            _result.Points.Add(Constants.DEFAULT_PLAYER_POINTS);

            foreach (AuditTrailItem item in _auditTrail.Items)
            {
                if (item.What.StartsWith(_playerScoreChangedText))
                {
                    _result.Points.Add(GetPointsChange(item));
                }
            }

            return _result;
        }

        private int GetPointsChange(AuditTrailItem item)
        {
            string sPoints = item.What.Substring(_playerScoreChangedText.Length);
            sPoints = sPoints.Substring(0, sPoints.IndexOf(" "));
            return Int32.Parse(sPoints);
        }
    }
}
