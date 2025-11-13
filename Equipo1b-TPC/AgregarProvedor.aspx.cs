using datos;
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
    public partial class WebForm4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    //si recibimos una id por url precargamos los txt y modificamos texto de los controles
                    int ID = int.Parse(Request.QueryString["id"]);
                    List<Proveedor> lprov = new List<Proveedor>();
                    ProvedoresNegocio negocio = new ProvedoresNegocio();
                    lprov = negocio.listar(true, ID);
                    txtCuit.Text = lprov[0].CUIT;
                    txtDireccion.Text = lprov[0].Direccion;
                    txtEmail.Text = lprov[0].Email;
                    txtRazonSocial.Text = lprov[0].RazonSocial;
                    txtTelefono.Text = lprov[0].Telefono;
                    btnAgregar.Text = "Modificar Provedor";
                    btnCancelar.Text = "Cancelar Modificacion";
                    lblTitulo.Text = "Modificar Proveedores";
                }
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {

            try
            {
                Proveedor prov = new Proveedor();
                ProvedoresNegocio negocio = new ProvedoresNegocio();
                if (string.IsNullOrEmpty(txtCuit.Text) || string.IsNullOrEmpty(txtRazonSocial.Text))
                {
                    lblConfirmacion.Visible = true;
                    lblConfirmacion.Text = "debe ingregar al menos una razon social y CUIT";
                    lblConfirmacion.CssClass = "text-danger fw-bold";
                    return;
                }
                prov.RazonSocial = txtRazonSocial.Text;
                prov.Email = txtEmail.Text;
                prov.Direccion = txtDireccion.Text;
                prov.Telefono = txtTelefono.Text;
                prov.CUIT = txtCuit.Text;
                //si vamos a modificar un provedor existente
                if (Request.QueryString["id"] != null)
                {
                    prov.Id = int.Parse(Request.QueryString["id"]);
                    negocio.modificar(prov);
                    lblConfirmacion.Visible = true;
                    lblConfirmacion.Text = "Provedor modificado correctamente";
                    lblConfirmacion.CssClass = "text-success fw-bold";
                }
                //si vamos agregar un nuevo provedor
                else
                {
                    negocio.agregar(prov);
                    lblConfirmacion.Visible = true;
                    lblConfirmacion.Text = "Provedor agregado correctamente";
                    lblConfirmacion.CssClass = "text-success fw-bold";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Provedores.aspx");
        }
    }
}