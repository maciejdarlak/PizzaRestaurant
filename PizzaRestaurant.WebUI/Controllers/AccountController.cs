using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzaRestaurant.WebUI.Infrastructure.Abstract;
using PizzaRestaurant.WebUI.Models;



//Kontroler do logowania na admina
namespace PizzaRestaurant.WebUI.Controllers
{
    public class AccountController : Controller
    {
        IAuthProvider authProvider;

        public AccountController(IAuthProvider auth)
        {
            authProvider = auth;
        }

        //"ActionResult" to klasa abstrakcyjna, która reprezentuje wynik metody akcji
        //Tu tylko wywołanie widoku "Login"
        public ActionResult Login()
        {
            return View();
        }

        //Wysłanie formularza przez użytkownika
        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            //"ModelState.IsValid" wskazuje, czy możliwe było prawidłowe powiązanie wartości przychodzących z żądania do modelu i 
            //czy jakiekolwiek jawnie określone reguły sprawdzania poprawności zostały złamane podczas procesu wiązania modelu.
            if (ModelState.IsValid)
            {
                //Poniższe parametry (każdy z osobna) są wzbogacone o metadane z klasy "LoginViewModel".
                //Jeśli login i hasło jest ok to następuje przekierownie na listę edycyjną.
                if (authProvider.Authenticate(model.UserName, model.Password))
                {
                    return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
                }
                else
                {
                    ModelState.AddModelError("", "Nieprawidłowa nazwa użytkownika lub niepoprawne hasło.");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
    }
}