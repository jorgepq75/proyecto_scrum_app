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
    
    public partial class sc_spring_meeting
    {
        public int id_spring_meeting { get; set; }
        public long fk_spring { get; set; }
        public int fk_spring_meeting_type { get; set; }
        public Nullable<System.DateTime> fecha_creacion { get; set; }
        public int fk_creado_por { get; set; }
        public string comment { get; set; }
    
        public virtual sc_spring sc_spring { get; set; }
        public virtual sc_usuario sc_usuario { get; set; }
        public virtual sc_spring_meeting_type sc_spring_meeting_type { get; set; }
    }
}
