using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Equipo1b_TPC
{
    public partial class gestionArticulos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGestionMarcas_Click(object sender, EventArgs e)
        {
            Response.Redirect("marcas.aspx");
        }

        protected void btnGestionCategorias_Click(object sender, EventArgs e)
        {
            Response.Redirect("categorias.aspx");
        }
    }
}