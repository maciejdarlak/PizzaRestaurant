using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzaRestaurant.Domain.Entities;
using PizzaRestaurant.Domain.Abstract;
using PizzaRestaurant.WebUI.Models;



//Product segregation to display
namespace PizzaRestaurant.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        //4 products will be vissible on the page
        public int PageSize = 4;

        //The constructor depends on the interface (specific product i.e. fulfillment of the contract for the interface)
        public ProductController(IProductRepository productRepository)
        {
            this.repository = productRepository;
        }

        // ViewResult - Renders the specified view of the response stream - returns product view from current and next pages (without previous ones)
        // At the start all products are displayed, after selecting pages other than 1, the preceding ones are deleted in the "List ()" method
        // here: "Skip ((page - 1) * PageSize)"
        public ViewResult List(string category, int page = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel
            {
                //Download, arrange, skip all previous pages, read the rest of the products + complete PagingInfo + category (all 3 parameters)
                Products = repository.Products
                // An additional filter for categories, such as "null", returns all categories, on the contrary - the selected category.
                .OrderBy(p => p.ProductID)
                .Where(p => category == null || p.Category == category)  
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    // If "category" == null is 1, if not then 2.
                    TotalItems = category == null ?  
                        repository.Products.Count() :  //1
                        repository.Products.Where(x => x.Category == category).Count()  //2
                },
                //The effect of adding another property in the "ProductsListViewModel" file.
                CurrentCategory = category  
            };
            return View(model);
        }

        // Download photo (pizza here)
        public FileContentResult GetImage(int productId)
        {
            Product prod = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            if (prod != null)
            {
                // In case of transferring the file to the client browser, the action method should return an object of type "FileContentResult",
                // and object instances are created using the "File ()" method from the base controller class.
                return File(prod.ImageData, prod.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}