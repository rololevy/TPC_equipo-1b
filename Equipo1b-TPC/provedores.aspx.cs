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
            lprov = negocio.listar(false);
            ddlProvedores.DataSource = lprov;
            ddlProvedores.DataTextField = "RazonSocial";
            ddlProvedores.DataValueField = "Id";
            ddlProvedores.DataBind();
            ddlProvedores.Items.Insert(0, new ListItem("Seleccione un Proveedor", "0"));

        }
        private void gvVacia()
        {
            gvProductos.DataSource = new List<object>();
            gvProductos.DataBind();
        }
        protected void txtFiltrarProvedores_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtFiltrarProvedores.Text.Trim();
            //si no esta seleccionado el check salimos
            if (string.IsNullOrWhiteSpace(filtro))
            {
                //si el textbox esta vacio mostramos todo
                lblMensaje.Visible = false;
                chkCuit.Checked = false;
                CargarProveedores();
                return;
            }
            if (!chkCuit.Checked)
            {
                lblMensaje.Visible = true;
                lblMensaje.Text = "Debe marcar filtar por cuit ";
                lblMensaje.CssClass = "text-danger fw-bold";
                return;
            }
            
            ProvedoresNegocio negocio = new ProvedoresNegocio();
            List<Proveedor> lprov = negocio.listar(false, 0, "", filtro);
            //si no encontro nada
            if (lprov == null || lprov.Count == 0)
            {
                lblMensaje.Visible = true;
                lblMensaje.Text = "No se encontraron proveedores";
                lblMensaje.CssClass = "text-danger fw-bold";
                ddlProvedores.Items.Clear();
                CargarProveedores();
                return;
            }
            ddlProvedores.DataSource = lprov;
            ddlProvedores.DataTextField = "RazonSocial";
            ddlProvedores.DataValueField = "Id";
            ddlProvedores.DataBind();
            lblMensaje.Visible = false;
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
            Response.Redirect("agregarProvedor.aspx?id=" + idSeleccionado);
        }
    }
}