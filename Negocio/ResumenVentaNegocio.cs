using datos;
using dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ResumenVentaNegocio
    {
        public List<ResumenVenta> listar(bool incluirCerradas,DateTime? fecha = null)
        {
            AccesoDatos datos = new AccesoDatos();
            List<ResumenVenta> lresumen = new List<ResumenVenta>();
            string consulta = "select NroDeCierre,TotalGeneral,TotalEfectivo,TotalTarjeta,TotalQr,TotalFA,TotalFB,TotalFC,TotalOperaciones,FechaResumenVenta from resumenVenta";
            try
            {
                //lista solo cajas abiertas
                if (incluirCerradas)
                {
                    consulta += " Where Cerrado = 1";
                }
                else
                {
                    consulta += " Where Cerrado = 0";
                }
                if (fecha != null)
                {
                    consulta += " And FechaResumenVenta=@fecha";
                    datos.setearParametro("@fecha", fecha.Value);
                }
                datos.setearConsulta(consulta);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    ResumenVenta aux = new ResumenVenta();
                    aux.NroDeCierre = (int)datos.Lector["NroDeCierre"];
                    aux.totalGeneral = (decimal)datos.Lector["TotalGeneral"];
                    aux.totalEfectivo = (decimal)datos.Lector["TotalEfectivo"];
                    aux.totalTarjeta = (decimal)datos.Lector["TotalTarjeta"];
                    aux.totalQr = (decimal)datos.Lector["TotalQr"];
                    aux.totalFa = (decimal)datos.Lector["TotalFA"];
                    aux.totalFb = (decimal)datos.Lector["TotalFB"];
                    aux.totalFc = (decimal)datos.Lector["TotalFC"];
                    aux.totalOperaciones = (int)datos.Lector["TotalOperaciones"];
                    aux.fechaResumenVenta = (DateTime)datos.Lector["FechaResumenVenta"];
                    lresumen.Add(aux);

                }
                return lresumen;

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
        public void agregar(ResumenVenta resumen)
        {
            AccesoDatos datos = new AccesoDatos();
            datos.setearConsulta("insert into resumenVenta (TotalGeneral,TotalEfectivo,TotalTarjeta,TotalQr,TotalFA,TotalFB,TotalFC,TotalOperaciones,FechaResumenVenta) values (@TotalGeneral,@TotalEfectivo,@TotalTarjeta,@TotalQr,@TotalFA,@TotalFB,@TotalFC,@TotalOperaciones,@FechaResumenVenta)");
            try
            {
                datos.setearParametro("@TotalGeneral", resumen.totalGeneral);
                datos.setearParametro("@TotalEfectivo", resumen.totalEfectivo);
                datos.setearParametro("@TotalTarjeta", resumen.totalTarjeta);
                datos.setearParametro("@TotalQr", resumen.totalQr);
                datos.setearParametro("@TotalFA", resumen.totalFa);
                datos.setearParametro("@TotalFB", resumen.totalFb);
                datos.setearParametro("@TotalFC", resumen.totalFc);
                datos.setearParametro("@TotalOperaciones", resumen.totalOperaciones);
                datos.setearParametro("@FechaResumenVenta", resumen.fechaResumenVenta);
                datos.ejecutarAccion();
                ;
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
        public bool actualizarResumenDeldia(venta v)
        {
            AccesoDatos datos = new AccesoDatos();
            DateTime dia = DateTime.Today;
            try
            {
                //verificamos si existe un resumen del dia de hoy
                datos.setearConsulta("select NroDeCierre,Cerrado from resumenVenta where FechaResumenVenta =@fecha");
                datos.setearParametro("@fecha", dia);
                datos.ejecutarLectura();
                bool existe = false;
                bool estaCerrado = false;

                if (datos.Lector.Read())
                {
                    existe = true;
                    estaCerrado =(bool)datos.Lector["Cerrado"];
                }

                datos.cerrarConexion();
                //si existe y esta cerrada la caja salimos
                if(existe && estaCerrado)
                {
                    return false;
                }
                //si no existe y no esta cerrada ninguna venta creamos una nueva
                if (!existe )
                {
                    
                    ResumenVenta resumen = new ResumenVenta();
                    resumen.sumarVenta(v);
                    resumen.fechaResumenVenta = DateTime.Today;

                    agregar(resumen);
                    return true;
                }
                datos = new AccesoDatos();
                //si existe actualizamos el resumen existente
                datos.setearConsulta("Update ResumenVenta set TotalGeneral=TotalGeneral+@total,TotalEfectivo=TotalEfectivo+@TotalE,TotalTarjeta=TotalTarjeta+@TotalT," +
                    "TotalQr=TotalQr+@TotalQ,TotalFA=TotalFA+@Tfa,TotalFB=TotalFB+@Tfb,TotalFC=TotalFC+@Tfc,TotalOperaciones=TotalOperaciones+1 Where FechaResumenVenta=@fecha");
                //valores en 0 para los que vienen vacios
                decimal TotalE = 0;
                decimal TotalT = 0;
                decimal TotalQ = 0;
                decimal Tfa = 0;
                decimal Tfb = 0;
                decimal Tfc = 0;
                switch (v.MedioPago)
                {
                    case "E":
                        TotalE = v.totalVenta;
                        break;
                    case "T":
                        TotalT = v.totalVenta;
                        break;
                    case "Q":
                        TotalQ = v.totalVenta;
                        break;
                }
                switch (v.tipoFactura)
                {
                    case "A":
                        Tfa = v.totalVenta;
                        break;
                    case "B":
                        Tfb = v.totalVenta;
                        break;
                    case "C":
                        Tfc = v.totalVenta;
                        break;
                }
                datos.setearParametro("@total", v.totalVenta);
                datos.setearParametro("@TotalE", TotalE);
                datos.setearParametro("@TotalT", TotalT);
                datos.setearParametro("@TotalQ", TotalQ);
                datos.setearParametro("@Tfa", Tfa);
                datos.setearParametro("@Tfb", Tfb);
                datos.setearParametro("@Tfc", Tfc);
                datos.setearParametro("@fecha", DateTime.Today);
                datos.ejecutarAccion();
                return true;
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
        public void CerrarVenta(int nroDeCierre)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "Update ResumenVenta set Cerrado=1 Where NroDeCierre=@NroDeCierre";
                datos.setearConsulta(consulta);
                datos.setearParametro("@NroDeCierre", nroDeCierre);
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
        public List<ResumenVenta> filtrarPorFechas(DateTime desde, DateTime hasta)
        {
            AccesoDatos datos = new AccesoDatos();
            List<ResumenVenta> lresumen = new List<ResumenVenta>();
            try
            {
                string consulta = "select NroDeCierre,TotalGeneral,TotalEfectivo,TotalTarjeta,TotalQr,TotalFA,TotalFB,TotalFC,TotalOperaciones,FechaResumenVenta from resumenVenta Where Cerrado=1 and FechaResumenVenta BETWEEN @desde and @hasta";
                datos.setearConsulta(consulta);
                datos.setearParametro("@desde", desde);
                datos.setearParametro("@hasta", hasta);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    ResumenVenta aux = new ResumenVenta();
                    aux.NroDeCierre = (int)datos.Lector["NroDeCierre"];
                    aux.totalGeneral = (decimal)datos.Lector["TotalGeneral"];
                    aux.totalEfectivo = (decimal)datos.Lector["TotalEfectivo"];
                    aux.totalTarjeta = (decimal)datos.Lector["TotalTarjeta"];
                    aux.totalQr = (decimal)datos.Lector["TotalQr"];
                    aux.totalFa = (decimal)datos.Lector["TotalFA"];
                    aux.totalFb = (decimal)datos.Lector["TotalFB"];
                    aux.totalFc = (decimal)datos.Lector["TotalFC"];
                    aux.totalOperaciones = (int)datos.Lector["TotalOperaciones"];
                    aux.fechaResumenVenta = (DateTime)datos.Lector["FechaResumenVenta"];
                    lresumen.Add(aux);

                }
                return lresumen;
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
        //devuelve el ultimo resumen de venta activo
        public ResumenVenta GetCierreActivo()
        {
         
            AccesoDatos datos = new AccesoDatos();
            
            try
            {
                datos.setearConsulta("select top 1 NroDeCierre,FechaResumenVenta from resumenVenta where cerrado=0 order by NroDeCierre Desc");
                datos.ejecutarLectura();
                if (datos.Lector.Read())
                {
                    ResumenVenta resumen = new ResumenVenta();
                    resumen.NroDeCierre =(int)datos.Lector["NroDeCierre"];
                    resumen.fechaResumenVenta = (DateTime)datos.Lector["FechaResumenVenta"];
                    return resumen;
                }

                return null;
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
        //creamos un nuevo resumen si no hay ninguno activo
        public int crearResumenVenta()
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Insert into resumenVenta(TotalGeneral,TotalEfectivo,TotalTarjeta,TotalQr,TotalFA,TotalFB,TotalFC,TotalOperaciones,FechaResumenVenta,Cerrado) values (0,0,0,0,0,0,0,0, GETDATE(),0)  select SCOPE_IDENTITY()");
                return datos.ejecutarScalar();
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
        public ResumenVenta ObtenerResumenDelDia()
        {
            //verificamos si existe un resumen activo
            ResumenVenta resumen = GetCierreActivo();
            //si no existe creamos uno nuevo
            if (resumen == null)
            {
                resumen = new ResumenVenta();
                int nro = crearResumenVenta();
                resumen.NroDeCierre = nro;
                resumen.fechaResumenVenta = DateTime.Today;
                return resumen;
            }
            //si existe pero no es de hoy y sigue activo cerramos y creamos uno nuevo
            if (resumen.fechaResumenVenta.Date != DateTime.Today.Date)
            {
                CerrarVenta(resumen.NroDeCierre);
                int nro = crearResumenVenta();
                resumen = new ResumenVenta();
                resumen.NroDeCierre = nro;
                resumen.fechaResumenVenta = DateTime.Today;
                return resumen;
            }
            //si existe y es de hoy
            return resumen;
          
        }
    }
}
