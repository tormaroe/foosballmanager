﻿using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Fussball.SimplePointsSystem;

namespace Fussball.Controls
{
    public partial class Stats : UserControl
    {
        public void Hide()
        {
            Visible = false;
        }

        public void Show()
        {
            Visible = true;

            if (PlayersAlreadyBound)
                return;

            _player1.DataSource = PlayersUtil.ThePlayers.AllPlayers;
            _player1.DataTextField = "Name";
            _player1.DataValueField = "Id";
            _player1.DataBind();

            _player2.DataSource = PlayersUtil.ThePlayers.AllPlayers;
            _player2.DataTextField = "Name";
            _player2.DataValueField = "Id";
            _player2.DataBind();

            _messagePanel.Visible = false;
        }

        private bool PlayersAlreadyBound
        {
            get
            {
                return _player1.Items.Count.Equals(PlayersUtil.ThePlayers.AllPlayers.Count);
            }
        }
        protected void _btnAnalyse_Click(object sender, EventArgs e)
        {
            _messagePanel.Visible = false;

            Guid player1Id = new Guid(_player1.SelectedValue);
            Guid player2Id = new Guid(_player2.SelectedValue);

            if (player1Id.Equals(player2Id))
            {
                ShowMessage("Can't analyze matches if you pick the same player twice!", "red");
                return;
            }

            AnalyseResult result = Fussball.SimplePointsSystem.AuditTrail.Instance.AnalyseMatches(
                _player1.SelectedItem.Text, 
                _player2.SelectedItem.Text);

            string message = string.Empty;

            message += string.Format("{0} won {1} matches, {2} won {3} matches.",
                _player1.SelectedItem.Text,
                result.GamesWonByPlayer1,
                _player2.SelectedItem.Text,
                result.GamesWonByPlayer2);

            if (result.GamesWonByPlayer1 != result.GamesWonByPlayer2)
            {
                message += string.Format("<br/>So all-in-all {0} won by {1} matches.",
                    (result.GamesWonByPlayer1 > result.GamesWonByPlayer2 ? _player1.SelectedItem.Text : _player2.SelectedItem.Text),
                    Math.Abs(result.GamesWonByPlayer1 - result.GamesWonByPlayer2));
            }

            message += string.Format("<br/>{0} took {1} points from {2} and lost {3}.",
                _player1.SelectedItem.Text,
                result.PointsEarnedByPlayer1,
                _player2.SelectedItem.Text,
                result.PointsLostByPlayer1);

            if (result.PointsEarnedByPlayer1 != result.PointsEarnedByPlayer2)
            {
                message += string.Format("<br/>So all-in-all {0} won {1} points.",
                    (result.PointsEarnedByPlayer1 > result.PointsEarnedByPlayer2 ? _player1.SelectedItem.Text : _player2.SelectedItem.Text),
                    Math.Abs(result.PointsEarnedByPlayer1 - result.PointsEarnedByPlayer2));
            }

            ShowMessage(message, "green");

            _player1Name.Text = _player1.SelectedItem.Text;
            _player2Name.Text = _player2.SelectedItem.Text;

            SparkLineData result1 = GenerateChart(player1Id, _chartPlayer1);
            SparkLineData result2 = GenerateChart(player2Id, _chartPlayer2);

            GenerateCombinedChart(result1, result2);

            _maxRatingPlayer1.Text = result1.Max.ToString();
            _maxRatingPlayer2.Text = result2.Max.ToString();
            _avgRatingPlayer1.Text = ((int)result1.Avg).ToString();
            _avgRatingPlayer2.Text = ((int)result2.Avg).ToString();
            _minRatingPlayer1.Text = result1.Min.ToString();
            _minRatingPlayer2.Text = result2.Min.ToString();
        }

        private void ShowMessage(string text, string color)
        {
            _messagePanel.Visible = true;
            _message.Text = text;
            _messagePanel.Attributes["style"] = string.Format("color:{0};", color);
        }

        private SparkLineData GenerateChart(Guid playerGuid, Image image)
        {

            AnalysePlayerResult result = Fussball.SimplePointsSystem.AuditTrail.Instance.AnalysePlayer(PlayersUtil.ThePlayers[playerGuid].Name);

            string chartKey = Guid.NewGuid().ToString();
            SparkLineData spd = new SparkLineData();
            spd.Data = result.Points.ToArray();
            spd.ImageWidth = 300;
            spd.SetAvg();
            Session[chartKey] = spd;

            image.ImageUrl = ResolveUrl("~/Charts/SparkLine.aspx") + "?data=" + chartKey;

            return spd;
        }

        private void GenerateCombinedChart(SparkLineData player1data, SparkLineData player2data)
        {
            SparkLineDuoData duoData = new SparkLineDuoData();
            duoData.DataSet1 = player1data.Data;
            duoData.DataSet2 = player2data.Data;
            duoData.ImageWidth = 500;
            duoData.ImageHeight = 200;
            duoData.Set1Text = _player1.SelectedItem.Text;
            duoData.Set2Text = _player2.SelectedItem.Text;
            string duoChartKey = Guid.NewGuid().ToString();
            Session[duoChartKey] = duoData;
            _chartCombined.ImageUrl = ResolveUrl("~/Charts/SparkLineDuo.aspx") + "?data=" + duoChartKey;
        }
    }
}