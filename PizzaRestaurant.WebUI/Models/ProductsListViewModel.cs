using System;
using System.Collections.Generic;
using PizzaRestaurant.Domain.Entities;
using System.Linq;
using System.Web;


//Klasa zawiera wszystkie dane przesyłane z konrolera do widoku (zamiast wysyłania np. VieBag)
namespace PizzaRestaurant.WebUI.Models
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}