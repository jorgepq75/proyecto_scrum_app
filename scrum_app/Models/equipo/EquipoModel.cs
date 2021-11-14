using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace scrum_app.Models.equipo
{
    public class EquipoModel
    {
        [Display(Name = "Id")]
        public int id_equipo { get; set; }

        [Display(Name = "Proyecto")]
        [Required(ErrorMessage = "Es un campo requedido")]
        public long fk_proyecto { get; set; }

        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "Es un campo requedido")]
        public int fk_usuario { get; set; }
    }
}