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
    public partial class GestionProveedores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // ADMIN
            SeguridadHelper.ValidarAcceso(TipoUsuario.Administrador);

            if (!IsPostBack)
            {
                cargarGrilla();
            }
        }
        private void cargarGrilla()
        {
            ProvedoresNegocio negocio = new ProvedoresNegocio();
            try
            {
                List<Proveedor> lprov = negocio.listar(true);
                gvProveedores.DataSource = lprov;
                gvProveedores.DataBind();
            }
            catch(Exception ex)
            {
                throw ex;
            }


        }
        protected void btnAgregarProveedor_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarProvedor.aspx");
        }

        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtFiltro.Text.Trim();
            //validamos si el txt esta vacio
            if (string.IsNullOrWhiteSpace(filtro))
            {
                lblFiltro.Visible = false;
                chkCuit.Checked = false;
                ChkRazonSocial.Checked = false;
                cargarGrilla();
                return;
            }
            //validamos que algun chk de los filtros este chekeado
            if (!chkCuit.Checked && !ChkRazonSocial.Checked)
            {
                lblFiltro.Visible = true;
                lblFiltro.Text = "selecione un tipo de filtro (CUIT o razon social)";
                lblFiltro.CssClass = "text-danger fw-bold";
                return;
            }

            ProvedoresNegocio negocio = new ProvedoresNegocio();
            List<Proveedor> lprov = new List<Proveedor>();
           
            //filtrar por CUIT
            if (chkCuit.Checked)
            {
                lprov = negocio.listar(true,0,"",filtro);
            }
            //Filtrar por razon social
            else if (ChkRazonSocial.Checked)
            {
                lprov = negocio.listar(true,0,filtro,"");
            }
            //verificamos si encontro algun proveedor
            if (lprov == null || lprov.Count == 0){
                lblFiltro.Visible = true;
                lblFiltro.Text = "No se encontraron proveedores";
                lblFiltro.CssClass = "text-danger fw-bold";
                gvProveedores.DataSource = null;
                gvProveedores.DataBind();
                return;
            }
            //mostramos resultados
            lblFiltro.Visible = false;
            gvProveedores.DataSource = lprov;
            gvProveedores.DataBind();
            return;
        }

        protected void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            cargarGrilla();
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            //obtenemos el boton que acciono el evento
            Button btn = (Button)sender;
            //obtenemos la fila
            GridViewRow fila = (GridViewRow)btn.NamingContainer;
            //obtenemos el id
            int id = int.Parse(btn.CommandArgument);
            Response.Redirect("AgregarProvedor.aspx?id="+id);
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            //obtenemos el boton que acciono el evento
            Button btn = (Button)sender;
            //obtenemos la fila
            GridViewRow fila = (GridViewRow)btn.NamingContainer;
            //obtenemos el id de la fila
            int id = int.Parse(btn.CommandArgument);
            ProvedoresNegocio negocio = new ProvedoresNegocio();
            negocio.bajaLogica(id);
            cargarGrilla();
        }

        protected void btnActivar_Click(object sender, EventArgs e)
        {
            //obtenemos el boton que acciono el evento
            Button btn = (Button)sender;
            //obtenemos la fila
            GridViewRow fila = (GridViewRow)btn.NamingContainer;
            //obtemos el id de la fila
            int id = int.Parse(btn.CommandArgument);
            ProvedoresNegocio negocio = new ProvedoresNegocio();
            negocio.AltaLogica(id);
            cargarGrilla();

        }
    }
}