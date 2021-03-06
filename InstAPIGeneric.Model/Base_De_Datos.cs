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
    
    public partial class Base_De_Datos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Base_De_Datos()
        {
            this.Catalogo_Conexiones = new HashSet<Catalogo_Conexiones>();
        }
    
        public int IdBaseDatos { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public Nullable<long> IdEmpresa { get; set; }
        public Nullable<int> CodigoPais { get; set; }
        public Nullable<long> UsuarioModificacion { get; set; }
        public Nullable<System.DateTime> FechaModificacion { get; set; }
        public Nullable<long> UsuarioCreador { get; set; }
        public Nullable<System.DateTime> FechaCreacion { get; set; }
    
        public virtual Empresa_Pais Empresa_Pais { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Catalogo_Conexiones> Catalogo_Conexiones { get; set; }
    }
}
