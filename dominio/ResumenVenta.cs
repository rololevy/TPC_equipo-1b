using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class ResumenVenta
    {
        public decimal totalGeneral { get; set; }
        //totales por medio de pago
        public decimal totalEfectivo { get; set; }
        public decimal totalTarjeta { get; set; }
        public decimal totalQr { get; set; }
        //totales por tipo de factura
        public decimal totalFa { get; set; }
        public decimal totalFc { get; set; }
        public decimal totalFb { get; set; }
        public int totalOperaciones { get; set; }
        public DateTime fechaResumenVenta { get; set; }

        public void sumarVenta(venta ventas)
        {
            if (ventas == null)
                return;

            totalGeneral += ventas.totalVenta;
            //actualizamos el contador de operaciones
            totalOperaciones++;

            switch (ventas.MedioPago)
            {
                case 'E':
                    totalEfectivo += ventas.totalVenta;
                    break;
                case 'T':
                    totalTarjeta += ventas.totalVenta;
                    break;
                case 'Q':
                    totalQr += ventas.totalVenta;
                    break;

            }
            switch (ventas.tipoFactura)
            {
                case 'A':
                    totalFa += ventas.totalVenta;
                    break;
                case 'B':
                    totalFb += ventas.totalVenta;
                    break;
                case 'C':
                    totalFc += ventas.totalVenta;
                    break;

            }
        }
        public ResumenVenta()
        {
            totalGeneral = 0;
            totalEfectivo = 0;
            totalTarjeta = 0;
            totalQr = 0;
            totalFa = 0;
            totalFc = 0;
            totalFb = 0;
            totalOperaciones = 0;
            fechaResumenVenta = DateTime.Now;
            

        }



    }
}
