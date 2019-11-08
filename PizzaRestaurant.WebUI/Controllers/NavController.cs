using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzaRestaurant.Domain.Abstract;



//Kontroler - przyciski kategorii pizzy po lewej stronie strony, domyślnie na starcie bez zaznaczonej kategorii. 
//Zmiana w "Nav" czyli wybranie jedej kategorii = uruchamia się routing = 
//w "Nav" - wyróznienie wybranej kategorii (np. "pizza red"), w "Product" - lista tylko wybranej kategorii (np. "pizza red").
//Zaznaczenie "Home" przywraca stronę startową.
namespace PizzaRestaurant.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IProductRepository repository;

        public NavController(IProductRepository repo)
        {
            repository = repo;
        }
            
        //"PartialViewResult" zwraca częściowy widok do strumienia odpowiedzi, zazwyczaj częściowy widok wywoływany z widoku głównego.
        //Parametr mówi że domyślnie żadna kategoria w kolumnie kategorii nie jest zaznaczona. 
        public PartialViewResult Menu(string category = null)  
        {
            //"Viewbag" - obiekt krótkotrwały, tworzony są na czas otwarcia widoku, po kliknięciu linka do innego adresu url zostaje skasowany.
            ViewBag.SelectedCategory = category;  
            IEnumerable<string> categories = repository.Products
                .Select(x => x.Category)
                //Zwraca wszystkie tu: kategorie bez powtórzeń (mogą się zdażyć takie same nazwy kategorii)
                .Distinct()  
                .OrderBy(x => x);

            //Renderuje widok częściowy przy użyciu określonego modelu.
            return PartialView(categories);  
        }
    }
}