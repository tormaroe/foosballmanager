using System;

namespace Fussball.SimplePointsSystem
{
    public class MatchAnalyser
    {
        private const string FIRST_WON_A_SINGLES_MATCH_AGAINST_SECOND_FORMATSTRING = "{0} won a singles match agains {1}.";
        private const string PLAYERS_SCORE_CHANGED_TO_FORMATSTRING = "{0}'s score changed to";
        private AuditTrail _AuditTrail;

        private bool _player1WasTheWinner;
        private AnalyseResult _result;
        private string _winnerWhatPrefix;
        private string _looserWhatPrefix;


        public MatchAnalyser(AuditTrail auditTrail)
        {
            _AuditTrail = auditTrail;
        }

        public AnalyseResult AnalyseMatches(string playerName1, string playerName2)
        {
            _result = new AnalyseResult();

            string player1WonText = string.Format(FIRST_WON_A_SINGLES_MATCH_AGAINST_SECOND_FORMATSTRING,
                playerName1,
                playerName2);
            string player2WonText = string.Format(FIRST_WON_A_SINGLES_MATCH_AGAINST_SECOND_FORMATSTRING,
                playerName2,
                playerName1);

            foreach (AuditTrailItem item in _AuditTrail.Items)
            {
                if (item.What.Equals(player1WonText))
                {
                    _player1WasTheWinner = true;
                    _result.GamesWonByPlayer1++;
                    AddPointsWonAndLost(item, playerName1, playerName2);
                }
                else if (item.What.Equals(player2WonText))
                {
                    _player1WasTheWinner = false;
                    _result.GamesWonByPlayer2++;
                    AddPointsWonAndLost(item, playerName2, playerName1);
                }
            }

            return _result;
        }

        
        private void AddPointsWonAndLost(AuditTrailItem matchItem, string winnerName, string looserName)
        {
            _winnerWhatPrefix = string.Format(PLAYERS_SCORE_CHANGED_TO_FORMATSTRING, winnerName);
            _looserWhatPrefix = string.Format(PLAYERS_SCORE_CHANGED_TO_FORMATSTRING, looserName);

            foreach (AuditTrailItem item in _AuditTrail.Items)
            {
                if (matchItem.IsRegisteredAtTheSameTimeAs(item) && AreNotTheSame(matchItem, item))
                {
                    AddPointsWonAndLostForItem(item);
                }
            }
        }

        private void AddPointsWonAndLostForItem(AuditTrailItem item)
        {
            int pointChange = GetPointsChange(item);

            if (item.What.StartsWith(_winnerWhatPrefix))
            {
                AddPointsEarned(pointChange);
            }
            else if (item.What.StartsWith(_looserWhatPrefix))
            {
                AddPointsLost(pointChange);
            }
        }

        private static bool AreNotTheSame(AuditTrailItem matchItem, AuditTrailItem item)
        {
            return !matchItem.What.Equals(item.What);
        }

        private static int GetPointsChange(AuditTrailItem item)
        {
            int changeStartIndex = item.What.IndexOf("(") + 1;
            int changeEndIndex = item.What.IndexOf(" points was") - 1;

            int pointsEarned = Int32.Parse(item.What.Substring(changeStartIndex, changeEndIndex - changeStartIndex + 1));
            return pointsEarned;
        }

        private void AddPointsEarned(int pointsEarned)
        {
            if (_player1WasTheWinner)
            {
                _result.PointsEarnedByPlayer1 += pointsEarned;
            }
            else
            {
                _result.PointsEarnedByPlayer2 += pointsEarned;
            }
        }

        private void AddPointsLost(int pointsEarned)
        {
            if (_player1WasTheWinner)
            {
                _result.PointsLostByPlayer2 += pointsEarned;
            }
            else
            {
                _result.PointsLostByPlayer1 += pointsEarned;
            }
        }
    }
}
