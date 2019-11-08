using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;



//Reprezentacja danych do wysyłki
namespace PizzaRestaurant.Domain.Entities
{
    public class ShippingDetails
    {
        //"[Required(ErrorMessage...] - atrybuty kontroli poprawności z przestrzeni nazw System.ComponentModel.DataAnnotations
        [Required(ErrorMessage = "Please enter last name.")]  
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter the first line of the address.")]
        [Display(Name = "Line 1")]
        public string Line1 { get; set; }
        [Display(Name = "Line 2")]
        public string Line2 { get; set; }
        [Display(Name = "Line 3")]
        public string Line3 { get; set; }

        [Required(ErrorMessage = "Please enter city name.")]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter the name of the province.")]
        [Display(Name = "Province")]
        public string State { get; set; }
        [Display(Name = "Postcode")]
        public string Zip { get; set; }

        [Required(ErrorMessage = "Please enter country name.")]
        [Display(Name = "Country")]
        public string Country { get; set; }

        public bool GiftWrap { get; set; }
    }
}