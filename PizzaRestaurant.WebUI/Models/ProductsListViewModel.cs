using System;
using System.Collections.Generic;
using PizzaRestaurant.Domain.Entities;
using System.Linq;
using System.Web;



// The class contains all data sent from the controller to the view (instead of sending e.g. single VieBags and others we have everything in one class)
namespace PizzaRestaurant.WebUI.Models
{
    public class ProductsListViewModel
    {
        // Browse the "Products" list in turn
        public IEnumerable<Product> Products { get; set; }  
        public PagingInfo PagingInfo { get; set; }
        // Selected pizza category
        public string CurrentCategory { get; set; }  
    }
}