using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PizzaRestaurant.Domain.Abstract;
using PizzaRestaurant.Domain.Entities;
using System.Data.Entity;



namespace PizzaRestaurant.Domain.Concrete
{
    //In the "DbContext" class you define properties for each table (here the "Products" property).
    //"DbContext" generally represents the database connection and table set.
    public class EFDbContext : DbContext
    {
        //"DBSet" is used to represent one specific database table
        public DbSet<Product> Products  { get; set; }
    }
}