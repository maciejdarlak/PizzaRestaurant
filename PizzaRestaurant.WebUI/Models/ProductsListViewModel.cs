using System;
using System.Collections.Generic;
using PizzaRestaurant.Domain.Entities;
using System.Linq;
using System.Web;



//Klasa zawiera wszystkie dane przesyłane z konrolera do widoku (zamiast wysyłania np. pojedyńczych VieBag i innych mamy wszystko w jednej klasie)
namespace PizzaRestaurant.WebUI.Models
{
    public class ProductsListViewModel
    {
        //Przeglądanie po kolei listy "Products"
        public IEnumerable<Product> Products { get; set; }  
        public PagingInfo PagingInfo { get; set; }
        //Wybrana kategoria pizzy
        public string CurrentCategory { get; set; }  
    }
}