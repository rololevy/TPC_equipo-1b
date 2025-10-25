using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Equipo1b_TPC.Dominio;

namespace Equipo1b_TPC
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Master != null)
            {
                var navbar = Master.FindControl("pnlNavBar");
                if (navbar != null)
                    navbar.Visible = false;
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            
            try
            {
                string usuario = TxtUsuario.Text.Trim();
                string password = txtPassword.Text.Trim();

                if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(password))
                    return;

                Usuario usuarioLogueado = negocio.Login(usuario, password);

                if (usuarioLogueado != null)
                {
                    Session["Usuario"] = usuarioLogueado;
                    Response.Redirect("Home.aspx");
                }
                else
                {
                    TxtUsuario.Text = string.Empty;
                    txtPassword.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                // Error login
            }
        }
    }
}