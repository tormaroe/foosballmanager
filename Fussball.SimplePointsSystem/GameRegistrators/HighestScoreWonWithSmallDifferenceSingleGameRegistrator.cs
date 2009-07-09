using System;

namespace Fussball.SimplePointsSystem
{
    internal class HighestScoreWonWithSmallDifferenceSingleGameRegistrator : SingleGameRegistrator
    {
        public HighestScoreWonWithSmallDifferenceSingleGameRegistrator(Player winner, Player looser)
            : base(winner, looser)
        {
        }

        protected override void AdjustPlayerPoints()
        {
            AdjustPlayersWith(5);
        }
    }
}
