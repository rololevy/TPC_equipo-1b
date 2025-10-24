using Equipo1b_TPC.Dominio; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    internal class Compra
    {
        public int idCompra { get; set; }
        public Proveedor proveedor { get; set; }
        public DateTime fechaCompra { get; set; }
        public List<detalleCompra> detalles {get;set;}
        public bool recibida { get; set; }//true si ya se recibio la compra
         public Compra()
        {
            detalles = new List<detalleCompra>();
            recibida = false;
        }
        public decimal calcularTotal()
        {
            decimal total = 0;
            foreach(var detalle in detalles)
            {
                total += detalle.CalcularTotal();
            }
            return total;
        }

    }
}
