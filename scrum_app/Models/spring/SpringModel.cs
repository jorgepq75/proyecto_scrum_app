using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace scrum_app.Models.spring
{
    public class SpringModel
    {
        [Display(Name = "Id")]
        public long id_spring { get; set; }

        [Display(Name = "Sprint")]
        [Required(ErrorMessage = "Es un campo requedido")]
        public string nombre { get; set; }

        [Display(Name = "Creado por")]
        [Required(ErrorMessage = "Es un campo requedido")]
        public int fk_creado_por { get; set; }


        [Display(Name = "Proyecto")]
        [Required(ErrorMessage = "Es un campo requedido")]
        public long fk_proyecto { get; set; }


        [Display(Name = "Estado")]
        [Required(ErrorMessage = "Es un campo requedido")]
        public int fk_estado_spring { get; set; }

        [Display(Name = "Fecha de creación")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> fecha_creacion { get; set; }
    }
}