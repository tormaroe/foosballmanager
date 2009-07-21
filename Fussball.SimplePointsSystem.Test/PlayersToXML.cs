using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Marosoft.Testing;

namespace Fussball.SimplePointsSystem.Test
{
    [TestFixture]
    public class PlayersToXML
    {
        [Test]
        public void Should_create_empty_XML_for_empty_collection()
        {   
            Given_an_empty_players_collection();
            When_making_XML_for_players();
            xml.should_equal("<players></players>");
        }

        [Test]
        public void Should_create_well_known_XML_for_known_person()
        {
            Given_a_players_collection_with(knownPerson);
            When_making_XML_for_players();
            xml.should_equal(
                "<players>"
                + "<player id=\"" + knownPerson.Id + "\">"
                + "<singleslost>" + knownPerson.SinglesLost + "</singleslost>"
                + "<singleswon>" + knownPerson.SinglesWon + "</singleswon>"
                + "<doubleslost>" + knownPerson.DoublesLost + "</doubleslost>"
                + "<doubleswon>" + knownPerson.DoublesWon + "</doubleswon>"
                + "<points>" + knownPerson.Points + "</points>"
                + "<leaguepoints>" + knownPerson.LeaguePoints + "</leaguepoints>"
                + "<leaguematched>" + knownPerson.LeagueMatchesPlayed + "</leaguematched>"
                + "<name>" + knownPerson.Name + "</name>"
                +"</player>" 
                + "</players>");
        }

        [Test]
        public void Should_create_XML_for_collection_with_multiple_persons()
        {
            Given_a_players_collection_with_10_persons();
            When_making_XML_for_players();
            XML_should_contain_name_elements_for_all_10_people();
        }

        static Action Given_an_empty_players_collection = () =>
            players = new Players();

        Action<Player> Given_a_players_collection_with = (person) =>
        {
            Given_an_empty_players_collection();
            players.Add(person);
        };

        Action Given_a_players_collection_with_10_persons = () =>
        {
            Given_an_empty_players_collection();
            ten_names.ForEach(name => players.Add(new Player(name)));
        };

        Action When_making_XML_for_players = () =>
            xml = PlayersXML.ToXml(players);

        Action XML_should_contain_name_elements_for_all_10_people = () =>
            ten_names.ForEach(name => 
                xml.should_contain(String.Format("<name>{0}</name>", name)));       

        static Players players;
        static Player knownPerson = new Player("Johnny");
        static List<string> ten_names = (new[] { "Arne", "Bob", "Charley", "David", "Edward", "Frank", "Garth", "Henry", "Ivan", "Job" }).ToList();
        static string xml;
    }
}
