using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Fussball.SimplePointsSystem.Test
{
    [TestFixture]
    public class PlayersCollectionTests
    {
        [Test]
        public void Collection_should_have_correct_count()
        {
            playerCollection.AllPlayers.Count.should_be(0);

            playerCollection.Add(bob);
            playerCollection.AllPlayers.Count.should_be(1);

            playerCollection.Add(carl);
            playerCollection.AllPlayers.Count.should_be(2);
        }

        [Test]
        public void Should_be_able_to_remove_player()
        {
            playerCollection.Add(bob);
            playerCollection.Remove(bob.Id);
            playerCollection.AllPlayers.Count.should_be(0);
        }

        [Test]
        public void Should_be_able_to_access_players_by_ID()
        {
            playerCollection.Add(bob);
            playerCollection[bob.Id].should_be_same_as(bob);
        }

        [Test]
        public void Should_be_able_to_locate_player_by_name()
        {
            playerCollection.Add(bob);
            playerCollection.Add(carl);
            playerCollection.GetByName(carl.Name).should_not_be_null();
        }

        [Test]
        public void Trying_to_get_an_unknown_player_should_result_in_exception()
        {
            playerCollection.Add(bob);
            typeof(Exception)
                .should_be_thrown_by(() 
                    => playerCollection.GetByName(carl.Name));
        }

        [Test]
        public void Adding_a_player_twice_should_result_in_exception()
        {
            playerCollection.Add(bob);
            typeof(Exception)
                .should_be_thrown_by(() 
                    => playerCollection.Add(bob));
        }

        [SetUp]
        public void SetUp()
        {
            playerCollection = new Players();
        }

        static Players playerCollection;
        static Player bob = new Player("Bob");
        static Player carl = new Player("Carl Franklin");
    }
}
