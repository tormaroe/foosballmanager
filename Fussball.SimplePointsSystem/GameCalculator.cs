using System;
using System.Collections.Generic;
using System.Text;

namespace Fussball.SimplePointsSystem
{

    public static class GameCalculator
    {

        public static void RegisterDoubleGame(Player winner1, Player winner2, Player looser1, Player looser2)
        {
            int winnerTeamPoints = GetTeamPoints(winner1, winner2);
            int looserTeamPoints = GetTeamPoints(looser1, looser2);

            CalculationResult result = GetGameResult(winnerTeamPoints, looserTeamPoints);
            switch (result)
            {
                case CalculationResult.HighestScoreWonWithBigDifference:
                    // no adjustments
                    break;
                case CalculationResult.HighestScoreWonWithSmallDifference:
                    winner1.Points += 3; AuditTrail.Instance.AddPointsChange(winner1, 3, winner1.Points);
                    winner2.Points += 3; AuditTrail.Instance.AddPointsChange(winner2, 3, winner2.Points);
                    looser1.Points -= 3; AuditTrail.Instance.AddPointsChange(looser1, -3, looser1.Points);
                    looser2.Points -= 3; AuditTrail.Instance.AddPointsChange(looser2, -3, looser2.Points);
                    break;
                case CalculationResult.LowestScoreWonWithBigDifference:
                    winner1.Points += 10; AuditTrail.Instance.AddPointsChange(winner1, 10, winner1.Points);
                    winner2.Points += 10; AuditTrail.Instance.AddPointsChange(winner2, 10, winner2.Points);
                    looser1.Points -= 10; AuditTrail.Instance.AddPointsChange(looser1, -10, looser1.Points);
                    looser2.Points -= 10; AuditTrail.Instance.AddPointsChange(looser2, -10, looser2.Points);
                    break;
                case CalculationResult.LowestScoreWinWithSmallDifference:
                    winner1.Points += 5; AuditTrail.Instance.AddPointsChange(winner1, 5, winner1.Points);
                    winner2.Points += 5; AuditTrail.Instance.AddPointsChange(winner2, 5, winner2.Points);
                    looser1.Points -= 5; AuditTrail.Instance.AddPointsChange(looser1, -5, looser1.Points);
                    looser2.Points -= 5; AuditTrail.Instance.AddPointsChange(looser2, -5, looser2.Points);
                    break;
            }

            winner1.DoublesWon++;
            winner2.DoublesWon++;
            looser1.DoublesLost++;
            looser2.DoublesLost++;

            AuditTrail.Instance.AddDoublesMatch(winner1, winner2, looser1, looser2);            
        }

        public static int GetTeamPoints(Player player1, Player player2)
        {            
            if (player1.Points > player2.Points)
            {
                return player1.Points + GetTeamLowerPlayerPart(player2);
            }
            else
            {
                return player2.Points + GetTeamLowerPlayerPart(player1);
            }
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

        public static void RegisterSimpleGame(Player winner, Player looser)
        {
            CalculationResult result = GetGameResult(winner.Points, looser.Points);
            switch (result)
            {
                case CalculationResult.HighestScoreWonWithBigDifference:
                    // no adjustments
                    break;
                case CalculationResult.HighestScoreWonWithSmallDifference:
                    winner.Points += 5; AuditTrail.Instance.AddPointsChange(winner, 5, winner.Points);
                    looser.Points -= 5; AuditTrail.Instance.AddPointsChange(looser, -5, looser.Points);
                    break;
                case CalculationResult.LowestScoreWonWithBigDifference:
                    winner.Points += 20; AuditTrail.Instance.AddPointsChange(winner, 20, winner.Points);
                    looser.Points -= 20; AuditTrail.Instance.AddPointsChange(looser, -20, looser.Points);
                    break;
                case CalculationResult.LowestScoreWinWithSmallDifference:
                    winner.Points += 10; AuditTrail.Instance.AddPointsChange(winner, 10, winner.Points);
                    looser.Points -= 10; AuditTrail.Instance.AddPointsChange(looser, -10, looser.Points);
                    break;                
            }

            winner.SinglesWon++;
            looser.SinglesLost++;

            AuditTrail.Instance.AddSinglesMatch(winner, looser);            
        }

        private enum CalculationResult
        {
            HighestScoreWonWithBigDifference,
            HighestScoreWonWithSmallDifference,
            LowestScoreWonWithBigDifference,
            LowestScoreWinWithSmallDifference,
        }

        private static CalculationResult GetGameResult(int winnerPoints, int looserPoints)
        {
            int difference = Math.Abs(winnerPoints - looserPoints);
            if (winnerPoints >= looserPoints) 
            {
                if (difference > 100)
                {
                    return CalculationResult.HighestScoreWonWithBigDifference;
                }
                else
                {
                    return CalculationResult.HighestScoreWonWithSmallDifference;
                }
            }
            else //if (winnerPoints < looserPoints)
            {
                if (difference > 100)
                {
                    return CalculationResult.LowestScoreWonWithBigDifference;
                }
                else
                {
                    return CalculationResult.LowestScoreWinWithSmallDifference;
                }
            }
        }
    }
}
