using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstAPIGeneric.Model.DTO
{
    public class ReglasMotorDecisiones
    {
        /// <summary> Contiene el identificador de la regla a consultar </summary>
        public int idRegla { set; get; }

        /// <summary>  Contiene el valor a evaluar y asignara para el proceso  </summary>
        public string valor { set; get; }
    }
}
