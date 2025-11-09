using Equipo1b_TPC.Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace Equipo1b_TPC
{
    public partial class productos : System.Web.UI.Page
    {
        private ProductosNegocio negocio = new ProductosNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarFiltros();
                CargarProductos();
            }
        }

        private void CargarFiltros()
        {
            try
            {
                MarcaNegocio marcaNegocio = new MarcaNegocio();
                CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

                // Cargar Marcas
                ddlMarca.DataSource = marcaNegocio.listar();
                ddlMarca.DataTextField = "Nombre";
                ddlMarca.DataValueField = "Id";
                ddlMarca.DataBind();
                ddlMarca.Items.Insert(0, new ListItem("-- Todas las marcas --", "0"));

                // Cargar Categorías
                ddlCategoria.DataSource = categoriaNegocio.listar();
                ddlCategoria.DataTextField = "Nombre";
                ddlCategoria.DataValueField = "Id";
                ddlCategoria.DataBind();
                ddlCategoria.Items.Insert(0, new ListItem("-- Todas las categorías --", "0"));
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar filtros: " + ex.Message, false);
            }
        }

        private void CargarProductos()
        {
            try
            {
                List<Producto> lista = negocio.listar();

                // Aplicar filtro de búsqueda
                if (!string.IsNullOrEmpty(txtBuscarProd.Text))
                {
                    lista = lista.Where(p => p.Nombre.ToUpper().Contains(txtBuscarProd.Text.ToUpper())).ToList();
                }

                // Aplicar filtro de marca
                if (ddlMarca.SelectedValue != "0")
                {
                    int idMarca = int.Parse(ddlMarca.SelectedValue);
                    lista = lista.Where(p => p.Marca != null && p.Marca.Id == idMarca).ToList();
                }

                // Aplicar filtro de categoría
                if (ddlCategoria.SelectedValue != "0")
                {
                    int idCategoria = int.Parse(ddlCategoria.SelectedValue);
                    lista = lista.Where(p => p.Categoria != null && p.Categoria.Id == idCategoria).ToList();
                }

                gvProductosPage.DataSource = lista;
                gvProductosPage.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar productos: " + ex.Message, false);
            }
        }

        protected void txtBuscarProd_TextChanged(object sender, EventArgs e)
        {
            CargarProductos();
        }

        protected void ddlMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarProductos();
        }

        protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarProductos();
        }

        protected void btnNuevoProd_Click(object sender, EventArgs e)
        {
            Response.Redirect("FormularioProducto.aspx", false);
        }

        protected void gvProductosPage_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int idProducto = int.Parse(e.CommandArgument.ToString());

                if (e.CommandName == "Editar")
                {
                    Response.Redirect("FormularioProducto.aspx?id=" + idProducto, false);
                }
                else if (e.CommandName == "Eliminar")
                {
                    negocio.eliminar(idProducto);
                    MostrarMensaje("Producto eliminado correctamente", true);
                    CargarProductos();
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message, false);
            }
        }

        private void MostrarMensaje(string mensaje, bool esExito)
        {
            lblMensaje.Visible = true;
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = esExito ? "alert alert-success" : "alert alert-danger";
        }
    }
}
