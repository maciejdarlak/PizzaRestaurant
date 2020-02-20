using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;



//Default routes - the order determines the priority of use (first highest, then lower)
namespace PizzaRestaurant.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");  //This tells the routing engine that we will not process requests that match this route pattern

            routes.MapRoute
                (null, 
                "", 
                new { controller = "Product", action = "List", category = (string)null, page = 1 });  //"new{...}" anonymous type - may be assigned in the future to another type, including the object

            routes.MapRoute
                (null,
                "Strona{page}",
                new { controller = "Product", action = "List", category = (string)null },
                new { page = @"\d+" });  //"\ d" means "a number in the range 0-9". "+" means "1 or more times". So "\ d +" means one or more digits - this is a restriction (only numbers are accepted

            routes.MapRoute
                (null,
                "{category}",
                new { controller = "Product", action = "List", page = 1 });

            routes.MapRoute
                (null,
                "{category}/Strona{page}",
                new { controller = "Product", action = "List" },
                new { page = @"\d+" });  //"\ d" means "a number between 0 and 9". "+" Means "1 or more times." So "\ d +" means one or more digits - this is a limit (only numbers exceed

            routes.MapRoute(null, "{controller}/{action}");
        }
    }
}