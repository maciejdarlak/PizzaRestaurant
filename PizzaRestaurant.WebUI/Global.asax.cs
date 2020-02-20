using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using PizzaRestaurant.Domain.Entities;
using PizzaRestaurant.WebUI.Infrastructure.Binders;




// The file applies to session events, among others.
// The Global.asax file is an optional file containing code that is responsible for responding to events reported 
// from the application level and session level such as application start, end of session, etc.
// This file is optional
namespace PizzaRestaurant.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //"WEDDING" - here the MVC platform is informed that the CartModelBinder class will be used when creating Cart objects
            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());  //"typeof()" - takes the name of the type
        }
    }
}
