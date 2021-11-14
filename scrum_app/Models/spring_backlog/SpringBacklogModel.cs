using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace scrum_app.Models.spring_backlog
{
    public class SpringBacklogModel
    {

        [Display(Name = "Id")]
        [Required(ErrorMessage = "Es un campo requedido")]
        public int id_spring_backlog { get; set; }

        [Display(Name = "Spring")]
        [Required(ErrorMessage = "Es un campo requedido")]
        public long fk_spring { get; set; }

        [Display(Name = "Historia de usuario")]
        [Required(ErrorMessage = "Es un campo requedido")]
        public long fk_historia_usuario { get; set; }
    }
}