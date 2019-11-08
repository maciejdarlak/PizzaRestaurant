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
        //Poniższe metadane projektu uniemożliwiają wyświetlenie tej właściwości w widoku "Edit" (administracja).
        [HiddenInput(DisplayValue =false)]
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Please enter a product name.")]
        public string Name { get; set; }

        //Atrybut "DataType" pozwala zdefiniować sposób prezentowania i edytowania wartości - tu umożliwiliśmy rozszerzenie o więcej 
        //niż 1 linijka tekstu w widoku "Edit" (administracja).
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

        //Poniżej nowe właściwości pomocne do przesyłania zdjęć pizzy
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
    }
}