using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstAPIGeneric.Model.DTO
{
    public class SolicitudAnalisisPerfil
    {
        public SolicitudGenericaDTO solicitudGenerico { get; set; }
        public SolicitudObtienePerfil SolicitudObtienePerfil { get; set; }
        public SolicitudAnalisisPerfil()
        {
            solicitudGenerico = new SolicitudGenericaDTO();
            SolicitudObtienePerfil = new SolicitudObtienePerfil();
        }
    }
}
