using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaRestaurant.Domain.Entities;



namespace PizzaRestaurant.Domain.Abstract
{
    //Using "Ninject" you can connect to a selected class that can be injected into the constructor of another class. It's the essence of ID
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
        void SaveProduct(Product product);
        Product DeleteProduct(int productID);
    }    
}
