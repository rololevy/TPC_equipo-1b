using dominio;
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
    public partial class WebForm7 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string nroFactura = Request.QueryString["nroDeFactura"];
                if (!string.IsNullOrEmpty(nroFactura) && int.TryParse(nroFactura, out int nroFAC))
                {
                    cargarItemsVenta(nroFAC);
                    if (gvItemsVentas.Rows.Count == 0)
                    {
                        lblMensaje.Text = "No se encontro ninguna venta con el Numero de factura " + nroFactura;
                        lblMensaje.Visible = true;
                    }
                }
                else
                {
                    //si ocurre un error o viene vacio

                    gvItemsVentas.DataSource = new List<detalleVenta>();
                    gvItemsVentas.DataBind();
                    lblMensaje.Text = "No se recibio por un numero de factura valido";
                    lblMensaje.Visible = true;
                }
            }
        }
        private void cargarItemsVenta(int NroFactura)
        {
            DetalleVentasNegocio negocio = new DetalleVentasNegocio();
            List<detalleVenta> ldetalle = negocio.ListarFactura(NroFactura);

            VentaNegocio ventaNegocio = new VentaNegocio();
            List<venta> lventas = ventaNegocio.listar(0, NroFactura);
            ClientesNegocio clientesNegocio = new ClientesNegocio();
            List<Cliente> lclientes = clientesNegocio.listar(true, lventas[0].cliente.Id);
            TxtNombreCliente.Text = lclientes[0].RazonSocial;
            if (lventas.Count == 0)
            {
                lblMensaje.Text = " no se encontro ninguna venta";
                lblMensaje.Visible = true;
                return;
            }
            txtNumeroFactura.Text = NroFactura.ToString();
            switch (lventas[0].MedioPago)
            {
                case "E":
                    txtMedioPago.Text = "Efectivo";
                    break;
                case "Q":
                    txtMedioPago.Text = "QR";
                    break;
                case "T":
                    txtMedioPago.Text = "Tarjeta";
                    break;
            }
            txtFecha.Text = lventas[0].FechaVenta.ToString("dd/MM/yyyy");
            txtTotal.Text = lventas[0].totalVenta.ToString("C");
            switch (lventas[0].tipoFactura)
            {
                case "A":
                    txtTipoFactura.Text = "Factura A";
                    break;
                case "B":
                    txtTipoFactura.Text = "Factura B";
                    break;
                case "C":
                    txtTipoFactura.Text = "Factura C";
                    break;
            }
            txtTipoFactura.Visible = true;
            txtMedioPago.Visible = true;
            lblTotalFactura.Visible = true;
            txtTotal.Visible = true;
            gvItemsVentas.DataSource = ldetalle;
            gvItemsVentas.DataBind();
        }
    }
}