using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using scrum_app.DB_Model;
using scrum_app.Filters;
using scrum_app.Models.usuario;

namespace scrum_app.Controllers.usuario
{
    [VerifyUserSession]
    public class UsuarioController : Controller
    {
        private proyecto_scrumEntities db = new proyecto_scrumEntities();

        // GET: Usuario
        public ActionResult Index()
        {
            var sc_usuario = db.sc_usuario.Include(s => s.sc_rol);
            return View(sc_usuario.ToList());
        }

        // GET: Usuario/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sc_usuario sc_usuario = db.sc_usuario.Find(id);
            if (sc_usuario == null)
            {
                return HttpNotFound();
            }
            return View(sc_usuario);
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            ViewBag.fk_rol = new SelectList(db.sc_rol, "id_rol", "rol");
            return View();
        }

        // POST: Usuario/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UsuarioModel usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    sc_usuario u = new sc_usuario()
                    {
                        nombre = usuario.nombre,
                        email = usuario.email,
                        contrasena = usuario.contrasena,
                        fecha_creacion = DateTime.Now,
                        fk_rol = usuario.fk_rol
                    };
                    db.sc_usuario.Add(u);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    
                    ModelState.AddModelError("email", "Ya existe un usuario con el mismo email");
                    ViewBag.fk_rol = new SelectList(db.sc_rol, "id_rol", "rol", usuario.fk_rol);
                    return View(usuario);
                }

               
                return RedirectToAction("Index");
            }

            ViewBag.fk_rol = new SelectList(db.sc_rol, "id_rol", "rol", usuario.fk_rol);
            return View(usuario);
        }

        // GET: Usuario/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sc_usuario sc_usuario = db.sc_usuario.Find(id);
            if (sc_usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.fk_rol = new SelectList(db.sc_rol, "id_rol", "rol", sc_usuario.fk_rol);
            return View(sc_usuario);
        }

        // POST: Usuario/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_usuario,nombre,email,contrasena,fk_rol,fecha_creacion")] sc_usuario sc_usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sc_usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fk_rol = new SelectList(db.sc_rol, "id_rol", "rol", sc_usuario.fk_rol);
            return View(sc_usuario);
        }

        // GET: Usuario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sc_usuario sc_usuario = db.sc_usuario.Find(id);
            if (sc_usuario == null)
            {
                return HttpNotFound();
            }
            return View(sc_usuario);
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            sc_usuario sc_usuario = db.sc_usuario.Find(id);
            db.sc_usuario.Remove(sc_usuario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
