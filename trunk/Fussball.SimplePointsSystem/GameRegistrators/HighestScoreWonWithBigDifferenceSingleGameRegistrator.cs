using System;

namespace Fussball.SimplePointsSystem
{
    internal class HighestScoreWonWithBigDifferenceSingleGameRegistrator : SingleGameRegistrator
    {
        public HighestScoreWonWithBigDifferenceSingleGameRegistrator(Player winner, Player looser)
            : base(winner, looser)
        {
        }

        protected override void AdjustPlayerPoints()
        {
            // no adjustments!    
        }
    }
}
