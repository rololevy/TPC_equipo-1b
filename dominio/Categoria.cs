using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Equipo1b_TPC.Dominio
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }

        public Categoria()
        {
            Activo = true;
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}