using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstAPIGeneric.Model.DTO
{
    public class RespuestaSolicitudMotorDecisiones
    {
        public ResultadoMensaje Resultado { get; set; }
        public DetalleResultadoMotor Detalle { get; set; }

    }
}
