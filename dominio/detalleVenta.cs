using Equipo1b_TPC.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class detalleVenta
    {
        public int id { get; set; }
        public int NumeroFactura { get; set; }

        public Producto producto { get; set; }
        public int cantidad { get; set; }
        public decimal subtotal { get; set; }
         public decimal PrecioUnitario { get; set;
        }
        public decimal total
        {
            get
            {
                return CalcularSubtotal();
            }
        }
        //calcula el subtotal segun el producto

        public decimal CalcularSubtotal()
        {
            return producto.CalcularPrecioVenta() * cantidad;

        }
        public detalleVenta()
        {
            producto = new Producto();
        }

    }
}
