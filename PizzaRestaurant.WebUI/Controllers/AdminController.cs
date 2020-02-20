using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzaRestaurant.Domain.Abstract;
using PizzaRestaurant.Domain.Entities;



//Controller - administrative methods - part "RenderBody ()" of "_AdminLayout".
namespace PizzaRestaurant.WebUI.Controllers
{
    //Below is the "Authorize" availability filter.
    //When the "Authorize" attribute is used without any parameters, it allows access to controller action methods only for authenticated users.
    [Authorize]
    public class AdminController : Controller
    {
        private IProductRepository repository;

        public AdminController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Products);
        }


        //Edition of the selected product.
        //IMPORTANT - the "Edit" view always sends the form to the repository (in this line: "@using (Html.BeginForm (" Edit "," Admin "))" - 
        //a link to the method [HttpPost] Edit "), and this view is referred to "Create" and "Edit" methods.
        public ViewResult Edit(int productID)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productID);
            return View(product);
        }


        //Calling the view when the "Save" key is pressed in the "Edit" view.
        //"HttpPostedFileBase" - thanks to this class, the data of the transferred file, the MVC platform will pass on to the action method.
        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase image = null)
        {
            //It was checked whether the model connector has the ability to check the correctness of data sent by the user
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    //Get properties of the HttpPostedFileBase class - file format
                    product.ImageMimeType = image.ContentType;
                    //Get the properties of the HttpPostedFileBase class - size of the transferred file in bytes
                    product.ImageData = new byte[image.ContentLength];
                    //HttpPostedFileBase.InputStream indicates the uploaded file.
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                }
                //Saving to the database
                repository.SaveProduct(product);
                //The message that saved, the life of "TempData" lasts until the message appears in the "Index" view (changing the view deletes it).
                //"ViewBag" only transfers information between the controller and the view, here it is redirected to the "Index" controller, so "TempData" is better.
                TempData["message"] = string.Format("Zapisano {0} ",product.Name);
                //Redirecting to the "Index" action method
                return RedirectToAction("Index");
            }
            else
            //Error in data values
            {
                return View(product);
            }
        }


        // Create a new product. It is perfectly acceptable for the action method to use a view that is usually associated with another view.
        // A new Product object has been injected into the model view so that the Edit view has empty fields.
        // IMPORTANT - the "Edit" view always sends the form to the repository (in this line: "@using (Html.BeginForm (" Edit "," Admin "))" - link to the method [HttpPost] Edit "),
        // and the "Create" and "Edit" methods refer to this view.
        public ViewResult Create()
        {
            return View("Edit", new Product());
        }


        //Product removal
        [HttpPost]
        public ActionResult Delete(int productID)
        {
            Product deletedProduct = repository.DeleteProduct(productID);
            if (deletedProduct != null)
            {
                TempData["message"] = string.Format("Usunięto {0}", deletedProduct.Name);
            }
            return RedirectToAction("Index");
        }
    }
}