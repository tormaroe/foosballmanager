using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Fussball.SimplePointsSystem
{
    public static class LeagueMatchesXML
    {
        private const string ID_ELEMENT = "id";
        private const string PLAYER1_ELEMENT = "player1";
        private const string PLAYER2_ELEMENT = "player2";
        private const string WHEN_ELEMENT = "when";
        private const string WINNER_ELEMENT = "winner";

        public static LeagueMatches CreateLeagueMatchesFromXml(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            var leagueMatches = new LeagueMatches();
            leagueMatches.AllMatches = CreateLeagueMatchesFromXml(doc.DocumentElement);
            return leagueMatches;
        }


        private static List<LeagueMatch> CreateLeagueMatchesFromXml(XmlNode node)
        {
            List<LeagueMatch> matches = new List<LeagueMatch>();

            XmlNodeList matchNodes = node.ChildNodes;
            foreach (XmlNode matchNode in matchNodes)
            {
                LeagueMatch lm = CreateSingleMatchFromXml(matchNode);
                matches.Add(lm);
            }

            return matches;
        }

        public static string ToXml(LeagueMatches matches)
        {
            StringBuilder xml = new StringBuilder("<leagueMatches>");

            foreach (LeagueMatch lm in matches.AllMatches)
            {
                xml.Append(MatchToXml(lm));
            }

            xml.Append("</leagueMatches>");

            return xml.ToString();
        }

        private static LeagueMatch CreateSingleMatchFromXml(XmlNode xml)
        {
            LeagueMatch lm = new LeagueMatch();

            lm.Id = new Guid(xml.SelectSingleNode(ID_ELEMENT).InnerText);
            lm.PlayerName1 = xml.SelectSingleNode(PLAYER1_ELEMENT).InnerText;
            lm.PlayerName2 = xml.SelectSingleNode(PLAYER2_ELEMENT).InnerText;
            if (xml.SelectSingleNode(WHEN_ELEMENT).InnerText != string.Empty)
                lm.PlayedWhen = UnixTime.GetDateFromUnixTime(Convert.ToDouble(xml.SelectSingleNode(WHEN_ELEMENT).InnerText));
            lm.Winner = xml.SelectSingleNode(WINNER_ELEMENT).InnerText;

            return lm;
        }

        private static string MatchToXml(LeagueMatch match)
        {
            return "<leagueMatch>"
                + CommonXml.CreateElement(ID_ELEMENT, match.Id.ToString())
                + CommonXml.CreateElement(PLAYER1_ELEMENT, match.PlayerName1)
                + CommonXml.CreateElement(PLAYER2_ELEMENT, match.PlayerName2)
                + CommonXml.CreateElement(WHEN_ELEMENT, (match.PlayedWhen.HasValue ? UnixTime.GetUnixTime(match.PlayedWhen.Value).ToString() : string.Empty))
                + CommonXml.CreateElement(WINNER_ELEMENT, match.Winner)
                + "</leagueMatch>";
        }
    }
}
