using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzaRestaurant.Domain.Abstract;
using PizzaRestaurant.Domain.Entities;



//Kontroler - metody administracyjne - część "RenderBody()" z "_AdminLayout".
namespace PizzaRestaurant.WebUI.Controllers
{
    //Poniżej filtr dostepności "Authorize".
    //Gdy atrybut "Authorize" jest użyty bez żadnych parametrów, pozwala na dostęp do metod akcji kontrolera wyłącznie uwierzytelnionym użytkownikom.
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


        //Edycja wybranego produktu.
        //WAŻNE - widok "Edit" zawsze wysyła formularz do repozytorium (tym wierszem: " @using (Html.BeginForm("Edit", "Admin"))" - 
        //link do metody [HttpPost] Edit"), a do tego widoku odwołują się metotody "Create" i "Edit".
        public ViewResult Edit(int productID)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productID);
            return View(product);
        }


        //Wywołanie widoku w momencie wciśnięcia klawisza "Zapisz" w widoku "Edit".
        //"HttpPostedFileBase" - dzięki tej klasie dane przesłanego pliku platforma MVC przekaże metodzie akcji.
        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase image = null)
        {
            //Upewniamy się, że łącznik modelu ma możliwość kontroli poprawności danych przesłanych przez użytkownika
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    //Pobranie właściwości klasy HttpPostedFileBase - formatu pliku.
                    product.ImageMimeType = image.ContentType;
                    //Pobranie właściwości klasy HttpPostedFileBase -  rozmiaru przekazanego pliku w bajtach.
                    product.ImageData = new byte[image.ContentLength];
                    //HttpPostedFileBase.InputStream wskazuje  przekazany plik.
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                }
                    //Zapisywanie w bazie danych
                    repository.SaveProduct(product);
                //Komunikat że zapisano, życie "TempData" trwa do momentu ukazania się komunikatu w widoku "Index"(zmiana widoku wykasowuje go).
                //"VieBag" przekazuje info tylko miedzy kontrolerem a widokiem, tu przekierowujemy do kontrolera "Index", więc "TempData" jest lepszy.
                TempData["message"] = string.Format("Zapisano {0} ",product.Name);
                //Przekierowanie do metody akcji "Index"
                return RedirectToAction("Index");
            }
            else
            //Błąd w wartościach danych
            {
                return View(product);
            }
        }


        //Tworzenie nowego produktu. Całkowicie akceptowalne jest to, żeby metoda akcji korzystała z widoku, który zwykle jest skojarzony z innym widokiem.
        //Wstrzykujemy nowy obiekt Product do widoku modelu, dzięki czemu widok Edit będzie miał puste pola.
        //WAŻNE - widok "Edit" zawsze wysyła formularz do repozytorium (tym wierszem: " @using (Html.BeginForm("Edit", "Admin"))" - link do metody [HttpPost] Edit"), 
        //a do tego widoku odwołują się metotody "Create" i "Edit".
        public ViewResult Create()
        {
            return View("Edit", new Product());
        }


        //Usuwanie produktu
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