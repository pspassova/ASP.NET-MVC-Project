using System;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Visions.Web.App_Start;

namespace Visions.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DatabaseConfig.InitializeDatabase();

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            string lang = this.Request.Url.LocalPath.Split('/')[1];

            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(lang);
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(lang);
        }
    }
}
