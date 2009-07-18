using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Fussball.SimplePointsSystem.Test
{
    [TestFixture]
    public class PlayersFromXML
    {
        [Test]
        public void Should_create_all_players_from_XML()
        {
            Given_XML_with_2_known_players();
            When_getting_players_from_XML();
            Players_count_should_be_2();
        }

        [Test]
        public void Should_create_player_with_correct_values()
        {
            Given_XML_with_2_known_players();
            When_getting_players_from_XML();
            MrT_should_have_all_the_correct_values();
        }

        Action Given_XML_with_2_known_players = () =>
            xml = "<players>"
                + "<player id=\"" + Guid.NewGuid() + "\">"
                + "<singleslost>1</singleslost>"
                + "<singleswon>2</singleswon>"
                + "<doubleslost>3</doubleslost>"
                + "<doubleswon>4</doubleswon>"
                + "<points>1234</points>"
                + "<leaguepoints>5</leaguepoints>"
                + "<leaguematched>10</leaguematched>"
                + "<name>Mr. T</name>"
                + "</player>"
                + "<player id=\"" + Guid.NewGuid() + "\">"
                + "<singleslost>0</singleslost>"
                + "<singleswon>0</singleswon>"
                + "<doubleslost>0</doubleslost>"
                + "<doubleswon>0</doubleswon>"
                + "<points>1200</points>"
                + "<leaguepoints>0</leaguepoints>"
                + "<leaguematched>0</leaguematched>"
                + "<name>Mr. X</name>"
                + "</player>"
                + "</players>";

        Action When_getting_players_from_XML = () =>
            players = PlayersXML.CreatePlayersFromXml(xml);

        Action Players_count_should_be_2 = () =>
            players.AllPlayers.Count.should_be(2);

        Action MrT_should_have_all_the_correct_values = () =>
        {
            var mrT = Find_player_with_name("Mr. T");
            mrT.SinglesLost.should_be(1);
            mrT.SinglesWon.should_be(2);
            mrT.DoublesLost.should_be(3);
            mrT.DoublesWon.should_be(4);
            mrT.Points.should_be(1234);
            mrT.LeaguePoints.should_be(5);
            mrT.LeagueMatchesPlayed.should_be(10);
        };

        static Func<string, Player> Find_player_with_name = (name) =>
            players.AllPlayers.Find(player => 
                player.Name.Equals(name));

        static string xml;
        static Players players;
    }
}
