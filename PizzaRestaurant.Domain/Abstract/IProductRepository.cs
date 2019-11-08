using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaRestaurant.Domain.Entities;



namespace PizzaRestaurant.Domain.Abstract
{
    //Za pomocą "Ninject" podłączamy się pod wybrana klasę, którą możemy wstrzyknąć w konstruktor innej klasy. To istota ID
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
        void SaveProduct(Product product);
        Product DeleteProduct(int productID);
    }    
}
