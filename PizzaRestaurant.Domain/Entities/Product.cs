using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;



namespace PizzaRestaurant.Domain.Entities
{
    public class Product
    {
        //The following project metadata prevents this property from being displayed in the "Edit" (administration) view.
        [HiddenInput(DisplayValue =false)]
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Please enter a product name.")]
        public string Name { get; set; }

        // The "DataType" attribute allows you to define how to present and edit the value - here it is possible to extend it by more than 1 line of text in the "Edit" view (administration).
        [DataType(DataType.MultilineText), Display(Name = "Description")]
        [Required(ErrorMessage = "Please add a description.")]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price.")]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Please specify a category.")]
        [Display(Name = "Category")]
        public string Category { get; set; }

        //New features below are helpful for sending pizza photos
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
    }
}