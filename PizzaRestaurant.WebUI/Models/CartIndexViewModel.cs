using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PizzaRestaurant.Domain.Entities;



//Model created for the view
namespace PizzaRestaurant.WebUI.Models
{
    public class CartIndexViewModel
    {
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }      
    }
}

