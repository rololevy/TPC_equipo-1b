using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Equipo1b_TPC.Dominio;
using Negocio;

namespace Equipo1b_TPC
{
    public partial class Categorias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGrilla();
            }
        }

        private void CargarGrilla()
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            try
            {
                List<Categoria> lista = negocio.listar();
                dgvCategorias.DataSource = lista;
                dgvCategorias.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar: " + ex.Message);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            Categoria categoria = new Categoria();

            try
            {
                categoria.Nombre = txtNombre.Text.Trim();
                categoria.Descripcion = txtDescripcion.Text.Trim();
                categoria.Activo = chkActivo.Checked;

                if (string.IsNullOrEmpty(categoria.Nombre))
                {
                    MostrarMensaje("Nombre obligatorio");
                    return;
                }

                if (hfIdCategoria.Value == "0")
                {
                    negocio.agregar(categoria);
                    MostrarMensaje("Categoría agregada");
                }
                else
                {
                    categoria.Id = int.Parse(hfIdCategoria.Value);
                    negocio.modificar(categoria);
                    MostrarMensaje("Categoría modificada");
                }

                Limpiar();
                CargarGrilla();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message);
            }
        }

        protected void dgvCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = (int)dgvCategorias.SelectedDataKey.Value;
            CategoriaNegocio negocio = new CategoriaNegocio();
            
            try
            {
                List<Categoria> lista = negocio.listar();
                Categoria categoria = lista.Find(c => c.Id == id);

                if (categoria != null)
                {
                    hfIdCategoria.Value = categoria.Id.ToString();
                    txtNombre.Text = categoria.Nombre;
                    txtDescripcion.Text = categoria.Descripcion;
                    chkActivo.Checked = categoria.Activo;
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Limpiar()
        {
            hfIdCategoria.Value = "0";
            txtNombre.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            chkActivo.Checked = true;
        }

        private void MostrarMensaje(string mensaje)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.Visible = true;
        }
    }
}
