using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Equipo1b_TPC.Dominio
{
    public class Proveedor
    {
        public int Id { get; set; }
        public string RazonSocial { get; set; }
        public string CUIT { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public bool Activo { get; set; }

        public Proveedor()
        {
            Activo = true;
        }

        public override string ToString()
        {
            return RazonSocial;
        }
    }
}