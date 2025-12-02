using datos;
using dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Equipo1b_TPC
{

    public partial class WebForm5 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarResumen();
                CargarResumenHistorico();
            }

        }
        private void cargarResumen()
        {

            ResumenVentaNegocio negocio = new ResumenVentaNegocio();
            gvResumenVenta.DataSource = negocio.listar(false,DateTime.Today);
            gvResumenVenta.DataBind();


        }
        private void CargarResumenHistorico()
        {
            ResumenVentaNegocio negocio = new ResumenVentaNegocio();
            List<ResumenVenta> lista = negocio.listar(true);
            //si la lista esta vacia
            if (lista == null || lista.Count == 0)
            {
                lista = new List<ResumenVenta>();

            }
            gvHistorialVentas.DataSource = lista;
            gvHistorialVentas.DataBind();
        }

        protected void btnCierreCaja_Click(object sender, EventArgs e)
        {

            ResumenVentaNegocio negocio = new ResumenVentaNegocio();
            ResumenVenta resuVenta = negocio.GetCierreActivo();
            negocio.CerrarVenta(resuVenta.NroDeCierre);
            lblMensaje.Visible = true;
            lblMensaje.CssClass = "h4 text-success fw-bold";
            lblMensaje.Text = "La caja del dia " + DateTime.Today.ToString("dd/MM/yyyy") + " se cerro correctamente ";
            cargarResumen();
            CargarResumenHistorico();

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
                lblMensajeHistorial.Text = "La primer fecha seleccionada no puede ser mayor a la segunda ";
                lblMensajeHistorial.Visible = true;
                gvHistorialVentas.DataSource = new List<ResumenVenta>();
                gvHistorialVentas.DataBind();
                return;
            }
            ResumenVentaNegocio negocio= new ResumenVentaNegocio();
            List<ResumenVenta> lresumen = negocio.filtrarPorFechas(desde, hasta);
            gvHistorialVentas.DataSource = lresumen;
            gvHistorialVentas.DataBind();
            if (lresumen.Count == 0)
            {
                lblMensajeHistorial.Text = "No se encontraron resultados para el rango ingresado";
                lblMensajeHistorial.Visible = true;
            }
            lblMensajeHistorial.Visible = false;



            
        }

        protected void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            lblMensajeHistorial.Visible = false;
            lblDesde.Text = "";
            lblHasta.Text = ""; 
            CargarResumenHistorico();
        }

        protected void btnVerDetalle_Click(object sender, EventArgs e)
        {
            //obtenemos el nro de cierre
            //obtenemos el boton que acciono el evento
            Button btn = (Button)sender;
            //obtenemos la fila
            GridViewRow fila = (GridViewRow)btn.NamingContainer;
            //obtenemos el nro
            int nroDeCierre = int.Parse(btn.CommandArgument);
            //redigirimos a pagina ver detalles
            Response.Redirect("DetalleVentas.aspx?nroDeCierre=" + nroDeCierre);
            
        }
    }
}