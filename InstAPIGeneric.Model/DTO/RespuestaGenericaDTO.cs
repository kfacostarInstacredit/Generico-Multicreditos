using System.Collections.Generic;

namespace InstAPIGeneric.Model.DTO
{
    /// <summary>    Clase que especicifca la respuesta generica de una configuracion </summary>
    public class resultadoFinalSolic
    {
        /// <summary>  Constructor  </summary>
        public resultadoFinalSolic()
        {
            Resultados = new List<ResultadoDTO>();
        }

        /// <summary> Especifica el codigo de la respuesta general del proceso 0 es el resultado correcto, los demas son incorrectos </summary>
        public int Codigo { set; get; }

        /// <summary>  Especifica el mensaje del resutado procesado  </summary>
        public string Mensaje { set; get; }

        /// <summary> Contiene el valor del id de configuracion del proceso  </summary>
        public int IdConfiguracionProceso { set; get; }

        /// <summary>  Especifica el resultado de los diferentes sub procesos que tiene la configuración </summary>
        public List<ResultadoDTO> Resultados { set; get; }
    }
}
