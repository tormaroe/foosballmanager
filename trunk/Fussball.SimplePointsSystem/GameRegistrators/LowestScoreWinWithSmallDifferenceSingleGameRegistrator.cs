using System;

namespace Fussball.SimplePointsSystem
{
    internal class LowestScoreWinWithSmallDifferenceSingleGameRegistrator : SingleGameRegistrator
    {
        public LowestScoreWinWithSmallDifferenceSingleGameRegistrator(Player winner, Player looser)
            : base(winner, looser)
        {
        }

        protected override void AdjustPlayerPoints()
        {
            AdjustPlayersWith(10);
        }
    }
}
