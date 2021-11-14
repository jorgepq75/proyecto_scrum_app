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

namespace scrum_app.Controllers.spring
{
    [VerifyUserSession]
    public class SpringController : Controller
    {
        private proyecto_scrumEntities db = new proyecto_scrumEntities();
        private long current_project = CurrentProject.GetCurrentProject().id_proyecto;

        // GET: Spring
        public ActionResult Index()
        {
            HttpContext.Session.Remove("spring");//remove spring session
            var sc_spring = db.sc_spring.Include(s => s.sc_proyecto).Include(s => s.sc_usuario)
                .Where(c=>c.fk_proyecto==current_project);
            return View(sc_spring.ToList());
        }

        // GET: Spring/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sc_spring sc_spring = db.sc_spring.Find(id);
            if (sc_spring == null)
            {
                return HttpNotFound();
            }
            return View(sc_spring);
        }

        // GET: Spring/Create
        public ActionResult Create()
        {
            ViewBag.fk_proyecto = new SelectList(db.sc_proyecto, "id_proyecto", "nombre");
            ViewBag.fk_creado_por = new SelectList(db.sc_usuario, "id_usuario", "nombre");
            return View();
        }

        // POST: Spring/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SpringModel spring)
        {
            if (ModelState.IsValid)
            {
                sc_spring s = new sc_spring()
                {
                    nombre = spring.nombre,
                    fk_proyecto = current_project,
                    fecha_creacion = DateTime.Now,
                    fk_creado_por = 1,//usuario logueado
                    fk_estado_spring=1
                };
                db.sc_spring.Add(s);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.fk_proyecto = new SelectList(db.sc_proyecto, "id_proyecto", "nombre", spring.fk_proyecto);
            ViewBag.fk_creado_por = new SelectList(db.sc_usuario, "id_usuario", "nombre", spring.fk_creado_por);
            return View(spring);
        }

        // GET: Spring/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sc_spring sc_spring = db.sc_spring.Find(id);
            if (sc_spring == null)
            {
                return HttpNotFound();
            }
            System.Diagnostics.Debug.WriteLine(sc_spring.fk_creado_por);
            SpringModel s = new SpringModel()
            {
                id_spring = sc_spring.id_spring,
                nombre = sc_spring.nombre,
                fk_proyecto = sc_spring.fk_proyecto,
                fecha_creacion = sc_spring.fecha_creacion,
                fk_creado_por = sc_spring.fk_creado_por,//usuario logueado
                fk_estado_spring = sc_spring.fk_estado_spring
            };
            ViewBag.fk_proyecto = new SelectList(db.sc_proyecto, "id_proyecto", "nombre", sc_spring.fk_proyecto);
            ViewBag.fk_creado_por = new SelectList(db.sc_usuario, "id_usuario", "nombre", sc_spring.fk_creado_por);
            ViewBag.fk_estado_spring = new SelectList(db.sc_estado_spring, "id_estado", "estado", sc_spring.fk_estado_spring);
            return View(s);
        }

        // POST: Spring/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SpringModel spring)
        {
            if (ModelState.IsValid)
            {
                sc_spring s = new sc_spring()
                {
                    id_spring=spring.id_spring,
                    nombre = spring.nombre,
                    fk_proyecto = spring.fk_proyecto,
                    fecha_creacion = spring.fecha_creacion,
                    fk_creado_por = spring.fk_creado_por,
                    fk_estado_spring = spring.fk_estado_spring
                };
                db.Entry(s).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fk_proyecto = new SelectList(db.sc_proyecto, "id_proyecto", "nombre", spring.fk_proyecto);
            ViewBag.fk_creado_por = new SelectList(db.sc_usuario, "id_usuario", "nombre", spring.fk_creado_por);
            ViewBag.fk_estado_spring = new SelectList(db.sc_estado_spring, "id_estado", "estado", spring.fk_estado_spring);
            return View(spring);
        }

       
    }
}
