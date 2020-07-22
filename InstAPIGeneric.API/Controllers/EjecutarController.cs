using InstAPIGeneric.BL;
using InstAPIGeneric.Model.DTO;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web;

namespace InstAPIGeneric.API.Controllers
{
    /// <summary>  Clase API </summary>
    [RoutePrefix("Generico")]
    public class EjecutarController : ApiController
    {
        /// <summary>  Método encargado de exponer el servicio para ejecutar una configuracion especifica </summary>
        /// <param name="solicitud">Parametros de la configuracion que deseo ejecutar</param>
        /// <returns>Resultado de la configuración</returns>
        [Route("Ejecutar")]
        [HttpPost]
        public resultadoFinalSolic EjecutarConfiguracion(SolicitudGenericaDTO solicitud)
        {
            resultadoFinalSolic resultado;
            var r = ManejadorBitacora.ObtenerMarcador();
            try
            {
                ManejadorBitacora.AgregarBitacora(r, solicitud.Usuario, solicitud.IdConfiguracionProceso, "Iniciando el proceso de ejecución de configuracion", "", JsonConvert.SerializeObject(solicitud), "EjecutarConfiguracion", "", solicitud.AplicacionOrigen);

                Logica logica = new Logica();
                resultado = logica.Ejecutar(solicitud, r);

                ManejadorBitacora.AgregarBitacora(r, solicitud.Usuario, solicitud.IdConfiguracionProceso, "Resultado del procesamiento", "", JsonConvert.SerializeObject(solicitud), "EjecutarConfiguracion", JsonConvert.SerializeObject(resultado), solicitud.AplicacionOrigen);

            }
            catch (Exception ex)
            {
                resultado = new resultadoFinalSolic() { Codigo = 1, Mensaje = ex.Message };
                ManejadorBitacora.AgregarBitacora(r, solicitud.Usuario, solicitud.IdConfiguracionProceso, "Cae el servicio", JsonConvert.SerializeObject(resultado), JsonConvert.SerializeObject(solicitud), "EjecutarConfiguracion", "", solicitud.AplicacionOrigen);

            }

            return resultado;
        }
        /// <summary>
        /// Es el mismo ejecutar solamente que no recibe parametros directamente, sino que se toman del Request
        /// </summary>
        /// <returns></returns>
        [Route("Ejecutar2")]
        [HttpPost]
        public resultadoFinalSolic EjecutarConfiguracion2()
        {
            resultadoFinalSolic resultado = new resultadoFinalSolic() ;
            SolicitudGenericaDTO solicitud = new SolicitudGenericaDTO();
            var r = ManejadorBitacora.ObtenerMarcador();
            try
            {
                /*
                form.append("IdConfiguracionProceso", "5");
                form.append("AplicacionOrigen", "postman");
                form.append("Usuario", "0");
                form.append("idUsuarioAsign", sessionStorage.getItem("IdUsuario"));
                form.append("idEstadoAsign", "2");
                */
                var IdConfiguracionProceso = HttpContext.Current.Request.Form["IdConfiguracionProceso"];
                var AplicacionOrigen = HttpContext.Current.Request.Form["AplicacionOrigen"];
                var Usuario = HttpContext.Current.Request.Form["Usuario"];
                var idUsuarioAsign = HttpContext.Current.Request.Form["idUsuarioAsign"];
                var idEstadoAsign = HttpContext.Current.Request.Form["idEstadoAsign"];

                solicitud.AplicacionOrigen = AplicacionOrigen;
                solicitud.IdConfiguracionProceso = int.Parse(IdConfiguracionProceso);
                solicitud.Usuario = int.Parse(Usuario);
                ParametroDTO p = new ParametroDTO
                {
                    Nombre = "idUsuarioAsign",
                    Valor = idUsuarioAsign
                };
                solicitud.Parametros.Add(p);
                p = new ParametroDTO
                {
                    Nombre = "idEstadoAsign",
                    Valor = idEstadoAsign
                };
                solicitud.Parametros.Add(p);

                ManejadorBitacora.AgregarBitacora(r, solicitud.Usuario, solicitud.IdConfiguracionProceso, "Iniciando el proceso de ejecución de configuracion", "", JsonConvert.SerializeObject(solicitud), "EjecutarConfiguracion", "", solicitud.AplicacionOrigen);

                Logica logica = new Logica();
                resultado = logica.Ejecutar(solicitud, r);

                ManejadorBitacora.AgregarBitacora(r, solicitud.Usuario, solicitud.IdConfiguracionProceso, "Resultado del procesamiento", "", JsonConvert.SerializeObject(solicitud), "EjecutarConfiguracion", JsonConvert.SerializeObject(resultado), solicitud.AplicacionOrigen);

            }
            catch (Exception ex)
            {
                resultado = new resultadoFinalSolic() { Codigo = 1, Mensaje = ex.Message };
                ManejadorBitacora.AgregarBitacora(r, solicitud.Usuario, solicitud.IdConfiguracionProceso, "Cae el servicio", JsonConvert.SerializeObject(resultado), JsonConvert.SerializeObject(solicitud), "EjecutarConfiguracion", "", solicitud.AplicacionOrigen);
            }

            return resultado;
        }


