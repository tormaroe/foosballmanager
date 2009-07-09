using System;

namespace Fussball.SimplePointsSystem
{
    internal class LowestScoreWonWithBigDifferenceDoubleGameRegistrator : DoubleGameRegistrator
    {
        public LowestScoreWonWithBigDifferenceDoubleGameRegistrator
            (Player winner1, Player winner2, Player looser1, Player looser2)
            : base(winner1, winner2, looser1, looser2)
        {
        }

        protected override void AdjustPlayerPoints()
        {
            AdjustPlayersWith(10);
        }
    }
}
