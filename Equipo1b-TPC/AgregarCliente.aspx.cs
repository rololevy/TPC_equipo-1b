using datos;
using Equipo1b_TPC.Dominio;
using Equipo1b_TPC.Helpers;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using Validaciones;

namespace Equipo1b_TPC
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // ADMIN
            SeguridadHelper.ValidarAcceso(TipoUsuario.Administrador);

            if (!IsPostBack)
            {
                //configuracion si estamos modificando 
                if (Request.QueryString["Id"] != null)
                {
                    //si recibimos una id precargamos los txt y modificamos los controles
                    int id = int.Parse(Request.QueryString["Id"]);
                    ClientesNegocio negocio = new ClientesNegocio();
                    List<Cliente> lcliente = negocio.listar(true, id);
                    if (lcliente != null && lcliente.Count > 0)
                    {
                        Cliente seleccionado = lcliente[0];
                        //modificamos nombre de los controles
                        btnAgregar.Text = "Modificar Cliente existente";
                        btnCancelar.Text = "Cancelar modificacion de cliente";
                        lblTitulo.Text = "Modificacion de clientes";
                        txtRazonSocial.Text = seleccionado.RazonSocial;
                        txtCuit.Text = seleccionado.Cuit;
                        txtEmail.Text = seleccionado.Email;
                        txtTelefono.Text = seleccionado.Telefono;
                        txtDireccion.Text = seleccionado.Direccion;
                        ddlTipoFactura.SelectedValue = seleccionado.TipoFactura;
                    }
                }
            }

        }



        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ventas.aspx");

        }

        protected void btnAgregar_Click(object sender, EventArgs e)

        {

            try
            {
                validacion validador = new validacion();
                Cliente cl = new Cliente();
                ClientesNegocio negocio = new ClientesNegocio();
                if ( string.IsNullOrEmpty(txtRazonSocial.Text))
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
                cl.RazonSocial = txtRazonSocial.Text.Trim();
                cl.Cuit = txtCuit.Text.Trim();
                cl.Email = txtEmail.Text.Trim();
                cl.Telefono = txtTelefono.Text.Trim();
                cl.Direccion = txtDireccion.Text.Trim();
                cl.TipoFactura = ddlTipoFactura.SelectedValue;
                //si vamos a modificar
                if (Request.QueryString["Id"] != null)
                {
                    cl.Id = int.Parse(Request.QueryString["Id"]);
                    negocio.modificar(cl);
                    lblConfirmacion.Visible = true;
                    lblConfirmacion.Text = "cliente modificado correctamente";
                    lblConfirmacion.CssClass = "text-success fw-bold";
                }
                //si vamos a agregar
                else
                {
                    negocio.agregar(cl);
                    lblConfirmacion.Visible = true;
                    lblConfirmacion.Text = "Cliente agregado correctamente.";
                    lblConfirmacion.CssClass = "text-success fw-bold";
                }
            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}