        public List<ReglasMotorDecisiones> ObtieneReglasMotorDecisiones( resultadoFinalSolic resultado, ResultadoSolicitudDetalle resultadoRemitePerfil, bool ObtieneReglasDecisiones) {
            List<ReglasMotorDecisiones> reglasDecisiones = new List<ReglasMotorDecisiones>();
            try
            {

                for (int i = 0; i < resultado.Resultados[0].Valores.Count; i++)
                {
                    for (int j = 1; j < resultado.Resultados.Count; j++)
                    {
                        //2.Hacer match de reglas, y obtener valor de la regla
                        if (resultado.Resultados[j].Nombre == resultado.Resultados[0].Valores[i][2].ToString())
                        {
                            //3.Agregar a la lista de reglas ya con el formato del modelo(idRegla, Valor)
                            ReglasMotorDecisiones regla = new ReglasMotorDecisiones();
                            regla.idRegla = (int)resultado.Resultados[0].Valores[i][2];
                            regla.valor = resultado.Resultados[j].Valor[0].ToString() == "" ? "0": resultado.Resultados[j].Valor[0].ToString();
                            if (ObtieneReglasDecisiones)
                            {
                                if (resultadoRemitePerfil.Detalle.reglas.Any(e => e.idRegla == regla.idRegla))
                                {
                                    reglasDecisiones.Add(regla);
                                }
                            }
                            else
                            {
                                reglasDecisiones.Add(regla);

                            }
                            break;
                        }
                    }
                }

                return reglasDecisiones;
            }
            catch (Exception)
            {

                return reglasDecisiones;
            }
        }

