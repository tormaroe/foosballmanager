using System;
using System.Collections.Generic;
using System.Text;

namespace Fussball.SimplePointsSystem
{
    public class League
    {
        private LeagueMatches _matches;

        public LeagueMatches Matches
        {
            get
            {
                return _matches;
            }
            set
            {
                _matches = value;
            }
        }

        private Players _players;

        public Players Players
        {
            get
            {
                return _players;
            }
            set
            {
                _players = value;
            }
        }


        public League(LeagueMatches matches, Players players)
        {
            _matches = matches;
            _players = players;
        }

        public static League Instance
        {
            get
            {
                //testing with application variable instead of static
                return System.Web.HttpContext.Current.Application["League"] as League;
            }
            set
            {
                System.Web.HttpContext.Current.Application["League"] = value;
            }
        }

        public int GenerateMatches(int leagueSize)
        {
            _matches = new LeagueMatches();
            int generatedMatches = 0;
            

            foreach (Player p1 in _players.AllPlayers)
            {
                foreach (Player p2 in _players.AllPlayers)
                {
                    if (p2.Name != p1.Name)
                    {
                        for (int i = 0; i < leagueSize; i++)
                        {
                            LeagueMatch m = new LeagueMatch(p1.Name, p2.Name);
                            _matches.Add(m);
                            generatedMatches++;
                        }
                    }
                }
            }
            return generatedMatches;
        }


        public bool TryAddMatchResult(Player winner, Player looser)
        {
            if (_matches == null)
                throw new Exception("LeagueMatches is null, can't add the match to league");
            
            foreach (LeagueMatch match in _matches.AllMatches)
            {
                if (string.IsNullOrEmpty(match.Winner))
                {
                    if (
                        (match.PlayerName1.Equals(winner.Name) && match.PlayerName2.Equals(looser.Name))
                        ||
                        (match.PlayerName1.Equals(looser.Name) && match.PlayerName2.Equals(winner.Name))
                        )
                    {
                        match.PlayedWhen = DateTime.Now;
                        match.Winner = winner.Name;
                        return true;
                    }
                }
            }
            return false;
        }

        public void ResetPlayerPoints()
        {
            foreach (Player p in _players.AllPlayers)
            {
                p.LeaguePoints = 0;
                p.LeagueMatchesPlayed = 0;
            }
        }

        public void ClearResult(Guid matchId)
        {
            LeagueMatch match = _matches.GetById(matchId);

            Player winner = _players.GetByName(match.Winner);
            Player looser = _players.GetByName((match.Winner == match.PlayerName1 ? match.PlayerName2 : match.PlayerName1));

            winner.LeaguePoints = winner.LeaguePoints - 1;
            winner.LeagueMatchesPlayed = winner.LeagueMatchesPlayed - 1;
            looser.LeagueMatchesPlayed = looser.LeagueMatchesPlayed - 1;

            match.Winner = null;
            match.PlayedWhen = null;
        }
    }
}
