using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstAPIGeneric.Model.DTO
{
    public class SolicitudAnalisis
    {
        public SolicitudGenericaDTO solicitudGenerico { get; set; }
        public SolicitudMotorDecisiones solicitudMotorDecisiones { get; set; }
        public string urlMotorDecisionesAPI { get; set; }

        public int ReglaIngresoManual { get; set; }

        public int ReglaEstabilidadManual { get; set; }

        public string MontoSolicitar{ get; set; }

        public SolicitudAnalisis()
        {
            solicitudGenerico = new SolicitudGenericaDTO();
            solicitudMotorDecisiones = new SolicitudMotorDecisiones();
        }
    }
}
