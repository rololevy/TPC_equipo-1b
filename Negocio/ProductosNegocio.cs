using datos; 
using Equipo1b_TPC.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    internal class ProductosNegocio
    {
        public List<Producto> listar()
        {
            List<Producto> lproductos = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();
            string consulta = "select Id,Nombre,Descripcion,MarcaId,CategoriaID,ProveedorId,PrecioCompra,PorcentajeGanancia,StockActual,StockMinimo,Activo FROM Productos;";
            try
            {
                datos.setearConsulta(consulta);
                datos.ejecutarLectura();
                {
                    while (datos.Lector.Read())
                    {
                        Producto aux = new Producto();
                        aux.Id = (int)datos.Lector["Id"];
                        aux.Nombre = (string)datos.Lector["Nombre"];
                        aux.Descripcion = (string)datos.Lector["Descripcion"];
                        aux.Marca.Id = (int)datos.Lector["MarcaId"];
                        aux.Categoria.Id=(int)datos.Lector["CategoriaID"];
                        aux.Provedor.Id = (int)datos.Lector["ProvedorId"];
                        aux.PrecioCompra = (decimal)datos.Lector["PrecioCompra"];
                        aux.PorcentajeGanancia = (decimal)datos.Lector["PorcentajeGanancia"];
                        aux.StockActual = (int)datos.Lector["StockActual"];
                        aux.StockMinimo = (int)datos.Lector["StockMinimo"];
                        aux.Activo = (bool)datos.Lector["activo"];

                        lproductos.Add(aux);
                    }
                    return lproductos;
                }
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
        public void Agregar(Producto prd)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("insert into Productos(id, Nombre, Descripcion, MarcaId, CategoriaId, ProveedorId, PrecioCompra, PorcentajeGanancia, StockActual, StockMinimo, Activo) values(@id, @Nombre, @Descripcion, @MarcaId, @CategoriaId, @ProveedorId, @PrecioCompra, @PorcentajeGanancia, @StockActual, @StockMinimo, @Activo)");
                datos.setearParametro("@id,", prd.Id);
                datos.setearParametro("@Nombre", prd.Id);
                datos.setearParametro("@Descripcion", prd.Descripcion);
                datos.setearParametro("@MarcaId", prd.Marca.Id);
                datos.setearParametro("@CategoriaId", prd.Categoria.Id);
                datos.setearParametro("@ProveedorId", prd.Provedor.Id);
                datos.setearParametro("@PrecioCompra", prd.PrecioCompra);
                datos.setearParametro("@PorcentajeGanancia", prd.PorcentajeGanancia);
                datos.setearParametro("@StockActual", prd.StockActual);
                datos.setearParametro("@StockMinimo", prd.StockMinimo);
                datos.setearParametro("@Activo", prd.Activo);
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
