using Equipo1b_TPC.Dominio;
using Equipo1b_TPC.Helpers;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace Equipo1b_TPC
{
    public partial class Stock : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // ADMIN
            SeguridadHelper.ValidarAcceso(TipoUsuario.Administrador);

            if (!IsPostBack)
            {
                CargarFiltros();
                CargarGrilla();
            }
        }

        private void CargarFiltros()
        {
            try
            {
                // Cargar marcas desde la BD
                MarcaNegocio marcaNegocio = new MarcaNegocio();
                List<Marca> marcas = marcaNegocio.listar();
                ddlMarca.DataSource = marcas;
                ddlMarca.DataTextField = "Nombre";
                ddlMarca.DataValueField = "Id";
                ddlMarca.DataBind();
                ddlMarca.Items.Insert(0, new ListItem("-- Todas las marcas --", "0"));

                // Cargar categorías desde la BD
                CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
                List<Categoria> categorias = categoriaNegocio.listar();
                ddlCategoria.DataSource = categorias;
                ddlCategoria.DataTextField = "Nombre";
                ddlCategoria.DataValueField = "Id";
                ddlCategoria.DataBind();
                ddlCategoria.Items.Insert(0, new ListItem("-- Todas las categorías --", "0"));
            }
            catch (Exception ex)
            {
                // Manejo de error
                lblError.Text = "Error al cargar filtros: " + ex.Message;
                lblError.Visible = true;
            }
        }

        private void CargarGrilla()
        {
            try
            {
                ProductosNegocio negocio = new ProductosNegocio();
                List<Producto> lista = new List<Producto>();

                // Aplicar filtros
                int idMarca = 0;
                int idCategoria = 0;

                if (ddlMarca.SelectedValue != "0")
                {
                    idMarca = int.Parse(ddlMarca.SelectedValue);
                }

                if (ddlCategoria.SelectedValue != "0")
                {
                    idCategoria = int.Parse(ddlCategoria.SelectedValue);
                }

                // Obtener productos desde la BD
                lista = negocio.listar(0, idMarca, idCategoria);

                // Filtro por búsqueda de texto
                if (!string.IsNullOrEmpty(txtBuscar.Text))
                {
                    lista = lista.Where(p => p.Nombre.ToLower().Contains(txtBuscar.Text.ToLower())).ToList();
                }

                dgvStock.DataSource = lista;
                dgvStock.DataBind();
            }
            catch (Exception ex)
            {
                // Manejo de error
                lblError.Text = "Error al cargar productos: " + ex.Message;
                lblError.Visible = true;
            }
        }

        protected void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        protected void ddlMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarGrilla();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            ddlMarca.SelectedValue = "0";
            ddlCategoria.SelectedValue = "0";
            CargarGrilla();
        }

        protected void dgvStock_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Ajustar")
            {
                int idProducto = int.Parse(e.CommandArgument.ToString());
                MostrarModal(idProducto);
            }
        }

        private void MostrarModal(int idProducto)
        {
            try
            {
                ProductosNegocio negocio = new ProductosNegocio();
                List<Producto> lista = negocio.listar(idProducto);
                Producto producto = lista.FirstOrDefault();

                if (producto != null)
                {
                    hfIdProducto.Value = producto.Id.ToString();
                    lblProducto.Text = producto.Nombre;
                    txtStockActual.Text = producto.StockActual.ToString();
                    txtStockMinimo.Text = producto.StockMinimo.ToString();
                    lblError.Visible = false;
                    pnlModal.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Error al abrir modal: " + ex.Message;
                lblError.Visible = true;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                int idProducto = int.Parse(hfIdProducto.Value);
                int nuevoStockActual = int.Parse(txtStockActual.Text);
                int nuevoStockMinimo = int.Parse(txtStockMinimo.Text);

                // Validaciones
                if (nuevoStockActual < 0)
                {
                    lblError.Text = "El stock actual no puede ser negativo.";
                    lblError.Visible = true;
                    return;
                }

                if (nuevoStockMinimo < 0)
                {
                    lblError.Text = "El stock mínimo no puede ser negativo.";
                    lblError.Visible = true;
                    return;
                }

                // Actualizar stocks en la BD
                ProductosNegocio negocio = new ProductosNegocio();
                negocio.ActualizarStocks(idProducto, nuevoStockActual, nuevoStockMinimo);

                pnlModal.Visible = false;
                lblError.Visible = false;
                CargarGrilla();
            }
            catch (Exception ex)
            {
                lblError.Text = "Error al actualizar stock: " + ex.Message;
                lblError.Visible = true;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            pnlModal.Visible = false;
            lblError.Visible = false;
        }
    }
}
