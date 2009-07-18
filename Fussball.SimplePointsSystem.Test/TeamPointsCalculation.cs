using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Fussball.SimplePointsSystem.Test
{
    [TestFixture]
    public class TeamPointsCalculation
    {
        [Test]
        public void Teams_where_both_players_contribute_to_points()
        {
            Points_plus_points_should_give_team_points(1800, 1200, 1950);
            Points_plus_points_should_give_team_points(1500, 1100, 1600);
            Points_plus_points_should_give_team_points(1300, 1000, 1350);
        }

        [Test]
        public void Only_one_player_contributes_to_team_points()
        {
            Points_plus_points_should_give_team_points(1200, 900, 1200);
            Points_plus_points_should_give_team_points(1200, 800, 1200);
        }

        [Test]
        public void Both_players_below_treshold()
        {
            Points_plus_points_should_give_team_points(888, 888, 888);
        }

        Action<int, int, int> Points_plus_points_should_give_team_points = 
            (p1_points, p2_points, teamPoints) =>
                GameCalculator.GetTeamPoints(player_with_points(p1_points),player_with_points(p2_points))
                .should_be(teamPoints);

        static Func<int, Player> player_with_points = (points) =>
            new Player { Points = points };
    }
}
