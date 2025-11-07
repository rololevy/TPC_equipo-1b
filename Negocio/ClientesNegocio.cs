using datos;
using Equipo1b_TPC.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ClientesNegocio
    {
        public List<Cliente> listar(bool incluirInactivos,string RazonSocial="")
        {
            List<Cliente> lClientes = new List<Cliente>();
            AccesoDatos datos = new AccesoDatos();
            string consulta = "select Id,RazonSocial,Cuit,Telefono,Direccion,Activo,Email from Clientes";
            //si no queremos incluir inactivos
            if (!incluirInactivos)
            {
                consulta += " WHERE activo =1";
            }
            else
            {
                consulta += " WHERE 1=1";//evitamos error si no entra en el if
            }
            if (RazonSocial != "")
                consulta += " AND RazonSocial LIKE '%" + RazonSocial + "%'";
            //cerramos la consulta alfinal
            consulta += ";";



            datos.setearConsulta(consulta);
            datos.ejecutarLectura();
            try
            {
                while (datos.Lector.Read())
                {
                    Cliente aux = new Cliente();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.RazonSocial = (string)datos.Lector["RazonSocial"];
                    aux.Cuit = (string)datos.Lector["Cuit"];
                    aux.Telefono = (string)datos.Lector["Telefono"];
                    aux.Direccion = (string)datos.Lector["Direccion"];
                    aux.Activo = (bool)datos.Lector["Activo"];
                    aux.Email = (string)datos.Lector["Email"];
            

                    lClientes.Add(aux);
                }
                return lClientes;

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
        public List<Cliente> ListarPorCuit(string Cuit,bool incluirInactivos)
        {
            List<Cliente> lClientes = new List<Cliente>();
            AccesoDatos datos = new AccesoDatos();
            string consulta = "select Id,RazonSocial,Cuit,Telefono,Direccion,Activo,Email from Clientes";
            //si no queremos incluir inactivos
            if (!incluirInactivos)
            {
                consulta += " WHERE activo =1";
            }
            else
            {
                consulta += " WHERE 1=1";//evitamos error si no entra en el if
            }
            consulta += " AND Cuit LIKE '%" + Cuit + "%';";
            datos.setearConsulta(consulta);
            datos.ejecutarLectura();

            try
            {
                while (datos.Lector.Read())
                {
                    Cliente aux = new Cliente();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.RazonSocial = (string)datos.Lector["RazonSocial"];
                    aux.Cuit = (string)datos.Lector["Cuit"];
                    aux.Telefono = (string)datos.Lector["Telefono"];
                    aux.Direccion = (string)datos.Lector["Direccion"];
                    aux.Activo = (bool)datos.Lector["Activo"];
                    aux.Email = (string)datos.Lector["Email"];


                    lClientes.Add(aux);
                }
                return lClientes;

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
        public void agregar(Cliente cl)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("insert into Clientes(RazonSocial, Cuit, Telefono, Direccion,Email)Values(@RazonSocial, @Cuit, @Telefono, @Direccion,@Email);");
                datos.setearParametro("@RazonSocial", cl.RazonSocial);
                datos.setearParametro("@Cuit", cl.Cuit);
                datos.setearParametro("@Telefono", cl.Telefono);
                datos.setearParametro("@Direccion", cl.Direccion);
                datos.setearParametro("@Email", cl.Email);
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
        public void modificar(Cliente cl,string cuitModificar)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update Clientes set RazonSocial = @RazonSocial, Cuit = @CuitNuevo, Telefono = @Telefono, Direccion = @Direccion, Email = @Email where Cuit = @CuitModificar");
                datos.setearParametro("@RazonSocial", cl.RazonSocial);
                datos.setearParametro("@CuitNuevo", cl.Cuit);
                datos.setearParametro("@Telefono", cl.Telefono);
                datos.setearParametro("@Direccion", cl.Direccion);
                datos.setearParametro("@Email", cl.Email);
                datos.setearParametro("@CuitModificar",cuitModificar);
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
        public void bajaLogica(string cuit)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update Clientes set Activo = 0 where Cuit = @Cuit;");
                datos.setearParametro("@Cuit", cuit);
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
        public void AltaLogica(string cuit)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update Clientes set Activo = 1 where Cuit = @Cuit;");
                datos.setearParametro("@Cuit", cuit);
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
