using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace scrum_app.Models.proyecto
{
    public class ProyectoModel
    {
        [Display(Name = "Id")]
        public long id_proyecto { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Es un campo requedido")]

        public string nombre { get; set; }

        [Display(Name = "Creado por")]
        [Required(ErrorMessage = "Es un campo requedido")]
        public int fk_creado_por { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "Es un campo requedido")]
        public string descripcion { get; set; }


        [Display(Name = "Fecha de creación")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public Nullable<System.DateTime> fecha_creacion { get; set; }

        [Display(Name = "Estado")]
        public int fk_estado_proyecto { get; set; }

        [Display(Name = "Fecha de finalización")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public Nullable<System.DateTime> fecha_cierre { get; set; }
    }
}