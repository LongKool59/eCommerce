using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace eCommerce
{
    public class MvcApplication : System.Web.HttpApplication
    {
        string connectionStr = ConfigurationManager.ConnectionStrings["sqlConString"].ConnectionString;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //start sql dependency
            SqlDependency.Start(connectionStr);
            NotificationComponents NC = new NotificationComponents();

            NC.RegisterNotification(true);
        }

        protected void Session_Start(object sender, EventArgs e)
        {
        }
        protected void Application_End()
        {
            //stop sql dependecy
            SqlDependency.Stop(connectionStr);
        }
    }
}
