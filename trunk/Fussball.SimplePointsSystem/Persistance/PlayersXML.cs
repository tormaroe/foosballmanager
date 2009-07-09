using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Fussball.SimplePointsSystem
{
    public static class PlayersXML
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


        public static Players CreatePlayersFromXml(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            return CreatePlayersFromXml(doc.DocumentElement);
        }

        private static Players CreatePlayersFromXml(XmlNode node)
        {
            var players = new Players();
            players._players = new Dictionary<Guid, Player>();

            XmlNodeList playersNodes = node.ChildNodes;
            foreach (XmlNode playerNode in playersNodes)
            {
                Player p = GetPlayer(playerNode);
                players._players.Add(p.Id, p);
            }
            return players;
        }

        private static Player GetPlayer(XmlNode node)
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

            p.Id = new Guid(node.Attributes[ID_ATTRIBUTE].Value);

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

        public static string ToXml(Players players)
        {
            StringBuilder xml = new StringBuilder("<players>");

            foreach (KeyValuePair<Guid, Player> playerInfo in players._players)
            {
                xml.Append(PlayerToXml(playerInfo.Value));
            }

            xml.Append("</players>");

            return xml.ToString();
        }

        internal static string PlayerToXml(Player player)
        {
            return string.Format("<player {0}=\"{1}\">", ID_ATTRIBUTE, player.Id.ToString())
                + CommonXml.CreateElement(SINGLESLOST_ELEMENT, player.SinglesLost.ToString())
                + CommonXml.CreateElement(SINGLESWON_ELEMENT, player.SinglesWon.ToString())
                + CommonXml.CreateElement(DOUBLESLOST_ELEMENT, player.DoublesLost.ToString())
                + CommonXml.CreateElement(DOUBLESWON_ELEMENT, player.DoublesWon.ToString())
                + CommonXml.CreateElement(POINTS_ELEMENT, player.Points.ToString())
                + CommonXml.CreateElement(LEAGUEPOINTS_ELEMENT, player.LeaguePoints.ToString())
                + CommonXml.CreateElement(LEAGUEMATCHES_ELEMENT, player.LeagueMatchesPlayed.ToString())
                + CommonXml.CreateElement(NAME_ELEMENT, player.Name)
                + "</player>";
        }
    }
}
