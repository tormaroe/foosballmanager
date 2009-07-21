using System;

namespace Fussball
{
    public class PlayerEventArgs : EventArgs
    {
        private Guid _playerId;
        public PlayerEventArgs(Guid playerId)
        {
            _playerId = playerId;

        }
        public Guid PlayerId
        {
            get
            {
                return _playerId;
            }
        }
    }
}
