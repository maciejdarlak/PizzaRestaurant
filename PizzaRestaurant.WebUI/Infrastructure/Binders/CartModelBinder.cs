using System.Web.Mvc;
using PizzaRestaurant.Domain.Entities;




// This class creates the object "Cart"
namespace PizzaRestaurant.WebUI.Infrastructure.Binders
{
    class CartModelBinder : IModelBinder  // The IModelBinder interface defines one method: BindModel
    {
        private const string sessionKey = "Cart";

        // "ControllerContext" provides access to all data from the controller class, including customer request information.
        // It has the HttpContext property, which in turn contains the Session property that allows us to read and change session data.
        // The Cart object is obtained by reading the key value from the session data, and if this object is not there, it must be created.
        // "ModelBindingContext" provides data on the subject
        // the object model we are building and provides tools to facilitate this task.
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)  
        {
            // Get the Cart object from the session
            Cart cart = null;

            if (controllerContext.HttpContext.Session != null)
            {
                cart = (Cart)controllerContext.HttpContext.Session[sessionKey];
            }

            // Create the Cart object if it was not found in the session data
            if (cart == null)
            {
                cart = new Cart();
                if (controllerContext.HttpContext.Session != null)
                {
                    controllerContext.HttpContext.Session[sessionKey] = cart;
                }
            }

            // Return the cart
            return cart;
        }
    }
}