using System.Web.Mvc;

namespace University.Areas.Dean
{
    public class DeanAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Dean";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Dean_default",
                "Dean/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "University.Areas.Dean.Controllers" }
            );
        }
    }
}