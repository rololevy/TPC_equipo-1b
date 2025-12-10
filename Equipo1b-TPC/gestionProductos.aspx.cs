using Equipo1b_TPC.Dominio;
using Equipo1b_TPC.Helpers;
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
            // ADMIN
            SeguridadHelper.ValidarAcceso(TipoUsuario.Administrador);
        }

        // NavMarcas
        protected void btnGestionMarcas_Click(object sender, EventArgs e)
        {
            Response.Redirect("marcas.aspx");
        }

        // NavCategorias
        protected void btnGestionCategorias_Click(object sender, EventArgs e)
        {
            Response.Redirect("categorias.aspx");
        }

        // GoProductos
        protected void btnGestionProductos_Click(object sender, EventArgs e)
        {
            Response.Redirect("productos.aspx");
        }
    }
}