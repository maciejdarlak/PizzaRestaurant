using System.Web.Security;
using PizzaRestaurant.WebUI.Infrastructure.Abstract;



//Sprawdzenie, czy login i hasło (parametry metody "Authenticate") są zgodne z prawdą (dopisek w pliku "Web.config")
namespace PizzaRestaurant.WebUI.Infrastructure.Concrete
{
    public class FormsAuthProvider : IAuthProvider
    {
        public bool Authenticate(string username, string password)
        {
            bool result = FormsAuthentication.Authenticate(username, password);

            //Jeśli powyższe jest prawdziwe ("true") to dodajemy ciacho.
            if (result)
            {
                //Metoda "SetAuthCookie()" dodaje plik cookie do odpowiedzi dla przeglądarki, dzięki czemu użytkownik
                //nie musi uwierzytelniać się przy każdym żądaniu.
                FormsAuthentication.SetAuthCookie(username, false);
            }
            //Zwracamy "true".
            return result;
        }
    }
}