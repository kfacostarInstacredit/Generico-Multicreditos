using InstAPIGeneric.Model;
using System;

namespace InstAPIGeneric.BL
{
    /// <summary> Clase encargada de la bitacora</summary>
    public class ManejadorBitacora
    {
        /// <summary> Método encargado de obtener la secuencia de inicio de un proceso </summary>
        /// <returns>El marcador del proceso</returns>
        public static int ObtenerMarcador()
        {
            using (var context = new GenericsEntities())
            {
                var rawQuery = context.Database.SqlQuery<int>("SELECT NEXT VALUE FOR bitacora_seq;");
                var task = rawQuery.SingleAsync();
                int nextVal = task.Result;

                return nextVal;
            }
        }

        /// <summary> Método encargado de insertar un linea del proceso de bitacora </summary>
        /// <param name="idBitacora"> Id del proceso </param>
        /// <param name="usuario"> Id del usuario ejecutor</param>
        /// <param name="idConfiguracionProceso"> Id de la configuración disparada</param>
        /// <param name="mensaje"> Mensaje de lo sucedio.</param>
        /// <param name="excepcion">Excepcion de lo sucedido</param>
        /// <param name="parametros">Parametros de la funcion por donde va</param>
        /// <param name="funcion"> El metodo que se esta ejecutando</param>
        /// <param name="resultado">Resultado del metoo¿d</param>
        /// <param name="origen">De donde proviene la petición</param>
        public static void AgregarBitacora(int idBitacora, int usuario, int idConfiguracionProceso, string mensaje, string excepcion, string parametros, string funcion, string resultado, string origen)
        {
            using(var context = new GenericsEntities())
            {
                Bitacora bitacora = new Bitacora()
                {
                    IdBitacora = idBitacora,
                    FechaHora = DateTime.Now,
                    FechaCreacion = DateTime.Now,
                    Usuario = usuario,
                    Exception = excepcion,
                    Mensaje = mensaje,
                    Origen = origen,
                    Parametros = parametros,
                    Proceso = funcion,
                    Referencia = idConfiguracionProceso,
                    UsuarioCreador = usuario,
                    Resultado = resultado
                };

                context.Bitacora.Add(bitacora);

                context.SaveChanges();
                context.Dispose();
            }
        }

        internal static void AgregarBitacora(object r, int usuario, int idConfiguracionProceso, string v1, string v2, object p, string v3, string v4, string aplicacionOrigen)
        {
            throw new NotImplementedException();
        }
    }
}
