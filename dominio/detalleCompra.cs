using Equipo1b_TPC.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class detalleCompra
    {
        public Producto producto { get; set; }
        public int cantidad { get; set; }
        

        public decimal CalcularTotal()
        {
            return cantidad * producto.PrecioCompra;
        }
    }
}