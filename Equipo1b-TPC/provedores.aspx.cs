using Equipo1b_TPC.Dominio;
using Equipo1b_TPC.Helpers;
using dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace Equipo1b_TPC
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        List<detalleVenta> ldetalle = new List<detalleVenta>();
        public bool filtroAvanzado { get; set; }

        private List<DetalleCompraTemp> DetallesCompra
        {
            get
            {
                if (Session["DetallesCompra"] == null)
                {
                    Session["DetallesCompra"] = new List<DetalleCompraTemp>();
                }
                return (List<DetalleCompraTemp>)Session["DetallesCompra"];
            }
            set
            {
                Session["DetallesCompra"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // ADMIN
            SeguridadHelper.ValidarAcceso(TipoUsuario.Administrador);

            if (!IsPostBack)
            {
                CargarProveedores();
                ActualizarGridProductos();
            }
        }

        private void CargarProveedores()
        {
            ProvedoresNegocio negocio = new ProvedoresNegocio();
            List<Proveedor> lprov = negocio.listar(false);
            ddlProvedores.DataSource = lprov;
            ddlProvedores.DataTextField = "RazonSocial";
            ddlProvedores.DataValueField = "Id";
            ddlProvedores.DataBind();
            ddlProvedores.Items.Insert(0, new ListItem("Seleccione un Proveedor", "0"));
        }

        private void CargarProductosPorProveedor()
        {
            int idProveedor = int.Parse(ddlProvedores.SelectedValue);

            if (idProveedor == 0)
            {
                ddlProductos.Items.Clear();
                ddlProductos.Items.Insert(0, new ListItem("Primero seleccione un proveedor", "0"));
                return;
            }

            ProductosNegocio negocio = new ProductosNegocio();

            // Si hay filtros avanzados activos
            int idMarca = 0;
            int idCategoria = 0;

            if (filtroAvanzado)
            {
                if (ddlMarcas.SelectedValue != "0")
                    idMarca = int.Parse(ddlMarcas.SelectedValue);

                if (ddlCategorias.SelectedValue != "0")
                    idCategoria = int.Parse(ddlCategorias.SelectedValue);
            }

            // Cargar solo productos del proveedor seleccionado
            List<Producto> lproducto = negocio.listarPorProveedor(idProveedor, idMarca, idCategoria);

            ddlProductos.DataSource = lproducto;
            ddlProductos.DataTextField = "Nombre";
            ddlProductos.DataValueField = "Id";
            ddlProductos.DataBind();
            ddlProductos.Items.Insert(0, new ListItem("Seleccione un Producto", "0"));

            if (lproducto.Count == 0)
            {
                MostrarMensaje("⚠️ Este proveedor no tiene productos asignados. Cree productos en Gestión de Productos.", "warning");
            }
        }

        private void CargarMarcas()
        {
            MarcaNegocio negocio = new MarcaNegocio();
            List<Marca> lmarca = negocio.listar();
            ddlMarcas.DataSource = lmarca;
            ddlMarcas.DataTextField = "Nombre";
            ddlMarcas.DataValueField = "Id";
            ddlMarcas.DataBind();
            ddlMarcas.Items.Insert(0, new ListItem("Seleccione una marca", "0"));
        }

        private void CargarCategorias()
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            List<Categoria> lcat = negocio.listar();
            ddlCategorias.DataSource = lcat;
            ddlCategorias.DataTextField = "Nombre";
            ddlCategorias.DataValueField = "Id";
            ddlCategorias.DataBind();
            ddlCategorias.Items.Insert(0, new ListItem("Seleccione una categoría", "0"));
        }

        private void ActualizarGridProductos()
        {
            gvProductos.DataSource = DetallesCompra;
            gvProductos.DataBind();
            ActualizarTotal();
        }

        private void ActualizarTotal()
        {
            decimal total = DetallesCompra.Sum(d => d.Subtotal);
            lblTotal.Text = total.ToString("C2");
        }

        protected void txtFiltrarProvedores_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtFiltrarProvedores.Text.Trim();

            if (string.IsNullOrWhiteSpace(filtro))
            {
                OcultarMensaje();
                CargarProveedores();
                return;
            }

            ProvedoresNegocio negocio = new ProvedoresNegocio();
            List<Proveedor> lprov = negocio.listar(false, 0, filtro);

            if (lprov == null || lprov.Count == 0)
            {
                MostrarMensaje("No se encontraron proveedores con ese criterio", "danger");
                ddlProvedores.Items.Clear();
                CargarProveedores();
                return;
            }

            ddlProvedores.DataSource = lprov;
            ddlProvedores.DataTextField = "RazonSocial";
            ddlProvedores.DataValueField = "Id";
            ddlProvedores.DataBind();
            ddlProvedores.Items.Insert(0, new ListItem("Seleccione un Proveedor", "0"));
            OcultarMensaje();
        }

        protected void ddlProvedores_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Al seleccionar un proveedor, cargar automáticamente sus productos
            CargarProductosPorProveedor();
        }

        protected void ddlProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idProducto = int.Parse(ddlProductos.SelectedValue);

            if (idProducto > 0)
            {
                ProductosNegocio negocio = new ProductosNegocio();
                List<Producto> productos = negocio.listar(idProducto);

                if (productos != null && productos.Count > 0)
                {
                    txtPrecioUnitario.Text = productos[0].PrecioCompra.ToString("F2");
                }
            }
        }

        protected void chkFiltros_CheckedChanged(object sender, EventArgs e)
        {
            filtroAvanzado = chkFiltros.Checked;

            if (filtroAvanzado)
            {
                CargarMarcas();
                CargarCategorias();
            }

            // Recargar productos con o sin filtros
            CargarProductosPorProveedor();
        }

        protected void ddlFiltros_Changed(object sender, EventArgs e)
        {
            // Recargar productos aplicando los filtros de marca y categoría
            CargarProductosPorProveedor();
        }

        protected void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            ddlMarcas.SelectedIndex = 0;
            ddlCategorias.SelectedIndex = 0;
            CargarProductosPorProveedor();
        }

        protected void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar proveedor seleccionado
                int idProveedor = int.Parse(ddlProvedores.SelectedValue);
                if (idProveedor == 0)
                {
                    MostrarMensaje("⚠️ Debe seleccionar un proveedor primero", "danger");
                    return;
                }

                // Validar producto seleccionado
                int idProducto = int.Parse(ddlProductos.SelectedValue);
                if (idProducto == 0)
                {
                    MostrarMensaje("⚠️ Debe seleccionar un producto", "danger");
                    return;
                }

                // Validar cantidad
                if (!int.TryParse(txtCantidad.Text, out int cantidad) || cantidad <= 0)
                {
                    MostrarMensaje("⚠️ Ingrese una cantidad válida mayor a 0", "danger");
                    return;
                }

                // Validar precio
                if (!decimal.TryParse(txtPrecioUnitario.Text, out decimal precioUnitario) || precioUnitario <= 0)
                {
                    MostrarMensaje("⚠️ Ingrese un precio válido mayor a 0", "danger");
                    return;
                }

                // Verificar si el producto ya está en la lista
                var productoExistente = DetallesCompra.FirstOrDefault(d => d.ProductoId == idProducto);

                if (productoExistente != null)
                {
                    // Actualizar cantidad y precio
                    productoExistente.Cantidad += cantidad;
                    productoExistente.PrecioUnitario = precioUnitario;
                    productoExistente.Subtotal = productoExistente.Cantidad * productoExistente.PrecioUnitario;
                }
                else
                {
                    // Obtener datos del producto
                    ProductosNegocio negocio = new ProductosNegocio();
                    List<Producto> productos = negocio.listar(idProducto);
                    Producto producto = productos[0];

                    // Crear nuevo detalle
                    DetalleCompraTemp detalle = new DetalleCompraTemp
                    {
                        ProductoId = idProducto,
                        NombreProducto = producto.Nombre,
                        Cantidad = cantidad,
                        PrecioUnitario = precioUnitario,
                        Subtotal = cantidad * precioUnitario
                    };

                    DetallesCompra.Add(detalle);
                }

                ActualizarGridProductos();
                LimpiarCamposProducto();
                MostrarMensaje("✅ Producto agregado correctamente", "success");
            }
            catch (Exception ex)
            {
                MostrarMensaje("❌ Error: " + ex.Message, "danger");
            }
        }

        protected void gvProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int idProducto = int.Parse(e.CommandArgument.ToString());
                DetallesCompra.RemoveAll(d => d.ProductoId == idProducto);
                ActualizarGridProductos();
                MostrarMensaje("🗑️ Producto eliminado del detalle", "warning");
            }
        }

        protected void btnRegistrarCompra_Click(object sender, EventArgs e)
        {
            try
            {
                int idProveedor = int.Parse(ddlProvedores.SelectedValue);

                if (idProveedor == 0)
                {
                    MostrarMensaje("⚠️ Debe seleccionar un proveedor", "danger");
                    return;
                }

                if (DetallesCompra.Count == 0)
                {
                    MostrarMensaje("⚠️ Debe agregar al menos un producto", "danger");
                    return;
                }

                // Crear objeto Compra
                Compra compra = new Compra();
                compra.Proveedor = new Proveedor { Id = idProveedor };

                foreach (var detalle in DetallesCompra)
                {
                    detalleCompra dc = new detalleCompra
                    {
                        Producto = new Producto { Id = detalle.ProductoId },
                        Cantidad = detalle.Cantidad,
                        PrecioUnitario = detalle.PrecioUnitario
                    };
                    compra.Detalles.Add(dc);
                }

                compra.CalcularTotal();

                // Registrar en la base de datos
                ComprasNegocio negocio = new ComprasNegocio();
                bool resultado = negocio.RegistrarCompraCompleta(compra);

                if (resultado)
                {
                    MostrarMensaje("✅ Compra registrada exitosamente - Total: " + compra.Total.ToString("C2"), "success");
                    LimpiarFormulario();
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("❌ Error al registrar: " + ex.Message, "danger");
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            MostrarMensaje("ℹ️ Operación cancelada", "warning");
        }

        private void MostrarMensaje(string mensaje, string tipo)
        {
            lblMensaje.Text = mensaje;

            // Limpiar clases anteriores
            lblMensaje.CssClass = "mensaje-box";

            // Agregar clase según el tipo
            switch (tipo.ToLower())
            {
                case "success":
                    lblMensaje.CssClass += " mensaje-success";
                    break;
                case "danger":
                    lblMensaje.CssClass += " mensaje-danger";
                    break;
                case "warning":
                    lblMensaje.CssClass += " mensaje-warning";
                    break;
            }

            lblMensaje.Visible = true;
        }

        private void OcultarMensaje()
        {
            lblMensaje.Visible = false;
            lblMensaje.Text = string.Empty;
        }

        private void LimpiarCamposProducto()
        {
            txtCantidad.Text = "";
            txtPrecioUnitario.Text = "";
            ddlProductos.SelectedIndex = 0;
        }

        private void LimpiarFormulario()
        {
            DetallesCompra.Clear();
            LimpiarCamposProducto();
            ddlProvedores.SelectedIndex = 0;
            ddlProductos.Items.Clear();
            ddlProductos.Items.Insert(0, new ListItem("Seleccione un Proveedor primero", "0"));

            if (filtroAvanzado)
            {
                ddlMarcas.SelectedIndex = 0;
                ddlCategorias.SelectedIndex = 0;
            }

            ActualizarGridProductos();
            OcultarMensaje();
        }

        [Serializable]
        public class DetalleCompraTemp
        {
            public int ProductoId { get; set; }
            public string NombreProducto { get; set; }
            public int Cantidad { get; set; }
            public decimal PrecioUnitario { get; set; }
            public decimal Subtotal { get; set; }
        }
    }
}
