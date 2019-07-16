using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc; //Stąd jest MvcHtmlString
using PizzaRestaurant.WebUI.Models;



namespace PizzaRestaurant.WebUI.HtmlHelpers
{
    public static class PagingHelpers
    {
        //Metoda rozszerzająca - dzięki helperom unikmy powielania kodu - jak chcemy coś zmienić wystarczy zmienić tylko w helperze
        //"This" wskazuje zawsze na akualną klasę a nie bazową (wtedy "base")
        //PageInfo - info o ilości stron, ilości pozycji na stronę oraz aktualnej stronie
        //Func - jest delegatem akceptującym int jako jedyny parametr i zwracającym string
        //Do delegata można podpiść dowolną metodę z identycznym typem zwrtu i parametru, tu metoda int zwracająca string
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl)
        {
            //StringBuilder - nie tworzy nowego obiektu w pamięci, ale dynamicznie rozszerza potrzebną pamięć, aby pomieścić zmodyfikowany lub nowy ciąg (oszczędność pamięci)
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                //TagBuilder to klasa specjalnie zaprojektowana do tworzenia tagów html i ich zawartości, łatwiejsza w obsłudze niż StringBuilder
                //Dla każdej strony (TotalPages) tworzony jest element HTML a (czyli link) 
                TagBuilder tag = new TagBuilder("a"); //Mamy: < a ></ a >

                //z linkiem wygenerowanym przez metodę pageUrl (przekazywaną w argumencie)
                //MergeAttribute - dodaje nowy atrybut do znacznika
                tag.MergeAttribute("href", pageUrl(i)); //Mamy: <a href="www.system.com/page/1" ></a>

                //i treścią ustawioną na numer strony
                //InnerHtml - pobiera (jak tu bo i = 1 na początku) lub ustawia zawartość znalezioną między znacznikami otwierającymi i zamykającymi określonej kontrolki serwera HTML
                tag.InnerHtml = i.ToString(); //Mamy: <a href="www.system.com/page/1" >1</a>

                //Dodatkowo, obecnie wybrana strona (CurrentPage) otrzymuje dodatkową klasę CSS selected
                if (i == pagingInfo.CurrentPage)
                {
                    tag.AddCssClass("selected"); //Mamy: <a href="www.system.com/page/1" class="selected">1</a>
                    tag.AddCssClass("btn-primary");
                }
                tag.AddCssClass("btn btn-default");
                //Append - dołącza kopię określonego ciągu dla tego wystąpienia.
                result.Append(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}