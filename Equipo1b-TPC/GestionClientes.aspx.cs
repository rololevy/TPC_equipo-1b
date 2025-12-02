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
    public partial class WebForm6 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarClientes();
            }
        }
        private void cargarClientes()
        {
            ClientesNegocio negocio = new ClientesNegocio();
            try
            {
                List<Cliente> lista = negocio.listar(true);
                gvClientes.DataSource = lista;
                gvClientes.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnAgregarCliente_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarCliente.aspx");

        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            //obtenemos el boton que acciono el evento
            Button btn = (Button)sender;
            //obtenemos la fila
            GridViewRow fila = (GridViewRow)btn.NamingContainer;
            //obtenemos el id de la fila
            int id = int.Parse(btn.CommandArgument);
            //redigirimos a la pagina para modificar
            Response.Redirect("AgregarCliente.aspx?id=" + id);

        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            //obtenemos el boton que acciono el evento
            Button btn = (Button)sender;
            //obtenemos la fila
            GridViewRow fila = (GridViewRow)btn.NamingContainer;

            int id = int.Parse(btn.CommandArgument);
            try
            {
                ClientesNegocio negocio = new ClientesNegocio();
                negocio.bajaLogica(id);
                cargarClientes();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        protected void btnActivar_Click(object sender, EventArgs e)
        {
            //obtenemos el boton que acciono el evento
            Button btn = (Button)sender;
            //obtenemos la fila
            GridViewRow fila = (GridViewRow)btn.NamingContainer;

            int id = int.Parse(btn.CommandArgument);
            try
            {
                ClientesNegocio negocio = new ClientesNegocio();
                negocio.AltaLogica(id);
                cargarClientes();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtFiltro.Text.Trim();
            //si el txt esta vacio recargamos los controles
            if (string.IsNullOrEmpty(filtro))
            {
                lblFiltro.Visible = false;
                chkCuit.Checked = false;
                ChkRazonSocial.Checked = false;
                cargarClientes();
                return;
            }
            if (!chkCuit.Checked && !ChkRazonSocial.Checked)
            {
                lblFiltro.Text = "selecione un tipo de filtro (CUIT o razon social)";
                lblFiltro.CssClass = "text-danger fw-bold";
                lblFiltro.Visible = true;
                return;
            }
            ClientesNegocio negocio = new ClientesNegocio();
            List<Cliente> listaFiltrada = new List<Cliente>();
            // si esta marcado filtrar por cuit
            if (chkCuit.Checked)
            {
                listaFiltrada = negocio.ListarPorCuit(filtro, true);
                
            }
            //si esta marcado filtrar por razon social
            else if (ChkRazonSocial.Checked)
            {
                listaFiltrada = negocio.ListarPorRazonSocial(filtro, true);
            }

            //verificamos si encontro algun cliente
            if (listaFiltrada == null || listaFiltrada.Count == 0)
            {
                lblFiltro.Text = "No se encontraron clientes.";
                lblFiltro.CssClass = "text-danger fw-bold";
                lblFiltro.Visible = true;
                gvClientes.DataSource = null;
                gvClientes.DataBind();
                return;
            }
            //mostramos los resultados
            lblFiltro.Visible = false;
            gvClientes.DataSource = listaFiltrada;
            gvClientes.DataBind();
            
        }

        protected void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            cargarClientes();
        }
    }
}