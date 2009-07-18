using System.Linq;
using NUnit.Framework;

namespace Fussball.SimplePointsSystem.Test
{
    [TestFixture]
    public class PlayerAnalyzerSpecification
    {
        [Test]
        public void Points_array_should_contain_all_points_changes()
        {
            GameRegistration.RegisterSimpleGame(player1, player2);
            GameRegistration.RegisterSimpleGame(player1, player2);
            GameRegistration.RegisterSimpleGame(player1, player2);
            GameRegistration.RegisterSimpleGame(player1, player2);

            Player_array_should_contain_values(player1, 1200d, 1205d, 1210d, 1215d);
            Player_array_should_contain_values(player2, 1200d, 1195d, 1190d, 1185d);
        }

        private static void Player_array_should_contain_values(Player player, params double[] values)
        {
            AnalysePlayerResult analyseResult = new PlayerAnalyser(AuditTrail.Instance)
                            .Analyse(player.Name);

            values.ToList().ForEach((pointsValue) 
                => analyseResult.Points.should_contain(pointsValue));
        }

        [SetUp]
        public void SetUp()
        {
            AuditTrail.Instance = new AuditTrail();
            player1 = new Player("Richard") { Points = 1200 };
            player2 = new Player("Carl") { Points = 1200 };
        }

        static Player player1;
        static Player player2;
    }
}
