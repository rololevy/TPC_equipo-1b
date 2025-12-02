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
         public decimal PrecioUnitario { get; set;}
        public string MedioPago { get; set; }
        public string TipoFactura { get; set; }
        //calcula el subtotal
        public void CalcularSubtotal()
        {
                subtotal=PrecioUnitario * cantidad; 
        }
        public detalleVenta()
        {
            producto = new Producto();
        }

    }
}