        /// <summary>  Método encargado de exponer el servicio para ejecutar una configuracion especifica y consultar el motor de decisiones </summary>
        /// <param name="solicitud">Parametros de la configuracion que deseo ejecutar</param>
        /// <returns>Resultado de la configuración</returns>
        [Route("EjecutarPerfil")]
        [HttpPost]
        public ResultadoSolicitudDetalle EjecutarPerfil(SolicitudAnalisis solicitud)
        {
            resultadoFinalSolic resultado;
            ResultadoSolicitudDetalle resultadoRemitePerfil =  new ResultadoSolicitudDetalle();
            RespuestaSolicitudMotorDecisiones resultEnviaSolic;
            ResultadoSolicitudDetalle resultadoFinalSolic;
            string salarioActual = "0";
            solicitud.solicitudGenerico.Parametros.Add(new ParametroDTO { Nombre = "idSolicitud", Valor = solicitud.solicitudMotorDecisiones.numSolicBackend.ToString() });
            var r = ManejadorBitacora.ObtenerMarcador();
            try
            {
                ManejadorBitacora.AgregarBitacora(r, solicitud.solicitudGenerico.Usuario, solicitud.solicitudGenerico.IdConfiguracionProceso, "Iniciando el proceso de ejecución de configuracion", "", JsonConvert.SerializeObject(solicitud), "EjecutarConfiguracion", "", solicitud.solicitudGenerico.AplicacionOrigen);

                Logica logica = new Logica();
                resultado = logica.Ejecutar(solicitud.solicitudGenerico, r);

                ManejadorBitacora.AgregarBitacora(r, solicitud.solicitudGenerico.Usuario, solicitud.solicitudGenerico.IdConfiguracionProceso, "Resultado del procesamiento", "", JsonConvert.SerializeObject(solicitud), "EjecutarConfiguracion", JsonConvert.SerializeObject(resultado), solicitud.solicitudGenerico.AplicacionOrigen);

                //Obtiene el macheo de las reglas, les da el formato correcto
                List<ReglasMotorDecisiones> reglasDecisiones = new List<ReglasMotorDecisiones>();
                reglasDecisiones = ObtieneReglasMotorDecisiones(resultado, resultadoRemitePerfil, false);
                ManejadorBitacora.AgregarBitacora(r, solicitud.solicitudGenerico.Usuario, solicitud.solicitudGenerico.IdConfiguracionProceso, "Asigna reglas ","", JsonConvert.SerializeObject(resultado) + "/" + JsonConvert.SerializeObject(resultadoRemitePerfil), "Obtener Datos", JsonConvert.SerializeObject(reglasDecisiones), solicitud.solicitudGenerico.AplicacionOrigen);
                /*jmejias, Obtiene el tipo de actividad laborar, esto con el salario que viene de parámetro de entrada , 20200606*/

                var vperfil = ConfigurationManager.AppSettings["vperfil"];
                var vtipolaboral = ConfigurationManager.AppSettings["vtipolaboral"];
                var vSolicitudInfoPerfil = ConfigurationManager.AppSettings["vSolicitudInfoPerfil"];

                ManejadorBitacora.AgregarBitacora(r, solicitud.solicitudGenerico.Usuario, solicitud.solicitudGenerico.IdConfiguracionProceso, "Asigna reglas ", "",$"ejecucion tipo laboral {vtipolaboral}", "Ejecutar", "", solicitud.solicitudGenerico.AplicacionOrigen);
                var resultadotipolaboral = logica.Ejecutar(new SolicitudGenericaDTO()
                {
                    IdConfiguracionProceso = Convert.ToInt32(vtipolaboral),
                    AplicacionOrigen = solicitud.solicitudGenerico.AplicacionOrigen,
                    Usuario = solicitud.solicitudGenerico.Usuario,
                    Parametros = new List<ParametroDTO>() { new ParametroDTO()
                    { Nombre = "salario", Valor = solicitud.solicitudMotorDecisiones.salario.ToString()} }
                }, r);
                
                /*jmejias, Obtiene el producto con base al rango de los ingresos minimos y máximos junto con el tipo de actividad laboral, 20200606*/
                salarioActual = solicitud.solicitudMotorDecisiones.salario.ToString() == "0" ? reglasDecisiones[2].valor : solicitud.solicitudMotorDecisiones.salario.ToString();
                ManejadorBitacora.AgregarBitacora(r, solicitud.solicitudGenerico.Usuario, solicitud.solicitudGenerico.IdConfiguracionProceso, "Asigna reglas ", "", $"ejecucion tipo laboral {vperfil} salario {salarioActual}", "Ejecutar", "", solicitud.solicitudGenerico.AplicacionOrigen);
                var resultadoperfil = logica.Ejecutar(new SolicitudGenericaDTO()
                {
                    IdConfiguracionProceso = Convert.ToInt32(vperfil),
                    AplicacionOrigen = solicitud.solicitudGenerico.AplicacionOrigen,
                    Usuario = solicitud.solicitudGenerico.Usuario,
                    Parametros = new List<ParametroDTO>() { new ParametroDTO()
                    { Nombre = "salario", Valor =  salarioActual.ToString()},
                        new ParametroDTO { Nombre = "idtipolaboral", Valor = resultadotipolaboral.Resultados[0].Valor[0].ToString() } }
                }, r);


                /*jmejias obtiene información de la solicitud, como la antiguedad seleccionada*/
                var resultadoSolicitudInfo = logica.Ejecutar(new SolicitudGenericaDTO()
                {
                    IdConfiguracionProceso = Convert.ToInt32(vSolicitudInfoPerfil),
                    AplicacionOrigen = solicitud.solicitudGenerico.AplicacionOrigen,
                    Usuario = solicitud.solicitudGenerico.Usuario,
                    Parametros = new List<ParametroDTO>() { new ParametroDTO()
                    { Nombre = "idSolicitud", Valor =  solicitud.solicitudMotorDecisiones.numSolicBackend.ToString()} }
                }, r);

                ManejadorBitacora.AgregarBitacora(r, solicitud.solicitudGenerico.Usuario, solicitud.solicitudGenerico.IdConfiguracionProceso, "Asigna reglas ", "", $"remite perfil", "Ejecutar", "", solicitud.solicitudGenerico.AplicacionOrigen);
                //Validar reglas de perfil
                using (var remitePerfil = new HttpClient())
                {
                    var url = ConfigurationManager.AppSettings["ApiMotor"];
                    url = string.Concat(url, "/remitePerfil");

                    remitePerfil.BaseAddress = new Uri(url);

                    remitePerfil.DefaultRequestHeaders.Accept.Clear();
                    remitePerfil.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    string requestJsonPerfil = JsonConvert.SerializeObject(new
                    {
                        idPais = solicitud.solicitudMotorDecisiones.idPais,
                        idEmpresa = solicitud.solicitudMotorDecisiones.idEmpresa,
                        origen = solicitud.solicitudGenerico.AplicacionOrigen,
                        idPerfil = resultadoperfil.Resultados[0].Valor[1]//solicitud.solicitudMotorDecisiones.idPerfil
                    });

                    HttpRequestMessage requestPerfil = new HttpRequestMessage();

                    var httpContent = new StringContent(requestJsonPerfil, Encoding.UTF8, "application/json");
                    requestPerfil.Content = httpContent;

                    //Consumir servicio de remite perfil
                    HttpResponseMessage response = remitePerfil.PostAsync(url, requestPerfil.Content).Result;

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        resultadoRemitePerfil = JsonConvert.DeserializeObject<ResultadoSolicitudDetalle>(result);
                    }
                    else
                    {
                        return resultadoFinalSolic = new ResultadoSolicitudDetalle() { Resultado = { Estado = false, Mensaje = response.Content.ReadAsStringAsync().Result } };
                    }

                    if (resultadoRemitePerfil.Detalle.reglas == null)
                    {
                        resultadoFinalSolic = new ResultadoSolicitudDetalle();
                        resultadoFinalSolic.Detalle = null;
                        resultadoFinalSolic.Resultado = new ResultadoMensaje();
                        resultadoFinalSolic.Resultado.Estado = false;
                        resultadoFinalSolic.Resultado.Mensaje = response.Content.ReadAsStringAsync().Result;
                        return resultadoFinalSolic;
                    }

                }
     
                //jmejias, macheo las reglas nuevamente, pero esta vez se utilizan unicamente las del perfil en especifico
                reglasDecisiones = ObtieneReglasMotorDecisiones(resultado, resultadoRemitePerfil, true);
                ManejadorBitacora.AgregarBitacora(r, solicitud.solicitudGenerico.Usuario, solicitud.solicitudGenerico.IdConfiguracionProceso, "Asigna reglas ","", JsonConvert.SerializeObject(resultado) + "/" + JsonConvert.SerializeObject(resultadoRemitePerfil), "Obtener Datos", JsonConvert.SerializeObject(reglasDecisiones), solicitud.solicitudGenerico.AplicacionOrigen);



                if (Convert.ToInt32(resultadotipolaboral.Resultados[0].Valor[0]) == 2)
                {
                    foreach (var regla in reglasDecisiones)
                    {
                        regla.valor = regla.idRegla == solicitud.ReglaEstabilidadManual ? resultadoSolicitudInfo.Resultados[0].Valor[0].ToString() : regla.valor.ToString();
                        regla.valor = regla.idRegla == solicitud.ReglaIngresoManual ? resultadoSolicitudInfo.Resultados[0].Valor[1].ToString() : regla.valor.ToString();
                        //regla.valor = regla.idRegla == 106 ? .ToString() : regla.valor.ToString();
                    }
                }
                using (var solicitudMotor = new HttpClient())
                {
                    // Establecer la url que proporciona acceso al servidor que publica la API 
                    solicitudMotor.BaseAddress = new Uri(solicitud.urlMotorDecisionesAPI);

                    // Configurar encabezados para que la petición de realice en formato JSON
                    solicitudMotor.DefaultRequestHeaders.Accept.Clear();
                    solicitudMotor.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Obtener el texto JSON con los datos de las reglas
                    string requestJson = JsonConvert.SerializeObject(new
                    {
                        idPais = solicitud.solicitudMotorDecisiones.idPais,
                        idEmpresa = solicitud.solicitudMotorDecisiones.idEmpresa,
                        idPerfil = resultadoperfil.Resultados[0].Valor[1],//solicitud.solicitudMotorDecisiones.idPerfil,
                        origen = solicitud.solicitudGenerico.AplicacionOrigen,
                        numSolicBackend = solicitud.solicitudMotorDecisiones.numSolicBackend,
                        usuarioAnaliza = solicitud.solicitudMotorDecisiones.usuarioAnalisa,
                        idCliente = solicitud.solicitudMotorDecisiones.idCliente,
                        nombreCliente = solicitud.solicitudMotorDecisiones.nombreCliente,
                        salario = 0 ,//Math.Round(Convert.ToDecimal(resultadoperfil.Resultados[0].Valor[0]),0) ,//solicitud.solicitudMotorDecisiones.salario,
                        reglas = reglasDecisiones
                    });

                    HttpRequestMessage request = new HttpRequestMessage();

                    var httpContent = new StringContent(requestJson, Encoding.UTF8, "application/json");
                    request.Content = httpContent;

                    //4.Consumir servicio de motor de decisiones(EnviaSolicitud)
                    HttpResponseMessage response = solicitudMotor.PostAsync(solicitud.urlMotorDecisionesAPI, request.Content).Result;

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        resultEnviaSolic = JsonConvert.DeserializeObject<RespuestaSolicitudMotorDecisiones>(result);
                        resultEnviaSolic.Detalle.origen = solicitud.solicitudGenerico.AplicacionOrigen;
                    }
                    else
                    {
                        resultadoFinalSolic = new ResultadoSolicitudDetalle();
                        resultadoFinalSolic.Detalle = null;
                        resultadoFinalSolic.Resultado = new ResultadoMensaje();
                        resultadoFinalSolic.Resultado.Estado = false;
                        resultadoFinalSolic.Resultado.Mensaje = response.Content.ReadAsStringAsync().Result;
                        return resultadoFinalSolic;
                    }

                }

