using datos; 
using Equipo1b_TPC.Dominio;
using System;
using System.Collections.Generic;

namespace Negocio
{
    public class ProductosNegocio
    {
        public List<Producto> listar(int id = 0,int idMarca=0,int idCategoria=0)
        {
            List<Producto> lproductos = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();
            
            try
            {
                // Query con JOINs para obtener nombres de Marca, Categoría y Proveedor
                string consulta = @"SELECT P.Id, P.Nombre, P.Descripcion, 
                                   P.MarcaId, P.CategoriaId, P.ProveedorId, 
                                   P.PrecioCompra, P.PorcentajeGanancia, 
                                   P.StockActual, P.StockMinimo, P.Activo,
                                   M.Nombre AS MarcaNombre,
                                   C.Nombre AS CategoriaNombre,
                                   PR.RazonSocial AS ProveedorNombre
                                   FROM Productos P
                                   LEFT JOIN Marcas M ON P.MarcaId = M.Id
                                   LEFT JOIN Categorias C ON P.CategoriaId = C.Id
                                   LEFT JOIN Proveedores PR ON P.ProveedorId = PR.Id
                                   WHERE P.Activo = 1";

                if (id != 0)
                {
                    consulta += " AND P.Id = " + id.ToString();
                }
                if (idMarca != 0)
                {
                    consulta += " AND P.MarcaId=" + idMarca.ToString();
                }
                if (idCategoria != 0)
                {
                    consulta += " AND P.CategoriaId=" + idCategoria.ToString();
                }

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto aux = new Producto();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = datos.Lector["Descripcion"] != DBNull.Value 
                        ? (string)datos.Lector["Descripcion"] 
                        : string.Empty;
                    
                    // Cargar Marca con su nombre
                    if (datos.Lector["MarcaId"] != DBNull.Value)
                    {
                        aux.Marca = new Marca 
                        { 
                            Id = (int)datos.Lector["MarcaId"],
                            Nombre = datos.Lector["MarcaNombre"] != DBNull.Value 
                                ? (string)datos.Lector["MarcaNombre"] 
                                : ""
                        };
                    }

                    // Cargar Categoría con su nombre
                    if (datos.Lector["CategoriaId"] != DBNull.Value)
                    {
                        aux.Categoria = new Categoria 
                        { 
                            Id = (int)datos.Lector["CategoriaId"],
                            Nombre = datos.Lector["CategoriaNombre"] != DBNull.Value 
                                ? (string)datos.Lector["CategoriaNombre"] 
                                : ""
                        };
                    }

                    // Cargar Proveedor con su nombre
                    if (datos.Lector["ProveedorId"] != DBNull.Value)
                    {
                        aux.Provedor = new Proveedor 
                        { 
                            Id = (int)datos.Lector["ProveedorId"],
                            RazonSocial = datos.Lector["ProveedorNombre"] != DBNull.Value 
                                ? (string)datos.Lector["ProveedorNombre"] 
                                : ""
                        };
                    }

                    aux.PrecioCompra = (decimal)datos.Lector["PrecioCompra"];
                    aux.PorcentajeGanancia = Convert.ToInt32(datos.Lector["PorcentajeGanancia"]);
                    aux.StockActual = (int)datos.Lector["StockActual"];
                    aux.StockMinimo = (int)datos.Lector["StockMinimo"];
                    aux.Activo = (bool)datos.Lector["Activo"];

                    lproductos.Add(aux);
                }
                return lproductos;
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

        /// <summary>
        /// Lista productos de un proveedor específico (para usar en compras)
        /// </summary>
        public List<Producto> listarPorProveedor(int proveedorId, int idMarca = 0, int idCategoria = 0)
        {
            List<Producto> lproductos = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();
            
            try
            {
                string consulta = @"SELECT P.Id, P.Nombre, P.Descripcion, 
                                   P.MarcaId, P.CategoriaId, P.ProveedorId, 
                                   P.PrecioCompra, P.PorcentajeGanancia, 
                                   P.StockActual, P.StockMinimo, P.Activo,
                                   M.Nombre AS MarcaNombre,
                                   C.Nombre AS CategoriaNombre,
                                   PR.RazonSocial AS ProveedorNombre
                                   FROM Productos P
                                   LEFT JOIN Marcas M ON P.MarcaId = M.Id
                                   LEFT JOIN Categorias C ON P.CategoriaId = C.Id
                                   LEFT JOIN Proveedores PR ON P.ProveedorId = PR.Id
                                   WHERE P.Activo = 1 
                                     AND P.ProveedorId = @ProveedorId";

                if (idMarca != 0)
                {
                    consulta += " AND P.MarcaId = " + idMarca.ToString();
                }
                if (idCategoria != 0)
                {
                    consulta += " AND P.CategoriaId = " + idCategoria.ToString();
                }

                datos.setearConsulta(consulta);
                datos.setearParametro("@ProveedorId", proveedorId);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto aux = new Producto();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = datos.Lector["Descripcion"] != DBNull.Value 
                        ? (string)datos.Lector["Descripcion"] 
                        : string.Empty;
                    
                    if (datos.Lector["MarcaId"] != DBNull.Value)
                    {
                        aux.Marca = new Marca 
                        { 
                            Id = (int)datos.Lector["MarcaId"],
                            Nombre = datos.Lector["MarcaNombre"] != DBNull.Value 
                                ? (string)datos.Lector["MarcaNombre"] 
                                : ""
                        };
                    }

                    if (datos.Lector["CategoriaId"] != DBNull.Value)
                    {
                        aux.Categoria = new Categoria 
                        { 
                            Id = (int)datos.Lector["CategoriaId"],
                            Nombre = datos.Lector["CategoriaNombre"] != DBNull.Value 
                                ? (string)datos.Lector["CategoriaNombre"] 
                                : ""
                        };
                    }

                    if (datos.Lector["ProveedorId"] != DBNull.Value)
                    {
                        aux.Provedor = new Proveedor 
                        { 
                            Id = (int)datos.Lector["ProveedorId"],
                            RazonSocial = datos.Lector["ProveedorNombre"] != DBNull.Value 
                                ? (string)datos.Lector["ProveedorNombre"] 
                                : ""
                        };
                    }

                    aux.PrecioCompra = (decimal)datos.Lector["PrecioCompra"];
                    aux.PorcentajeGanancia = Convert.ToInt32(datos.Lector["PorcentajeGanancia"]);
                    aux.StockActual = (int)datos.Lector["StockActual"];
                    aux.StockMinimo = (int)datos.Lector["StockMinimo"];
                    aux.Activo = (bool)datos.Lector["Activo"];

                    lproductos.Add(aux);
                }
                return lproductos;
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

        public void agregar(Producto prd)
        {
            AccesoDatos datos = new AccesoDatos();
            
            try
            {
                datos.setearConsulta(@"INSERT INTO Productos 
                    (Nombre, Descripcion, MarcaId, CategoriaId, ProveedorId, 
                     PrecioCompra, PorcentajeGanancia, StockActual, StockMinimo, Activo) 
                    VALUES 
                    (@Nombre, @Descripcion, @MarcaId, @CategoriaId, @ProveedorId, 
                     @PrecioCompra, @PorcentajeGanancia, @StockActual, @StockMinimo, @Activo)");
                
                datos.setearParametro("@Nombre", prd.Nombre);
                datos.setearParametro("@Descripcion", string.IsNullOrEmpty(prd.Descripcion) 
                    ? (object)DBNull.Value 
                    : prd.Descripcion);
                datos.setearParametro("@MarcaId", prd.Marca != null && prd.Marca.Id != 0 
                    ? (object)prd.Marca.Id 
                    : DBNull.Value);
                datos.setearParametro("@CategoriaId", prd.Categoria != null && prd.Categoria.Id != 0 
                    ? (object)prd.Categoria.Id 
                    : DBNull.Value);
                datos.setearParametro("@ProveedorId", prd.Provedor != null && prd.Provedor.Id != 0 
                    ? (object)prd.Provedor.Id 
                    : DBNull.Value);
                datos.setearParametro("@PrecioCompra", prd.PrecioCompra);
                datos.setearParametro("@PorcentajeGanancia", prd.PorcentajeGanancia);
                datos.setearParametro("@StockActual", prd.StockActual);
                datos.setearParametro("@StockMinimo", prd.StockMinimo);
                datos.setearParametro("@Activo", prd.Activo);

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

        public void modificar(Producto prd)
        {
            AccesoDatos datos = new AccesoDatos();
            
            try
            {
                datos.setearConsulta(@"UPDATE Productos SET 
                    Nombre = @Nombre, 
                    Descripcion = @Descripcion, 
                    MarcaId = @MarcaId, 
                    CategoriaId = @CategoriaId, 
                    ProveedorId = @ProveedorId, 
                    PrecioCompra = @PrecioCompra, 
                    PorcentajeGanancia = @PorcentajeGanancia, 
                    StockActual = @StockActual, 
                    StockMinimo = @StockMinimo, 
                    Activo = @Activo 
                    WHERE Id = @Id");

                datos.setearParametro("@Id", prd.Id);
                datos.setearParametro("@Nombre", prd.Nombre);
                datos.setearParametro("@Descripcion", string.IsNullOrEmpty(prd.Descripcion) 
                    ? (object)DBNull.Value 
                    : prd.Descripcion);
                datos.setearParametro("@MarcaId", prd.Marca != null && prd.Marca.Id != 0 
                    ? (object)prd.Marca.Id 
                    : DBNull.Value);
                datos.setearParametro("@CategoriaId", prd.Categoria != null && prd.Categoria.Id != 0 
                    ? (object)prd.Categoria.Id 
                    : DBNull.Value);
                datos.setearParametro("@ProveedorId", prd.Provedor != null && prd.Provedor.Id != 0 
                    ? (object)prd.Provedor.Id 
                    : DBNull.Value);
                datos.setearParametro("@PrecioCompra", prd.PrecioCompra);
                datos.setearParametro("@PorcentajeGanancia", prd.PorcentajeGanancia);
                datos.setearParametro("@StockActual", prd.StockActual);
                datos.setearParametro("@StockMinimo", prd.StockMinimo);
                datos.setearParametro("@Activo", prd.Activo);

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

        public void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            
            try
            {
                datos.setearConsulta("UPDATE Productos SET Activo = 0 WHERE Id = @Id");
                datos.setearParametro("@Id", id);
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

        /// <summary>
        /// Actualiza únicamente el stock actual de un producto
        /// </summary>
        public void ActualizarStock(int productoId, int nuevoStock)
        {
            AccesoDatos datos = new AccesoDatos();
            
            try
            {
                datos.setearConsulta(@"UPDATE Productos 
                                      SET StockActual = @StockActual 
                                      WHERE Id = @Id");

                datos.setearParametro("@Id", productoId);
                datos.setearParametro("@StockActual", nuevoStock);

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

        /// <summary>
        /// Actualiza el stock actual y el stock mínimo de un producto
        /// </summary>
        public void ActualizarStocks(int productoId, int nuevoStockActual, int nuevoStockMinimo)
        {
            AccesoDatos datos = new AccesoDatos();
            
            try
            {
                datos.setearConsulta(@"UPDATE Productos 
                                      SET StockActual = @StockActual,
                                          StockMinimo = @StockMinimo
                                      WHERE Id = @Id");

                datos.setearParametro("@Id", productoId);
                datos.setearParametro("@StockActual", nuevoStockActual);
                datos.setearParametro("@StockMinimo", nuevoStockMinimo);

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
