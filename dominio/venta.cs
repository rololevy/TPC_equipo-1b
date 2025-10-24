using Equipo1b_TPC.Dominio;
using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class venta
    {
        public int numeroFactura { get; set; }
        public char tipoFactura { get; set; }//'a' ,'b' o 'c'
        public List<detalleVenta> detalle { get; set; }
        public Cliente cliente { get; set; }
    }
}
