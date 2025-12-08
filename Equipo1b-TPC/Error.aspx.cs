using System;
using System.Web;
using System.Web.UI;

namespace Equipo1b_TPC
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarError();
            }
        }

        private void CargarError()
        {
            if (Session["LastError"] != null)
            {
                lblErrorMessage.Text = Session["LastError"].ToString();
            }
            else
            {
                lblErrorMessage.Text = "Ha ocurrido un error inesperado.";
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            LimpiarSesion();
            Response.Redirect("Home.aspx", false);
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            LimpiarSesion();
            Response.Redirect("Home.aspx", false);
        }

        private void LimpiarSesion()
        {
            if (Session["LastError"] != null)
                Session.Remove("LastError");
            
            if (Session["LastErrorPage"] != null)
                Session.Remove("LastErrorPage");
        }
    }
}
