using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzaRestaurant.Domain.Entities;
using PizzaRestaurant.Domain.Abstract;
using PizzaRestaurant.WebUI.Models;



//Segregacja produktów do wyświetlenia
namespace PizzaRestaurant.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        //Chcemy widziec na stronie 4 produkty
        public int PageSize = 4;

        //Konstruktor zależny od interfejsu (konkretny produkt czyli spełnienie kontraktu dla interfejsa)
        public ProductController(IProductRepository productRepository)
        {
            this.repository = productRepository;
        }

        //ViewResult - Renderuje określony widok strumienia odpowiedzi - tu zwraca widok produktów z bieżącej i następnych stron (bez poprzednich).
        //Na starcie wyświetlane sa wszystkie produkty, po wybraniu stron innej niż 1 poprzedzające wybrana są kasowane w metodzie "List()" 
        //tu: "Skip((page - 1) * PageSize)".
        public ViewResult List(string category, int page = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel
            {
                //Pobranie, ułożenie, pominięcie wszystkich poprzednich stron, odczytanie reszty produktów + uzupełnienie PagingInfo + kategoria (wszystkie 3 parametry)
                Products = repository.Products
                //Dodatkowy filtr na kategorie, jak "null", zwraca wszystkie kategorie, przeciwnie - wybraną kategorię.
                .OrderBy(p => p.ProductID)
                .Where(p => category == null || p.Category == category)  
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    //Jeśli "category" == null to 1, jak nie to 2.
                    TotalItems = category == null ?  
                        repository.Products.Count() :  //1
                        repository.Products.Where(x => x.Category == category).Count()  //2
                },
                //Skutek dodania kolejnej właściwosci w pliku "ProductsListViewModel".
                CurrentCategory = category  
            };
            return View(model);
        }

        //Pobranie zdjęcia (tu pizzy)
        public FileContentResult GetImage(int productId)
        {
            Product prod = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            if (prod != null)
            {
                //Jeżeli chcemy przesłać plik do przeglądarki klienta, metoda akcji powinna zwrócić obiekt typu "FileContentResult",
                //a egzemplarze obiektu są tworzone za pomocą metody "File()" z bazowej klasy kontrolera.
                return File(prod.ImageData, prod.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}