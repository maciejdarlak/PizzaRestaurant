using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzaRestaurant.Domain.Entities;
using PizzaRestaurant.Domain.Abstract;
using PizzaRestaurant.WebUI.Models;



namespace PizzaRestaurant.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        //Chcemy widziec na stronie 4 produkty
        public int PageSize = 4;

        //Konstruktor zależny od interfejsu (konkretny produkt czyli spełnienie kontraktu dla interfejsa)
        public ProductController (IProductRepository productRepository)
        {
            this.repository = productRepository;
        }

        //ViewResult - Renderuje określony widok strumienia odpowiedzi - tu zwraca widok produktów z bieżącej i następnych stron (bez poprzednich)
        public ViewResult List(int page = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel
            {
                //Pobranie, ułożenie, pominięcie wszystkich poprzednich stron, odczytanie reszty produktów
                Products = repository.Products
                .OrderBy(p => p.ProductID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Products.Count()
                }
            };
            return View(model);
        }
    }
}