using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstAPIGeneric.Model.DTO
{
    public class DetalleResultadoMotor
    {
        public int idPais { get; set; }
        public int idEmpresa { get; set; }
        public int idPerfil { get; set; }
        public string numSolicBackEnd { get; set;}
        public int idProceso { get; set; }
        public string origen { get; set; }
    }
}
