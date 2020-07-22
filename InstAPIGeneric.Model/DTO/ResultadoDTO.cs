using System.Collections.Generic;

namespace InstAPIGeneric.Model.DTO
{
    /// <summary>  Clase que define la estructura de la respuesta generica </summary>
    public class ResultadoDTO
    {
        /// <summary> Contructor   </summary>
        public ResultadoDTO()
        {
            Valores = new List<object[]>();
        }

        /// <summary>  Contiene el valor del resultado a evaluado proveniente de BD </summary>
        public string Nombre { set; get; }

        /// <summary> Contiene los valores unicos de un proceso </summary>
        public object[] Valor { set; get; }

        /// <summary>  Contiene los valores listados de un proceso </summary>
        public List<object[]> Valores { set; get; }

        /// <summary>  Contiene respuesta 0 como valor correcto, los demás como casos de error </summary>
        public int Codigo { set; get; }

        /// <summary>  Especifica el resultado del proceso </summary>
        public string Mensaje { set; get; }
    }
}
