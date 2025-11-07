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
            ddlClientes.DataBind();

            ddlClientes.Items.Insert(0, new ListItem("seleccione un cliente"));
        }
        private void gvVacia()
        {
            gvProductos.DataSource = new List<object>();
            gvProductos.DataBind();
        }
        protected void txtFiltrarClientes_TextChanged(object sender, EventArgs e)
        {
            
            if (!chkFiltrarCuit.Checked) {
                return;
            }
            //si el textbox esta vacio ocultamos el label
            if (string.IsNullOrWhiteSpace(txtFiltrarClientes.Text))
            {
                lblModificarCliente.Visible = false;
                return;
            }

            if (txtFiltrarClientes.Text.Length == 11)
            {
                ClientesNegocio negocio = new ClientesNegocio();
                List<Cliente> lcliente = negocio.ListarPorCuit(txtFiltrarClientes.Text, false);
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
                ddlClientes.DataBind();
            }
            else
            {
                lblModificarCliente.Text = "Debe ingresar 11 digitos para filtrar por CUIT";
                lblModificarCliente.Visible = true;
                ddlClientes.Items.Clear();
                CargarClientes();
            }
            
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
            if (chkFiltrarCuit.Checked)
            {
                //checkeamos que el txt tenga una longitud de 11 para filtar por cuit
                if (txtFiltrarClientes.Text.Length == 11)
                {
                    //si esta checkeado mandamos el cuit por url
                    string cuit = txtFiltrarClientes.Text;
                    ClientesNegocio negocio = new ClientesNegocio();
                    List<Cliente> lclientes = negocio.ListarPorCuit(cuit, false);
                    if (lclientes != null && lclientes.Count > 0)
                    {
                        //si el cuit filtrado existe redirigimos a agregarcliente para modificarlo 
                        Response.Redirect("agregarCliente.aspx?cuit=" + cuit);
                    }
                    else
                    {
                        lblModificarCliente.Text = "no se encontro ningun cliente con el CUIT ingresado";
                        lblModificarCliente.CssClass = "text-danger fw-bold";
                        lblModificarCliente.Visible = true;
                    }



                }
                else
                {
                    lblModificarCliente.Text = "Para filtrar por CUIT debe ingresar exactamente 11 digitos numericos.";
                    lblModificarCliente.CssClass = "text-danger fw-bold";
                    lblModificarCliente.Visible = true;

                }
            }



            else
            {

                string razonSeleccionada = ddlClientes.SelectedValue;
                //si tenemos un cliente seleccionado en la ddl
                if (razonSeleccionada != "seleccione un cliente")
                {
                    //mandamos la razon social de la ddl por url
                    //codificamos la razon social para que rellene espacios vacios
                    Response.Redirect("AgregarCliente.aspx?razonSocial=" + Server.UrlEncode(razonSeleccionada));
                }
                else
                {
                    lblModificarCliente.Visible = true;
                }
            }
        }

        protected void chkFiltrarCuit_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}