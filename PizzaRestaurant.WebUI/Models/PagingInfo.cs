using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



// Information about pages + how many pages are needed at all (for example, having 4 products per page for 23 products, 6 pages are needed)
namespace PizzaRestaurant.WebUI.Models
{
    public class PagingInfo
    {
        // Info about the number of pages, the number of items per page and the current page
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }

        // Math.Ceiling () - returns the smallest integer that is greater than or equal to the given decimal number.
        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
        }
    }
}