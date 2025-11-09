using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Equipo1b_TPC.Dominio
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Marca Marca { get; set; }
        public Categoria Categoria { get; set; }
        public Proveedor Provedor { get; set; }
        public decimal PrecioCompra { get; set; }
        public int PorcentajeGanancia { get; set; }  // Cambié de decimal a int
        public int StockActual { get; set; }
        public int StockMinimo { get; set; }
        public bool Activo { get; set; }

        public Producto()
        {
            Marca = new Marca();
            Categoria = new Categoria();
            Provedor = new Proveedor();  // Agregué inicialización
            Activo = true;
        }

        // Calcula el precio de venta aplicando el porcentaje de ganancia
        public decimal CalcularPrecioVenta()
        {
            return Math.Round(PrecioCompra * (1 + PorcentajeGanancia / 100.0m), 2);
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}