﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PasteBook
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }


        //stackoverflow.com/questions/25321794/custom-error-in-global-asax
        //protected void Application_Error(object sender, EventArgs e)
        //{
        //    var ex = Server.GetLastError() as HttpException;

        //    if (ex != null)
        //    {
        //        int errorCode = ex.GetHttpCode();

        //        if (errorCode == 404)
        //        {
        //            Response.Redirect("~/Error/Error404");
        //        }

        //        else
        //        {
        //            Response.Redirect("~/Error/GlobalError");
        //        }
        //    }

        //    else
        //    {
        //        Response.Redirect("~/Error/GlobalError");
        //    }
        //}
    }
}
