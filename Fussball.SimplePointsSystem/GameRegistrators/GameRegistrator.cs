using System;

namespace Fussball.SimplePointsSystem
{
    internal abstract class GameRegistrator
    {
        public abstract void DoRegistration();

        protected void AdjustPlayerPoints(Player player, int pointsChange)
        {
            player.Points += pointsChange;
            AuditTrail.Instance.AddPointsChange(player, pointsChange, player.Points);
        }
    }
}
