using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstAPIGeneric.Model.DTO
{
    public class ReglasResultadoSolicitud
    {
        public int idRegla { set; get; }
        public string nombre { get; set; }
        public string tipoRegla { get; set; }
        public string valorAnalizar { get; set; }
        public int idResultadoAnalisis { get; set; }
        public string resultadoAnalisis { get; set; }
        public string valorReferencia { get; set; }
        public string leyendaAnalisis { get; set; }
        public string pesoReferencia { get; set; }
        public string pesoObtenido { get; set; }
    }
}
