using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Fussball.SimplePointsSystem
{
    public class Players
    {
        private Dictionary<Guid, Player> _players;

        public Players()
        {
            _players = new Dictionary<Guid, Player>();
        }

        public Players(string xml)
        {            

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            LoadFromXml(doc.DocumentElement);
        }

        public Players(XmlNode node)
        {
            LoadFromXml(node);
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

        private void LoadFromXml(XmlNode node)
        {
            _players = new Dictionary<Guid, Player>();

            XmlNodeList playersNodes = node.ChildNodes;
            foreach (XmlNode playerNode in playersNodes)
            {
                Player p = Player.GetPlayer(playerNode);
                _players.Add(p.Id, p);
            }
        }

        public string ToXml()
        {
            StringBuilder xml = new StringBuilder("<players>");

            foreach (KeyValuePair<Guid, Player> playerInfo in _players)
            {
                xml.Append(playerInfo.Value.ToXml());
            }

            xml.Append("</players>");

            return xml.ToString();
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
