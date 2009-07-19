using System;
using System.Web.UI;

namespace Fussball.Controls
{
    public partial class AuditTrail : UserControl
    {
        public void Refresh()
        {
            Visible = true;

            if (Fussball.SimplePointsSystem.AuditTrail.Instance == null)
            {
                PlayersUtil.LoadAuditTrail();
            }
            _grid.DataSource = Fussball.SimplePointsSystem.AuditTrail.Instance.DefaultView;
            _grid.DataBind();
        }
    }
}