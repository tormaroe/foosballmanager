using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Fussball.SimplePointsSystem
{
    public class LeagueMatch
    {
        
        public LeagueMatch(string player1Name, string player2Name)
        {
            PlayerName1 = player1Name;
            PlayerName2 = player2Name;
            PlayedWhen = null;
            Winner = null;
            Id = Guid.NewGuid();
        }

        internal LeagueMatch()
        {

        }
        
        public Guid Id  { get; internal set; }
        public string PlayerName1 { get; internal set; }
        public string PlayerName2 { get; internal set; }
        public DateTime? PlayedWhen { get; internal set; }
        public string Winner { get; internal set; }

        public bool IsNotPlayedYet
        {
            get
            {
                return string.IsNullOrEmpty(Winner);
            }
        }

        public bool IsMatchBetween(Player winner, Player looser)
        {
            return (PlayerName1.Equals(winner.Name) && PlayerName2.Equals(looser.Name))
                    ||
                    (PlayerName1.Equals(looser.Name) && PlayerName2.Equals(winner.Name));
        }

        public void SetResult(Player winner)
        {
            PlayedWhen = DateTime.Now;
            Winner = winner.Name;
        }

        public void ClearResult()
        {
            Winner = null;
            PlayedWhen = null;
        }
    }


}
