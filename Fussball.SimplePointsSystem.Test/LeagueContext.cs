using System;
using NUnit.Framework;

namespace Fussball.SimplePointsSystem.Test
{
    public class LeagueContext
    {
        protected static Players players;
        protected static League league;

        [SetUp]
        public void SetUp()
        {
            players = new Players();
        }

        protected Action<int> Given_the_number_of_players_is = (num) =>
        {
            for (int i = 1; i <= num; i++)
                players.Add(new Player(i.ToString()));
        };

        protected Func<int, Player> get_player = (index) =>
            league.Players.GetByName(index.ToString());

        protected Action<int> When_generating_a_leage_of_size = (size) =>
        {
            league = new League(new LeagueMatches(), players);
            league.GenerateMatches(size);
        };
    }
}
