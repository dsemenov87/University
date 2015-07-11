using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity;

using University.Models;
using University.Areas.Dean;

namespace University
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer<ApplicationDbContext>(new AppDbInitializer());
            
            //AreaRegistration.RegisterAllAreas();

            var deanArea = new DeanAreaRegistration();
            var adminAreaContext = new AreaRegistrationContext(deanArea.AreaName, RouteTable.Routes);
            deanArea.RegisterArea(adminAreaContext);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
