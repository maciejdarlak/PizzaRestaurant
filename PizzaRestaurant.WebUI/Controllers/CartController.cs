using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzaRestaurant.Domain.Abstract;
using PizzaRestaurant.Domain.Entities;
using PizzaRestaurant.WebUI.Models;



// Below, adding a single product of a given category and removing all products of a given category.
// Each action (adding / removing a product) is one-off, so it is always determined what "productID" was added + a return link on each of them to the "List" view
// (same names as in the single "productSummary" button file).
// Each activity is contained in the session state, hence "GetCart ()" is added with each addition / subtraction (it is responsible for adding the session state).
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

        // NP: IS PIZZA DIAVOLLA ("productId")?
        // "RedirectToRouteResult" represents the result that redirects using the specified route value dictionary,
        // here using the "RedirectToAction" method, which redirects to another action, here to "Index".
        public RedirectToRouteResult AddToCart (Cart cart, int productId, string returnUrl)  
        {
            Product product = repository.Products.
            FirstOrDefault(p => p.ProductID == productId);  //CHECKING IS DIAVOLLA or is the productId you are looking for equal to a product from our database

            if (product != null)  //If the product exists          
            {
                cart.AddItem(product, 1);  //WE ADD 1 DIAVOLLA                                                                                                                                        
            }
            return RedirectToAction("Index", new { returnUrl });  //Redirecting to another action, here to "Index".
        }

        // E.g: IS PIZZA DIAVOLLA ("productId")?
        // "RedirectToRouteResult" represents the result that redirects using the specified route value dictionary,
        // here using the "RedirectToAction" method, which redirects to another action, here to "Index".
        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)  
        {
            Product product = repository.Products
            .FirstOrDefault(p => p.ProductID == productId);  //CHECKING IS DIAVOLLA or is the productId you are looking for equal to a product from our database?

            if (product != null)  //If the product exists      
            {
                cart.RemoveLine(product);  //SUBSCRIPTION 1 DIAVOLLA
            }
            return RedirectToAction("Index", new { returnUrl });  //Redirecting to another action, here to "Index".
        }

        //Action method returning "Index" view
        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel  //Cart + ReturnUrl
            {
                Cart = cart,
                ReturnUrl = returnUrl  //Return path to the "List" view for a given product from the parameter
            });
        }

        //An action method that provides all the information about the cart to the view (the object is created in "CartModelBinder" so you don't have to do it here) - 
        //it is "Your cart: x pieces, y PLN"
        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        //Action method that provides all information about shipping data to the view, i.e. an object of the "ShippingDetails" class
        public ViewResult Checkout()
        {
            return View(new ShippingDetails());  
        }

        //Action method - sending the order form by the user (POST request)
        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            //"Lines" - one product x quantity
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Koszyk jest pusty!");
            }
            //"ModelState.IsValid "indicates whether it was possible to correctly associate inbound values from the request to the model and whether any
            //explicitly specified validation rules were broken during the model binding process.
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



















