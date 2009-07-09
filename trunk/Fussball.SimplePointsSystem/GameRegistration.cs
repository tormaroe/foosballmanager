using System;

namespace Fussball.SimplePointsSystem
{
    public static class GameRegistration
    {
        public static void RegisterDoubleGame(Player winner1, Player winner2, Player looser1, Player looser2)
        {
            int winnerTeamPoints = GameCalculator.GetTeamPoints(winner1, winner2);
            int looserTeamPoints = GameCalculator.GetTeamPoints(looser1, looser2);

            CalculationResult result = GameCalculator.GetGameResult(winnerTeamPoints, looserTeamPoints);
            GameRegistrator registrator = null;
            switch (result)
            {
                case CalculationResult.HighestScoreWonWithBigDifference:
                    registrator = new HighestScoreWonWithBigDifferenceDoubleGameRegistrator(winner1, winner2, looser1, looser2);
                    break;
                case CalculationResult.HighestScoreWonWithSmallDifference:
                    registrator = new HighestScoreWonWithSmallDifferenceDoubleGameRegistrator(winner1, winner2, looser1, looser2);
                    break;
                case CalculationResult.LowestScoreWonWithBigDifference:
                    registrator = new LowestScoreWonWithBigDifferenceDoubleGameRegistrator(winner1, winner2, looser1, looser2);
                    break;
                case CalculationResult.LowestScoreWinWithSmallDifference:
                    registrator = new LowestScoreWinWithSmallDifferenceDoubleGameRegistrator(winner1, winner2, looser1, looser2);
                    break;
            }

            registrator.DoRegistration();
        }

        public static void RegisterSimpleGame(Player winner, Player looser)
        {
            CalculationResult result = GameCalculator.GetGameResult(winner.Points, looser.Points);
            GameRegistrator registrator = null;
            switch (result)
            {
                case CalculationResult.HighestScoreWonWithBigDifference:
                    registrator = new HighestScoreWonWithBigDifferenceSingleGameRegistrator(winner, looser);
                    break;
                case CalculationResult.HighestScoreWonWithSmallDifference:
                    registrator = new HighestScoreWonWithSmallDifferenceSingleGameRegistrator(winner, looser);
                    break;
                case CalculationResult.LowestScoreWonWithBigDifference:
                    registrator = new LowestScoreWonWithBigDifferenceSingleGameRegistrator(winner, looser);
                    break;
                case CalculationResult.LowestScoreWinWithSmallDifference:
                    registrator = new LowestScoreWinWithSmallDifferenceSingleGameRegistrator(winner, looser);
                    break;
            }

            registrator.DoRegistration();
        }
    }
}