                //5.Consumir servicio de motor de decisiones(consultaResultadoSolicitud) con la respuesta anterior
                using (var solicitudResultadoMotor = new HttpClient())
                {
                    // Establecer la url que proporciona acceso al servidor que publica la API 
                    var url = ConfigurationManager.AppSettings["ApiMotor"];
                    url = string.Concat(url, "/consultaResultadoSolicitud");

                    solicitudResultadoMotor.BaseAddress = new Uri(url);

                    // Configurar encabezados para que la petición de realice en formato JSON
                    solicitudResultadoMotor.DefaultRequestHeaders.Accept.Clear();
                    solicitudResultadoMotor.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    string requestJsonResult = JsonConvert.SerializeObject(resultEnviaSolic.Detalle);


                    HttpRequestMessage requestResult = new HttpRequestMessage();

                    var httpContent = new StringContent(requestJsonResult, Encoding.UTF8, "application/json");
                    requestResult.Content = httpContent;

                    HttpResponseMessage response = solicitudResultadoMotor.PostAsync(url, requestResult.Content).Result;

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        resultadoFinalSolic = JsonConvert.DeserializeObject<ResultadoSolicitudDetalle>(result);
                        //resultadoperfil.Resultados[0].Valor[0] = solicitud.MontoSolicitar; //seteamos el monto que se ingresó desde la interfaz
                        resultadoFinalSolic.Detalle.montoEndeudamiento = solicitud.MontoSolicitar == "0" ? Convert.ToDecimal(resultadoperfil.Resultados[0].Valor[0].ToString()) : Convert.ToDecimal(solicitud.MontoSolicitar);
                        resultadoFinalSolic.Detalle.idProducto = Convert.ToInt32(resultadoperfil.Resultados[0].Valor[2].ToString());
                    }
                    else
                    {
                        resultadoFinalSolic = new ResultadoSolicitudDetalle();
                        resultadoFinalSolic.Detalle = null;
                        resultadoFinalSolic.Resultado = new ResultadoMensaje();
                        resultadoFinalSolic.Resultado.Estado = false;
                        resultadoFinalSolic.Resultado.Mensaje = response.Content.ReadAsStringAsync().Result;
                        return resultadoFinalSolic;
                    }

                }

