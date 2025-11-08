using datos;
using Equipo1b_TPC.Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Equipo1b_TPC
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //configuracion si estamos modificando por razon social
                string razonSocial = Server.UrlDecode(Request.QueryString["razonSocial"]);
                //si modificamos por cuit
                String cuit = Request.QueryString["cuit"];
                
                if (!string.IsNullOrEmpty(razonSocial))
                {
                    ViewState["modo"] = "Modificar";
                    ClientesNegocio negocio = new ClientesNegocio();
                    List<Cliente> lcliente = negocio.listar(true,razonSocial);
                    Cliente seleccionado = lcliente[0];
                    ViewState["cuitOriginal"] =seleccionado.Cuit;//guardamos el cuit original
                    //modificamos nombre de los controles
                    btnAgregar.Text = "Modificar Cliente";
                    btnCancelar.Text = "Cancelar modificacion de cliente";
                    lblTitulo.Text = "Modificacion de clientes";

                    if (seleccionado != null)
                    {
                        txtRazonSocial.Text = seleccionado.RazonSocial;
                        txtCuit.Text = seleccionado.Cuit;
                        txtEmail.Text = seleccionado.Email;
                        txtTelefono.Text = seleccionado.Telefono;
                        txtDireccion.Text = seleccionado.Direccion;
                    }
                }
                else
                {
                    ViewState["modo"] = "Modificar";
                    //modificamos nombre de los controles
                    lblTitulo.Text = "Modificacion de clientes";
                    btnAgregar.Text = "Modificar Cliente";
                    btnCancelar.Text = "Cancelar modificacion de cliente";
                    if (!string.IsNullOrEmpty(cuit))
                    {
                        
                        ClientesNegocio negocio = new ClientesNegocio();
                        List<Cliente> lcliente = negocio.ListarPorCuit(cuit,true);
                        Cliente seleccionado = lcliente[0];
                        ViewState["cuitOriginal"] = seleccionado.Cuit;
                        if (seleccionado != null)
                        {
                            txtRazonSocial.Text = seleccionado.RazonSocial;
                            txtCuit.Text = seleccionado.Cuit;
                            txtEmail.Text = seleccionado.Email;
                            txtTelefono.Text = seleccionado.Telefono;
                            txtDireccion.Text = seleccionado.Direccion;

                            
                        }

                    }
                    else
                    {
                        ViewState["modo"] = "Agregar";
                        lblTitulo.Text = "Agregar Cliente";
                        btnAgregar.Text = "Agregar nuevo cliente";
                        btnCancelar.Text = "Cancelar alta de cliente";
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
            string modo = ViewState["modo"]?.ToString();
            ClientesNegocio clienteNegocio = new ClientesNegocio();
            Cliente cl = new Cliente();
            try
            {
                if (string.IsNullOrEmpty(txtCuit.Text) || string.IsNullOrEmpty(txtRazonSocial.Text))
                {
                    lblConfirmacion.Visible = true;
                    lblConfirmacion.CssClass = "text-danger fw-bold";
                    lblConfirmacion.Text = "debe ingregar al menos una razon social y CUIT";
                    return;
                }
               
               
                
                cl.RazonSocial = txtRazonSocial.Text.Trim();
                cl.Cuit = txtCuit.Text.Trim();
                cl.Email = txtEmail.Text.Trim();
                cl.Telefono = txtTelefono.Text.Trim();
                cl.Direccion = txtDireccion.Text.Trim();
            }
            catch(Exception ex)
            {

                throw ex;
            }
            try
            {
                if (modo == "Modificar")
                {
                    //almacenamos el cuit del cliente para filtrar en caso de que deseemos cambiar el cuit
                    string cuitModificar = ViewState["cuitOriginal"].ToString();

                    clienteNegocio.modificar(cl,cuitModificar);
                    lblConfirmacion.Visible = true;
                    lblConfirmacion.Text = "cliente modificado correctamente";
                    lblConfirmacion.CssClass = "text-success fw-bold";

                }
                else
                {
                    clienteNegocio.agregar(cl);
                    lblConfirmacion.Visible = true;
                    lblConfirmacion.Text = "Cliente agregado correctamente.";
                    lblConfirmacion.CssClass = "text-success fw-bold";
                }

            }
            catch (Exception)
            {
                lblConfirmacion.Visible = true;
                lblConfirmacion.Text = "Error al agregar cliente, intente nuevamente mas tarde";
                lblConfirmacion.CssClass = "text-danger fw-bold";

            }


        }
    }
}