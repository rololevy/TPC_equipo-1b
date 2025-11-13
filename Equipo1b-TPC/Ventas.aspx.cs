using Equipo1b_TPC.Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Equipo1b_TPC
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        public bool filtroAvanzado { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gvVacia();
                CargarClientes();
            }
        }
        private void CargarClientes()
        {
            ClientesNegocio cliente = new ClientesNegocio();
            List<Cliente> lcliente = cliente.listar(false);
            ddlClientes.DataSource = lcliente;
            ddlClientes.DataTextField = "RazonSocial";
            ddlClientes.DataValueField = "Id";
            ddlClientes.DataBind();

            ddlClientes.Items.Insert(0, new ListItem("seleccione un cliente", "0"));
        }
        private void gvVacia()
        {
            gvProductos.DataSource = new List<object>();
            gvProductos.DataBind();
        }
        protected void txtFiltrarClientes_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtFiltrarClientes.Text.Trim();
            //si el txt esta vacio volvemos a recargar los controles
            if (string.IsNullOrWhiteSpace(filtro))
            {
                lblModificarCliente.Visible = false;
                chkFiltrarCuit.Checked = false;
                CargarClientes();
                return;
            }
            //si no esta seleccionado el checkbox salimos
            if (!chkFiltrarCuit.Checked)
            {
                lblModificarCliente.Visible = true;
                lblModificarCliente.Text = "Debe marcar filtar por cuit";
                lblModificarCliente.CssClass = "text-danger fw-bold";
                return;
            }

            ClientesNegocio negocio = new ClientesNegocio();
            List<Cliente> lcliente = negocio.ListarPorCuit(filtro, false);
            //si no encontro nada
            if (lcliente == null || lcliente.Count == 0)
            {
                lblModificarCliente.Text = "no se encontro ningun cliente con el CUIT ingresado";
                lblModificarCliente.CssClass = "text-danger fw-bold";
                lblModificarCliente.Visible = true;
                ddlClientes.Items.Clear();
                CargarClientes();
                return;
            }
            ddlClientes.DataSource = lcliente;
            ddlClientes.DataTextField = "razonSocial";
            ddlClientes.DataValueField = "Id";
            ddlClientes.DataBind();
            lblModificarCliente.Visible = false;
        }
        protected void btnAgregarCliente_Click(object sender, EventArgs e)
        {
            Response.Redirect("agregarCliente.aspx");
        }

        protected void txtIdProducto_TextChanged(object sender, EventArgs e)
        {

        }

        protected void chkFiltro_CheckedChanged(object sender, EventArgs e)
        {
            filtroAvanzado = chkFiltro.Checked;

        }

        protected void btnModificarCliente_Click(object sender, EventArgs e)
        {
            int idSeleccionado = int.Parse(ddlClientes.SelectedValue);
            if (idSeleccionado == 0)
            {
                lblModificarCliente.Visible = true;
                lblModificarCliente.Text = "Debe seleccionar un cliente para modificar";
                lblModificarCliente.CssClass= "text-danger fw-bold";
                return;
               
            }
            Response.Redirect("AgregarCliente.aspx?id=" + idSeleccionado);
        }

        protected void chkFiltrarCuit_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}