using scrum_app.DB_Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace scrum_app.Models.usuario
{
    public class LoginModel
    {
        [Display(Name = "Correo electrónico")]
        [Required(ErrorMessage = "Correo electrónico es requerido")]
        public string email { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "Contraseña es requerido")]
        public string contrasena { get; set; }

        public static void createUserSession(sc_usuario user)
        {
            HttpContext.Current.Session["user"] = user;
        }

        public static sc_usuario getUserSession()
        {
            sc_usuario user = (sc_usuario)HttpContext.Current.Session["user"];
            return user;
        }

        public static bool validUser(string password, string password_hash)
        {
            //return password_hash == null ? false : Crypto.VerifyHashedPassword(password_hash, password);
            return true;
        }

        public static void closeUserSession()
        {
            HttpContext.Current.Session["user"] = null;
        }
    }

}