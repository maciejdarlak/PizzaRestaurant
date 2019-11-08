using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PizzaRestaurant.Domain.Abstract;
using PizzaRestaurant.Domain.Entities;



//Wrota do bazy daty.
namespace PizzaRestaurant.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        //Tworzymy obiekt mający dostęp do całej bazy danych.
        private EFDbContext context = new EFDbContext();

        //Poniższe pobiera konkretne produkty.
        public IEnumerable<Product> Products
        {
            get { return context.Products; }
        }

        //Tworzenie nowego produktu lub zmienianie właściwości istniejącego.
        public void SaveProduct(Product product)
        {
            //Nowy produkt.
            if (product.ProductID == 0)
            {
                context.Products.Add(product);
            }
            //Istniejący produkt.
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
            //Zapisywanie zmian w zasadniczej bazie danych.
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