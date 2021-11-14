using scrum_app.Filters;
using System.Web;
using System.Web.Mvc;

namespace scrum_app
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new VerifyUserSession());//add user session filter 
        }
    }
}
