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
                cargarGrilla();
            }
        }
        private void cargarGrilla()
        {
            ClientesNegocio negocio = new ClientesNegocio();
            try
            {
                List<Cliente> lista = negocio.listar(true);
                gvClientes.DataSource = lista;
                gvClientes.DataBind();
            }
            catch(Exception ex)
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
            //obtenemos el cuit de la fila
            String cuitSeleccionado = btn.CommandArgument;
            //redigirimos a la pagina para modificar
            Response.Redirect("AgregarCliente.aspx?cuit=" + cuitSeleccionado);

        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            //obtenemos el boton que acciono el evento
            Button btn = (Button)sender;
            //obtenemos la fila
            GridViewRow fila = (GridViewRow)btn.NamingContainer;

            String cuitSeleccionado = btn.CommandArgument;
            try
            {
                ClientesNegocio negocio = new ClientesNegocio();
                negocio.bajaLogica(cuitSeleccionado);
                cargarGrilla();
            }
            catch(Exception ex)
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

            String cuitSeleccionado = btn.CommandArgument;
            try
            {
                ClientesNegocio negocio = new ClientesNegocio();
                negocio.AltaLogica(cuitSeleccionado);
                cargarGrilla();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            ClientesNegocio negocio = new ClientesNegocio();
            List<Cliente> listaFiltrada = new List<Cliente>();
            string filtro = txtFiltro.Text.Trim();
            //si el campo esta vacio recargamos todo
            if(string.IsNullOrEmpty(filtro))
            if (chkCuit.Checked)
            {
                    cargarGrilla();
                    return;
            }
            //si esta marcado filtrar por cuit
            if (chkCuit.Checked)
            {
                    listaFiltrada = negocio.ListarPorCuit(filtro, true);
            }
            //si esta marcado filtrar por razon social
            else if (ChkRazonSocial.Checked)
            {
                listaFiltrada = negocio.listar(true, filtro);
            }
            else
            {
                lblFiltro.Text = "selecione un tipo de filtro (CUIT o razon social)";
                lblFiltro.Visible = true;
            }
            //mostramos mensaje si no hay resultados
            if(listaFiltrada==null|| listaFiltrada.Count == 0)
            {
                lblFiltro.Text = "No se encontraron clientes.";
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
            cargarGrilla();
        }
    }
}