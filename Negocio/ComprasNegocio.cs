using datos;
using dominio;
using Equipo1b_TPC.Dominio;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;

namespace Negocio
{
    public class ComprasNegocio
    {
        public bool RegistrarCompraCompleta(Compra compra)
        {
            AccesoDatos datos = new AccesoDatos();
            int idCompra = 0;
            
            try
            {
                // Validar que todos los productos pertenezcan al proveedor seleccionado
                foreach (var detalle in compra.Detalles)
                {
                    if (!ValidarProductoProveedor(detalle.Producto.Id, compra.Proveedor.Id))
                    {
                        throw new Exception($"El producto '{detalle.Producto.Id}' no pertenece al proveedor seleccionado. Verifique la lista de productos.");
                    }
                }

                datos.setearConsulta(@"INSERT INTO Compras (ProveedorId, FechaCompra, Recibida, Total) 
                                      VALUES (@ProveedorId, @FechaCompra, @Recibida, @Total); 
                                      SELECT SCOPE_IDENTITY()");
                
                datos.setearParametro("@ProveedorId", compra.Proveedor.Id);
                datos.setearParametro("@FechaCompra", compra.FechaCompra);
                datos.setearParametro("@Recibida", compra.Recibida);
                datos.setearParametro("@Total", compra.Total);

                datos.ejecutarLectura();
                
                if (datos.Lector.Read())
                {
                    idCompra = Convert.ToInt32(datos.Lector[0]);
                }
                datos.cerrarConexion();

                foreach (var detalle in compra.Detalles)
                {
                    RegistrarDetalleCompra(idCompra, detalle);
                    ActualizarStockYPrecio(detalle.Producto.Id, detalle.Cantidad, detalle.PrecioUnitario);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar compra: " + ex.Message, ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        //un producto pertenezca a un proveedor

        private bool ValidarProductoProveedor(int productoId, int proveedorId)
        {
            AccesoDatos datos = new AccesoDatos();
            
            try
            {
                datos.setearConsulta(@"SELECT COUNT(*) 
                                      FROM Productos 
                                      WHERE Id = @ProductoId 
                                        AND ProveedorId = @ProveedorId 
                                        AND Activo = 1");
                
                datos.setearParametro("@ProductoId", productoId);
                datos.setearParametro("@ProveedorId", proveedorId);
                
                datos.ejecutarLectura();
                
                if (datos.Lector.Read())
                {
                    return Convert.ToInt32(datos.Lector[0]) > 0;
                }
                
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar producto-proveedor: " + ex.Message, ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        private void RegistrarDetalleCompra(int compraId, detalleCompra detalle)
        {
            AccesoDatos datos = new AccesoDatos();
            
            try
            {
                datos.setearConsulta(@"INSERT INTO DetalleCompras (CompraId, ProductoId, Cantidad, PrecioUnitario) 
                                      VALUES (@CompraId, @ProductoId, @Cantidad, @PrecioUnitario)");
                
                datos.setearParametro("@CompraId", compraId);
                datos.setearParametro("@ProductoId", detalle.Producto.Id);
                datos.setearParametro("@Cantidad", detalle.Cantidad);
                datos.setearParametro("@PrecioUnitario", detalle.PrecioUnitario);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar detalle: " + ex.Message, ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        private void ActualizarStockYPrecio(int productoId, int cantidadAgregar, decimal nuevoPrecioCompra)
        {
            AccesoDatos datos = new AccesoDatos();
            
            try
            {
                datos.setearConsulta(@"UPDATE Productos 
                                      SET StockActual = StockActual + @Cantidad, 
                                          PrecioCompra = @PrecioCompra 
                                      WHERE Id = @ProductoId");
                
                datos.setearParametro("@Cantidad", cantidadAgregar);
                datos.setearParametro("@PrecioCompra", nuevoPrecioCompra);
                datos.setearParametro("@ProductoId", productoId);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar stock: " + ex.Message, ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        public List<Compra> listar(int idCompra = 0) {
            AccesoDatos datos = new AccesoDatos();
            string consulta = "select C.id AS CompraId,C.ProveedorId,C.FechaCompra,C.Recibida,C.Total,P.RazonSocial from Compras C inner join Proveedores P on ProveedorId=p.Id";
            if (idCompra != 0)
            {
                consulta += " WHERE C.id=" + idCompra;
            }
            try
            {
                List<Compra> lcompra = new List<Compra>();
                datos.setearConsulta(consulta);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Compra aux = new Compra();
                    aux.Id =(int)datos.Lector["CompraId"];
                    aux.Proveedor.Id = (int)datos.Lector["ProveedorId"];
                    aux.Proveedor.RazonSocial = (string)datos.Lector["RazonSocial"];
                    aux.FechaCompra = (DateTime)datos.Lector["FechaCompra"];
                    aux.Recibida = (bool)datos.Lector["Recibida"];
                    aux.Total = (decimal)datos.Lector["Total"];
                    lcompra.Add(aux);
                }
                return lcompra;
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
        public List<Compra> listarPorFecha(DateTime desde,DateTime hasta)
        {
            AccesoDatos datos = new AccesoDatos();
            string consulta = "select id,ProveedorId,FechaCompra,Recibida,Total from Compras Where Convert(date,FechaCompra) BETWEEN @desde and @hasta";
            List<Compra> lcompras = new List<Compra>();
            try
            {
                datos.setearConsulta(consulta);
                datos.setearParametro("@desde", desde.Date);
                datos.setearParametro("@hasta", hasta.Date);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Compra aux = new Compra();
                    aux.Id =(int)datos.Lector["id"];
                    aux.Proveedor.Id = (int)datos.Lector["ProveedorId"];
                    aux.FechaCompra = (DateTime)datos.Lector["FechaCompra"];
                    aux.Recibida = (bool)datos.Lector["Recibida"];
                    aux.Total = (decimal)datos.Lector["Total"];

                    lcompras.Add(aux);

                }
                return lcompras;
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
        public List<detalleCompra> listarDetalleCompras(int NroCompra = 0)
        {
            AccesoDatos datos = new AccesoDatos();
            List<detalleCompra> ldetalle = new List<detalleCompra>();
            string consulta = "select CompraId,ProductoId,Cantidad,PrecioUnitario,Subtotal,P.Nombre from DetalleCompras inner join Productos P on ProductoId=P.Id";
            if (NroCompra != 0)
            {
                consulta += " Where CompraID=" + NroCompra;
            }
            try
            {
                datos.setearConsulta(consulta);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    detalleCompra aux = new detalleCompra();
                    aux.Id =(int)datos.Lector["CompraId"];
                    aux.Producto.Id = (int)datos.Lector["ProductoId"];
                    aux.Cantidad = (int)datos.Lector["Cantidad"];
                    aux.PrecioUnitario = (decimal)datos.Lector["PrecioUnitario"];
                    aux.subtotal = (decimal)datos.Lector["Subtotal"];
                    aux.Producto.Nombre = (string)datos.Lector["Nombre"];

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




        }
}
