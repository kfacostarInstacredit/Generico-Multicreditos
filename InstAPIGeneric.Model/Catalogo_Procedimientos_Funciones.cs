//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InstAPIGeneric.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Catalogo_Procedimientos_Funciones
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Catalogo_Procedimientos_Funciones()
        {
            this.Configuracion = new HashSet<Configuracion>();
            this.Procedimiento_Funcion_Parametro = new HashSet<Procedimiento_Funcion_Parametro>();
        }
    
        public long IdProcedimientoFuncion { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string ValorMostar { get; set; }
        public Nullable<int> IdEstructura { get; set; }
        public Nullable<long> UsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public Nullable<long> UsuarioCreador { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
    
        public virtual Estructuras_De_Base_Datos Estructuras_De_Base_Datos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Configuracion> Configuracion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Procedimiento_Funcion_Parametro> Procedimiento_Funcion_Parametro { get; set; }
    }
}
