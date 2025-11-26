using datos;
using dominio;
using Equipo1b_TPC.Dominio;
using System;

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

        /// <summary>
        /// Valida que un producto pertenezca a un proveedor específico
        /// </summary>
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
    }
}