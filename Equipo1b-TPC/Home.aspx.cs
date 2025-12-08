using Equipo1b_TPC.Dominio;
using Equipo1b_TPC.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Equipo1b_TPC
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // AMBOS
            SeguridadHelper.ValidarAccesoMultiple(TipoUsuario.Administrador, TipoUsuario.Vendedor);

            if (!IsPostBack)
            {
                ConfigurarVisibilidadPorRol();
            }
        }

        private void ConfigurarVisibilidadPorRol()
        {
            if (SeguridadHelper.EsVendedor())
            {
                // VENDEDOR: solo Ventas y ResumenVenta
                OcultarCard("cardStock");
                OcultarCard("cardProvedores");
                OcultarCard("cardArticulos");
                OcultarCard("cardGestionClientes");
                OcultarCard("cardGestionProveedores");
                OcultarCard("cardHistorialCompras");
            }
        }

        private void OcultarCard(string cardId)
        {
            var card = FindControlRecursive(this, cardId) as HtmlGenericControl;
            if (card != null)
            {
                card.Visible = false;
            }
        }

        private Control FindControlRecursive(Control root, string id)
        {
            if (root.ID == id)
                return root;

            foreach (Control control in root.Controls)
            {
                Control found = FindControlRecursive(control, id);
                if (found != null)
                    return found;
            }

            return null;
        }

        protected void btnVentas_Click(object sender, EventArgs e)
        {
            Response.Redirect("ventas.aspx");
        }

        protected void btnStock_Click(object sender, EventArgs e)
        {
            Response.Redirect("Stock.aspx");
        }

        protected void btnProvedores_Click(object sender, EventArgs e)
        {
            Response.Redirect("provedores.aspx");
        }

        protected void btnArticulos_Click(object sender, EventArgs e)
        {
            Response.Redirect("gestionProductos.aspx");
        }

        protected void btnResumenVenta_Click(object sender, EventArgs e)
        {
            Response.Redirect("resumenVenta.aspx");
        }

        protected void btnGestionClientes_Click(object sender, EventArgs e)
        {
            Response.Redirect("GestionClientes.aspx");
        }

        protected void btnGestionProveedores_Click(object sender, EventArgs e)
        {
            Response.Redirect("GestionProveedores.aspx");
        }

        protected void btnHistorialCompras_Click(object sender, EventArgs e)
        {
            Response.Redirect("HistorialCompras.aspx");
        }
    }
}