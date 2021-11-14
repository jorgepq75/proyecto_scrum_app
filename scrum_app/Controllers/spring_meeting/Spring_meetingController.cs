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
using scrum_app.Models.spring;
using scrum_app.Models.spring_meeting;

namespace scrum_app.Controllers.spring_event
{
    [VerifyUserSession]
    public class Spring_meetingController : Controller
    {
        private proyecto_scrumEntities db = new proyecto_scrumEntities();
        private long current_spring = CurrentSpring.GetCurrentSpring().id_spring;
        // GET: Spring_meeting
        public ActionResult Index(sc_spring spring)
        {
            if (current_spring <= 0)
            {
                CurrentSpring.SetCurrentSpring(spring);
                current_spring = CurrentSpring.GetCurrentSpring().id_spring;

            }
            var sc_spring_meeting = db.sc_spring_meeting.Include(s => s.sc_spring).Include(s => s.sc_usuario).Include(s => s.sc_spring_meeting_type)
                     .Where(c => c.fk_spring == current_spring);

            return View(sc_spring_meeting.ToList());
        }

        // GET: Spring_meeting/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sc_spring_meeting sc_spring_meeting = db.sc_spring_meeting.Find(id);
            if (sc_spring_meeting == null)
            {
                return HttpNotFound();
            }
            return View(sc_spring_meeting);
        }

        // GET: Spring_meeting/Create
        public ActionResult Create()
        {
            ViewBag.fk_spring = new SelectList(db.sc_spring, "id_spring", "nombre");
            ViewBag.fk_creado_por = new SelectList(db.sc_usuario, "id_usuario", "nombre");
            ViewBag.fk_spring_meeting_type = new SelectList(db.sc_spring_meeting_type, "id_meeting_type", "nombre");
            return View();
        }

        // POST: Spring_meeting/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SpringMeetingModel springMeeting)
        {
            if (ModelState.IsValid)
            {
                sc_spring_meeting sm = new sc_spring_meeting()
                {
                    comment=springMeeting.comment,
                    fecha_creacion=DateTime.Now,
                    fk_spring=current_spring,
                    fk_creado_por=1,//add current user
                    fk_spring_meeting_type=springMeeting.fk_spring_meeting_type
                };
                db.sc_spring_meeting.Add(sm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.fk_spring = new SelectList(db.sc_spring, "id_spring", "nombre", springMeeting.fk_spring);
            ViewBag.fk_creado_por = new SelectList(db.sc_usuario, "id_usuario", "nombre", springMeeting.fk_creado_por);
            ViewBag.fk_spring_meeting_type = new SelectList(db.sc_spring_meeting_type, "id_meeting_type", "nombre", springMeeting.fk_spring_meeting_type);
            return View(springMeeting);
        }

        // GET: Spring_meeting/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sc_spring_meeting sc_spring_meeting = db.sc_spring_meeting.Find(id);
            if (sc_spring_meeting == null)
            {
                return HttpNotFound();
            }
            ViewBag.fk_spring = new SelectList(db.sc_spring, "id_spring", "nombre", sc_spring_meeting.fk_spring);
            ViewBag.fk_creado_por = new SelectList(db.sc_usuario, "id_usuario", "nombre", sc_spring_meeting.fk_creado_por);
            ViewBag.fk_spring_meeting_type = new SelectList(db.sc_spring_meeting_type, "id_meeting_type", "nombre", sc_spring_meeting.fk_spring_meeting_type);
            return View(sc_spring_meeting);
        }

        // POST: Spring_meeting/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_spring_meeting,fk_spring,fk_spring_meeting_type,fecha_creacion,fk_creado_por,comment")] sc_spring_meeting sc_spring_meeting)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sc_spring_meeting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fk_spring = new SelectList(db.sc_spring, "id_spring", "nombre", sc_spring_meeting.fk_spring);
            ViewBag.fk_creado_por = new SelectList(db.sc_usuario, "id_usuario", "nombre", sc_spring_meeting.fk_creado_por);
            ViewBag.fk_spring_meeting_type = new SelectList(db.sc_spring_meeting_type, "id_meeting_type", "nombre", sc_spring_meeting.fk_spring_meeting_type);
            return View(sc_spring_meeting);
        }

        
    }
}
