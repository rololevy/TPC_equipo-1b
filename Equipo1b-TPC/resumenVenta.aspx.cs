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
                gvVacia();
                
            }

        }
        private void gvVacia()
        {
            gvResumenVenta.DataSource = new List<object>();
            gvResumenVenta.DataBind();
            gvHistorialVentas.DataSource = new List<object>();
            gvHistorialVentas.DataBind();

        }
            
    }
}