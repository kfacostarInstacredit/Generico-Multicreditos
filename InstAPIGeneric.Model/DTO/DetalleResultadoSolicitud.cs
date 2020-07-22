using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstAPIGeneric.Model.DTO
{
    
    
    public class DetalleResultadoSolicitud
    {

        public DetalleResultadoSolicitud(){
            reglas = new List<ReglasResultadoSolicitud>();

            }
        public int idProducto { get; set; }
        public int idPais { get; set; }
        public int idEmpresa { get; set; }
        public int idPerfil { get; set; }
        public string numSolicBackend { get; set; }
        public string descPerfil { get; set; }
        public int idProceso { get; set; }
        public DateTime fechaAnalisis { get; set; }
        public string usuarioAnaliza { get; set; }
        public string tipoAnalisis { get; set; }
        public string pesoObtenido { get; set; }
        public decimal salario { get; set; }
        public decimal? montoFactor { get; set; }
        public decimal? montoEndeudamiento { get; set; }
        public string reglaObligatoria { get; set; }
        public int idResultadoAnalisis { get; set; }
        public string resultadoAnalisis { get; set; }


        public List<ReglasResultadoSolicitud> reglas { get; set; }
    }
}
