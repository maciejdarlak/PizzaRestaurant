using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PizzaRestaurant.Domain.Abstract;
using PizzaRestaurant.Domain.Entities;



//Gates to the date base
namespace PizzaRestaurant.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        //Creating an object having access to the entire database.
        private EFDbContext context = new EFDbContext();

        //The following downloads specific products.
        public IEnumerable<Product> Products
        {
            get { return context.Products; }
        }

        //Creating a new product or changing existing properties.
        public void SaveProduct(Product product)
        {
            //New product
            if (product.ProductID == 0)
            {
                context.Products.Add(product);
            }
            //Existing product
            else
            {
                Product dbEntry = context.Products.Find(product.ProductID);

                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                    dbEntry.ImageData = product.ImageData;
                    dbEntry.ImageMimeType = product.ImageMimeType;
                }
            }
            //Saving changes to the main database
            context.SaveChanges();
            }

        public Product DeleteProduct(int productID)
        {
            Product dbEntry = context.Products.Find(productID);

            if (dbEntry != null)
            {
                context.Products.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}