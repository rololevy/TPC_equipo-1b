using Equipo1b_TPC.Dominio;
using System; 
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class venta
    {
        public int numeroFactura { get; set; }
        public string tipoFactura { get; set; }//'a' ,'b' o 'c'
        public string MedioPago { get; set; }//'t' tarjeta , 'q' qr(modo,mercadopago,etc),'e' Efectivo
        public List<detalleVenta> detalleV { get; set; }
        public Cliente cliente { get; set; }
        public decimal totalVenta { get; set; }

        public DateTime FechaVenta { get; set; }
        public venta()
        {
            detalleV = new List<detalleVenta>();
            totalVenta = 0;
            FechaVenta = DateTime.Today;//asignamos la fecha actual
            cliente = new Cliente();
            
        }
        //meotdo que calcula el total de la venta 
        public void calcularTotal()
        {
            totalVenta = detalleV.Sum(d => d.total);
        }
        //metodo para agregar productos y recalcular el total 
        public void agregarDetalle(detalleVenta detalle)
        {
            detalleV.Add(detalle);
            calcularTotal();
        }
        //public void SetMedioPago(string mPago)
        //{
            
        //    switch (mPago)
        //    {
        //        case "T":
        //            MedioPago = "Tarjeta";
        //            break;
        //        case "Q":
        //            MedioPago = "Codigo Qr";
        //            break;
        //        case "E":
        //            MedioPago = "Efectivo";
        //            break;

        //    }
        //}
        
    }
}
