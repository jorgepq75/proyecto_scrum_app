using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace scrum_app.Models.epica
{
    public class EpicaModel
    {
        [Display(Name = "Id")]
        [Required(ErrorMessage = "Es un campo requedido")]
        public long id_epica { get; set; }

        [Display(Name = "Título")]
        [Required(ErrorMessage = "Es un campo requedido")]
        public string titulo { get; set; }

        [Display(Name = "Proyecto")]
        [Required(ErrorMessage = "Es un campo requedido")]
        public long fk_proyecto { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "Es un campo requedido")]
        public string descricion { get; set; }

        [Display(Name = "Creado por")]
        [Required(ErrorMessage = "Es un campo requedido")]
        public int fk_creado_por { get; set; }

        public Nullable<System.DateTime> fecha_creacion { get; set; }
    }
}