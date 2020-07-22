using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstAPIGeneric.Model.DTO
{
    public class Perfiles
    {
        public int idPerfil { set; get; }
        public string nombre { get; set; }
        public decimal salario { get; set; }
        public decimal? montoFactor { get; set; }
        public decimal? montoEndeudamiento { get; set; }
        public string estadoAnalisis { get; set; }
    }
}
