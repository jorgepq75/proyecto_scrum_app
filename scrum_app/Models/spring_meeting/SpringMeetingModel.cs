using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace scrum_app.Models.spring_meeting
{
    public class SpringMeetingModel
    {
        [Display(Name = "Id")]
        [Required(ErrorMessage = "Es un campo requedido")]
        public int id_spring_meeting { get; set; }

        [Display(Name = "Spring")]
        [Required(ErrorMessage = "Es un campo requedido")]
        public long fk_spring { get; set; }

        [Display(Name = "Reunión")]
        [Required(ErrorMessage = "Es un campo requedido")]
        public int fk_spring_meeting_type { get; set; }

        [Display(Name = "Creado por")]
        [Required(ErrorMessage = "Es un campo requedido")]
        public int fk_creado_por { get; set; }

        [Display(Name = "Comentario")]
        [Required(ErrorMessage = "Es un campo requedido")]
        public string comment { get; set; }
    }
}