using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Equipo1b_TPC
{
    public partial class productos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Pageload
            if (!IsPostBack)
            {
                CargarProductosDemo();
            }
        }

        // DemoData
        private void CargarProductosDemo()
        {
            var productosDemo = new[]
            {
                new {
                    Id = 1,
                    Nombre = "Samsung Galaxy S21",
                    Marca = "Samsung",
                    Categoria = "Electrónica",
                    PrecioCompra = 350000m,
                    PorcentajeGanancia = 30,
                    StockActual = 15,
                    StockMinimo = 10,
                    Activo = true
                },
                new {
                    Id = 2,
                    Nombre = "Nike Air Max",
                    Marca = "Nike",
                    Categoria = "Indumentaria",
                    PrecioCompra = 45000m,
                    PorcentajeGanancia = 40,
                    StockActual = 5,
                    StockMinimo = 8,
                    Activo = true
                },
                new {
                    Id = 3,
                    Nombre = "Sony PS5",
                    Marca = "Sony",
                    Categoria = "Electrónica",
                    PrecioCompra = 550000m,
                    PorcentajeGanancia = 25,
                    StockActual = 20,
                    StockMinimo = 15,
                    Activo = false
                }
            };

            // BindGrid
            gvProductosPage.DataSource = productosDemo;
            gvProductosPage.DataBind();
        }
    }
}
