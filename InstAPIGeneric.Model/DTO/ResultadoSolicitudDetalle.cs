using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstAPIGeneric.Model.DTO
{
    public class ResultadoSolicitudDetalle
    {
        public ResultadoSolicitudDetalle()
        {
            Resultado = new ResultadoMensaje();
            Detalle = new DetalleResultadoSolicitud();
        }
        public ResultadoMensaje Resultado { get; set; }
        public DetalleResultadoSolicitud Detalle { get; set; }
    }
}
