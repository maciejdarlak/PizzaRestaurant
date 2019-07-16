using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


//Informacja o stronach + ile stron potrzebujemy w ogóle (mając tutaj np. 4 produkty na stronę dla 23 produktów potrzeba 6 stron)
namespace PizzaRestaurant.WebUI.Models
{
    public class PagingInfo
    {
        //Info o ilości stron, ilości pozycji na stronę oraz aktualnej stronie
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }

        //Math.Ceiling() - zwraca najmniejszą wartość całkowitą, która jest większa niż lub równa podanej liczbie dziesiętnej.
        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
        }
    }
}