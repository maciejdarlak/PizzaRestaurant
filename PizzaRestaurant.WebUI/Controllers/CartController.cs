using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzaRestaurant.Domain.Abstract;
using PizzaRestaurant.Domain.Entities;
using PizzaRestaurant.WebUI.Models;



//Poniżej dodanie pojedyńczego produktu danej kategorii oraz usuniecie wszystkich produktów danej kategorii.
//Każda czynność (dodania / usuwania produktu) jest jednorazowa, dlatego zawsze okrelamy jaki "productID" dodajemy + link powrotu na każdym z nich do widoku "List" 
//(te same nazwy co w pliku pojedyńczego przycisku "productSummary").
//Każda czynność zawiera się w stanie sesji, stąd przy każdym doawaniu/odejmowaniu jest "GetCart()" (odpowiada za dodanie stanu sesji).
namespace PizzaRestaurant.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;
        private IOrderProcessor orderProcessor;

        public CartController(IProductRepository repo, IOrderProcessor proc)
        {
            repository = repo;
            orderProcessor = proc;
        }

        //NP: CZY JEST PIZZA DIAVOLLA("productId")?
        //"RedirectToRouteResult" reprezentuje wynik, który dokonuje przekierowania przy użyciu określonego słownika wartości trasy, 
        //tu przy pomocy metody "RedirectToAction", która przekierowuje do innej akcji, tu do "Index".
        public RedirectToRouteResult AddToCart (Cart cart, int productId, string returnUrl)  
        {
            Product product = repository.Products.
            FirstOrDefault(p => p.ProductID == productId);  //SPRAWDZAMY CZY JEST DIAVOLLA czyli czy poszukiwany "productId" jest równy jakiemuś produktowi z naszej bazy danych.

            if (product != null)  //Jeśli produkt istnieje              
            {
                cart.AddItem(product, 1);  //DODAJEMY 1 DIAVOLLĘ                                                                                                                                           
            }
            return RedirectToAction("Index", new { returnUrl });  //Przekierowanie do innej akcji, tu do "Index".
        }

        //NP: CZY JEST PIZZA DIAVOLLA ("productId")?
        //"RedirectToRouteResult" reprezentuje wynik, który dokonuje przekierowania przy użyciu określonego słownika wartości trasy, 
        //tu przy pomocy metody "RedirectToAction", która przekierowuje do innej akcji, tu do "Index".
        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)  
        {
            Product product = repository.Products
            .FirstOrDefault(p => p.ProductID == productId);  //SPRAWDZAMY CZY JEST DIAVOLLA czyli czy poszukiwany "productId" jest równy jakiemuś produktowi z naszej bazy danych.

            if (product != null)  //Jeśli produkt istnieje    
            {
                cart.RemoveLine(product);  //ODEJMUJEMY 1 DIAVOLLĘ
            }
            return RedirectToAction("Index", new { returnUrl });  //Przekierowanie do innej akcji, tu do "Index".
        }

        //Metoda akcji zwracająca widok "Index"
        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel  //Czyli koszyk + ReturnUrl
            {
                Cart = cart,
                ReturnUrl = returnUrl  //Ścieżka powrotna do widoku "List" dla danego produktu z parametru
            });
        }

        //Metoda akcji dostarczjąca do widoku wszystkie informacje o koszyku (obiekt jest tworzony w "CartModelBinder" (ŚLUB) więc tu nie musimy go robić) - jest to "Twój koszyk: x sztuk, y zł"
        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        //Metoda akcji dostarczjąca do widoku wszystkie informacje o danych do wysyłki, czyli obiekt klasy "ShippingDetails"
        public ViewResult Checkout()
        {
            return View(new ShippingDetails());  
        }

        //Metoda akcji - wysyłanie formularza zamówienia przez użytkownika (żądanie POST)
        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            //"Lines" - jeden produkt x ilość
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Koszyk jest pusty!");
            }
            //"ModelState.IsValid" wskazuje, czy możliwe było prawidłowe powiązanie wartości przychodzących z żądania do modelu i czy jakiekolwiek 
            //jawnie określone reguły sprawdzania poprawności zostały złamane podczas procesu wiązania modelu.
            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }

            else
            {
                return View(shippingDetails);
            }       
        }
    }
}



















