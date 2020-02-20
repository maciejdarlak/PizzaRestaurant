using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzaRestaurant.WebUI.Infrastructure.Abstract;
using PizzaRestaurant.WebUI.Models;



//Controller for login to admin
namespace PizzaRestaurant.WebUI.Controllers
{
    public class AccountController : Controller
    {
        IAuthProvider authProvider;

        public AccountController(IAuthProvider auth)
        {
            authProvider = auth;
        }

        // "ActionResult" is an abstract class that represents the result of the action method - just calling the "Login" view
        public ActionResult Login()
        {
            return View();
        }

        //User submitting the form
        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            //"ModelState.IsValid "indicates whether it was possible to correctly link inbound values from the request to the model and
            //whether any explicitly specified validation rules were broken during the model binding process.
            if (ModelState.IsValid)
            {
                //The following parameters (each individually) are enriched with metadata from the "LoginViewModel" class.
                //If the login and password are ok then you will be redirected to the editing list.
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