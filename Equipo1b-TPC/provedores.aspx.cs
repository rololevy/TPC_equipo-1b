using Equipo1b_TPC.Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Equipo1b_TPC
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        public bool filtroAvanzado { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                CargarProveedores();
                gvVacia();
            }
        }
        private void CargarProveedores()
        {
            ProvedoresNegocio negocio = new ProvedoresNegocio();
            List<Proveedor> lprov = new List<Proveedor>();
            lprov = negocio.listar();
            ddlProvedores.DataSource = lprov;
            ddlProvedores.DataTextField = "RazonSocial";
            ddlProvedores.DataValueField = "Id";
            ddlProvedores.DataBind();
            ddlProvedores.Items.Insert(0, new ListItem("Seleccione un Proveedor","0"));
            
        }
        private void gvVacia()
        {
            gvProductos.DataSource = new List<object>();
            gvProductos.DataBind();
        }
        protected void txtFiltrarProvedores_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnAgregarProvedores_Click(object sender, EventArgs e)
        {
            Response.Redirect("agregarProvedor.aspx");
        }

        protected void txtIdProducto_TextChanged(object sender, EventArgs e)
        {

        }

        protected void chkFiltros_CheckedChanged(object sender, EventArgs e)
        {
            filtroAvanzado = chkFiltros.Checked;

        }

        protected void btnModificarProvedores_Click(object sender, EventArgs e)
        {
            int idSeleccionado = int.Parse(ddlProvedores.SelectedValue);
            if (idSeleccionado == 0)
            {
                lblMensaje.Visible = true;
                lblMensaje.Text = "Debe seleccionar un proveedor para modificar";
                lblMensaje.CssClass = "text-danger fw-bold";
                return;
            }
            Response.Redirect("agregarProvedor.aspx?id="+idSeleccionado);
        }
    }
}