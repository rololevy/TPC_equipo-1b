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
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            LimpiarSesion();
            Response.Redirect("~/Default.aspx", false);
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            LimpiarSesion();
            
            if (Request.UrlReferrer != null && !Request.UrlReferrer.ToString().Contains("Error.aspx"))
            {
                Response.Redirect(Request.UrlReferrer.ToString(), false);
            }
            else
            {
                Response.Redirect("~/Default.aspx", false);
            }
        }

        private void LimpiarSesion()
        {
            Session.Remove("LastError");
            Session.Remove("LastErrorPage");
        }
    }
}
