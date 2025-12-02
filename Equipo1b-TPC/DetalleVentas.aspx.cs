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
    public partial class HistorialVentas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string nro = Request.QueryString["nroDeCierre"];
                if(!string.IsNullOrEmpty(nro) && int.TryParse(nro,out int nroCierre))
                {
                    CargarVentas(nroCierre);
                    if (gvDetalleVentas.Rows.Count == 0)
                    {
                        lblMensaje.Text = "No hay ventas registradas";
                        lblMensaje.Visible = true;
                    }
                }
                else
                {
                    //si ocurre un error o viene vacio
                    gvDetalleVentas.DataSource = new List<venta>();
                    gvDetalleVentas.DataBind();
                    lblMensaje.Text = "No se recibio por un numero de cierre valido";
                    lblMensaje.Visible = true;
                    
                }
                
            }
        }
       private void CargarVentas(int nroDeCierre){
            VentaNegocio negocio = new VentaNegocio();
            List<venta> lventas = negocio.listar(nroDeCierre);
            gvDetalleVentas.DataSource = lventas;
            gvDetalleVentas.DataBind();
            
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
                Label1.Text = " no se encontro ninguna venta";
                Label1.Visible = true;
                return;
            }
            txtNumeroFactura.Text = NroFactura.ToString();
            switch (lventas[0].MedioPago)
            {
                case "E":
                    txtMedioPago.Text = "Efectivo";
                    break;
                case "Q":
                    txtMedioPago.Text = "Codigo QR";
                    break;
                case "T":
                    txtMedioPago.Text = "Tarjeta";
                    break;
            }
            txtFecha.Text = lventas[0].FechaVenta.ToString("dd/MM/yyyy");
            txtTotal.Text = lventas[0].totalVenta.ToString("C");
            lblTotalFactura.Visible = true;
            txtTotal.Visible = true;
            gvItemsVentas.DataSource = ldetalle;
            gvItemsVentas.DataBind();
        }
        protected void btnVerDetalle_Click(object sender, EventArgs e)
        {
            //obtenemos el boton que acciono el evento
            Button btn = (Button)sender;
            //obtenemos la fila
            GridViewRow fila = (GridViewRow)btn.NamingContainer;
            //obtenemos el nro
            int nroDeFactura = int.Parse(btn.CommandArgument);
            cargarItemsVenta(nroDeFactura);
        }
    }
}