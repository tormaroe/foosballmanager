using NUnit.Framework;

namespace Fussball.SimplePointsSystem.Test
{
    [TestFixture]
    public class LeagueOperationsTests : LeagueContext
    {
        [Test]
        public void Test_initial_conditions()
        {
            Given_the_number_of_players_is(3);
            When_generating_a_leage_of_size(2);

            league.Matches.AllMatches.ForEach((match) 
                => match.IsNotPlayedYet.should_be_true());
        }

        [Test]
        public void Should_be_able_to_register_a_result()
        {
            Given_the_number_of_players_is(2);
            When_generating_a_leage_of_size(1);
            league.TryAddMatchResult(get_player(1), get_player(2)).should_be_true();
        }

        [Test]
        public void Should_not_be_able_to_add_too_many_matches_between_players()
        {
            Given_the_number_of_players_is(2);
            When_generating_a_leage_of_size(1);
            league.TryAddMatchResult(get_player(1), get_player(2)).should_be_true();
            league.TryAddMatchResult(get_player(1), get_player(2)).should_be_true();
            league.TryAddMatchResult(get_player(1), get_player(2)).should_be_false();
        }

        [Test]
        [Ignore("It actually didn't work like that. League has bad cohesion, should be mended.")]
        public void Should_get_league_points_and_league_match_count()
        {
            Given_the_number_of_players_is(2);
            When_generating_a_leage_of_size(1);
            league.TryAddMatchResult(get_player(1), get_player(2));
            league.TryAddMatchResult(get_player(2), get_player(1));

            get_player(1).LeagueMatchesPlayed.should_be(2);
        }
    }
}
