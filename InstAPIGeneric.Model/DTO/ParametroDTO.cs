namespace InstAPIGeneric.Model.DTO
{
    /// <summary> Clase que define la estructura de un parametro de entrada  </summary>
    public class ParametroDTO
    {
        /// <summary> Contiene el valor del nombre del paramtro de base de datos especiicado para un proceso </summary>
        public string Nombre { set; get; }

        /// <summary>  Contiene el valor a evaluar y asignara par el proceso  </summary>
        public string Valor { set; get; }
    }
}
