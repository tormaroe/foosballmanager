using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Fussball.SimplePointsSystem
{
    public class Player
    {
        private const string ID_ATTRIBUTE = "id";
        private const string NAME_ELEMENT = "name";
        private const string SINGLESLOST_ELEMENT = "singleslost";
        private const string SINGLESWON_ELEMENT = "singleswon";
        private const string DOUBLESLOST_ELEMENT = "doubleslost";
        private const string DOUBLESWON_ELEMENT = "doubleswon";
        private const string POINTS_ELEMENT = "points";
        private const string LEAGUEPOINTS_ELEMENT = "leaguepoints";
        private const string LEAGUEMATCHES_ELEMENT = "leaguematched";
        

        public Player(string name)
        {
            _id = Guid.NewGuid();
            _name = name;
            SinglesWon = 0;
            SinglesLost = 0;
            DoublesWon = 0;
            DoublesLost = 0;
            LeaguePoints = 0;
            LeagueMatchesPlayed = 0;
            _points = Constants.DEFAULT_PLAYER_POINTS;
        }

        private Player()
        {

        }

        public static Player GetPlayer(XmlNode node)
        {
            /**
             *  SAMPLE XML:
             * 
             * <player id="4588B3A3-AE5E-48db-9434-228AF2BAEF69">
             *      <name>Torbjørn Marø</name>
             *      <singleswon>0</singleswon>
             *      <singleslost>0</singleslost>
             *      <doubleswon>0</doubleswon>
             *      <doubleslost>0</doubleslost>
             *      <points>1200</points>
             * </player>
             */

            Player p = new Player();

            p._id = new Guid(node.Attributes[ID_ATTRIBUTE].Value);

            p._name = node.SelectSingleNode(NAME_ELEMENT).InnerText;
            p._numOfDoublesLost = Convert.ToInt32(node.SelectSingleNode(DOUBLESLOST_ELEMENT).InnerText);
            p._numOfDoublesWon = Convert.ToInt32(node.SelectSingleNode(DOUBLESWON_ELEMENT).InnerText);
            p._numOfSinglesLost = Convert.ToInt32(node.SelectSingleNode(SINGLESLOST_ELEMENT).InnerText);
            p._numOfSinglesWon = Convert.ToInt32(node.SelectSingleNode(SINGLESWON_ELEMENT).InnerText);
            p._points = Convert.ToInt32(node.SelectSingleNode(POINTS_ELEMENT).InnerText);

            if (node.SelectSingleNode(LEAGUEPOINTS_ELEMENT) != null)
            {
                p._leaguePoints = Convert.ToInt32(node.SelectSingleNode(LEAGUEPOINTS_ELEMENT).InnerText);
            }

            if (node.SelectSingleNode(LEAGUEMATCHES_ELEMENT) != null)
            {
                p._leagueMatchesPlayed = Convert.ToInt32(node.SelectSingleNode(LEAGUEMATCHES_ELEMENT).InnerText);
            }

            return p;
        }

        public string ToXml()
        {
            return string.Format("<player {0}=\"{1}\">", ID_ATTRIBUTE, _id.ToString())
                + Common.CreateElement(SINGLESLOST_ELEMENT, _numOfSinglesLost.ToString())
                + Common.CreateElement(SINGLESWON_ELEMENT, _numOfSinglesWon.ToString())
                + Common.CreateElement(DOUBLESLOST_ELEMENT, _numOfDoublesLost.ToString())
                + Common.CreateElement(DOUBLESWON_ELEMENT, _numOfDoublesWon.ToString())
                + Common.CreateElement(POINTS_ELEMENT, _points.ToString())
                + Common.CreateElement(LEAGUEPOINTS_ELEMENT, _leaguePoints.ToString())
                + Common.CreateElement(LEAGUEMATCHES_ELEMENT, _leagueMatchesPlayed.ToString())
                + Common.CreateElement(NAME_ELEMENT, _name)
                + "</player>";
        }

        #region properties

        private Guid _id;

        public Guid Id
        {
            get
            {
                return _id;
            }
        }


        private int _points;

        public int Points
        {
            get
            {
                return _points;
            }
            set
            {
                _points = value;
            }
        }

        private int _leagueMatchesPlayed;

        public int LeagueMatchesPlayed
        {
            get
            {
                return _leagueMatchesPlayed;
            }
            set
            {
                _leagueMatchesPlayed = value;
            }
        }


        private int _leaguePoints;

        public int LeaguePoints
        {
            get
            {
                return _leaguePoints;
            }
            set
            {
                _leaguePoints = value;
            }
        }


        private string _name;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        
        public int GamesPlayed
        {
            get
            {
                return SinglesWon + SinglesLost + DoublesWon + DoublesLost;
            }            
        }

        private int _numOfSinglesWon;

        public int SinglesWon
        {
            get
            {
                return _numOfSinglesWon;
            }
            set
            {
                _numOfSinglesWon = value;
            }
        }

        private int _numOfSinglesLost;

        public int SinglesLost
        {
            get
            {
                return _numOfSinglesLost;
            }
            set
            {
                _numOfSinglesLost = value;
            }
        }

        private int _numOfDoublesWon;

        public int DoublesWon
        {
            get
            {
                return _numOfDoublesWon;
            }
            set
            {
                _numOfDoublesWon = value;
            }
        }

        private int _numOfDoublesLost;

        public int DoublesLost
        {
            get
            {
                return _numOfDoublesLost;
            }
            set
            {
                _numOfDoublesLost = value;
            }
        }


        #endregion
    }
}
