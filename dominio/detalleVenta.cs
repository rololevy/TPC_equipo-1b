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
       public Producto producto { get; set; }
        public int cantidad { get; set; }
        //calcula el subtotal segun el producto
        public decimal CalcularSubtotal()
        {
            return producto.CalcularprecioVenta() * cantidad;
        }
        
    }
}
