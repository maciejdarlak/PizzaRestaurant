using System.Web.Mvc;
using PizzaRestaurant.Domain.Entities;




//Ta klasa umożliwia utworzenie obiektu "Cart".
namespace PizzaRestaurant.WebUI.Infrastructure.Binders
{
    class CartModelBinder : IModelBinder  //Interfejs IModelBinder definiuje jedną metodę: BindModel
    {
        private const string sessionKey = "Cart";

        //"ControllerContext" zapewnia dostęp do wszystkich danych z klasy
        //kontrolera, w tym informacje na temat żądania klienta.
        //Posiada ona właściwość HttpContext, która z kolei zawiera właściwość Session, 
        //pozwalającą nam odczytać i zmieniać dane sesji. 
        //Obiekt Cart uzyskujemy przez odczyt wartości klucza z danych sesji, a jeżeli nie ma tam tego obiektu, tworzymy go.
        //"ModelBindingContext" dostarcza danych na temat
        //modelu obiektów, jakie budujemy, oraz zapewnia narzędzia ułatwiające to zadanie.
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)  
        {
            // Pobranie obiektu Cart z sesji
            Cart cart = null;

            if (controllerContext.HttpContext.Session != null)
            {
                cart = (Cart)controllerContext.HttpContext.Session[sessionKey];
            }

            // Utworzenie obiektu Cart, jeżeli nie został znaleziony w danych sesji
            if (cart == null)
            {
                cart = new Cart();
                if (controllerContext.HttpContext.Session != null)
                {
                    controllerContext.HttpContext.Session[sessionKey] = cart;
                }
            }

            // Zwróć koszyk
            return cart;
        }
    }
}