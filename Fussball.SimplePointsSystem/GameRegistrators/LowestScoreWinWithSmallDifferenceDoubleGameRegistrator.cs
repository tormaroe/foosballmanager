using System;

namespace Fussball.SimplePointsSystem
{
    internal class LowestScoreWinWithSmallDifferenceDoubleGameRegistrator : DoubleGameRegistrator
    {
        public LowestScoreWinWithSmallDifferenceDoubleGameRegistrator
            (Player winner1, Player winner2, Player looser1, Player looser2)
            : base(winner1, winner2, looser1, looser2)
        {
        }
        protected override void AdjustPlayerPoints()
        {
            AdjustPlayersWith(5);
        }
    }
}
