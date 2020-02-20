using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzaRestaurant.Domain.Abstract;



// Controller - pizza category buttons on the left side of the page, by default at the start without a category selected.
// Change in "Nav" or select one category = routing starts = in "Nav" - distinction of the selected category (e.g. "pizza red"), in "Product" - list of only the selected category (e.g. "pizza red").
// Selecting "Home" restores the start page.
namespace PizzaRestaurant.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IProductRepository repository;

        public NavController(IProductRepository repo)
        {
            repository = repo;
        }

        // "PartialViewResult" returns a partial view to the response stream, usually a partial view called from the main view.
        // The parameter says that by default no category in the category column is selected.
        public PartialViewResult Menu(string category = null)  
        {
            //"Viewbag" - short-term object, created for the time of opening the view, after clicking the link to another url is deleted.
            ViewBag.SelectedCategory = category;  
            IEnumerable<string> categories = repository.Products
                .Select(x => x.Category)
                //Returns all - here: categories without repetition (the same category names may occur)
                .Distinct()  
                .OrderBy(x => x);

            //Renders a partial view using the specified model
            return PartialView(categories);  
        }
    }
}