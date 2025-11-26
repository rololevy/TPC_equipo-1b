using datos;
using dominio;
using Equipo1b_TPC.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class VentaNegocio
    {
        public List<venta> listar(int numeroFactura = 0)
        {
            AccesoDatos datos = new AccesoDatos();
            List<venta> lventas = new List<venta>();
            try
            {
                string consulta = "select NumeroFactura, TipoFactura, Fecha, ClienteId, Total,MedioPago from Ventas";
                if (numeroFactura != 0)
                {
                    consulta += " WHERE NumeroFactura=" + numeroFactura;
                }
                datos.setearConsulta(consulta);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    venta aux = new venta();
                    aux.numeroFactura = (int)datos.Lector["NumeroFactura"];
                    aux.tipoFactura = (string)datos.Lector["TipoFactura"];
                    aux.FechaVenta = (DateTime)datos.Lector["Fecha"];
                    aux.cliente.Id = (int)datos.Lector["ClienteId"];
                    aux.totalVenta = (decimal)datos.Lector["Total"];
                    aux.MedioPago = (string)datos.Lector["MedioPago"];

                    lventas.Add(aux);

                }
                return lventas;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

        }
        public int Agregar(venta venta)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "insert into Ventas(TipoFactura,Fecha,ClienteId,Total,MedioPago) VALUES (@TipoFactura,@Fecha,@ClienteId,@Total,@MedioPago) SELECT SCOPE_IDENTITY()";
                datos.setearConsulta(consulta);
                datos.setearParametro("@TipoFactura", venta.tipoFactura);
                datos.setearParametro("@Fecha", venta.FechaVenta);
                datos.setearParametro("@ClienteId", venta.cliente.Id);
                datos.setearParametro("@Total", venta.totalVenta);
                datos.setearParametro("@MedioPago", venta.MedioPago);
                return datos.ejecutarScalar();
            }
            catch(Exception ex)
            {
                throw ex;

            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public void Modificar(venta venta)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = ("Update Ventas SET  TipoFactura=@TipoFactura,Fecha=@Fecha,ClienteId=@ClienteId,Total=@Total,MedioPago=@MedioPago WHERE NumeroFactura=@NumeroFactura");
                datos.setearConsulta(consulta);
                datos.setearParametro("@NumeroFactura", venta.numeroFactura);
                datos.setearParametro("@TipoFactura", venta.tipoFactura);
                datos.setearParametro("@Fecha", venta.FechaVenta);
                datos.setearParametro("@ClienteId", venta.cliente.Id);
                datos.setearParametro("@Total", venta.totalVenta);
                datos.setearParametro("@MedioPago", venta.MedioPago);
                datos.ejecutarAccion();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        
      
    }
}
