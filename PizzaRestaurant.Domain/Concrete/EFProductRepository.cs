using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PizzaRestaurant.Domain.Abstract;
using PizzaRestaurant.Domain.Entities;



namespace PizzaRestaurant.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        //??? Tworzymy obiekt mający dostęp do całej bazy danych
        private EFDbContext context = new EFDbContext();

        //Poniższe pobiera konkretne produkty
        public IEnumerable<Product> Products
        {
            get { return context.Products; }
        }
    }
}