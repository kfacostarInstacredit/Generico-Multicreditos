using System.Collections.Generic;

namespace InstAPIGeneric.Model.DTO
{
    /// <summary> Detalla la estructura e la solicitud </summary>
    public class SolicitudMotorDecisiones
    {
        /// <summary> Contructor de la clase </summary>
        public SolicitudMotorDecisiones()
        {

        }

        /// <summary> Contiene el identificador del pais  </summary>
        public int idPais { set; get; }

        /// <summary> Contiene el identificador de la empresa</summary>
        public int idEmpresa { set; get; }

        /// <summary> Contiene el perfil que se va a utilizar en el analsis de la solicitud</summary>
        public int idPerfil { set; get; }



        /// <summary> Contiene el número de la solicitud generada en Genesis </summary>
        public int numSolicBackend { set; get; }



        /// <summary> Contiene el usuario que realiza la solicitud de analisis </summary>
        public string usuarioAnalisa { set; get; }


        /// <summary> Contiene la cédula o identificador del cliente </summary>
        public string idCliente { set; get; }


        /// <summary> Contiene el nombre del cliente que genera la solicitud</summary>
        public string nombreCliente { set; get; }

        /// <summary> Contiene salario del cliente </summary>
        public double salario { set; get; }

        /// <summary> Contiene la estructura de los diferentes parametros a utilizar en las reglas </summary>
        public List<ReglasMotorDecisiones> Reglas { set; get; }
    }
}
