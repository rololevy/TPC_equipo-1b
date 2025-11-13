using Equipo1b_TPC.Dominio;
using Negocio;
using System;
using System.Collections.Generic;

namespace Equipo1b_TPC
{
    public partial class FormularioProducto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDropDowns();

                if (Request.QueryString["id"] != null)
                {
                    int ID = int.Parse(Request.QueryString["id"]);
                    ProductosNegocio negocio = new ProductosNegocio();
                    List<Producto> lProd = negocio.listar(ID);

                    if (lProd != null && lProd.Count > 0)
                    {
                        Producto prod = lProd[0];
                        txtNombre.Text = prod.Nombre;
                        txtDescripcion.Text = prod.Descripcion;
                        txtPrecioCompra.Text = prod.PrecioCompra.ToString();
                        txtPorcentajeGanancia.Text = prod.PorcentajeGanancia.ToString();
                        txtStockActual.Text = prod.StockActual.ToString();
                        txtStockMinimo.Text = prod.StockMinimo.ToString();
                        chkActivo.Checked = prod.Activo;

                        if (prod.Marca != null && prod.Marca.Id != 0)
                            ddlMarca.SelectedValue = prod.Marca.Id.ToString();

                        if (prod.Categoria != null && prod.Categoria.Id != 0)
                            ddlCategoria.SelectedValue = prod.Categoria.Id.ToString();

                        if (prod.Provedor != null && prod.Provedor.Id != 0)
                            ddlProveedor.SelectedValue = prod.Provedor.Id.ToString();

                        lblTitulo.Text = "Modificar Producto";
                        btnGuardar.Text = "Actualizar";
                    }
                }
            }
        }

        private void CargarDropDowns()
        {
            try
            {
                MarcaNegocio marcaNegocio = new MarcaNegocio();
                CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
                ProvedoresNegocio proveedorNegocio = new ProvedoresNegocio();

                ddlMarca.DataSource = marcaNegocio.listar();
                ddlMarca.DataTextField = "Nombre";
                ddlMarca.DataValueField = "Id";
                ddlMarca.DataBind();
                ddlMarca.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione...", "0"));

                ddlCategoria.DataSource = categoriaNegocio.listar();
                ddlCategoria.DataTextField = "Nombre";
                ddlCategoria.DataValueField = "Id";
                ddlCategoria.DataBind();
                ddlCategoria.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione...", "0"));

                ddlProveedor.DataSource = proveedorNegocio.listar(false);
                ddlProveedor.DataTextField = "RazonSocial";
                ddlProveedor.DataValueField = "Id";
                ddlProveedor.DataBind();
                ddlProveedor.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Seleccione...", "0"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtPrecioCompra.Text))
                {
                    MostrarMensaje("Debe ingresar al menos el Nombre y el Precio de Compra", false);
                    return;
                }

                ProductosNegocio negocio = new ProductosNegocio();
                Producto prod = new Producto
                {
                    Nombre = txtNombre.Text.Trim(),
                    Descripcion = txtDescripcion.Text.Trim(),
                    PrecioCompra = decimal.Parse(txtPrecioCompra.Text),
                    PorcentajeGanancia = int.Parse(txtPorcentajeGanancia.Text),
                    StockActual = int.Parse(txtStockActual.Text),
                    StockMinimo = int.Parse(txtStockMinimo.Text),
                    Activo = chkActivo.Checked
                };

                if (ddlMarca.SelectedValue != "0")
                    prod.Marca = new Marca { Id = int.Parse(ddlMarca.SelectedValue) };

                if (ddlCategoria.SelectedValue != "0")
                    prod.Categoria = new Categoria { Id = int.Parse(ddlCategoria.SelectedValue) };

                if (ddlProveedor.SelectedValue != "0")
                    prod.Provedor = new Proveedor { Id = int.Parse(ddlProveedor.SelectedValue) };

                if (Request.QueryString["id"] != null)
                {
                    prod.Id = int.Parse(Request.QueryString["id"]);
                    negocio.modificar(prod);
                    MostrarMensaje("Producto modificado correctamente", true);
                }
                else
                {
                    negocio.agregar(prod);
                    MostrarMensaje("Producto agregado correctamente", true);
                    LimpiarCampos();
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message, false);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("productos.aspx", false);
        }

        private void MostrarMensaje(string mensaje, bool esExito)
        {
            lblMensaje.Visible = true;
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = esExito ? "alert alert-success" : "alert alert-danger";
        }

        private void LimpiarCampos()
        {
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtPrecioCompra.Text = "";
            txtPorcentajeGanancia.Text = "";
            txtStockActual.Text = "";
            txtStockMinimo.Text = "";
            ddlMarca.SelectedValue = "0";
            ddlCategoria.SelectedValue = "0";
            ddlProveedor.SelectedValue = "0";
            chkActivo.Checked = true;
        }
    }
}
