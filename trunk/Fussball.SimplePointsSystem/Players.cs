using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Fussball.SimplePointsSystem
{
    public class Players
    {
        internal Dictionary<Guid, Player> _players;

        public Players()
        {
            _players = new Dictionary<Guid, Player>();
        }

        public List<Player> AllPlayers
        {
            get
            {
                List<Player> pList = new List<Player>(_players.Count);
                foreach (KeyValuePair<Guid, Player> playerInfo in _players)
                {
                    pList.Add(playerInfo.Value);
                }
                return pList;
            }
        }

        public void Add(Player player)
        {
            if (_players.ContainsKey(player.Id))
            {
                throw new Exception("Player already added");
            }

            _players.Add(player.Id, player);
        }

        public void Remove(Guid playerId)
        {
            _players.Remove(playerId);
        }

        public Player this[Guid Id]
        {
            get
            {
                return _players[Id];
            }
        }

        internal Player GetByName(string playerName)
        {
            foreach (Player p in _players.Values)
            {
                if (p.Name.Equals(playerName))
                {
                    return p;
                }
            }
            throw new Exception("Can't find player with name " + playerName);
        }
    }
}
