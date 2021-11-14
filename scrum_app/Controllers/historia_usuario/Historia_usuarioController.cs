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
using scrum_app.Models.historia_usuario;
using scrum_app.Models.proyecto;

namespace scrum_app.Controllers.historia_usuario
{
    [VerifyUserSession]
    public class Historia_usuarioController : Controller
    {
        private proyecto_scrumEntities db = new proyecto_scrumEntities();
        private long current_project = CurrentProject.GetCurrentProject().id_proyecto;
        private long current_epica = CurrentEpica.GetCurrentEpica().id_epica;
        // GET: Historia_usuario
        public ActionResult Index(sc_epica epica)
        {

            if (current_epica <= 0)
            {
                CurrentEpica.SetCurrentEpica(epica);
                current_epica = CurrentEpica.GetCurrentEpica().id_epica;

            }
            var sc_historia_usuario = db.sc_historia_usuario.Include(s => s.sc_usuario).Include(s => s.sc_usuario1).Include(s => s.sc_epica)
                   .Where(c => c.fk_epica == current_epica); 
            return View(sc_historia_usuario.ToList());
        }

        // GET: Historia_usuario/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sc_historia_usuario sc_historia_usuario = db.sc_historia_usuario.Find(id);
            if (sc_historia_usuario == null)
            {
                return HttpNotFound();
            }
            return View(sc_historia_usuario);
        }

        // GET: Historia_usuario/Create
        public ActionResult Create()
        {
            ViewBag.fk_asignado_a = new SelectList(db.sc_usuario, "id_usuario", "nombre");
            ViewBag.fk_estado_historia_usuario = new SelectList(db.sc_estado_historia_usuario, "id_estado", "estado");
            return View();
        }

        // POST: Historia_usuario/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HistoriaUsuarioModel historiaUsuario)
        {
            if (ModelState.IsValid)
            {
                Nullable<int> value = null;
                sc_historia_usuario hu = new sc_historia_usuario()
                {
                    //fk_proyecto=current_project,
                    fk_creado_por=1,//agregar usuario logueado
                    descricion=historiaUsuario.descricion,
                    fecha_creacion=DateTime.Now,
                    fk_epica=current_epica,
                    titulo =historiaUsuario.titulo,
                    fk_estado_historia_usuario=1
                };
                db.sc_historia_usuario.Add(hu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.fk_asignado_a = new SelectList(db.sc_usuario, "id_usuario", "nombre", historiaUsuario.fk_asignado_a);
            ViewBag.fk_estado_historia_usuario = new SelectList(db.sc_estado_historia_usuario, "id_estado", "estado");

            return View(historiaUsuario);
        }

        // GET: Historia_usuario/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sc_historia_usuario sc_historia_usuario = db.sc_historia_usuario.Find(id);
            if (sc_historia_usuario == null)
            {
                return HttpNotFound();
            }

            HistoriaUsuarioModel hu = new HistoriaUsuarioModel()
            {
                id_historia_usuario= sc_historia_usuario.id_historia_usuario,
                fk_creado_por = sc_historia_usuario.fk_creado_por,//agregar usuario logueado
                descricion = sc_historia_usuario.descricion,
                fecha_creacion = sc_historia_usuario.fecha_creacion,
                fk_epica = sc_historia_usuario.fk_epica,
                fk_asignado_a = sc_historia_usuario.fk_asignado_a,
                titulo = sc_historia_usuario.titulo,
                fk_estado_historia_usuario = sc_historia_usuario.fk_estado_historia_usuario
            };

            var usuarios = db.sc_usuario.Join(db.sc_equipo,
                    u=>u.id_usuario,
                    e=>e.fk_usuario,
                    (u,e)=>new {
                        id_usuario=u.id_usuario,
                        nombre=u.nombre,
                        id_proyecto=e.fk_proyecto,
                        rol=u.sc_rol.rol
                    } ).Where(c=>c.id_proyecto==current_project)
                       .Select(x => new SelectListItem
                       {
                           Value = x.id_usuario.ToString(),
                           Text = x.nombre + "-" + x.rol,
                       });
            ViewBag.fk_asignado_a = new SelectList(usuarios, "Value", "Text", sc_historia_usuario.fk_asignado_a);
            ViewBag.fk_creado_por = new SelectList(db.sc_usuario, "id_usuario", "nombre", sc_historia_usuario.fk_creado_por);
            ViewBag.fk_epica = new SelectList(db.sc_epica, "id_epica", "titulo", sc_historia_usuario.fk_epica);
            ViewBag.fk_estado_historia_usuario = new SelectList(db.sc_estado_historia_usuario, "id_estado", "estado", sc_historia_usuario.fk_estado_historia_usuario);
            return View(hu);
        }

        // POST: Historia_usuario/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HistoriaUsuarioModel historiaUsuario)
        {
            if (ModelState.IsValid)
            {

                sc_historia_usuario hu = new sc_historia_usuario()
                {
                    id_historia_usuario=historiaUsuario.id_historia_usuario,
                    fk_creado_por = historiaUsuario.fk_creado_por,//agregar usuario logueado
                    descricion = historiaUsuario.descricion,
                    fecha_creacion = historiaUsuario.fecha_creacion,
                    fk_epica = historiaUsuario.fk_epica,
                    fk_asignado_a = historiaUsuario.fk_asignado_a,
                    titulo = historiaUsuario.titulo,
                    fk_estado_historia_usuario = historiaUsuario.fk_estado_historia_usuario
                };
                db.Entry(hu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Proyecto");
            }
            ViewBag.fk_asignado_a = new SelectList(db.sc_usuario, "id_usuario", "nombre", historiaUsuario.fk_asignado_a);
            ViewBag.fk_creado_por = new SelectList(db.sc_usuario, "id_usuario", "nombre", historiaUsuario.fk_creado_por);
            ViewBag.fk_epica = new SelectList(db.sc_epica, "id_epica", "titulo", historiaUsuario.fk_epica);
            ViewBag.fk_estado_historia_usuario = new SelectList(db.sc_estado_historia_usuario, "id_estado", "estado", historiaUsuario.fk_estado_historia_usuario);

            return View(historiaUsuario);
        }


    }
}
