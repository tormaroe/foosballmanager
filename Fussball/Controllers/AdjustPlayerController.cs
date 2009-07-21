using System;
using Fussball.SimplePointsSystem;

namespace Fussball
{
    public class AdjustPlayerController
    {
        private Players _players;
        private AuditTrail _auditTrail;
        private IAdjustPlayerView _view;
        public AdjustPlayerController(IAdjustPlayerView view, Players players, AuditTrail auditTrail)
        {
            _auditTrail = auditTrail;
            _players = players;
            _view = view;
            SubscribeToViewEvents();
        }

        private void SubscribeToViewEvents()
        {
            _view.PlayerSelectedForAdjustments += OnPlayerSelectedForAdjustments;
            _view.AdjustPlayerRequest += OnAdjustPlayerRequest;
        }

        void OnPlayerSelectedForAdjustments(object sender, PlayerEventArgs e)
        {
            Player player = GetPlayer(e);
            _view.PlayerName = player.Name;
            _view.SinglesWon = player.SinglesWon;
            _view.SignlesLost = player.SinglesLost;
            _view.DoublesWon = player.DoublesWon;
            _view.DoublesLost = player.DoublesLost;
            _view.Points = player.Points;

            _view.DisplayEditPanel();
        }

        void OnAdjustPlayerRequest(object sender, PlayerEventArgs e)
        {
            ValidateRequest();
            var player = GetPlayer(e);
            AddAuditTrailEntry_BeforeAdjustment(player);
            AdjustPlayer(player);
        }

        private void ValidateRequest()
        {
            if (!_view.IsValid())
                throw new Exception("One of the new values can't be converted to a number");
        }

        private Player GetPlayer(PlayerEventArgs e)
        {
            return _players[e.PlayerId];            
        }

        private void AddAuditTrailEntry_BeforeAdjustment(Player playerToAdjust)
        {
            _auditTrail.AddManualAudit(string.Format(
                                "Manual adjustment of player {0}: SW: {1}->{2}, SL: {3}->{4}, DW: {5}->{6}, DL: {7}->{8}, Points: {9}->{10}",
                                playerToAdjust.Name,
                                playerToAdjust.SinglesWon,
                                _view.SinglesWon,
                                playerToAdjust.SinglesLost,
                                _view.SignlesLost,
                                playerToAdjust.DoublesWon,
                                _view.DoublesWon,
                                playerToAdjust.DoublesLost,
                                _view.DoublesLost,
                                playerToAdjust.Points,
                                _view.Points
                                ));
        }

        private void AdjustPlayer(Player player)
        {
            player.Points = _view.Points;
            player.SinglesWon = _view.SinglesWon;
            player.SinglesLost = _view.SignlesLost;
            player.DoublesWon = _view.DoublesWon;
            player.DoublesLost = _view.DoublesLost;
        }
    }
}
