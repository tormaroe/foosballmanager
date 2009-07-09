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
            internal set
            {
                _matches = value;
            }
        }

        public LeagueMatches()
        {
            _matches = new List<LeagueMatch>();
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
