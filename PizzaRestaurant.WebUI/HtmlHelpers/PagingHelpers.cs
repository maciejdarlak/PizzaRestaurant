using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc; //Stąd jest MvcHtmlString
using PizzaRestaurant.WebUI.Models;



//Konfiguracja TYLKO przycisków do nawigacji stronami (1,2,3....n)
namespace PizzaRestaurant.WebUI.HtmlHelpers
{
    public static class PagingHelpers
    {
        //Metoda rozszerzająca - dzięki helperom unikmy powielania kodu - jak chcemy coś zmienić wystarczy zmienić tylko w helperze.      

        //"This" wskazuje zawsze na aktualną klasę (tu rozszerza klasę HtmlHelper) a nie bazową (wtedy "base").

        //"PageInfo" - info o ilości stron, ilości pozycji na stronę oraz aktualnej stronie.

        //"Func" - jest delegatem akceptującym "int" jako jedyny parametr i zwracającym "string".
        //Do delegata można podpiść dowolną metodę z identycznym typem zwrtu i parametru, tu metoda typu "int" zwracająca "string".
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl)
        {
            //Tu używamy "StringBuilder" - jest to rozciągliwy obiekt - na koniec metody wklejamy do niego finalny ciąg (link - długośc linków zmienia sie dynamicznie)
            StringBuilder result = new StringBuilder();

            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                //"TagBuilder" to klasa specjalnie zaprojektowana do tworzenia tagów html i ich zawartości, łatwiejsza w obsłudze niż "StringBuilder".
                //Dla każdej strony ("TotalPages") tworzony jest element HTML a (czyli link).
                //Mamy: < a ></ a >
                TagBuilder tag = new TagBuilder("a");

                //Z linkiem wygenerowanym przez metodę "pageUrl()" (przekazywaną w argumencie)
                //"MergeAttribute" - dodaje nowy atrybut do znacznika
                //Mamy: <a href="www.system.com/page/1" ></a>
                tag.MergeAttribute("href", pageUrl(i));

                //I treścią ustawioną na numer strony
                //"InnerHtml" - pobiera (jak tu, bo i = 1 na początku) lub ustawia zawartość znalezioną między znacznikami otwierającymi i zamykającymi określonej kontrolki serwera HTML
                //Mamy: <a href="www.system.com/page/1" >1</a>
                tag.InnerHtml = i.ToString();

                //Dodatkowo, obecnie wybrana strona (CurrentPage) otrzymuje dodatkową klasę CSS selected.
                //!!!!!!!!!!!!!!!!!!!!!!!    Tu jest POŁĄCZENIE klawiszy nawigacji z resztą świata  !!!!!!!!!!!!!!!!!!!!!!!
                if (i == pagingInfo.CurrentPage)  
                {
                    //Mamy: <a href="www.system.com/page/1" class="selected">1</a>
                    tag.AddCssClass("selected");
                    //Przycisk "wyrózniajacy sie na tle innych"
                    tag.AddCssClass("btn-primary"); 
                }

                //Przycisk "domyślny"
                tag.AddCssClass("btn btn-default");

                //"Append()"- dodaje lub dołącza ciąg do "StringBuilder".
                result.Append(tag.ToString());
            }
            //Poniżej robimy ciąg zakodowany w HTML (link Html) ze stringa powyżej.
            //"MvcHtmlString" reprezentuje ciąg zakodowany w formacie HTML, który nie powinien być ponownie kodowany.
            return MvcHtmlString.Create(result.ToString());
        }
    }
}