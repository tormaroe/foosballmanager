using System;
using System.Linq;
using NUnit.Framework;
using Fussball.SimplePointsSystem;
using Moq;

namespace Fussball.Test
{
    [TestFixture]
    public class AddUserRequest
    {
        [Test]
        public void Should_add_new_user_with_the_provided_name()
        {
            Given_an_AddUserController();
            When_add_user_is_requested();
            Assert.AreEqual(1, players.AllPlayers.Count);
            Assert.AreEqual(ObiFoos, players.AllPlayers.First().Name);
        }

        [Test]
        public void Should_not_add_new_user_if_provided_name_is_empty()
        {
            Given_an_AddUserController();
            When_add_user_is_requested_with_empty_name();
            Assert.AreEqual(0, players.AllPlayers.Count);
        }

        Action Given_an_AddUserController = () =>
        {
            players = new Players();
            controller = new AddUserController(GetView(), players);
        };

        static Func<IAddUserView> GetView = () =>
        {
            viewMock = new Mock<IAddUserView>();            
            return viewMock.Object;
        };

        Action When_add_user_is_requested = () =>
        {
            viewMock
                .SetupGet(view => view.Username)
                .Returns(ObiFoos);
            Raise_AddUserRequest_event();
        };

        Action When_add_user_is_requested_with_empty_name = () =>
        {
            Raise_AddUserRequest_event();
        };

        static Action Raise_AddUserRequest_event = () =>
            viewMock.Raise(view => view.AddUserRequest += null, EventArgs.Empty);

        static string ObiFoos = "Obi-Foos";
        static Players players;
        static AddUserController controller;
        static Mock<IAddUserView> viewMock;
    }
}
