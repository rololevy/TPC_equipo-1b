using Equipo1b_TPC.Dominio;
using System;
using System.Web;

namespace Equipo1b_TPC.Helpers
{
    public static class SeguridadHelper
    {
        public static bool ValidarSesion()
        {
            return HttpContext.Current.Session["Usuario"] != null;
        }

        public static Usuario ObtenerUsuarioActual()
        {
            if (ValidarSesion())
            {
                return (Usuario)HttpContext.Current.Session["Usuario"];
            }
            return null;
        }

        public static bool EsAdmin()
        {
            var usuario = ObtenerUsuarioActual();
            return usuario != null && usuario.TipoUsuario == TipoUsuario.Administrador;
        }

        public static bool EsVendedor()
        {
            var usuario = ObtenerUsuarioActual();
            return usuario != null && usuario.TipoUsuario == TipoUsuario.Vendedor;
        }

        public static void ValidarAcceso(TipoUsuario tipoRequerido)
        {
            if (!ValidarSesion())
            {
                HttpContext.Current.Session.Add("LastError", "?? Debes iniciar sesión para acceder.");
                HttpContext.Current.Response.Redirect("Login.aspx", false);
                return;
            }

            var usuario = ObtenerUsuarioActual();
            if (usuario.TipoUsuario != tipoRequerido)
            {
                string mensaje = tipoRequerido == TipoUsuario.Administrador
                    ? "? Sin permisos. Requiere nivel administrador."
                    : "? Sin permisos.";

                HttpContext.Current.Session.Add("LastError", mensaje);
                HttpContext.Current.Response.Redirect("Error.aspx", false);
            }
        }

        public static void ValidarAccesoMultiple(params TipoUsuario[] tiposPermitidos)
        {
            if (!ValidarSesion())
            {
                HttpContext.Current.Session.Add("LastError", "?? Debes iniciar sesión.");
                HttpContext.Current.Response.Redirect("Login.aspx", false);
                return;
            }

            var usuario = ObtenerUsuarioActual();
            foreach (var tipo in tiposPermitidos)
            {
                if (usuario.TipoUsuario == tipo)
                    return;
            }

            HttpContext.Current.Session.Add("LastError", "? Sin permisos.");
            HttpContext.Current.Response.Redirect("Error.aspx", false);
        }
    }
}
