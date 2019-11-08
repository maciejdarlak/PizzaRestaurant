using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



//Proces uwierzytelniania, żeby być administratorem.
//Wydzielamy metody statyczne z kontrolera (dot.logowania jako admin) bo testy jednostkowe nie akceptują tych metod.
//Metoda "Authenicate()" pozwala sprawdzić dane uwierzytelniania podane przez użytkownika (login i hasło zawarte w pliku "Web.config" - w życiu robi się inaczej).
namespace PizzaRestaurant.WebUI.Infrastructure.Abstract
{
    public interface IAuthProvider
    {
        bool Authenticate(string username, string password);
    }
}
