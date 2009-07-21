using System;
using NUnit.Framework;
using Marosoft.Testing;

namespace Fussball.SimplePointsSystem.Test
{
    [TestFixture]
    public class SinglesGameResultSpecifications : GameResultSpecificationsContext
    {
        [Test]
        public void The_master_wins_as_expected()
        {
            When_points_difference_is_greater_than_100();
            And_higher_beats_lower();
            No_point_changes();
        }

        [Test]
        public void Slightly_better_player_wins()
        {
            When_points_difference_is_less_than_100();
            And_higher_beats_lower();
            Winner_is_awarded(five_points);
            Looser_is_deducted(five_points);
        }

        [Test]
        public void Newbee_beats_master_unexpectedly()
        {
            When_points_difference_is_greater_than_100();
            And_lower_beats_higher();
            Winner_is_awarded(twenty_points);
            Looser_is_deducted(twenty_points);
        }

        [Test]
        public void Slightly_worse_player_wins()
        {
            When_points_difference_is_less_than_100();
            And_lower_beats_higher();
            Winner_is_awarded(ten_points);
            Looser_is_deducted(ten_points);            
        }

        Action When_points_difference_is_greater_than_100 = () =>
            SetPoints(1350, 1210);

        Action When_points_difference_is_less_than_100 = () =>
            SetPoints(1290, 1210);

        private static void SetPoints(int player1Points, int player2Points)
        {
            player1.Points = player1Points;
            player2.Points = player2Points;
        }

        Action No_point_changes = () =>
        {
            Winner_is_awarded(0);
            Looser_is_deducted(0);
        };

        Action And_higher_beats_lower = () =>
        {
            winner = player1;
            looser = player2;
            Register_game();
        };

        Action And_lower_beats_higher = () =>
        {
            winner = player2;
            looser = player1;
            Register_game();
        };

        static Action Register_game = () =>
        {
            old_winner_points = winner.Points;
            old_looser_points = looser.Points;
            GameRegistration.RegisterSimpleGame(winner, looser);
        };

        static Action<int> Winner_is_awarded = (awarded) =>
        {
            winner.Points.should_be(old_winner_points + awarded);
        };

        static Action<int> Looser_is_deducted = (deducted) =>
        {
            looser.Points.should_be(old_looser_points - deducted);
        };

        static readonly Player player1 = new Player("1");
        static readonly Player player2 = new Player("2");

        static Player winner;
        static Player looser;

        static int old_winner_points;
        static int old_looser_points;
    }
}