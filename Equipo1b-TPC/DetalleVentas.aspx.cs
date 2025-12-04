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
   
        protected void btnVerDetalle_Click(object sender, EventArgs e)
        {
            //obtenemos el boton que acciono el evento
            Button btn = (Button)sender;
            //obtenemos la fila
            GridViewRow fila = (GridViewRow)btn.NamingContainer;
            //obtenemos el nro
            int nroDeFactura = int.Parse(btn.CommandArgument);
            Response.Redirect("VerFactura.aspx?NroDeFactura="+nroDeFactura);
        }
    }
}