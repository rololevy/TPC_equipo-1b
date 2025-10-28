using System; 
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Equipo1b_TPC
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        public bool filtroAvanzado { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gvVacia();
            }
        }

        private void gvVacia()
        {
            gvProductos.DataSource = new List<object>();
            gvProductos.DataBind();
        }
        protected void txtFiltrarProvedores_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnAgregarProvedores_Click(object sender, EventArgs e)
        {
            Response.Redirect("agregarProvedor.aspx");
        }

        protected void txtIdProducto_TextChanged(object sender, EventArgs e)
        {

        }

        protected void chkFiltros_CheckedChanged(object sender, EventArgs e)
        {
            filtroAvanzado = chkFiltros.Checked;
            
        }
    }
}