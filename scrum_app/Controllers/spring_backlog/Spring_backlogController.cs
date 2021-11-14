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
using scrum_app.Models.proyecto;
using scrum_app.Models.spring;
using scrum_app.Models.spring_backlog;

namespace scrum_app.Controllers.spring_backlog
{
    [VerifyUserSession]
    public class Spring_backlogController : Controller
    {
        private proyecto_scrumEntities db = new proyecto_scrumEntities();
        private long current_spring= CurrentSpring.GetCurrentSpring().id_spring;
        private long current_project = CurrentProject.GetCurrentProject().id_proyecto;
        // GET: Spring_backlog
        public ActionResult Index(sc_spring spring)
        {

            if (current_spring <= 0)
            {
                CurrentSpring.SetCurrentSpring(spring);
                current_spring = CurrentSpring.GetCurrentSpring().id_spring;

            }
            var sc_spring_backlog = db.sc_spring_backlog.Include(s => s.sc_historia_usuario).Include(s => s.sc_spring)
                 .Where(c => c.fk_spring == current_spring);
            return View(sc_spring_backlog.ToList());
        }

        // GET: Spring_backlog/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sc_spring_backlog sc_spring_backlog = db.sc_spring_backlog.Find(id);
            if (sc_spring_backlog == null)
            {
                return HttpNotFound();
            }
            return View(sc_spring_backlog);
        }

        // GET: Spring_backlog/Create
        public ActionResult Create()
        {


            var product_backlog = db.sc_spring_backlog.Select(x => x.fk_historia_usuario)
                .ToList();
            var historias_por_proyecto=db.sc_historia_usuario
               .Where(c => c.sc_epica.fk_proyecto == current_project);


            var historias_disponibles = historias_por_proyecto.Where(c => !product_backlog.Contains(c.id_historia_usuario))
                .Select(x => new SelectListItem
                {
                    Value = x.id_historia_usuario.ToString(),
                    Text = x.titulo,
                });

            ViewBag.fk_historia_usuario = new SelectList(historias_disponibles, "Value", "Text");
            ViewBag.fk_spring = new SelectList(db.sc_spring, "id_spring", "nombre");
            return View();
        }

        // POST: Spring_backlog/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SpringBacklogModel springBacklog)
        {
            if (ModelState.IsValid)
            {
                sc_spring_backlog sbl = new sc_spring_backlog()
                {
                    fk_spring=current_spring,
                    fecha_creacion=DateTime.Now,
                    fk_historia_usuario=springBacklog.fk_historia_usuario
                };
                db.sc_spring_backlog.Add(sbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.fk_historia_usuario = new SelectList(db.sc_historia_usuario, "id_historia_usuario", "titulo", springBacklog.fk_historia_usuario);
            ViewBag.fk_spring = new SelectList(db.sc_spring, "id_spring", "nombre", springBacklog.fk_spring);
            return View(springBacklog);
        }

        // GET: Spring_backlog/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sc_spring_backlog sc_spring_backlog = db.sc_spring_backlog.Find(id);
            if (sc_spring_backlog == null)
            {
                return HttpNotFound();
            }
          //  ViewBag.fk_historia_usuario = new SelectList(db.sc_historia_usuario, "id_historia_usuario", "titulo", sc_spring_backlog.fk_historia_usuario);
            ViewBag.fk_spring = new SelectList(db.sc_spring, "id_spring", "nombre", sc_spring_backlog.fk_spring);
            return View(sc_spring_backlog);
        }

        // POST: Spring_backlog/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_spring_backlog,fk_spring,fk_historia_usuario,fecha_creacion")] sc_spring_backlog sc_spring_backlog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sc_spring_backlog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           // ViewBag.fk_historia_usuario = new SelectList(db.sc_historia_usuario, "id_historia_usuario", "titulo", sc_spring_backlog.fk_historia_usuario);
            ViewBag.fk_spring = new SelectList(db.sc_spring, "id_spring", "nombre", sc_spring_backlog.fk_spring);
            return View(sc_spring_backlog);
        }

        // GET: Spring_backlog/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sc_spring_backlog sc_spring_backlog = db.sc_spring_backlog.Find(id);
            if (sc_spring_backlog == null)
            {
                return HttpNotFound();
            }
            return View(sc_spring_backlog);
        }

        // POST: Spring_backlog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            sc_spring_backlog sc_spring_backlog = db.sc_spring_backlog.Find(id);
            db.sc_spring_backlog.Remove(sc_spring_backlog);
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
