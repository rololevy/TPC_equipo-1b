using datos; 
using Equipo1b_TPC.Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ProvedoresNegocio
    {
        public List<Proveedor> listar(bool incluirInactivos,int id=0,string razonSocial="",string cuit="")
        {
            AccesoDatos datos = new AccesoDatos();
            List<Proveedor> lProveedores = new List<Proveedor>();
            string consulta = "select Id,RazonSocial,CUIT,Email,Telefono,Direccion,Activo from Proveedores";

            try
            {
                //Si no queremos incluir inactivos
                if (!incluirInactivos)
                {
                    consulta += " WHERE Activo=1";
                }
                //evitamos que de error
                else
                {
                    consulta += " WHERE 1=1";
                }
                if (id != 0)
                {
                    consulta += " AND Id=" + id.ToString();
                }
                //filtro por razon social
                if (!string.IsNullOrEmpty(razonSocial))
                {
                    consulta += " AND RazonSocial LIKE '%" + razonSocial + "%'";
                }
                if (!string.IsNullOrEmpty(cuit))
                {
                    consulta += " AND Cuit LIKE '%" + cuit + "%';";

                }

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Proveedor aux = new Proveedor();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.RazonSocial = (string)datos.Lector["RazonSocial"];
                    aux.CUIT = (string)datos.Lector["CUIT"];
                    aux.Email = (string)datos.Lector["Email"];
                    aux.Telefono = (string)datos.Lector["Telefono"];
                    aux.Direccion = (string)datos.Lector["Direccion"];
                    aux.Activo = (bool)datos.Lector["Activo"];

                    lProveedores.Add(aux);
                }
                return lProveedores;
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
        public void agregar(Proveedor prov)
        {
            
            AccesoDatos datos= new AccesoDatos();
            
            try
            {
                datos.setearConsulta("INSERT INTO Proveedores(RazonSocial,CUIT,Email,Telefono,Direccion) values (@RazonSocial,@CUIT,@Email,@Telefono,@Direccion);");
                datos.setearParametro("@RazonSocial", prov.RazonSocial);
                datos.setearParametro("@CUIT", prov.CUIT);
                datos.setearParametro("@Email", prov.Email);
                datos.setearParametro("@Telefono", prov.Telefono);
                datos.setearParametro("@Direccion", prov.Direccion);

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
        public void bajaLogica(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Proveedores SET Activo = 0 WHERE Id = @Id;");
                datos.setearParametro("@Id", id);
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
        public void AltaLogica(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Proveedores SET Activo = 1 WHERE Id = @Id;");
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
        public void modificar(Proveedor prov)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Proveedores SET RazonSocial=@RazonSocial, CUIT=@CUIT, Email=@Email, Telefono=@Telefono, Direccion=@Direccion WHERE Id=@Id");
                datos.setearParametro("@Id", prov.Id);
                datos.setearParametro("@RazonSocial", prov.RazonSocial);
                datos.setearParametro("@CUIT", prov.CUIT);
                datos.setearParametro("@Email", prov.Email);
                datos.setearParametro("@Telefono",prov.Telefono);
                datos.setearParametro("@Direccion", prov.Direccion);
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
