// file:	Logica.cs
//
// summary:	Implements the logica class

using InstAPIGeneric.BL.BD;
using InstAPIGeneric.Model;
using InstAPIGeneric.Model.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InstAPIGeneric.BL
{
    /// <summary>   Clase que tiene la ogica general del servicio. </summary>
    /// <remarks>   Kfacostar, 20/2/2020. </remarks>
    public class Logica
    {
        /// <summary>   Objeto de conexion a procesar SQL server. </summary>
        private readonly SQLBD executeSQL = new SQLBD();

        /// <summary>   Objeto de conexion a procesar informix. </summary>
        private readonly InformixBD executeInformix = new InformixBD();

        /// <summary> Método encargado de procesar la solicitud para un configuracion especifica. </summary>
        /// <remarks>   Kfacostar, 20/2/2020. </remarks>
        /// <exception cref="Exception">    Thrown when an exception error condition occurs. </exception>
        /// <param name="solicitud" >    Detalle de la solicitud y los parametros para ejecutar una configuracion. </param>
        /// <returns>   A RespuestaGenericaDTO. </returns>
        public resultadoFinalSolic Ejecutar(SolicitudGenericaDTO solicitud, int idBitacora)
        {
            ResultadoDTO dt = new ResultadoDTO();
            resultadoFinalSolic res = new resultadoFinalSolic();
            try
            {
                GenericoBD bd = new GenericoBD();

                ManejadorBitacora.AgregarBitacora(idBitacora, solicitud.Usuario, solicitud.IdConfiguracionProceso, "Iniciando el proceso de ejecución de configuracion", "", JsonConvert.SerializeObject(solicitud), "Ejecutar", "", solicitud.AplicacionOrigen);

                var configuraciones = bd.ObtenerConfiguracion(solicitud.IdConfiguracionProceso);
                string json = JsonConvert.SerializeObject(configuraciones);
                ManejadorBitacora.AgregarBitacora(idBitacora, solicitud.Usuario, solicitud.IdConfiguracionProceso, "Obtiene la configuración", "", "", "Ejecutar", json, solicitud.AplicacionOrigen);

                foreach (var configuracion in configuraciones)
                {
                    var conexion = bd.ObtenerConexion(configuracion.IdConexion.Value);
                    ManejadorBitacora.AgregarBitacora(idBitacora, solicitud.Usuario, solicitud.IdConfiguracionProceso, "Obtiene la conexion", "", "", "Ejecutar", "", solicitud.AplicacionOrigen);

                    var procedimiento = bd.ObtenerProcedimientoFuncion(configuracion.IdProcedimientoFuncion.Value);
                    ManejadorBitacora.AgregarBitacora(idBitacora, solicitud.Usuario, solicitud.IdConfiguracionProceso, "Obtiene la procedimiento", "", "", "Ejecutar", "", solicitud.AplicacionOrigen);

                    var parametros = AsignarValores(bd, configuracion, solicitud.Parametros);
                    ManejadorBitacora.AgregarBitacora(idBitacora, solicitud.Usuario, solicitud.IdConfiguracionProceso, "Obtiene la procedimiento", "", "", "Ejecutar", "", solicitud.AplicacionOrigen);

                    switch (conexion.IdTipoBD)
                    {
                        case 1:
                            dt = executeSQL.ResutlDataTable(conexion, procedimiento, parametros);
                            ManejadorBitacora.AgregarBitacora(idBitacora, solicitud.Usuario, solicitud.IdConfiguracionProceso, "Resultado de la ejecucion en el motor de BD SQL", "", "", "Ejecutar", JsonConvert.SerializeObject(dt), solicitud.AplicacionOrigen);
                            break;
                        case 2:
                            dt = executeInformix.ResutlDataTable(conexion, procedimiento, parametros);
                            ManejadorBitacora.AgregarBitacora(idBitacora, solicitud.Usuario, solicitud.IdConfiguracionProceso, "Resultado de la ejecucion en el motor de BD informix", "", "", "Ejecutar", JsonConvert.SerializeObject(dt), solicitud.AplicacionOrigen);
                            break;
                    }

                    res.Resultados.Add(dt);
                }

                ManejadorBitacora.AgregarBitacora(idBitacora, solicitud.Usuario, solicitud.IdConfiguracionProceso, "Finaliza la ejecución de los procedimientos o funciones almacenadas", "", "", "Ejecutar","", solicitud.AplicacionOrigen);

                res.IdConfiguracionProceso = solicitud.IdConfiguracionProceso;

                var verificacionResultados = res.Resultados.Any(x => x.Codigo == 1);

                if (verificacionResultados)
                {
                    res.Codigo = 2;
                    res.Mensaje = "Existen resultados incorrectos.";
                }
                else
                {
                    res.Codigo = 0;
                    res.Mensaje = "Solicitud finalizada con éxito.";
                }

                ManejadorBitacora.AgregarBitacora(idBitacora, solicitud.Usuario, solicitud.IdConfiguracionProceso, "Resultado de la ejecucion general", "", "", "Ejecutar", JsonConvert.SerializeObject(res), solicitud.AplicacionOrigen);
            }
            catch (Exception ex)
            {
                ManejadorBitacora.AgregarBitacora(idBitacora, solicitud.Usuario, solicitud.IdConfiguracionProceso, "Hubo una excepción en el proceso", ex.Message, "", "Ejecutar", "", solicitud.AplicacionOrigen);
                throw new Exception(ex.Message);
            }

            return res;
        }

        /// <summary>  Método encargado de asignar el valor de los prametros a los asiganados en la BD para la ejecución de la configuracion. </summary>
        /// <remarks>   Kfacostar, 20/2/2020. </remarks>
        /// <exception cref="Exception">    Thrown when an exception error condition occurs. </exception>
        /// <param name="bd">               Conexión abierta de la BD configurada. </param>
        /// <param name="configuracion">    Defincición de la configuración. </param>
        /// <param name="parametros">       Lista de parametros para un proceso de terminado. </param>
        /// <returns>   La lista de parametros con su respectivo valor asiganado. </returns>
        private List<Parametro> AsignarValores(GenericoBD bd, Configuracion configuracion, List<ParametroDTO> parametros)
        {
            List<Parametro> parametrosFinales = new List<Parametro>();

            try
            {
                parametrosFinales = bd.ObtenerParametros(configuracion.IdProcedimientoFuncion.Value);
                foreach (var parametro in parametrosFinales)
                {
                    parametro.Valor = string.Empty;
                    var aux = parametros.FirstOrDefault(x => x.Nombre.Equals(parametro.Nombre));
                    if (aux != null)
                    {
                        parametro.Valor = aux.Valor;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return parametrosFinales;
        }
    }
}
// End of Logica.cs
// End of Logica.cs
