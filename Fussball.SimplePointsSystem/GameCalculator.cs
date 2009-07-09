using System;

namespace Fussball.SimplePointsSystem
{
    internal static class GameCalculator
    {
        public static int GetTeamPoints(Player player1, Player player2)
        {
            if (player1.Points > player2.Points)
            {
                return player1.Points + GetTeamLowerPlayerPart(player2);
            }
            return player2.Points + GetTeamLowerPlayerPart(player1);
        }

        private static int GetTeamLowerPlayerPart(Player player)
        {
            int foo = player.Points - 900;
            if (foo > 0)
            {
                return foo / 2;
            }
            return 0;
        }

        internal static CalculationResult GetGameResult(int winnerPoints, int looserPoints)
        {
            int difference = Math.Abs(winnerPoints - looserPoints);
            if (winnerPoints >= looserPoints)
            {
                if (difference > 100)
                {
                    return CalculationResult.HighestScoreWonWithBigDifference;
                }
                return CalculationResult.HighestScoreWonWithSmallDifference;
            }
            else //if (winnerPoints < looserPoints)
            {
                if (difference > 100)
                {
                    return CalculationResult.LowestScoreWonWithBigDifference;
                }
                return CalculationResult.LowestScoreWinWithSmallDifference;
            }
        }
    }
}