using System.Collections.Generic;

namespace InstAPIGeneric.Model.DTO
{
    /// <summary> Detalla la estructura e la solicitud </summary>
    public class SolicitudGenericaDTO
    { 
        /// <summary> Contructor de la clase </summary>
        public SolicitudGenericaDTO() {
            Parametros = new List<ParametroDTO>();
        }

        /// <summary>  Continene el id de la configuración del proceso especificada en la bd  </summary>
        public int IdConfiguracionProceso { set; get; }

        /// <summary> Contiene la aplciación de donde se hizo la patición al servicio </summary>
        public string AplicacionOrigen { set; get; }

        /// <summary> Contiene el id del usuario que realiza la acción</summary>
        public int Usuario { set; get; }

        /// <summary> Contiene la estructura de los diferentes parametros a utilizar en la configuracion </summary>
        public List<ParametroDTO> Parametros { set; get; }
    }
}
