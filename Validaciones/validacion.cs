using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Validaciones
{
    public class validacion
    {
        public bool validarTxtCuit(string cuit)
        {
            //verificamos que no este vacio
            if (string.IsNullOrWhiteSpace(cuit))
            {
                return false;
            }
            //verificamos que no tenga letras, espacios y guiones
            if (!long.TryParse(cuit, out _))
            {
                return false;
            }
            //verificamos que tenga una longitud de 11
            if (cuit.Length != 11)
            {
                return false;
            }

            return true;
        }
        public bool validarEmail(string Email)
        {
            //si esta vacio no hay que validar
            if (string.IsNullOrWhiteSpace(Email))
            {
                return true;
            }
            //si no esta vacio validamos que tenga un formato valido
            return Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }
    }
}
