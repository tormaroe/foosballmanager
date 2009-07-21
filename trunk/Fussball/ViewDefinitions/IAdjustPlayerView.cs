using System;

namespace Fussball
{
    public interface IAdjustPlayerView : IView
    {
        event EventHandler<PlayerEventArgs> PlayerSelectedForAdjustments;
        event EventHandler<PlayerEventArgs> AdjustPlayerRequest;

        string PlayerName { get; set; }
        int DoublesWon { get; set; }
        int DoublesLost { get; set; }
        int SinglesWon { get; set; }
        int SignlesLost { get; set; }
        int Points { get; set; }

        void DisplayEditPanel();
        bool IsValid();
    }
}
