using System;

namespace Fussball.SimplePointsSystem
{
    internal abstract class DoubleGameRegistrator : GameRegistrator
    {

        protected Player _winner2;
        protected Player _looser1;
        protected Player _looser2;
        protected Player _winner1;

        public DoubleGameRegistrator(Player winner1, Player winner2, Player looser1, Player looser2)
        {
            _looser2 = looser2;
            _looser1 = looser1;
            _winner2 = winner2;
            _winner1 = winner1;
        }

        public override void DoRegistration()
        {
            AdjustPlayerPoints();

            _winner1.DoublesWon++;
            _winner2.DoublesWon++;
            _looser1.DoublesLost++;
            _looser2.DoublesLost++;

            AuditTrail.Instance.AddDoublesMatch(_winner1, _winner2, _looser1, _looser2);
        }

        protected void AdjustPlayersWith(int points)
        {
            AdjustPlayerPoints(_winner1, points);
            AdjustPlayerPoints(_winner2, points);
            AdjustPlayerPoints(_looser1, points * -1);
            AdjustPlayerPoints(_looser2, points * -1);
        }

        protected abstract void AdjustPlayerPoints();

    }
}
