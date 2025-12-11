using datos;
using Equipo1b_TPC.Dominio;
using Equipo1b_TPC.Helpers;
using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Validaciones;

namespace Equipo1b_TPC
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // ADMIN
            SeguridadHelper.ValidarAcceso(TipoUsuario.Administrador);

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
                    btnAgregar.Text = "Modificar Proveedor existente";
                    btnCancelar.Text = "Cancelar Modificacion de proveedor";
                    lblTitulo.Text = "Modificar Proveedores";
                }
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {

            try
            {
                validacion validador = new validacion();
                Proveedor prov = new Proveedor();
                ProvedoresNegocio negocio = new ProvedoresNegocio();
                if (string.IsNullOrEmpty(txtRazonSocial.Text))
                {
                    lblConfirmacion.Visible = true;
                    lblConfirmacion.CssClass = "text-danger fw-bold";
                    lblConfirmacion.Text = "La razon social es obligatoria";
                    return;
                }
                if (!validador.validarTxtCuit(txtCuit.Text))
                {
                    lblConfirmacion.Visible = true;
                    lblConfirmacion.CssClass = "text-danger fw-bold";
                    lblConfirmacion.Text = "El cuit ingresado es invalido";
                    return;
                }
                if (!validador.validarEmail(txtEmail.Text))
                {
                    lblConfirmacion.Visible = true;
                    lblConfirmacion.CssClass = "text-danger fw-bold";
                    lblConfirmacion.Text = "El email ingresado no tiene un formato valido";
                    return;
                }
                if (!string.IsNullOrEmpty(txtTelefono.Text))
                {
                    //si el usuario ingresa un telefono validamos que sean solo numeros
                    if (!txtTelefono.Text.All(char.IsDigit))
                    {
                        lblConfirmacion.Visible = true;
                        lblConfirmacion.CssClass = "text-danger fw-bold";
                        lblConfirmacion.Text = "El telefono debe contener solo numeros";
                        return;
                    }
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