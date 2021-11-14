using scrum_app.DB_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace scrum_app.Models.epica
{
    public class CurrentEpica
    {
        public static void SetCurrentEpica(sc_epica epica)
        {
            HttpContext.Current.Session["epica"] = epica;
        }

        public static sc_epica GetCurrentEpica()
        {
            sc_epica epica = (sc_epica)HttpContext.Current.Session["epica"];
            return epica == null ? new sc_epica() : epica;
        }
    }
}