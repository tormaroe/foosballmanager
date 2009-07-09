using System;

namespace Fussball.SimplePointsSystem
{
    internal class LowestScoreWonWithBigDifferenceSingleGameRegistrator : SingleGameRegistrator
    {
        public LowestScoreWonWithBigDifferenceSingleGameRegistrator(Player winner, Player looser)
            : base(winner, looser)
        {
        }

        protected override void AdjustPlayerPoints()
        {
            AdjustPlayersWith(20);
        }
    }
}
