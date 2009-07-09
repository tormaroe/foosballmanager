using System;

namespace Fussball.SimplePointsSystem
{
    internal abstract class SingleGameRegistrator : GameRegistrator
    {
        protected Player _winner;
        protected Player _looser;

        public SingleGameRegistrator(Player winner, Player looser)
        {
            _winner = winner;
            _looser = looser;
        }

        public override void DoRegistration()
        {
            AdjustPlayerPoints();

            _winner.SinglesWon++;
            _looser.SinglesLost++;

            AuditTrail.Instance.AddSinglesMatch(_winner, _looser);
        }

        protected void AdjustPlayersWith(int points)
        {
            AdjustPlayerPoints(_winner, points);
            AdjustPlayerPoints(_looser, points * -1);    
        }

        protected abstract void AdjustPlayerPoints();
    }
}
