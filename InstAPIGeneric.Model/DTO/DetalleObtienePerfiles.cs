using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstAPIGeneric.Model.DTO
{
    public class DetalleObtienePerfiles
    {
        public int idPais { get; set; }
        public int idEmpresa { get; set; }
        public string usuarioAnaliza { get; set; }
        public int idProceso { get; set; }
        public List<Perfiles> perfiles { get; set; }
    }
}
