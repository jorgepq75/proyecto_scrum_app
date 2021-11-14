using scrum_app.DB_Model;
using scrum_app.Models.usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace scrum_app.Controllers.usuario
{
    public class LoginController : Controller
    {
        private proyecto_scrumEntities db = new proyecto_scrumEntities();

       // [VerifyUserSession(true, null)]//no check user session
        public ActionResult LoginForm()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[VerifyUserSession(true, null)]
        public ActionResult LoginForm(LoginModel login)
        {
            if (ModelState.IsValid)
            {
                sc_usuario userRepo= db.sc_usuario.Where(c=>c.email==login.email && c.contrasena==login.contrasena).FirstOrDefault();
                if (userRepo != null)
                {
                    if (LoginModel.validUser(login.contrasena, userRepo.contrasena))
                    {
                        LoginModel.createUserSession(userRepo);
                        return RedirectToAction("Index", "Proyecto");
                    }
                    else
                    {
                        ModelState.AddModelError("contrasena", "Correo electrónico o contraseña incorrectos");
                    }
                }
                else
                {
                    ModelState.AddModelError("contrasena", "Correo electrónico o contraseña incorrectos");
                }


            }

            return View();
        }

        public ActionResult closeSession()
        {

            HttpContext.Session.Remove("proyecto");//remove proyecto session
            LoginModel.closeUserSession();
            return RedirectToAction("LoginForm", "Login");
        }

    }
}
