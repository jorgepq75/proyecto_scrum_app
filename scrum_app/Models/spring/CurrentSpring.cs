using scrum_app.DB_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace scrum_app.Models.spring
{
    public class CurrentSpring
    {
        public static void SetCurrentSpring(sc_spring spring)
        {
            HttpContext.Current.Session["spring"] = spring;
        }

        public static sc_spring GetCurrentSpring()
        {
            sc_spring spring = (sc_spring)HttpContext.Current.Session["spring"];
            return spring == null ? new sc_spring() : spring;
        }
    }
}