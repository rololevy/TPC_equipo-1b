using datos;
using dominio;
using Equipo1b_TPC.Dominio;
using Equipo1b_TPC.Helpers;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Equipo1b_TPC
{
    public partial class WebForm8 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // ADMIN
            SeguridadHelper.ValidarAcceso(TipoUsuario.Administrador);

            if (!IsPostBack)
            {
                CargarCompras();
            }
        }
        private void CargarCompras()
        {
            ComprasNegocio negocio = new ComprasNegocio();
            List<Compra> lcompras = negocio.listar();
            gvHistorialCompras.DataSource = lcompras;
            gvHistorialCompras.DataBind();
        }
        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFechaDesde.Text) || string.IsNullOrEmpty(txtFechaHasta.Text))
            {
                lblMensajeHistorial.Text = "Debe seleccionar al menos dos fechas para filtrar ";
                lblMensajeHistorial.Visible = true;
                return;
            }
            DateTime desde = DateTime.Parse(txtFechaDesde.Text);
            DateTime hasta = DateTime.Parse(txtFechaHasta.Text);
            //validamos que tenga un rango correcto
            if (desde > hasta)
            {
                lblMensajeHistorial.Visible = true;
                lblMensajeHistorial.Text = "La primer fecha seleccionada no puede ser mayor a la segunda";
                gvHistorialCompras.DataSource = new List<Compra>();
                gvHistorialCompras.DataBind();
                return;
            }
            ComprasNegocio negocio = new ComprasNegocio();
            List<Compra> lcompras = negocio.listarPorFecha(desde, hasta);
            if (lcompras.Count == 0)
            {
                lblMensajeHistorial.Visible = true;
                lblMensajeHistorial.Text = "No se encontro ninguna compra en la fecha seleccionada";
                gvHistorialCompras.DataSource = new List<Compra>();
                gvHistorialCompras.DataBind();
                return;
            }
            gvHistorialCompras.DataSource = lcompras;
            gvHistorialCompras.DataBind();

        }

        protected void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            lblMensajeHistorial.Visible = false;
            lblDesde.Text = "";
            lblHasta.Text = "";
            CargarCompras();
        }

        protected void btnVerDetalle_Click(object sender, EventArgs e)
        {
            //obtenemos el nro de compra del boton que acciono el evento
            Button btn = (Button)sender;
            //obtenemos la fial
            GridViewRow fila= (GridViewRow)btn.NamingContainer;
            //obtenemos el nro
            int NroDeCompra = int.Parse(btn.CommandArgument);
            //redirigimos a la pagina ver compras
            Response.Redirect("VerCompra.aspx?NroCompra=" + NroDeCompra);
        }
    }
}