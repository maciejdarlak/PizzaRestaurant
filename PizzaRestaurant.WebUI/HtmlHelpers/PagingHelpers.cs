﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc; //Hence it MvcHtmlString
using PizzaRestaurant.WebUI.Models;



// Configuration ONLY buttons for navigating pages (1,2,3 .... n)
namespace PizzaRestaurant.WebUI.HtmlHelpers
{
    public static class PagingHelpers
    {
        // Extending method - thanks to helpers you can avoid duplication of code.

        // "This" always points to the current class (here it extends the HtmlHelper class) and not the base class (then "base").

        // "PageInfo" - info about the number of pages, the number of items per page and the current page.

        // "Func" - is a delegate accepting "int" as the only parameter and returning "string".
        // You can sign any method with the same return type and parameter to the delegate, here the "int" method returns "string".
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl)
        {
            // "StringBuilder" - this is a stretchy object - at the end of the method pasted the final string (link - the length of links changes dynamically)
            StringBuilder result = new StringBuilder();

            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                // "TagBuilder" is a class specially designed for creating html tags and their content, easier to use than "StringBuilder".
                // For each page ("TotalPages") an HTML element is created (i.e. a link).
                // So it is: <a> </ a>
                TagBuilder tag = new TagBuilder("a");

                // With the link generated by the "pageUrl ()" method (passed in the argument)
                // "MergeAttribute" - adds a new attribute to the tag
                // We have: <a href="www.system.com/page/1"> </a>
                tag.MergeAttribute("href", pageUrl(i));

                // And the content set to the page number
                // "InnerHtml" - gets (like here because i = 1 at the beginning) or sets the content found between the opening and closing tags of the specified HTML server control
                // So it is: <a href="www.system.com/page/1"> 1 </a>
                tag.InnerHtml = i.ToString();

                // In addition, the currently selected page (CurrentPage) receives an additional CSS selected class.
                // !!!!!!!!!!!!!!!!!!!!!!! Here is the COMBINATION of the navigation keys with the rest of the world !!!!!!!!!!!!!!!!!!!!!!!
                if (i == pagingInfo.CurrentPage)  
                {
                    // It's here: <a href="www.system.com/page/1" class="selected"> 1 </a>
                    tag.AddCssClass("selected");
                    //"Stand out from the others" button
                    tag.AddCssClass("btn-primary"); 
                }

                //"Default" button
                tag.AddCssClass("btn btn-default");

                //"Append ()" - adds or appends a string to "StringBuilder".
                result.Append(tag.ToString());
            }
            // Below is the string encoded in HTML (Html link) from the string above.
            // "MvcHtmlString" represents a string encoded in HTML that should not be re-encoded.
            return MvcHtmlString.Create(result.ToString());
        }
    }
}