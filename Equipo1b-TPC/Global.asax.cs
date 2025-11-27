using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Equipo1b_TPC
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exc = Server.GetLastError();

            if (exc == null)
                return;

            //Mensaje amigable
            string mensajeUsuario = "Ha ocurrido un error inesperado. Por favor, intente nuevamente.";

            if (exc is System.Data.SqlClient.SqlException)
            {
                mensajeUsuario = "Error al comunicarse con la base de datos. Por favor, intente nuevamente.";
            }
            else if (exc is FormatException)
            {
                mensajeUsuario = "Los datos ingresados no tienen el formato correcto.";
            }
            else if (exc is ArgumentNullException || exc is NullReferenceException)
            {
                mensajeUsuario = "Se encontraron datos incompletos o faltantes.";
            }

            if (Session != null)
            {
                Session["LastError"] = mensajeUsuario;
                Session["LastErrorPage"] = Request.Url?.ToString() ?? "Desconocida";
            }

            Server.ClearError();
            Response.Redirect("~/Error.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }

        protected void Session_Start(object sender, EventArgs e)
        {
        }

        protected void Session_End(object sender, EventArgs e)
        {
        }
    }
}