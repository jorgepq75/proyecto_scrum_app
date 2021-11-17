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
using scrum_app.Models.epica;
using scrum_app.Models.proyecto;

namespace scrum_app.Controllers.epica
{
    [VerifyUserSession]
    public class EpicaController : Controller
    {
        private proyecto_scrumEntities db = new proyecto_scrumEntities();
        private long current_project = CurrentProject.GetCurrentProject().id_proyecto;
        // GET: Epica
        public ActionResult Index(sc_proyecto proyecto)
        {
            HttpContext.Session.Remove("epica");//remove epica session
            if (current_project <= 0)
            {
                CurrentProject.SetCurrentProject(proyecto);
                current_project = CurrentProject.GetCurrentProject().id_proyecto;

            }
            var sc_epica = db.sc_epica.Include(s => s.sc_usuario).Include(s => s.sc_proyecto).
                Where(c=>c.fk_proyecto==current_project);
            return View(sc_epica.ToList());
        }

        // GET: Epica/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sc_epica sc_epica = db.sc_epica.Find(id);
            if (sc_epica == null)
            {
                return HttpNotFound();
            }
            return View(sc_epica);
        }

        // GET: Epica/Create
        public ActionResult Create()
        {
            ViewBag.fk_creado_por = new SelectList(db.sc_usuario, "id_usuario", "nombre");
            ViewBag.fk_proyecto = new SelectList(db.sc_proyecto, "id_proyecto", "nombre");
            return View();
        }

        // POST: Epica/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EpicaModel epica)
        {
            if (ModelState.IsValid)
            {
                sc_epica e = new sc_epica()
                {
                    titulo =epica.titulo,
                    fk_proyecto= current_project,
                    descricion = epica.descricion,
                    fk_creado_por = Models.usuario.LoginModel.getUserSession().id_usuario,//user here
                    fecha_creacion = DateTime.Now
                };
                db.sc_epica.Add(e);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.fk_creado_por = new SelectList(db.sc_usuario, "id_usuario", "nombre", epica.fk_creado_por);
            ViewBag.fk_proyecto = new SelectList(db.sc_proyecto, "id_proyecto", "nombre", epica.fk_proyecto);
            return View(epica);
        }

        // GET: Epica/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sc_epica sc_epica = db.sc_epica.Find(id);
            if (sc_epica == null)
            {
                return HttpNotFound();
            }
            ViewBag.fk_creado_por = new SelectList(db.sc_usuario, "id_usuario", "nombre", sc_epica.fk_creado_por);
            ViewBag.fk_proyecto = new SelectList(db.sc_proyecto, "id_proyecto", "nombre", sc_epica.fk_proyecto);
            return View(sc_epica);
        }

        // POST: Epica/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_epica,titulo,fk_proyecto,descricion,fk_creado_por,fecha_creacion")] sc_epica sc_epica)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sc_epica).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fk_creado_por = new SelectList(db.sc_usuario, "id_usuario", "nombre", sc_epica.fk_creado_por);
            ViewBag.fk_proyecto = new SelectList(db.sc_proyecto, "id_proyecto", "nombre", sc_epica.fk_proyecto);
            return View(sc_epica);
        }


    }
}