                //// Realizar la petición GET
                ////HttpResponseMessage response = solicitudMotor.GetAsync("api/auth/login?JsonLoginRequest=" + HttpUtility.UrlEncode(request)).Result;
                //HttpResponseMessage response = solicitudMotor.PostAsJsonAsync(solicitud.urlMotorDecisionesAPI, request).Result;
                //if (response.IsSuccessStatusCode)
                //{
                //    /*
                //      5. Consumir servicio de motor de decisiones (consultaResultadoSolicitud) con la respuesta anterior
                //   */
                //    // Obtener el resultado como objeto dynamic 
                //    var result = response.Content.ReadAsAsync<dynamic>().Result;
                //    Console.WriteLine("ReturnCode: {0}", result.Returncode);
                //    Console.WriteLine("Message: {0}", result.Message);
                //    Console.WriteLine("Token: {0}", result.Token);
                //}

            }
            catch (Exception ex)
            {

                resultadoFinalSolic = new ResultadoSolicitudDetalle() { Resultado = { Estado = false, Mensaje = ex.Message } };
                ManejadorBitacora.AgregarBitacora(r, solicitud.solicitudGenerico.Usuario, solicitud.solicitudGenerico.IdConfiguracionProceso, "Cae el servicio", JsonConvert.SerializeObject(resultadoFinalSolic), JsonConvert.SerializeObject(solicitud), "EjecutarConfiguracion", "", solicitud.solicitudGenerico.AplicacionOrigen);

            }

            return resultadoFinalSolic;
        }



        /// <summary> Métoo encargado de encriptar un valor</summary>
        /// <param name="solicitud">Valor a encriptar</param>
        /// <returns>Valor encriptado</returns>
        [Route("Encriptar")]
        [HttpPost]
        public string Encriptar(SolicitudGenericaDTO solicitud)
        {
            var r = ManejadorBitacora.ObtenerMarcador();
            try
            {
                ManejadorBitacora.AgregarBitacora(r, solicitud.Usuario, solicitud.IdConfiguracionProceso, "Iniciando el proceso de encriptación", "", JsonConvert.SerializeObject(solicitud), "Encriptar", "", solicitud.AplicacionOrigen);

                var texto = solicitud.Parametros.FirstOrDefault();

                ManejadorBitacora.AgregarBitacora(r, solicitud.Usuario, solicitud.IdConfiguracionProceso, "Obtiene el valor a encriptar", "", JsonConvert.SerializeObject(solicitud), "Encriptar", "", solicitud.AplicacionOrigen);

                var respuesta = Encriptacion.Encriptar(texto.Valor, ConfigurationManager.AppSettings["HashKey"]);
                var respuesta2 = Encriptacion.Desencriptar(texto.Nombre, ConfigurationManager.AppSettings["HashKey"]);

                ManejadorBitacora.AgregarBitacora(r, solicitud.Usuario, solicitud.IdConfiguracionProceso, "valor enciptado", "", JsonConvert.SerializeObject(solicitud), "Encriptar", respuesta, solicitud.AplicacionOrigen);

                return respuesta;
            }
            catch (Exception ex)
            {
                ManejadorBitacora.AgregarBitacora(r, solicitud.Usuario, solicitud.IdConfiguracionProceso, "valor enciptado", ex.Message, JsonConvert.SerializeObject(solicitud), "Encriptar", "", solicitud.AplicacionOrigen);
                return ex.Message;
            }
        }


        /// <summary>  Método encargado de exponer el servicio para ejecutar una configuracion especifica y obtener perfiles</summary>
        /// <param name="solicitud">Parametros de la configuracion que deseo ejecutar</param>
        /// <returns>Resultado de obtener perfiles</returns>
        [Route("ObtenerPerfil")]
        [HttpPost]
        public ResultadoObtienePerfiles ObtenerPerfil(SolicitudAnalisisPerfil solicitud)
        {
            resultadoFinalSolic resultado;
            ResultadoObtienePerfiles resultadoObtienePerfiles;

            var r = ManejadorBitacora.ObtenerMarcador();
            try
            {
                ManejadorBitacora.AgregarBitacora(r, solicitud.solicitudGenerico.Usuario, solicitud.solicitudGenerico.IdConfiguracionProceso, "Iniciando el proceso de ejecución de configuracion", "", JsonConvert.SerializeObject(solicitud), "EjecutarConfiguracion", "", solicitud.solicitudGenerico.AplicacionOrigen);

                Logica logica = new Logica();
                resultado = logica.Ejecutar(solicitud.solicitudGenerico, r);

                ManejadorBitacora.AgregarBitacora(r, solicitud.solicitudGenerico.Usuario, solicitud.solicitudGenerico.IdConfiguracionProceso, "Resultado del procesamiento", "", JsonConvert.SerializeObject(solicitud), "EjecutarConfiguracion", JsonConvert.SerializeObject(resultado), solicitud.solicitudGenerico.AplicacionOrigen);

                //1. Recorrer la posición 0 que tiene e objeto resultado.
                List<ReglasMotorDecisiones> reglasDecisiones = new List<ReglasMotorDecisiones>();

                for (int i = 0; i < resultado.Resultados[0].Valores.Count; i++)
                {
                    for (int j = 0; j < resultado.Resultados.Count; j++)
                    {
                        //2.Hacer match de reglas, y obtener valor de la regla
                        if (resultado.Resultados[j].Nombre == resultado.Resultados[0].Valores[i].FirstOrDefault().ToString())
                        {
                            //3.Agregar a la lista de reglas ya con el formato del modelo(idRegla, Valor)
                            ReglasMotorDecisiones regla = new ReglasMotorDecisiones();
                            regla.idRegla = (int)resultado.Resultados[0].Valores[i].FirstOrDefault();

                            bool isNum;
                            double retNum;
                            isNum = Double.TryParse(resultado.Resultados[j].Valor[0].ToString(), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
                            if (resultado.Resultados[0].Valores[i][4].ToString() == "N" && isNum == false)
                            {
                                regla.valor = "0";
                            }
                            else
                            {
                                regla.valor = resultado.Resultados[j].Valor[0].ToString();
                            }
                            reglasDecisiones.Add(regla);
                            break;
                        }
                    }
                }

                while (reglasDecisiones.Count < 4)
                {
                    ReglasMotorDecisiones regla = new ReglasMotorDecisiones();
                    regla.idRegla = reglasDecisiones.Count + 1;
                    regla.valor = "0";
                    reglasDecisiones.Add(regla);
                }

                //Obtener perfiles
                using (var ObtienePerfil = new HttpClient())
                {
                    var url = ConfigurationManager.AppSettings["ApiMotor"];
                    url = string.Concat(url, "/obtienePerfiles");

                    ObtienePerfil.BaseAddress = new Uri(url);

                    ObtienePerfil.DefaultRequestHeaders.Accept.Clear();
                    ObtienePerfil.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    string requestJsonPerfil = JsonConvert.SerializeObject(new
                    {
                        idPais = solicitud.SolicitudObtienePerfil.idPais,
                        idEmpresa = solicitud.SolicitudObtienePerfil.idEmpresa,
                        usuarioAnaliza = solicitud.SolicitudObtienePerfil.usuarioAnaliza,
                        tipoAnalisis = solicitud.SolicitudObtienePerfil.tipoAnalisis,
                        origen = solicitud.solicitudGenerico.AplicacionOrigen,
                        estado = solicitud.SolicitudObtienePerfil.estado,
                        reglas = reglasDecisiones
                    });

                    HttpRequestMessage requestPerfil = new HttpRequestMessage();

                    var httpContent = new StringContent(requestJsonPerfil, Encoding.UTF8, "application/json");
                    requestPerfil.Content = httpContent;

                    //Consumir servicio de remite perfil
                    HttpResponseMessage response = ObtienePerfil.PostAsync(url, requestPerfil.Content).Result;

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        resultadoObtienePerfiles = JsonConvert.DeserializeObject<ResultadoObtienePerfiles>(result);
                    }
                    else
                    {
                        return resultadoObtienePerfiles = new ResultadoObtienePerfiles() { Resultado = { Estado = false, Mensaje = response.Content.ReadAsStringAsync().Result } };
                    }

                    if (resultadoObtienePerfiles.Detalle.perfiles == null)
                    {
                        resultadoObtienePerfiles = new ResultadoObtienePerfiles();
                        resultadoObtienePerfiles.Detalle = null;
                        resultadoObtienePerfiles.Resultado = new ResultadoMensaje();
                        resultadoObtienePerfiles.Resultado.Estado = false;
                        resultadoObtienePerfiles.Resultado.Mensaje = response.Content.ReadAsStringAsync().Result;
                        return resultadoObtienePerfiles;
                    }

                }


            }
            catch (Exception ex)
            {
                resultadoObtienePerfiles = new ResultadoObtienePerfiles() { Resultado = { Estado = false, Mensaje = ex.Message } };
                ManejadorBitacora.AgregarBitacora(r, solicitud.solicitudGenerico.Usuario, solicitud.solicitudGenerico.IdConfiguracionProceso, "Cae el servicio", JsonConvert.SerializeObject(resultadoObtienePerfiles), JsonConvert.SerializeObject(solicitud), "ObtenerPerfil", "", solicitud.solicitudGenerico.AplicacionOrigen);

            }

            return resultadoObtienePerfiles;
        }
    }
}
