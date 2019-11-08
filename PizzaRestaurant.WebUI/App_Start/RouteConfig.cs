using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;



//Domyślne trasy - kolejność decyduje o pierszeństwie użycia (najpierw ten najwyżej, potem niższe)
namespace PizzaRestaurant.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");  //Mówi to silnikowi routingu, że nie będziemy przetwarzać tych żądań, które pasują do tego wzorca trasy

            routes.MapRoute
                (null, 
                "", 
                new { controller = "Product", action = "List", category = (string)null, page = 1 });  //"new{...}" typ anonimowy - może byc przypisany w przyszłości do innego typu, w tym do obiektu

            routes.MapRoute
                (null,
                "Strona{page}",
                new { controller = "Product", action = "List", category = (string)null },
                new { page = @"\d+" });  //"\d" oznacza „cyfrę w zakresie 0-9”. "+" oznacza „1 lub więcej razy”. Więc "\d+" oznacza jedną lub więcej cyfr - jest to ograniczenie (akceptowane sa tylko liczby

            routes.MapRoute
                (null,
                "{category}",
                new { controller = "Product", action = "List", page = 1 });

            routes.MapRoute
                (null,
                "{category}/Strona{page}",
                new { controller = "Product", action = "List" },
                new { page = @"\d+" });  //"\d" oznacza „cyfrę w zakresie 0-9”. "+" oznacza „1 lub więcej razy”. Więc "\d+" oznacza jedną lub więcej cyfr - jest to ograniczenie (akceptowane sa tylko liczby

            routes.MapRoute(null, "{controller}/{action}");
        }
    }
}