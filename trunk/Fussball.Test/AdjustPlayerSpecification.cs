using System;
using NUnit.Framework;
using Fussball.SimplePointsSystem;
using Moq;

namespace Fussball.Test
{
    [TestFixture]
    public class AdjustPlayerSpecification
    {        
        [Test]
        public void Should_set_view_display_values_when_player_is_selected()
        {
            Given_an_AdjustPlayerController();
            When_a_player_is_selected();
            Then_view_should_display_values_for_edit();
        }

        [Test]
        public void Should_update_player_with_new_values()
        {
            Given_an_AdjustPlayerController();
            When_an_adjust_player_request_is_sent_with_some_new_values();
            Then_player_should_have_been_updated_with_the_new_values();            
        }

        [Test]
        public void Should_check_input_validity_when_adjusting_player()
        {
            Given_an_AdjustPlayerController();
            When_an_adjust_player_request_is_sent_with_some_new_values();
            Then_isValid_should_have_been_checked();
        }

        [Test]
        [ExpectedException()]
        public void Should_throw_and_exception_is_view_is_not_valid()
        {
            Given_an_AdjustPlayerController();
            When_an_adjust_player_request_is_sent_with_invalid_data();
            Then_the_expected_exception_is_thrown();
        }

        [Test]
        public void Should_add_entry_into_audit_trail()
        {
            Given_an_AdjustPlayerController();
            When_an_adjust_player_request_is_sent_with_some_new_values();
            auditTrail.Items.should_not_be_empty();
        }

        Action Given_an_AdjustPlayerController = () =>
            controller = new AdjustPlayerController(GetView(), GetPlayers(), GetAuditTrail());

        Action When_a_player_is_selected = () =>
            viewMock.Raise(view => view.PlayerSelectedForAdjustments += null, new PlayerEventArgs(playerId));

        Action When_an_adjust_player_request_is_sent_with_some_new_values = () =>
        {
            viewMock.Setup(view => view.IsValid()).Returns(true);

            viewMock.Setup(view => view.Points).Returns(NEW_POINTS);
            viewMock.Setup(view => view.SinglesWon).Returns(NEW_SINGLES_WON);
            viewMock.Setup(view => view.SignlesLost).Returns(NEW_SINGLES_LOST);
            viewMock.Setup(view => view.DoublesWon).Returns(NEW_DOUBLES_WON);
            viewMock.Setup(view => view.DoublesLost).Returns(NEW_DOUBLES_LOST);            

            viewMock.Raise(view => view.AdjustPlayerRequest += null, new PlayerEventArgs(playerId));
        };

        Action When_an_adjust_player_request_is_sent_with_invalid_data = () =>
        {
            viewMock.Setup(view => view.IsValid()).Returns(false);

            viewMock.Raise(view => view.AdjustPlayerRequest += null, new PlayerEventArgs(playerId));
        };

        Action Then_view_should_display_values_for_edit = () =>
        {
            var player = players[playerId];
            
            viewMock.VerifySet(v => v.PlayerName = player.Name);
            viewMock.VerifySet(v => v.SinglesWon = player.SinglesWon);
            viewMock.VerifySet(v => v.SignlesLost = player.SinglesLost);
            viewMock.VerifySet(v => v.DoublesWon = player.DoublesWon);
            viewMock.VerifySet(v => v.DoublesLost = player.DoublesLost); 
            viewMock.VerifySet(v => v.Points = player.Points);

            viewMock.Verify(v => v.DisplayEditPanel());
        };

        Action Then_player_should_have_been_updated_with_the_new_values = () =>
        {
            var player = players[playerId];

            player.Points.should_be(NEW_POINTS);
            player.SinglesWon.should_be(NEW_SINGLES_WON);
            player.SinglesLost.should_be(NEW_SINGLES_LOST);
            player.DoublesWon.should_be(NEW_DOUBLES_WON);
            player.DoublesLost.should_be(NEW_DOUBLES_LOST);
        };

        Action Then_isValid_should_have_been_checked = () =>
            viewMock.Verify(view => view.IsValid());

        Action Then_the_expected_exception_is_thrown = () => { };

        static Func<IAdjustPlayerView> GetView = () =>
        {
            viewMock = new Mock<IAdjustPlayerView>();
            return viewMock.Object;
        };

        static Func<Players> GetPlayers = () =>
        {
            players = new Players();
            players.Add(new Player("Samson") 
            {
                Id = playerId,
                SinglesWon = 12,
                SinglesLost = 23,
                DoublesWon = 3,
                DoublesLost = 2,
                Points = 1178,                
            });
            return players;
        };

        static Func<AuditTrail> GetAuditTrail = () =>
        {
            auditTrail = new AuditTrail();
            return auditTrail;
        };

        static AdjustPlayerController controller;
        static Players players;
        static AuditTrail auditTrail;

        static Mock<IAdjustPlayerView> viewMock;
        static Guid playerId = Guid.NewGuid();

        private const int NEW_POINTS = 1201;
        private const int NEW_SINGLES_WON = 233;
        private const int NEW_SINGLES_LOST = 123;
        private const int NEW_DOUBLES_WON = 34;
        private const int NEW_DOUBLES_LOST = 65;
    }
}
