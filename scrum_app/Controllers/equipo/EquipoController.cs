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
using scrum_app.Models.equipo;
using scrum_app.Models.proyecto;

namespace scrum_app.Controllers.equipo
{
    [VerifyUserSession]
    public class EquipoController : Controller
    {
        private proyecto_scrumEntities db = new proyecto_scrumEntities();
        private long current_project = CurrentProject.GetCurrentProject().id_proyecto;
        // GET: Equipo
        public ActionResult Index(sc_proyecto proyecto)
        {

            if (current_project <= 0)
            {
                CurrentProject.SetCurrentProject(proyecto);
                current_project = CurrentProject.GetCurrentProject().id_proyecto;

            }

            var sc_equipo = db.sc_equipo.Include(s => s.sc_proyecto).Include(s => s.sc_usuario)
                        .Where(c => c.fk_proyecto == current_project);
            return View(sc_equipo.ToList());
        }

        // GET: Equipo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sc_equipo sc_equipo = db.sc_equipo.Find(id);
            if (sc_equipo == null)
            {
                return HttpNotFound();
            }
            return View(sc_equipo);
        }

        // GET: Equipo/Create
        public ActionResult Create()
        {
            var usuarios = db.sc_usuario
                        .Select(x => new SelectListItem
                        {
                            Value = x.id_usuario.ToString(),
                            Text = x.nombre+"-"+x.sc_rol.rol,
                        });
            ViewBag.fk_proyecto = new SelectList(db.sc_proyecto, "id_proyecto", "nombre");
            ViewBag.fk_usuario = new SelectList(usuarios, "Value", "Text");
            return View();
        }

        // POST: Equipo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EquipoModel equipo)
        {
            if (ModelState.IsValid)
            {
                sc_equipo e = new sc_equipo()
                {
                    fk_proyecto = current_project,
                    fk_usuario = equipo.fk_usuario,
                    fecha_creacion = DateTime.Now
                };
                db.sc_equipo.Add(e);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.fk_proyecto = new SelectList(db.sc_proyecto, "id_proyecto", "nombre", equipo.fk_proyecto);
            ViewBag.fk_usuario = new SelectList(db.sc_usuario, "id_usuario", "nombre", equipo.fk_usuario);
            return View(equipo);
        }

        // GET: Equipo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sc_equipo sc_equipo = db.sc_equipo.Find(id);
            if (sc_equipo == null)
            {
                return HttpNotFound();
            }
            ViewBag.fk_proyecto = new SelectList(db.sc_proyecto, "id_proyecto", "nombre", sc_equipo.fk_proyecto);
            ViewBag.fk_usuario = new SelectList(db.sc_usuario, "id_usuario", "nombre", sc_equipo.fk_usuario);
            return View(sc_equipo);
        }

        // POST: Equipo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_equipo,fk_proyecto,fk_usuario,fecha_creacion")] sc_equipo sc_equipo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sc_equipo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fk_proyecto = new SelectList(db.sc_proyecto, "id_proyecto", "nombre", sc_equipo.fk_proyecto);
            ViewBag.fk_usuario = new SelectList(db.sc_usuario, "id_usuario", "nombre", sc_equipo.fk_usuario);
            return View(sc_equipo);
        }

        // GET: Equipo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sc_equipo sc_equipo = db.sc_equipo.Find(id);
            if (sc_equipo == null)
            {
                return HttpNotFound();
            }
            return View(sc_equipo);
        }

        // POST: Equipo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            sc_equipo sc_equipo = db.sc_equipo.Find(id);
            db.sc_equipo.Remove(sc_equipo);
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
