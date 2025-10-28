using Equipo1b_TPC.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace Equipo1b_TPC
{
    public partial class Stock : System.Web.UI.Page
    {
        //hardcodeo temporal
        private static List<Producto> listaProductos = new List<Producto>
        {
            new Producto 
            { 
                Id = 1, 
                Nombre = "Samsung Galaxy S21", 
                Marca = new Marca { Id = 1, Nombre = "Samsung" },
                Categoria = new Categoria { Id = 1, Nombre = "Electrónica" },
                StockActual = 15, 
                StockMinimo = 10, 
                PrecioCompra = 350000,
                PorcentajeGanancia = 30,
                Activo = true
            },
            new Producto 
            { 
                Id = 2, 
                Nombre = "Nike Air Max", 
                Marca = new Marca { Id = 2, Nombre = "Nike" },
                Categoria = new Categoria { Id = 2, Nombre = "Indumentaria" },
                StockActual = 5, 
                StockMinimo = 8, 
                PrecioCompra = 45000,
                PorcentajeGanancia = 40,
                Activo = true
            },
            new Producto 
            { 
                Id = 3, 
                Nombre = "Sony PS5", 
                Marca = new Marca { Id = 3, Nombre = "Sony" },
                Categoria = new Categoria { Id = 1, Nombre = "Electrónica" },
                StockActual = 20, 
                StockMinimo = 15, 
                PrecioCompra = 550000,
                PorcentajeGanancia = 25,
                Activo = true
            }
        };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarFiltros();
                CargarGrilla();
            }
        }

        private void CargarFiltros()
        {
            //marcas
            var marcas = listaProductos.Select(p => p.Marca).Where(m => m != null).GroupBy(m => m.Id).Select(g => g.First()).ToList();
            ddlMarca.DataSource = marcas;
            ddlMarca.DataTextField = "Nombre";
            ddlMarca.DataValueField = "Id";
            ddlMarca.DataBind();
            ddlMarca.Items.Insert(0, new ListItem("-- Todas las marcas --", "0"));

            //categorias
            var categorias = listaProductos.Select(p => p.Categoria).Where(c => c != null).GroupBy(c => c.Id).Select(g => g.First()).ToList();
            ddlCategoria.DataSource = categorias;
            ddlCategoria.DataTextField = "Nombre";
            ddlCategoria.DataValueField = "Id";
            ddlCategoria.DataBind();
            ddlCategoria.Items.Insert(0, new ListItem("-- Todas las categorías --", "0"));
        }

        private void CargarGrilla()
        {
            List<Producto> lista = listaProductos;

            //filtro busqueda
            if (!string.IsNullOrEmpty(txtBuscar.Text))
            {
                lista = lista.Where(p => p.Nombre.ToLower().Contains(txtBuscar.Text.ToLower())).ToList();
            }

            //filtro marca
            if (ddlMarca.SelectedValue != "0")
            {
                int idMarca = int.Parse(ddlMarca.SelectedValue);
                lista = lista.Where(p => p.Marca != null && p.Marca.Id == idMarca).ToList();
            }

            //filtro categoria
            if (ddlCategoria.SelectedValue != "0")
            {
                int idCategoria = int.Parse(ddlCategoria.SelectedValue);
                lista = lista.Where(p => p.Categoria != null && p.Categoria.Id == idCategoria).ToList();
            }

            dgvStock.DataSource = lista;
            dgvStock.DataBind();
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
            Producto producto = listaProductos.FirstOrDefault(p => p.Id == idProducto);

            if (producto != null)
            {
                hfIdProducto.Value = producto.Id.ToString();
                lblProducto.Text = producto.Nombre;
                txtStock.Text = producto.StockActual.ToString();
                pnlModal.Visible = true;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                int idProducto = int.Parse(hfIdProducto.Value);
                int nuevoStock = int.Parse(txtStock.Text);

                Producto producto = listaProductos.FirstOrDefault(p => p.Id == idProducto);
                if (producto != null)
                {
                    producto.StockActual = nuevoStock;
                }

                pnlModal.Visible = false;
                CargarGrilla();
            }
            catch (Exception ex)
            {
                //manejo error
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            pnlModal.Visible = false;
        }
    }
}
