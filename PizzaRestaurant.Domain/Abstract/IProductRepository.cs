using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaRestaurant.Domain.Entities;



namespace PizzaRestaurant.Domain.Abstract
{
    //Dostęp do bazy danych 
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
    }
}
