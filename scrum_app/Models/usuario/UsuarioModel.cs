using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace scrum_app.Models.usuario
{
    public class UsuarioModel
    {
        [Display(Name = "Id")]
        public int id_usuario { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Es un campo requedido")]
        public string nombre { get; set; }

        [Display(Name = "Correo electrónico")]
        [Required(ErrorMessage = "Es un campo requedido")]
        public string email { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "Es un campo requedido")]
        public string contrasena { get; set; }

        [Display(Name = "Rol")]
        [Required(ErrorMessage = "Es un campo requedido")]
        public int fk_rol { get; set; }

        public string createHashPassword()
        {
            return Crypto.HashPassword(this.contrasena);
        }


    }
}