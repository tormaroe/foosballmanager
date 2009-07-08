using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Fussball.SimplePointsSystem
{
    public class LeagueMatch
    {
        private const string ID_ELEMENT = "id";
        private const string PLAYER1_ELEMENT = "player1";
        private const string PLAYER2_ELEMENT = "player2";
        private const string WHEN_ELEMENT = "when";
        private const string WINNER_ELEMENT = "winner";

        public LeagueMatch(string player1Name, string player2Name)
        {
            _playerName1 = player1Name;
            _playerName2 = player2Name;
            _playedWhen = null;
            _winner = null;
            _id = Guid.NewGuid();
        }

        private LeagueMatch()
        {

        }

        public static LeagueMatch GetLeagueMatch(XmlNode node)
        {
            LeagueMatch lm = new LeagueMatch();

            lm.Id = new Guid(node.SelectSingleNode(ID_ELEMENT).InnerText);
            lm.PlayerName1 = node.SelectSingleNode(PLAYER1_ELEMENT).InnerText;
            lm.PlayerName2 = node.SelectSingleNode(PLAYER2_ELEMENT).InnerText;
            if(node.SelectSingleNode(WHEN_ELEMENT).InnerText != string.Empty)
                lm.PlayedWhen = Common.GetDateFromUnixTime(Convert.ToDouble(node.SelectSingleNode(WHEN_ELEMENT).InnerText));
            lm.Winner = node.SelectSingleNode(WINNER_ELEMENT).InnerText;

            return lm;
        }

        public string ToXml()
        {
            return "<leagueMatch>"
                + Common.CreateElement(ID_ELEMENT, Id.ToString())
                + Common.CreateElement(PLAYER1_ELEMENT, PlayerName1)
                + Common.CreateElement(PLAYER2_ELEMENT, PlayerName2)
                + Common.CreateElement(WHEN_ELEMENT, (PlayedWhen.HasValue ? Common.GetUnixTime(PlayedWhen.Value).ToString() : string.Empty))
                + Common.CreateElement(WINNER_ELEMENT, Winner)
                + "</leagueMatch>";
        }

        #region properties
        private Guid _id;

        public Guid Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }


        private string _playerName1;

        public string PlayerName1
        {
            get
            {
                return _playerName1;
            }
            set
            {
                _playerName1 = value;
            }
        }
        private string _playerName2;

        public string PlayerName2
        {
            get
            {
                return _playerName2;
            }
            set
            {
                _playerName2 = value;
            }
        }

        private DateTime? _playedWhen;

        public DateTime? PlayedWhen
        {
            get
            {
                return _playedWhen;
            }
            set
            {
                _playedWhen = value;
            }
        }

        private string _winner;

        public string Winner
        {
            get
            {
                return _winner;
            }
            set
            {
                _winner = value;
            }
        }
        #endregion
    }


}
