using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Fussball.SimplePointsSystem
{
    public class LeagueMatches
    {
        private List<LeagueMatch> _matches;

        public List<LeagueMatch> AllMatches
        {
            get
            {
                return _matches;
            }
        }

        public LeagueMatches()
        {
            _matches = new List<LeagueMatch>();
        }

        public LeagueMatches(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            LoadFromXml(doc.DocumentElement);
        }

        public LeagueMatches(XmlNode node)
        {
            LoadFromXml(node);
        }

        private void LoadFromXml(XmlNode node)
        {
            _matches = new List<LeagueMatch>();

            XmlNodeList matchNodes = node.ChildNodes;
            foreach (XmlNode matchNode in matchNodes)
            {
                LeagueMatch lm = LeagueMatch.GetLeagueMatch(matchNode);
                _matches.Add(lm);
            }
        }

        public string ToXml()
        {
            StringBuilder xml = new StringBuilder("<leagueMatches>");

            foreach (LeagueMatch lm in _matches)
            {
                xml.Append(lm.ToXml());
            }

            xml.Append("</leagueMatches>");

            return xml.ToString();
        }

        public void Add(LeagueMatch leagueMatch)
        {
            _matches.Add(leagueMatch);
        }

        internal LeagueMatch GetById(Guid matchId)
        {
            foreach (LeagueMatch match in _matches)
            {
                if (match.Id.Equals(matchId))
                {
                    return match;                    
                }
            }
            throw new Exception("Can't find match");
        }
    }
}
