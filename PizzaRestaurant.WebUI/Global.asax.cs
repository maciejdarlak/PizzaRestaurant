using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using PizzaRestaurant.Domain.Entities;
using PizzaRestaurant.WebUI.Infrastructure.Binders;




//Plik dot. min. zdarzeń sesji.
//Plik Global.asax jest to opcjonalny plik zawierający kod, który odpowiada za reagowanie na zdarzenia zgłoszone z poziomu aplikacji i poziomu sesji takie jak start aplikacji, zakończenie sesji, itp. 
//Plik ten jest opcjonalny, czyli nie musimy reagować na te zdarzenia, jeżeli nie chcemy.
namespace PizzaRestaurant.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //ŚLUB - tu informujemy platformę MVC, że przy tworzeniu obiektów Cart używana będzie nasza klasa CartModelBinder.
            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());  //"typeof()" - przyjmuje nazwę typu.
        }
    }
}
