using System;
using NUnit.Framework;
using Marosoft.Testing;

namespace Fussball.SimplePointsSystem.Test
{
    [TestFixture]
    public class MatchAnalyserSpecification
    {
        [Test]
        public void A_single_game_is_played()
        {
            GameRegistration.RegisterSimpleGame(player1, player2);

            When_analysing_matches();
            
            analyseResult.GamesWonByPlayer1.should_be(1);
            analyseResult.GamesWonByPlayer2.should_be(0);
            analyseResult.PointsEarnedByPlayer1.should_be(5);
            analyseResult.PointsEarnedByPlayer2.should_be(0);
            analyseResult.PointsLostByPlayer1.should_be(0);
            analyseResult.PointsLostByPlayer2.should_be(5);
        }

        [Test]
        [Category("LongRunning")]
        public void Several_matches_between_players()
        {
            /**
             * Due to the way AuditTrail works we need to pause a full second 
             * between registrations, 
             * so that they get a unique timestamp.
             */

            GameRegistration.RegisterSimpleGame(player1, player2);
            System.Threading.Thread.Sleep(1000);
            GameRegistration.RegisterSimpleGame(player1, player2);
            
            System.Threading.Thread.Sleep(1000);
            GameRegistration.RegisterSimpleGame(player2, player1);

            When_analysing_matches();

            analyseResult.GamesWonByPlayer1.should_be(2);
            analyseResult.GamesWonByPlayer2.should_be(1);
            analyseResult.PointsEarnedByPlayer1.should_be(10);
            analyseResult.PointsEarnedByPlayer2.should_be(10);
            analyseResult.PointsLostByPlayer1.should_be(10);
            analyseResult.PointsLostByPlayer2.should_be(10);
        }

        Action When_analysing_matches = () =>
        {
            MatchAnalyser matchAnalyser = new MatchAnalyser(AuditTrail.Instance);
            analyseResult = matchAnalyser.AnalyseMatches(player1.Name, player2.Name);            
        };

        [SetUp]
        public void SetUp()
        {
            AuditTrail.Instance = new AuditTrail();
            player1 = new Player("Richard") { Points = 1200 };
            player2 = new Player("Carl") { Points = 1200 };
        }

        static Player player1;
        static Player player2;
        static AnalyseResult analyseResult;
    }
}
