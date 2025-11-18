using Equipo1b_TPC.Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Globalization;
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
                CargarProductos();
            }
        }
        private void CargarProductos()
        {
            ProductosNegocio negocio= new ProductosNegocio();
            List<Producto> lproducto = new List<Producto>();
            lproducto = negocio.listar();
            ddlProductos.DataSource = lproducto;
            ddlProductos.DataTextField = "Nombre";
            ddlProductos.DataValueField = "Id";
            ddlProductos.DataBind();
            ddlProductos.Items.Insert(0, new ListItem("Seleccione un producto", "0"));
        }
        private void FiltrarProductos()
        {
            
            int idMarcas = int.Parse(ddlMarcas.SelectedValue);
            int idCategorias = int.Parse(ddlCategorias.SelectedValue);
            ProductosNegocio negocio = new ProductosNegocio();
            List<Producto> lprod = new List<Producto>();
            if (idMarcas != 0 && idCategorias != 0)
            {  
                lprod = negocio.listar(0, idMarcas, idCategorias);
            }
            else if (idMarcas != 0)
            {     
                lprod = negocio.listar(0, idMarcas, 0);
            }
            else if (idCategorias != 0)
            {
                lprod = negocio.listar(0, 0, idCategorias);
            }
            else
            {
                return;
            }
            ddlProductos.DataSource = lprod;
            ddlProductos.DataValueField = "Id";
            ddlProductos.DataTextField = "Nombre";
            ddlProductos.DataBind();
            lblFiltro.Visible = false;
            if (lprod.Count == 0)
            {
                lblFiltro.Visible = true;
                lblFiltro.Text = "No se encontro ningun producto con los filtros selecionados";
            }
            return;

        }
        private void CargarMarcas()
        {
            MarcaNegocio negocio = new MarcaNegocio();
            List<Marca> lmarca = new List<Marca>();
            lmarca = negocio.listar();
            ddlMarcas.DataSource = lmarca;
            ddlMarcas.DataTextField = "Nombre";
            ddlMarcas.DataValueField = "Id";
            ddlMarcas.DataBind();
            ddlMarcas.Items.Insert(0, new ListItem("Seleccione una marca", "0"));
        }
        private void CargarCategorias()
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            List<Categoria> lcat = new List<Categoria>();
            lcat = negocio.listar();
            ddlCategorias.DataSource = lcat;
            ddlCategorias.DataTextField = "Nombre";
            ddlCategorias.DataValueField = "Id";
            ddlCategorias.DataBind();
            ddlCategorias.Items.Insert(0, new ListItem("Seleccione un producto", "0"));
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
            //si esta vacio 
            string id = txtIdProducto.Text;
            if (string.IsNullOrWhiteSpace(id))
            {
                lblProducto.Visible = false;
                CargarProductos();
                return;
            }
            if(!int.TryParse(id,out int idProducto))
            {
                lblProducto.Text = "El ID debe ser numerico";
                lblProducto.CssClass += "text-danger";
                lblProducto.Visible = true;
                CargarProductos();
                return;
            }
            ProductosNegocio negocio = new ProductosNegocio();
            List<Producto> lproductos = negocio.listar(int.Parse(id));
            //si no encontro nada
            if(lproductos==null || lproductos.Count == 0)
            {
                lblProducto.CssClass += "text-danger";
                lblProducto.Text = "No se encontro ningun producto con el id ingresado";
                lblProducto.Visible = true;
                ddlProductos.Items.Clear();
                CargarProductos();
                return;
            }
            //si encontro 
            ddlProductos.DataSource = lproductos;
            ddlProductos.DataTextField = "Nombre";
            ddlProductos.DataValueField = "Id";
            ddlProductos.DataBind();
            lblProducto.Visible = false;

        }

        protected void chkFiltro_CheckedChanged(object sender, EventArgs e)
        {
            filtroAvanzado = chkFiltro.Checked;
            if (filtroAvanzado) {
                CargarCategorias();
                CargarMarcas();
            }
            else
            {
                lblFiltro.Visible = false;
                CargarProductos();
            }

           
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


   

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            if (chkFiltro.Checked) {
                FiltrarProductos();
            }
            
        }
    }
}