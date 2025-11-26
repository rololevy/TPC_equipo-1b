using datos;
using dominio;
using Equipo1b_TPC.Dominio;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class DetalleVentasNegocio
    {
        public List<detalleVenta> listar(int id = 0)
        {
            AccesoDatos datos = new AccesoDatos();
            List<detalleVenta> ldetalle = new List<detalleVenta>();
  
            string consulta = "select Id,NumeroFactura,ProductoId,Cantidad,PrecioUnitario,Subtotal from DetalleVentas;";
            if (id != 0)
            {
                consulta += " Where Id=" + id;
            }
            datos.setearConsulta(consulta);
            try
            {
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    detalleVenta aux = new detalleVenta();
                    aux.id = (int)datos.Lector["Id"];
                    aux.NumeroFactura = (int)datos.Lector["NumeroFactura"];
                    aux.producto.Id = (int)datos.Lector["ProductoId"];
                    aux.cantidad = (int)datos.Lector["Cantidad"];
                    aux.PrecioUnitario = (decimal)datos.Lector["PrecioUnitario"];
                    aux.subtotal = (decimal)datos.Lector["Subtotal"];
                    ldetalle.Add(aux);
                }
                return ldetalle;
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
        public void AgregarDetalle(detalleVenta detalle)
        {
            AccesoDatos datos = new AccesoDatos();
            datos.setearConsulta("insert into DetalleVentas(NumeroFactura, ProductoId, Cantidad, PrecioUnitario) values(@NumeroFactura, @ProductoId, @Cantidad, @PrecioUnitario)");
            try
            {
                datos.setearParametro("@NumeroFactura", detalle.NumeroFactura);
                datos.setearParametro("@ProductoId", detalle.producto.Id);
                datos.setearParametro("@Cantidad", detalle.cantidad);
                datos.setearParametro("@PrecioUnitario", detalle.PrecioUnitario);
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
        public void EliminarLogico(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            datos.setearConsulta("Delete from DetalleVentas where id=" + id);
            try
            {
                datos.ejecutarAccion();
            }
            catch (Exception ex)
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
