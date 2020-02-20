using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzaRestaurant.Domain.Entities;
using PizzaRestaurant.Domain.Abstract;
using PizzaRestaurant.WebUI.Models;



//Contact + map of location
namespace PizzaRestaurant.WebUI.Controllers
{
    public class ContactController : Controller
    {
        ContactDetails contactDetails;

        public ContactController(ContactDetails contact)
        {
            contactDetails = contact;
        }

        public ViewResult Address()
        {
            return View(contactDetails);
        }
    }
}