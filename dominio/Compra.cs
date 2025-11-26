using Equipo1b_TPC.Dominio; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Compra
    {
        public int Id { get; set; }
        public Proveedor Proveedor { get; set; }
        public DateTime FechaCompra { get; set; }
        public List<detalleCompra> Detalles { get; set; }
        public bool Recibida { get; set; }
        public decimal Total { get; set; }

        public Compra()
        {
            Detalles = new List<detalleCompra>();
            Recibida = true;
            FechaCompra = DateTime.Now;
        }

        public decimal CalcularTotal()
        {
            decimal total = 0;
            foreach (var detalle in Detalles)
            {
                total += detalle.CalcularTotal();
            }
            Total = total;
            return total;
        }
    }
}
