using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Equipo1b_TPC
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Master != null)
            {
                var navbar = Master.FindControl("pnlNavBar");
                navbar.Visible = false;//ocultamos la navbar en el login
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("home.aspx");
        }
    }
}