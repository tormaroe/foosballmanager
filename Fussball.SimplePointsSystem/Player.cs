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
            Name = name;
            SinglesWon = 0;
            SinglesLost = 0;
            DoublesWon = 0;
            DoublesLost = 0;
            LeaguePoints = 0;
            LeagueMatchesPlayed = 0;
            Points = Constants.DEFAULT_PLAYER_POINTS;
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

            p.Name = node.SelectSingleNode(NAME_ELEMENT).InnerText;
            p.DoublesLost = Convert.ToInt32(node.SelectSingleNode(DOUBLESLOST_ELEMENT).InnerText);
            p.DoublesWon = Convert.ToInt32(node.SelectSingleNode(DOUBLESWON_ELEMENT).InnerText);
            p.SinglesLost = Convert.ToInt32(node.SelectSingleNode(SINGLESLOST_ELEMENT).InnerText);
            p.SinglesWon = Convert.ToInt32(node.SelectSingleNode(SINGLESWON_ELEMENT).InnerText);
            p.Points = Convert.ToInt32(node.SelectSingleNode(POINTS_ELEMENT).InnerText);

            if (node.SelectSingleNode(LEAGUEPOINTS_ELEMENT) != null)
            {
                p.LeaguePoints = Convert.ToInt32(node.SelectSingleNode(LEAGUEPOINTS_ELEMENT).InnerText);
            }

            if (node.SelectSingleNode(LEAGUEMATCHES_ELEMENT) != null)
            {
                p.LeagueMatchesPlayed = Convert.ToInt32(node.SelectSingleNode(LEAGUEMATCHES_ELEMENT).InnerText);
            }

            return p;
        }

        public string ToXml()
        {
            return string.Format("<player {0}=\"{1}\">", ID_ATTRIBUTE, _id.ToString())
                + CommonXml.CreateElement(SINGLESLOST_ELEMENT, SinglesLost.ToString())
                + CommonXml.CreateElement(SINGLESWON_ELEMENT, SinglesWon.ToString())
                + CommonXml.CreateElement(DOUBLESLOST_ELEMENT, DoublesLost.ToString())
                + CommonXml.CreateElement(DOUBLESWON_ELEMENT, DoublesWon.ToString())
                + CommonXml.CreateElement(POINTS_ELEMENT, Points.ToString())
                + CommonXml.CreateElement(LEAGUEPOINTS_ELEMENT, LeaguePoints.ToString())
                + CommonXml.CreateElement(LEAGUEMATCHES_ELEMENT, LeagueMatchesPlayed.ToString())
                + CommonXml.CreateElement(NAME_ELEMENT, Name)
                + "</player>";
        }

        private Guid _id;

        public Guid Id
        {
            get
            {
                return _id;
            }
        }
        
        public int Points { get; set; }
        public int LeagueMatchesPlayed { get; set; }        
        public int LeaguePoints { get; set; }        
        public string Name { get; set; }
        
        public int GamesPlayed
        {
            get
            {
                return SinglesWon + SinglesLost + DoublesWon + DoublesLost;
            }            
        }

        public int SinglesWon { get; set; }
        public int SinglesLost { get; set; }
        public int DoublesWon { get; set; }
        public int DoublesLost { get; set; }

    }
}
