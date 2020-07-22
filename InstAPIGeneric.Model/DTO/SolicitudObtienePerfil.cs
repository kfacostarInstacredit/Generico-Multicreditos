using System.Collections.Generic;

namespace InstAPIGeneric.Model.DTO
{
    /// <summary> Detalla la estructura e la solicitud </summary>
    public class SolicitudObtienePerfil
    {
        /// <summary> Contructor de la clase </summary>
        public SolicitudObtienePerfil()
        {

        }

        /// <summary> Contiene el identificador del pais  </summary>
        public int idPais { set; get; }

        /// <summary> Contiene el identificador de la empresa</summary>
        public int idEmpresa { set; get; }

        /// <summary> Contiene el usuario que realiza la solicitud de analisis </summary>
        public string usuarioAnaliza { set; get; }

        /// <summary> Contiene el tipo de analisis </summary>
        public string tipoAnalisis { set; get; }

        /// <summary> Contiene el origen </summary>
        public string origen { set; get; }

        /// <summary> Contiene el estado</summary>
        public int estado { set; get; }

        /// <summary> Contiene la estructura de los diferentes parametros a utilizar en las reglas </summary>
        public List<ReglasMotorDecisiones> reglas { set; get; }
    }
}
