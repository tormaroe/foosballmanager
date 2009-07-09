using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Fussball.SimplePointsSystem
{
    public class Player
    {

        public Player(string name)
        {
            _id = Guid.NewGuid();
            Name = name;
            SinglesWon = 0;
            SinglesLost = 0;
            DoublesWon = 0;
            DoublesLost = 0;
            LeaguePoints = 0;
            LeagueMatchesPlayed = 0;
            Points = Constants.DEFAULT_PLAYER_POINTS;
        }

        internal Player()
        {
        }        

        private Guid _id;

        public Guid Id
        {
            get
            {
                return _id;
            }
            internal set
            {
                _id = value;
            }
        }
        
        public int Points { get; set; }
        public int LeagueMatchesPlayed { get; set; }        
        public int LeaguePoints { get; set; }        
        public string Name { get; set; }
        
        public int GamesPlayed
        {
            get
            {
                return SinglesWon + SinglesLost + DoublesWon + DoublesLost;
            }            
        }

        public int SinglesWon { get; set; }
        public int SinglesLost { get; set; }
        public int DoublesWon { get; set; }
        public int DoublesLost { get; set; }

    }
}
