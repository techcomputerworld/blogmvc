using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity;
using blogmvc.Models;
using System.Data.SqlClient;

namespace blogmvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //aqui pongo el setinitializer de la DB para poder recrearla entera tal cual
            //Database.SetInitializer(new DropCreateDatabaseAlways<ApplicationDbContext>());
            //using de statemant y cuando se termina se ejecuta el metodo dispose()
            
            using (var db = new ApplicationDbContext())
            {
                
                db.Database.Initialize(true);
            }
            //crear un fichero xml 

            //con esto realizamos el inicializador de la DB y debe hacerse el metodo seed
            //Database.SetInitializer(new BlogInit());
        }
    }
}
