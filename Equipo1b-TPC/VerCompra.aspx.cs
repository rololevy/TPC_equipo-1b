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
using System.Web.WebSockets;

namespace Equipo1b_TPC
{
    public partial class WebForm9 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // ADMIN
            SeguridadHelper.ValidarAcceso(TipoUsuario.Administrador);

            if (!IsPostBack)
            {
                string nroCompra = Request.QueryString["NroCompra"];
                if(!string.IsNullOrEmpty(nroCompra) && int.TryParse(nroCompra,out int NumeroCompra)){
                    CargarDetalle(NumeroCompra);
                    if (gvItemsCompra.Rows.Count == 0)
                    {
                        lblMensaje.Text = "No se encontro ninguna venta con el numero de compra " + NumeroCompra;
                        lblMensaje.Visible = true;
                    }
                }
                else
                {
                    //si ocurre error o viene vacio
                    gvItemsCompra.DataSource = new List<detalleCompra>();
                    gvItemsCompra.DataBind();
                    lblMensaje.Text = "No se recibio un numero de compra valido";
                    lblMensaje.Visible = true;
                }
            }

        }
        private void CargarDetalle(int NroCompra)
        {
            
            ComprasNegocio negocio = new ComprasNegocio();
            List<Compra> lcompra = negocio.listar(NroCompra);
            List<detalleCompra> ldetalle = negocio.listarDetalleCompras(NroCompra);
            txtNumeroCompra.Text = NroCompra.ToString();
            txtFecha.Text = lcompra[0].FechaCompra.ToShortDateString();
            txtRazonSocial.Text = lcompra[0].Proveedor.RazonSocial;
            txtTotal.Text = lcompra[0].Total.ToString();
            txtTotal.Visible = true;

            gvItemsCompra.DataSource = ldetalle;
            gvItemsCompra.DataBind();
        }
    }
}