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
using scrum_app.Models.usuario;

namespace scrum_app.Controllers.proyecto
{
    [VerifyUserSession]
    public class ProyectoController : Controller
    {
        private proyecto_scrumEntities db = new proyecto_scrumEntities();

        // GET: Proyecto
        public ActionResult Index()
        {
            var sc_proyecto = db.sc_proyecto.Include(s => s.sc_usuario);
            ViewBag.proyectos = new SelectList(db.sc_proyecto, "id_proyecto", "nombre");
            return View(sc_proyecto.ToList());
        }

        // GET: Proyecto/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sc_proyecto sc_proyecto = db.sc_proyecto.Find(id);
            if (sc_proyecto == null)
            {
                return HttpNotFound();
            }
            return View(sc_proyecto);
        }

        // GET: Proyecto/Create
        public ActionResult Create()
        {
            ViewBag.fk_creado_por = new SelectList(db.sc_usuario, "id_usuario", "nombre");
            return View();
        }

        // POST: Proyecto/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProyectoModel proyecto)
        {
            if (ModelState.IsValid)
            {
                sc_proyecto p = new sc_proyecto(){ 
                    nombre=proyecto.nombre,
                    fk_creado_por=LoginModel.getUserSession().id_usuario,//agregar usuario logueado
                    fecha_creacion= DateTime.Now,
                    descripcion=proyecto.descripcion,
                    fk_estado_proyecto=1
                };
                db.sc_proyecto.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.fk_creado_por = new SelectList(db.sc_usuario, "id_usuario", "nombre", proyecto.fk_creado_por);
            return View(proyecto);
        }

        // GET: Proyecto/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sc_proyecto sc_proyecto = db.sc_proyecto.Find(id);

            if (sc_proyecto == null)
            {
                return HttpNotFound();
            }

            CurrentProject.SetCurrentProject(sc_proyecto);

            ProyectoModel p = new ProyectoModel()
            {
                id_proyecto = sc_proyecto.id_proyecto,
                nombre = sc_proyecto.nombre,
                fk_creado_por = sc_proyecto.fk_creado_por,//agregar usuario logueado
                fecha_creacion = sc_proyecto.fecha_creacion,
                descripcion = sc_proyecto.descripcion,
                fecha_cierre = sc_proyecto.fecha_cierre,
                fk_estado_proyecto = sc_proyecto.fk_estado_proyecto
            };
            ViewBag.fk_estado_proyecto = new SelectList(db.sc_estado_proyecto, "id_estado", "estado", sc_proyecto.fk_estado_proyecto);
            ViewBag.fk_creado_por = new SelectList(db.sc_usuario, "id_usuario", "nombre", sc_proyecto.fk_creado_por);
            return View(p);
        }

        // POST: Proyecto/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProyectoModel proyecto)
        {
            if (ModelState.IsValid)
            {
                sc_proyecto p = new sc_proyecto()
                {
                    id_proyecto=proyecto.id_proyecto,
                    nombre = proyecto.nombre,
                    fk_creado_por = proyecto.fk_creado_por,
                    fecha_creacion = proyecto.fecha_creacion,
                    descripcion = proyecto.descripcion,
                    fecha_cierre = proyecto.fecha_cierre,
                    fk_estado_proyecto = proyecto.fk_estado_proyecto
                };
                db.Entry(p).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fk_estado_proyecto = new SelectList(db.sc_estado_proyecto, "id_estado", "estado", proyecto.fk_estado_proyecto);
            ViewBag.fk_creado_por = new SelectList(db.sc_usuario, "id_usuario", "nombre", proyecto.fk_creado_por);
            return View(proyecto);
        }


        public ActionResult Choose(sc_proyecto proyecto)
        {
            CurrentProject.SetCurrentProject(proyecto);
            
            return RedirectToAction("Index","Epica", new { area = "" });
        }
    }
}
