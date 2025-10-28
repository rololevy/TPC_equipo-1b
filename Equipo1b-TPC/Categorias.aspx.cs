using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Equipo1b_TPC.Dominio;

namespace Equipo1b_TPC
{
    public partial class Categorias : System.Web.UI.Page
    {
        //hardcodeo temporal
        private static List<Categoria> listaCategorias = new List<Categoria>
        {
            new Categoria { Id = 1, Nombre = "Electrónica", Descripcion = "Dispositivos y accesorios", Activo = true },
            new Categoria { Id = 2, Nombre = "Indumentaria", Descripcion = "Ropa y calzado", Activo = true },
            new Categoria { Id = 3, Nombre = "Alimentos", Descripcion = "Productos comestibles", Activo = true }
        };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGrilla();
            }
        }

        private void CargarGrilla()
        {
            dgvCategorias.DataSource = listaCategorias;
            dgvCategorias.DataBind();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtNombre.Text.Trim()))
                {
                    MostrarMensaje("Completa el nombre");
                    return;
                }

                if (hfIdCategoria.Value == "0")
                {
                    //nueva
                    Categoria nueva = new Categoria
                    {
                        Id = listaCategorias.Count + 1,
                        Nombre = txtNombre.Text.Trim(),
                        Descripcion = txtDescripcion.Text.Trim(),
                        Activo = chkActivo.Checked
                    };
                    listaCategorias.Add(nueva);
                    MostrarMensaje("Categoría agregada");
                }
                else
                {
                    //modifico
                    int id = int.Parse(hfIdCategoria.Value);
                    Categoria existente = listaCategorias.Find(c => c.Id == id);
                    if (existente != null)
                    {
                        existente.Nombre = txtNombre.Text.Trim();
                        existente.Descripcion = txtDescripcion.Text.Trim();
                        existente.Activo = chkActivo.Checked;
                        MostrarMensaje("Categoría modificada");
                    }
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
            Categoria categoria = listaCategorias.Find(c => c.Id == id);

            if (categoria != null)
            {
                hfIdCategoria.Value = categoria.Id.ToString();
                txtNombre.Text = categoria.Nombre;
                txtDescripcion.Text = categoria.Descripcion;
                chkActivo.Checked = categoria.Activo;
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
