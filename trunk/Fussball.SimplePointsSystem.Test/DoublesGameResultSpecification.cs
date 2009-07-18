using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Fussball.SimplePointsSystem.Test
{
    [TestFixture]
    public class DoublesGameResultSpecification : GameResultSpecificationsContext
    {
        [Test]
        public void The_master_team_wins_as_expected()
        {
            When_points_difference_is_greater_than_100();
            And_higher_beats_lower();
            No_point_changes();
        }

        [Test]
        public void Slightly_better_team_wins()
        {
            When_points_difference_is_less_than_100();
            And_higher_beats_lower();
            Winners_are_awarded(three_points);
            Loosers_are_deducted(three_points);
        }

        [Test]
        public void Newbee_team_beats_master_team_unexpectedly()
        {
            When_points_difference_is_greater_than_100();
            And_lower_beats_higher();
            Winners_are_awarded(ten_points);
            Loosers_are_deducted(ten_points);
        }

        [Test]
        public void Slightly_worse_team_wins()
        {
            When_points_difference_is_less_than_100();
            And_lower_beats_higher();
            Winners_are_awarded(five_points);
            Loosers_are_deducted(five_points);
        }

        Action When_points_difference_is_less_than_100 = () =>
        {
            teamA_player1.Points = 1500;
            teamA_player2.Points = 1160;
            teamB_player1.Points = 1500;
            teamB_player2.Points = 1100;
        };

        Action When_points_difference_is_greater_than_100 = () =>
        {
            teamA_player1.Points = 1800;
            teamA_player2.Points = 1200;
            teamB_player1.Points = 1500;
            teamB_player2.Points = 1100;
        };

        Action And_higher_beats_lower = () =>
        {
            winner1 = teamA_player1;
            winner2 = teamA_player2;
            looser1 = teamB_player1;
            looser2 = teamB_player2;
            Register_game();
        };

        Action And_lower_beats_higher = () =>
        {
            winner1 = teamB_player1;
            winner2 = teamB_player2;
            looser1 = teamA_player1;
            looser2 = teamA_player2;
            Register_game();
        };

        static Action Register_game = () =>
        {
            old_winner1_points = winner1.Points;
            old_winner2_points = winner2.Points;
            old_looser1_points = looser1.Points;
            old_looser2_points = looser2.Points;
            GameRegistration.RegisterDoubleGame(winner1, winner2, looser1, looser2);
        };

        Action No_point_changes = () =>
        {
            Winners_are_awarded(0);
            Loosers_are_deducted(0);
        };

        static Action<int> Winners_are_awarded = (awarded) =>
        {
            winner1.Points.should_be(old_winner1_points + awarded);
            winner2.Points.should_be(old_winner2_points + awarded);
        };

        static Action<int> Loosers_are_deducted = (deducted) =>
        {
            looser1.Points.should_be(old_looser1_points - deducted);
            looser2.Points.should_be(old_looser2_points - deducted);
        };

        static readonly Player teamA_player1 = new Player("TeamA 1");
        static readonly Player teamA_player2 = new Player("TeamA 2");
        static readonly Player teamB_player1 = new Player("TeamB 1");
        static readonly Player teamB_player2 = new Player("TeamB 2");

        static Player winner1;
        static Player looser1;
        static Player winner2;
        static Player looser2;

        static int old_winner1_points;
        static int old_looser1_points;
        static int old_winner2_points;
        static int old_looser2_points;
    }
}
