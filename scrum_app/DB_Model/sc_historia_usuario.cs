//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace scrum_app.DB_Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class sc_historia_usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public sc_historia_usuario()
        {
            this.sc_spring_backlog = new HashSet<sc_spring_backlog>();
        }
    
        public long id_historia_usuario { get; set; }
        public string titulo { get; set; }
        public string descricion { get; set; }
        public Nullable<long> fk_epica { get; set; }
        public Nullable<int> fk_asignado_a { get; set; }
        public int fk_creado_por { get; set; }
        public int fk_estado_historia_usuario { get; set; }
        public Nullable<System.DateTime> fecha_creacion { get; set; }
    
        public virtual sc_epica sc_epica { get; set; }
        public virtual sc_estado_historia_usuario sc_estado_historia_usuario { get; set; }
        public virtual sc_usuario sc_usuario { get; set; }
        public virtual sc_usuario sc_usuario1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sc_spring_backlog> sc_spring_backlog { get; set; }
    }
}
