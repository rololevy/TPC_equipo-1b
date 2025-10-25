using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Equipo1b_TPC.Dominio;
using Negocio;

namespace Equipo1b_TPC
{
    public partial class Marcas : System.Web.UI.Page
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
            MarcaNegocio negocio = new MarcaNegocio();
            try
            {
                List<Marca> lista = negocio.listar();
                dgvMarcas.DataSource = lista;
                dgvMarcas.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar: " + ex.Message);
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            MarcaNegocio negocio = new MarcaNegocio();
            Marca marca = new Marca();

            try
            {
                marca.Nombre = txtNombre.Text.Trim();
                marca.Activo = chkActivo.Checked;

                if (string.IsNullOrEmpty(marca.Nombre))
                {
                    MostrarMensaje("Nombre obligatorio");
                    return;
                }

                if (hfIdMarca.Value == "0")
                {
                    negocio.agregar(marca);
                    MostrarMensaje("Marca agregada");
                }
                else
                {
                    marca.Id = int.Parse(hfIdMarca.Value);
                    negocio.modificar(marca);
                    MostrarMensaje("Marca modificada");
                }

                LimpiarFormulario();
                CargarGrilla();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message);
            }
        }

        protected void dgvMarcas_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = (int)dgvMarcas.SelectedDataKey.Value;
            hfIdMarca.Value = id.ToString();
            txtNombre.Text = dgvMarcas.SelectedRow.Cells[1].Text;
            chkActivo.Checked = ((CheckBox)dgvMarcas.SelectedRow.Cells[2].Controls[0]).Checked;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            hfIdMarca.Value = "0";
            txtNombre.Text = string.Empty;
            chkActivo.Checked = true;
        }

        private void MostrarMensaje(string mensaje)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.Visible = true;
        }
    }
}
