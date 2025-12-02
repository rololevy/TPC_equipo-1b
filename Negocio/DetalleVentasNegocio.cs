using datos;
using dominio;
using Equipo1b_TPC.Dominio;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net.Configuration;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class DetalleVentasNegocio
    {
        public List<detalleVenta> listar(int id = 0, int NumeroFactura = 0)
        {
            AccesoDatos datos = new AccesoDatos();
            List<detalleVenta> ldetalle = new List<detalleVenta>();

            string consulta = "select Id,NumeroFactura,ProductoId,Cantidad,PrecioUnitario,Subtotal from DetalleVentas";
            if (id != 0)
            {
                consulta += " Where Id=" + id;
            }
            else
            {
                consulta += " Where 1=1";
            }
            if (NumeroFactura != 0)
            {
                consulta += " AND NumeroFactura=" + NumeroFactura;
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
            catch (Exception ex)
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
            catch (Exception ex)
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
        public List<detalleVenta> ListarFactura(int NumeroFactura)
        {
            AccesoDatos datos = new AccesoDatos();
            List<detalleVenta> ldetalle = new List<detalleVenta>();
            try
            {
                datos.setearConsulta("select DV.NumeroFactura,DV.ProductoId,P.Nombre As NombreProducto,DV.Cantidad,DV.PrecioUnitario,DV.SubTotal,V.MedioPago" +
                " as MedioPago,V.TipoFactura as TipoFactura from DetalleVentas DV inner join Productos P ON P.Id=DV.ProductoId INNER JOIN Ventas v on" +
                " V.NumeroFactura=DV.NumeroFactura WHERE DV.NumeroFactura=@NumeroFactura;");
                datos.setearParametro("@NumeroFactura", NumeroFactura);
                datos.ejecutarLectura();
                while(datos.Lector.Read())
                {
                    detalleVenta aux = new detalleVenta();
                    aux.NumeroFactura = (int)datos.Lector["NumeroFactura"];
                    aux.producto.Id = (int)datos.Lector["ProductoId"];
                    aux.producto.Nombre = (string)datos.Lector["NombreProducto"];
                    aux.cantidad = (int)datos.Lector["Cantidad"];
                    aux.PrecioUnitario = (decimal)datos.Lector["PrecioUnitario"];
                    aux.subtotal = (decimal)datos.Lector["SubTotal"];
                    aux.MedioPago = (string)datos.Lector["MedioPago"];
                    aux.TipoFactura = (string)datos.Lector["TipoFactura"];
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

    }
}
