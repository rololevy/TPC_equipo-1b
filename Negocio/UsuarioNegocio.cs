using System;
using datos;
using Equipo1b_TPC.Dominio;

namespace Negocio
{
    public class UsuarioNegocio
    {
        public Usuario Login(string nombreUsuario, string contrasena)
        {
            AccesoDatos datos = new AccesoDatos();
            Usuario usuario = null;

            try
            {
                datos.setearConsulta("SELECT Id, NombreUsuario, Nombre, Apellido, TipoUsuario, Activo FROM Usuarios WHERE NombreUsuario = @Usuario AND Contrasena = @Pass AND Activo = 1");
                datos.setearParametro("@Usuario", nombreUsuario);
                datos.setearParametro("@Pass", contrasena);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    usuario = new Usuario();
                    usuario.Id = (int)datos.Lector["Id"];
                    usuario.NombreUsuario = (string)datos.Lector["NombreUsuario"];
                    usuario.Nombre = (string)datos.Lector["Nombre"];
                    usuario.Apellido = (string)datos.Lector["Apellido"];
                    usuario.TipoUsuario = (TipoUsuario)(int)datos.Lector["TipoUsuario"];
                    usuario.Activo = (bool)datos.Lector["Activo"];
                }

                return usuario;
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
