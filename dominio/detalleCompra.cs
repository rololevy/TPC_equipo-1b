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
        public int Id { get; set; }
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal subtotal { get; set; }
        public decimal CalcularTotal()
        {
            return Cantidad * PrecioUnitario;
        }
        public detalleCompra()
        {
            Producto = new Producto();
        }
    }
}