using MVCCMS.App_Start;
using MVCCMS.Models;
using MVCCMS.Models.ModelBinders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVCCMS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected async void Application_Start()
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
			AreaRegistration.RegisterAllAreas();

			await AuthDbConfig.RegisterAdmin();

			ModelBinders.Binders.Add(typeof(Post), new PostModelBinder());
        }
    }
}
