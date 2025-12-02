using dominio;
using Equipo1b_TPC.Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.WebSockets;


namespace Equipo1b_TPC
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private List<detalleVenta> ldetalle
        {
            get
            {
                if (Session["ldetalle"] == null)
                {
                    Session["ldetalle"] = new List<detalleVenta>();
                }
                return (List<detalleVenta>)Session["ldetalle"];
            }
            set
            {
                Session["ldetalle"] = value;
            }
        }
       
        public bool filtroAvanzado { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDetalle();
                CargarClientes();
                CargarProductos();
            }

        }
        private void CargarProductos()
        {
            ProductosNegocio negocio = new ProductosNegocio();
            List<Producto> lproducto = new List<Producto>();
            lproducto = negocio.listar();
            ddlProductos.DataSource = lproducto;
            ddlProductos.DataTextField = "Nombre";
            ddlProductos.DataValueField = "Id";
            ddlProductos.DataBind();
            ddlProductos.Items.Insert(0, new ListItem("Seleccione un producto", "0"));
        }
        private void CargarDetalle()
        {
            gvProductos.DataSource = ldetalle;
            gvProductos.DataBind();

            decimal total = ldetalle.Sum(d => d.subtotal);
            var cultaAR = new CultureInfo("es-AR");
            if (ldetalle.Count > 0)
            {
                txtTotal.Text = total.ToString("C2", cultaAR);
            }
        }
        private void FiltrarProductos()
        {

            int idMarcas = int.Parse(ddlMarcas.SelectedValue);
            int idCategorias = int.Parse(ddlCategorias.SelectedValue);
            ProductosNegocio negocio = new ProductosNegocio();
            List<Producto> lprod = new List<Producto>();
            if (idMarcas != 0 && idCategorias != 0)
            {
                lprod = negocio.listar(0, idMarcas, idCategorias);
            }
            else if (idMarcas != 0)
            {
                lprod = negocio.listar(0, idMarcas, 0);
            }
            else if (idCategorias != 0)
            {
                lprod = negocio.listar(0, 0, idCategorias);
            }
            else
            {
                return;
            }
            ddlProductos.DataSource = lprod;
            ddlProductos.DataValueField = "Id";
            ddlProductos.DataTextField = "Nombre";
            if (int.Parse(ddlProductos.SelectedValue) != 0)
            {
                txtIdProducto.Text = ddlProductos.SelectedValue;
            }
            ddlProductos.DataBind();
            lblMensaje.Visible = false;
            if (lprod.Count == 0)
            {

                lblMensaje.Visible = true;
                lblMensaje.Text = "No se encontro ningun producto con los filtros selecionados";
                txtIdProducto.Text = "";
                return;
            }


        }
        private void CargarMarcas()
        {
            MarcaNegocio negocio = new MarcaNegocio();
            List<Marca> lmarca = new List<Marca>();
            lmarca = negocio.listar();
            ddlMarcas.DataSource = lmarca;
            ddlMarcas.DataTextField = "Nombre";
            ddlMarcas.DataValueField = "Id";
            ddlMarcas.DataBind();
            ddlMarcas.Items.Insert(0, new ListItem("Seleccione una marca", "0"));
        }
        private void CargarCategorias()
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            List<Categoria> lcat = new List<Categoria>();
            lcat = negocio.listar();
            ddlCategorias.DataSource = lcat;
            ddlCategorias.DataTextField = "Nombre";
            ddlCategorias.DataValueField = "Id";
            ddlCategorias.DataBind();
            ddlCategorias.Items.Insert(0, new ListItem("Seleccione un producto", "0"));
        }
        private void CargarClientes()
        {
            ClientesNegocio cliente = new ClientesNegocio();
            List<Cliente> lcliente = cliente.listar(false);
            ddlClientes.DataSource = lcliente;
            ddlClientes.DataTextField = "RazonSocial";
            ddlClientes.DataValueField = "Id";
            ddlClientes.DataBind();

            ddlClientes.Items.Insert(0, new ListItem("seleccione un cliente", "0"));
        }

        protected void txtFiltrarClientes_TextChanged(object sender, EventArgs e)
        {
            string filtro = txtFiltrarClientes.Text.Trim();
            //si el txt esta vacio volvemos a recargar los controles
            if (string.IsNullOrWhiteSpace(filtro))
            {
                lblModificarCliente.Visible = false;
                chkFiltrarCuit.Checked = false;
                CargarClientes();
                return;
            }
            //si no esta seleccionado el checkbox salimos
            if (!chkFiltrarCuit.Checked)
            {
                lblModificarCliente.Visible = true;
                lblModificarCliente.Text = "Debe marcar filtar por cuit";
                lblModificarCliente.CssClass = "text-danger fw-bold";
                return;
            }

            ClientesNegocio negocio = new ClientesNegocio();
            List<Cliente> lcliente = negocio.ListarPorCuit(filtro, false);
            //si no encontro nada
            if (lcliente == null || lcliente.Count == 0)
            {
                lblModificarCliente.Text = "no se encontro ningun cliente con el CUIT ingresado";
                lblModificarCliente.CssClass = "text-danger fw-bold";
                lblModificarCliente.Visible = true;
                ddlClientes.Items.Clear();
                CargarClientes();
                return;
            }
            ddlClientes.DataSource = lcliente;
            ddlClientes.DataTextField = "razonSocial";
            ddlClientes.DataValueField = "Id";
            ddlClientes.DataBind();
            lblModificarCliente.Visible = false;
        }
        protected void btnAgregarCliente_Click(object sender, EventArgs e)
        {
            Response.Redirect("agregarCliente.aspx");
        }

        protected void txtIdProducto_TextChanged(object sender, EventArgs e)
        {
            //si esta vacio 
            string id = txtIdProducto.Text;
            if (string.IsNullOrWhiteSpace(id))
            {
                lblMensaje.Visible = false;
                CargarProductos();
                return;
            }
            if (!int.TryParse(id, out int idProducto))
            {
                lblMensaje.Text = "El ID ingresado debe ser numerico";
                lblMensaje.Visible = true;
                CargarProductos();
                return;
            }
            ProductosNegocio negocio = new ProductosNegocio();
            List<Producto> lproductos = negocio.listar(int.Parse(id));
            //si no encontro nada
            if (lproductos == null || lproductos.Count == 0)
            {
                txtIdProducto.Text = "";
                lblMensaje.Text = "No se encontro ningun producto con el id ingresado";
                lblMensaje.Visible = true;
                ddlProductos.Items.Clear();
                CargarProductos();
                return;
            }
            //si encontro 
            ddlProductos.DataSource = lproductos;
            ddlProductos.DataTextField = "Nombre";
            ddlProductos.DataValueField = "Id";
            ddlProductos.DataBind();
            lblMensaje.Visible = false;

        }

        protected void chkFiltro_CheckedChanged(object sender, EventArgs e)
        {
            filtroAvanzado = chkFiltro.Checked;
            if (filtroAvanzado)
            {
                CargarCategorias();
                CargarMarcas();
            }
            else
            {
                lblMensaje.Visible = false;
                CargarProductos();
            }


        }

        protected void btnModificarCliente_Click(object sender, EventArgs e)
        {
            int idSeleccionado = int.Parse(ddlClientes.SelectedValue);
            if (idSeleccionado == 0)
            {
                lblModificarCliente.Visible = true;
                lblModificarCliente.Text = "Debe seleccionar un cliente para modificar";
                lblModificarCliente.CssClass = "text-danger fw-bold";
                return;

            }
            Response.Redirect("AgregarCliente.aspx?id=" + idSeleccionado);
        }

        protected void chkFiltrarCuit_CheckedChanged(object sender, EventArgs e)
        {

        }




        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            if (chkFiltro.Checked)
            {
                FiltrarProductos();
            }

        }

        protected void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            if (int.Parse(ddlClientes.SelectedValue) == 0)
            {
                lblMensaje.Visible = true;
                lblMensaje.Text = "Debe seleccionar al menos un cliente para realizar una venta";
                return;

            }
            if (int.Parse(ddlProductos.SelectedValue) == 0)
            {
                lblMensaje.Visible = true;
                lblMensaje.Text = "Debe seleccionar al menos un producto para agregarlo a ala venta";
                return;

            }
            if (string.IsNullOrEmpty(txtCantidad.Text))
            {
                lblMensaje.Visible = true;
                lblMensaje.Text = "Debe ingresar la cantidad para agregar el producto";
                return;
            }

            int cantidad = int.Parse(txtCantidad.Text);
            int id = int.Parse(ddlProductos.SelectedValue);

            ProductosNegocio negocio = new ProductosNegocio();
            List<Producto> lprod = negocio.listar(id);

            Producto producto = lprod[0];

            //cantidad del producto actual
            int cantidadActual = ldetalle.Where(d => d.producto.Id == id).Sum(d => d.cantidad);
            int cantidadTotal = cantidad + cantidadActual;
            if (cantidadTotal > producto.StockActual)
            {
                lblMensaje.Visible = true;
                lblMensaje.Text = "No hay stock suficiente, stock disponible :" + producto.StockActual;
                return;
            }

            detalleVenta detalle = new detalleVenta();
            detalle.producto = producto;
            detalle.cantidad = cantidad;
            detalle.PrecioUnitario = producto.PrecioVenta;
            detalle.CalcularSubtotal();
            ldetalle.Add(detalle);
            CargarDetalle();
            lblMensaje.Visible = false;






        }

        protected void ddlProductos_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (int.Parse(ddlProductos.SelectedValue) == 0)

            {
                txtIdProducto.Text = "";
                return;
            }
            txtIdProducto.Text = ddlProductos.SelectedValue;
        }

        protected void txtCantidad_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnFinalizar_Click(object sender, EventArgs e)
        {
            //validaciones iniciales
            if (ldetalle == null || ldetalle.Count == 0)
            {
                lblMensaje.Visible = true;
                lblMensaje.Text = "No puede finalizar una venta sin ningun articulo cargado";
                return;
            }
            if (ddlMedioPago.SelectedValue == "0")
            {
                lblMensaje.Visible = true;
                lblMensaje.Text = "Debe seleccionar un medio de pago";
                return;
            }
            
            VentaNegocio ventaNegocio = new VentaNegocio();
            DetalleVentasNegocio detalleNegocio = new DetalleVentasNegocio();
            ResumenVentaNegocio resumenVentaNegocio = new ResumenVentaNegocio();
            ProductosNegocio prodNegocio = new ProductosNegocio();
            //obtenemos o creamos nuevo resumen
            ResumenVenta resumen = resumenVentaNegocio.ObtenerResumenDelDia();
            
            //asignacion de venta
            venta venta = new venta();
            venta.tipoFactura = ddlFactura.SelectedValue;
            venta.MedioPago = ddlMedioPago.SelectedValue;
            venta.detalleV = ldetalle;
            ClientesNegocio negocio = new ClientesNegocio();
            List<Cliente> lclientes = negocio.listar(false, int.Parse(ddlClientes.SelectedValue));
            venta.cliente = lclientes[0];
            venta.nroCierreCaja = resumen.NroDeCierre;
            venta.calcularTotal();

            //intentamos actualizar resumen de venta con los datos
            //sumamos la venta al resumen
            bool actualizo = resumenVentaNegocio.actualizarResumenDeldia(venta);
            //si no actualizo mostramos mensaje
            if (!actualizo)
            {
                lblMensaje.Text = "No se puede agregar la venta al resumen debido a que , la venta del dia " + DateTime.Today.ToString("dd/MM/yyyy") + " esta cerrada";
                lblMensaje.Visible = true;
                return;
            }
            //si el resumen se actualizo
            //obtenemos el id de venta agregado
            int numeroFactura = ventaNegocio.Agregar(venta);
           
            //guardamos los detalles
            foreach (var det in ldetalle)
            {
                det.NumeroFactura = numeroFactura;
                detalleNegocio.AgregarDetalle(det);
                prodNegocio.descontarStock(det.producto.Id,det.cantidad);

            }
           //si la venta se grabo correctamente
            lblMensaje.Visible = true;
            lblMensaje.CssClass = "text-success fw-bold";
            lblMensaje.Text = "La venta N° " + numeroFactura + " se registro correctamente";

            //reiniciamos la lista y controles
            ldetalle = new List<detalleVenta>();
            CargarDetalle();
            txtCantidad.Text = "";
            txtIdProducto.Text = "";
            txtTotal.Text = "";
            ddlProductos.Items.Clear();
            CargarProductos();
            ddlClientes.SelectedIndex = 0;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            ldetalle = new List<detalleVenta>();
            CargarDetalle();
            txtCantidad.Text = "";
            txtIdProducto.Text = "";
            txtTotal.Text = "";
            ddlProductos.Items.Clear();
            CargarProductos();
            ddlClientes.SelectedIndex = 0;



        }

        protected void ddlClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlClientes.SelectedIndex != 0)
            {
                int id = int.Parse(ddlClientes.SelectedValue);
                ClientesNegocio negocio = new ClientesNegocio();
                List<Cliente> lcliente = negocio.listar(false, id);
                string TipoFactura = lcliente[0].TipoFactura;
                switch (TipoFactura)
                {
                    case "A":
                        ddlFactura.SelectedValue = "A";
                        break;
                    case "B":
                        ddlFactura.SelectedValue = "B";
                        break;
                    case "C":
                        ddlFactura.SelectedValue = "C";
                        break;
                }
            }
        }
    }
}