using System;
using NUnit.Framework;
using Marosoft.Testing;

namespace Fussball.SimplePointsSystem.Test
{
    [TestFixture]
    public class LeagueGenerationSpecification : LeagueContext
    {
        [Test]
        public void Two_players_and_minimum_league()
        {
            Given_the_number_of_players_is(2);
            When_generating_a_leage_of_size(1);
            Then_the_number_of_matches_should_be(2);
        }

        [Test]
        public void Two_players_and_really_big_league()
        {
            Given_the_number_of_players_is(2);
            When_generating_a_leage_of_size(10);
            Then_the_number_of_matches_should_be(20);
        }

        [Test]
        public void Three_players_and_minimum_league()
        {
            Given_the_number_of_players_is(3);
            When_generating_a_leage_of_size(1);
            Then_the_number_of_matches_should_be(6);
        }

        [Test]
        public void Five_players_and_double_league()
        {
            Given_the_number_of_players_is(5);
            When_generating_a_leage_of_size(2);
            Then_the_number_of_matches_should_be(40);
        }
       
        Action<int> Then_the_number_of_matches_should_be = (num) =>
            league.Matches.AllMatches.Count.should_be(num);

    }
}