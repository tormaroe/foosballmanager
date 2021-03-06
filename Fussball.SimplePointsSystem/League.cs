using System;

namespace Fussball.SimplePointsSystem
{
    public class League
    {
        public LeagueMatches Matches { get; set; }
        public Players Players { get; set; }

        public League(LeagueMatches matches, Players players)
        {
            Matches = matches;
            Players = players;
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
            Matches = new LeagueMatches();

            foreach (Player p1 in Players.AllPlayers)
            {
                foreach (Player p2 in Players.AllPlayers)
                {
                    if (is_different_players(p1, p2))
                    {
                        GenerateMatchesForPlayers(leagueSize, p1, p2);
                    }
                }
            }

            ResetPlayerPoints();

            return Matches.AllMatches.Count;
        }

        private static bool is_different_players(Player p1, Player p2)
        {
            return p2.Name != p1.Name;
        }

        private void GenerateMatchesForPlayers(int numberToGenerat, Player p1, Player p2)
        {
            for (int i = 0; i < numberToGenerat; i++)
            {
                LeagueMatch m = new LeagueMatch(p1.Name, p2.Name);
                Matches.Add(m);
            }
        }

        public bool TryAddMatchResult(Player winner, Player looser)
        {
            if (Matches == null)
                throw new Exception("LeagueMatches is null, can't add the match to league");
            
            foreach (LeagueMatch match in Matches.AllMatches)
            {
                if (match.IsNotPlayedYet && match.IsMatchBetween(winner, looser))
                {
                    match.SetResult(winner);
                    winner.LeaguePoints++;
                    winner.LeagueMatchesPlayed++;
                    looser.LeagueMatchesPlayed++;
                    return true;
                }
            }
            return false;
        }

        private void ResetPlayerPoints()
        {
            foreach (Player p in Players.AllPlayers)
            {
                p.LeaguePoints = 0;
                p.LeagueMatchesPlayed = 0;
            }
        }

        public void ClearResult(Guid matchId)
        {
            LeagueMatch match = Matches.GetById(matchId);

            Player winner = Players.GetByName(match.Winner);
            Player looser = Players.GetByName((match.Winner == match.PlayerName1 ? match.PlayerName2 : match.PlayerName1));

            winner.LeaguePoints = winner.LeaguePoints - 1;
            winner.LeagueMatchesPlayed = winner.LeagueMatchesPlayed - 1;
            looser.LeagueMatchesPlayed = looser.LeagueMatchesPlayed - 1;

            match.ClearResult();
        }
    }
}
