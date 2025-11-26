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
            negocio.CerrarVenta();
            lblMensaje.Visible = true;
            lblMensaje.CssClass = "text-succes fw-bold";
            lblMensaje.Text = "La caja del dia :" + DateTime.Today + " se cerro correctamente ";
            cargarResumen();
            CargarResumenHistorico();

        }
    }
}