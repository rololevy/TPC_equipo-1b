using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Equipo1b_TPC.Dominio
{
    public class Marca
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }

        public Marca()
        {
            Activo = true;
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}