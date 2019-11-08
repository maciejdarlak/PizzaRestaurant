using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PizzaRestaurant.Domain.Abstract;
using PizzaRestaurant.Domain.Entities;
using System.Data.Entity;



namespace PizzaRestaurant.Domain.Concrete
{
    //W klasie "DbContext" definiujemy właściwości dla każdej tabeli, z której chcemy skorzytać (tu właściwość "Products").
    //"DbContext" ogólnie reprezentuje połączenie z bazą danych i zestaw tabel.
    public class EFDbContext : DbContext
    {
        //"DBSet" służy do reprezentowania jednej konkretnej tabeli bazy danych
        public DbSet<Product> Products  { get; set; }
    }
}