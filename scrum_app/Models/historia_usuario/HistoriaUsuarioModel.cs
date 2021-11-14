using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace scrum_app.Models.historia_usuario
{
    public class HistoriaUsuarioModel
    {
        [Display(Name = "Id")]
        [Required(ErrorMessage = "Es un campo requedido")]
        public long id_historia_usuario { get; set; }

        [Display(Name = "Título")]
        [Required(ErrorMessage = "Es un campo requedido")]
        public string titulo { get; set; }

        [Display(Name = "Proyecto")]
        [Required(ErrorMessage = "Es un campo requedido")]
        public long fk_proyecto { get; set; }

        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "Es un campo requedido")]
        public int fk_tipo { get; set; }

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "Es un campo requedido")]
        public string descricion { get; set; }

        [Display(Name = "Epica")]
        public Nullable<long> fk_epica { get; set; }
        public Nullable<int> fk_asignado_a { get; set; }

        [Display(Name = "Creado por")]
        [Required(ErrorMessage = "Es un campo requedido")]
        public int fk_creado_por { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "Es un campo requedido")]
        public int fk_estado_historia_usuario { get; set; }

        [Display(Name = "Fecha de creación")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> fecha_creacion { get; set; }
    }
}