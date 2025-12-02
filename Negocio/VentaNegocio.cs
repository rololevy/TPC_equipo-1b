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
        public List<venta> listar(int nroCierre=0,int numeroFactura = 0)
        {
            AccesoDatos datos = new AccesoDatos();
            List<venta> lventas = new List<venta>();
            try
            {
                string consulta = "select NumeroFactura, TipoFactura, Fecha, ClienteId, Total,MedioPago,NroCierreCaja from Ventas";
                if (nroCierre != 0)
                {
                    consulta += " WHERE NroCierreCaja=" + nroCierre;
                }
                else
                {
                    consulta += " WHERE 1=1"; 
                }
                if (numeroFactura != 0)
                {
                    consulta += " AND NumeroFactura=" + numeroFactura;
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
                    aux.nroCierreCaja = (int)datos.Lector["NroCierreCaja"];
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
                string consulta = "insert into Ventas(TipoFactura,Fecha,ClienteId,Total,MedioPago,NroCierreCaja) VALUES (@TipoFactura,@Fecha,@ClienteId,@Total,@MedioPago,@NroCierreCaja) SELECT SCOPE_IDENTITY()";
                datos.setearConsulta(consulta);
                datos.setearParametro("@TipoFactura", venta.tipoFactura);
                datos.setearParametro("@Fecha", venta.FechaVenta);
                datos.setearParametro("@ClienteId", venta.cliente.Id);
                datos.setearParametro("@Total", venta.totalVenta);
                datos.setearParametro("@MedioPago", venta.MedioPago);
                datos.setearParametro("@NroCierreCaja", venta.nroCierreCaja);
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
                string consulta = ("Update Ventas SET  TipoFactura=@TipoFactura,Fecha=@Fecha,ClienteId=@ClienteId,Total=@Total,MedioPago=@MedioPago,NroCierreCaja=@NroCierreCaja WHERE NumeroFactura=@NumeroFactura");
                datos.setearConsulta(consulta);
                datos.setearParametro("@NumeroFactura", venta.numeroFactura);
                datos.setearParametro("@TipoFactura", venta.tipoFactura);
                datos.setearParametro("@Fecha", venta.FechaVenta);
                datos.setearParametro("@ClienteId", venta.cliente.Id);
                datos.setearParametro("@Total", venta.totalVenta);
                datos.setearParametro("@MedioPago", venta.MedioPago);
                datos.setearParametro("@NroCierreCaja", venta.nroCierreCaja);
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
