using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Equipo1b_TPC.Dominio
{
    public class Usuario
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasena { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        public bool Activo { get; set; }


        public Usuario()
        {
            Activo = true;
        }

        public string NombreCompleto
        {
            get { return Nombre + " " + Apellido; }
        }

        public override string ToString()
        {
            return NombreUsuario;
        }
    }

    public enum TipoUsuario
    {
        Vendedor = 1,
        Administrador = 2
    }
}