using InstAPIGeneric.Model;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace InstAPIGeneric.BL.BD
{
    /// <summary>                                                                                                                                                                                                                                                                                                    Clase que contiene ls metodos de  ejecucion general de laaplicacion generica </summary>
    public class GenericoBD
    {
        /// <summary>  Obtiene una configuracion general </summary>
        /// <param name="idConfiguracionProceso">Id general de la configuracion a obtener</param>
        /// <returns>lista de pasos de la configuracion</returns>
        public List<Configuracion> ObtenerConfiguracion(int idConfiguracionProceso)
        {
            using (var context = new GenericsEntities())
            {
                var configuraciones = context.Configuracion.Where(x => x.IdConfiguracionProceso == idConfiguracionProceso && x.Activo.Value).OrderBy(x => x.Orden).ToList();

                context.Dispose();

                return configuraciones;
            }
        }
        ///<summary>Obtiene los parametros asociados a un procedimientoo o funcion </summary>                                                                                                                                                 
        /// <param name="idprocedimiento">id del procedimieno o funcion almacenada a ejecutar</param>
        /// <returns>lista de parametros asociados al procedimiento almacenado</returns>
        public List<Parametro> ObtenerParametros(long idprocedimiento)
        {
            using (var context = new GenericsEntities())
            {
                var r = context.Parametro.Where(x => x.Procedimiento_Funcion_Parametro.Any(y => y.IdProcedimientoFuncion == idprocedimiento)).ToList();
                context.Dispose();
                return r;
            }
        }

        /// <summary> Obtiene la informacion del procedimiento </summary>
        /// <param name="idprocedimiento">id del procedimieno o funcion almacenada a ejecutar</param>
        /// <returns>las caracteristicas del procedimiento o funcion</returns>
        public Catalogo_Procedimientos_Funciones ObtenerProcedimientoFuncion(long idprocedimeinto)
        {
            using (var context = new GenericsEntities())
            {
                var r = context.Catalogo_Procedimientos_Funciones.FirstOrDefault(x => x.IdProcedimientoFuncion == idprocedimeinto);
                context.Dispose();
                return r;
            }
        }

        /// <summary> Obtiene las conexiones </summary>
        /// <param name="idConexion">id de la conexion a utilizar para la ejecucion</param>
        /// <returns>caracteristicas de la conexion autilizar</returns>
        public Catalogo_Conexiones ObtenerConexion(int idConexion)
        {
            using (var context = new GenericsEntities())
            {
                var conexion = context.Catalogo_Conexiones.FirstOrDefault(x => x.IdConexion == idConexion);

                var tipoConexion = context.Tipos_Base_De_Datos.FirstOrDefault(x => x.IdTipoBD == conexion.IdTipoBD);
                var servidor = context.Servidor.FirstOrDefault(x => x.IdServidor == conexion.IdServidor);
                var baseDatos = context.Base_De_Datos.FirstOrDefault(x => x.IdBaseDatos == conexion.IdBaseDatos);

                baseDatos.Clave = Encriptacion.Desencriptar(baseDatos.Clave, ConfigurationManager.AppSettings["HashKey"]);

                conexion.ConnectionString = string.Format(tipoConexion.EstructuraConexion, servidor.IP, servidor.Puerto, servidor.Nombre, baseDatos.Nombre, baseDatos.Usuario, baseDatos.Clave).Replace(@":\", @"\");

                context.Dispose();

                return conexion;
            }
        }

        /// <summary>  Obtiene el tipo de conexion a ejecutar </summary>
        /// <param name="idConexion">id de la conexion a utilizar para la ejecucion</param>
        /// <returns>caracteristicas del tipo de conexion asociada a la conexion</returns>
        public Tipos_Base_De_Datos ObtenerTipoConexion(int idConexion)
        {
            using (var context = new GenericsEntities())
            {
                var r = context.Tipos_Base_De_Datos.FirstOrDefault(x => x.Catalogo_Conexiones.Any(y => y.IdConexion == idConexion));
                context.Dispose();
                return r;
            }
        }
    }
}