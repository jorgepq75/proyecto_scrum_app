using scrum_app.DB_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace scrum_app.Models.proyecto
{
    public class CurrentProject
    {
        public static void SetCurrentProject(sc_proyecto proyecto)
        {
            HttpContext.Current.Session["proyecto"] =proyecto;
        }

        public static sc_proyecto GetCurrentProject()
        {
            sc_proyecto proyecto = (sc_proyecto)HttpContext.Current.Session["proyecto"];
            return proyecto==null ? new sc_proyecto() : proyecto;
        }
    }
}