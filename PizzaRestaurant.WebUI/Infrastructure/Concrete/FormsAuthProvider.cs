using System.Web.Security;
using PizzaRestaurant.WebUI.Infrastructure.Abstract;



// Check if the login and password (parameters of the "Authenticate" method) are true (note in the "Web.config" file)
namespace PizzaRestaurant.WebUI.Infrastructure.Concrete
{
    public class FormsAuthProvider : IAuthProvider
    {
        public bool Authenticate(string username, string password)
        {
            bool result = FormsAuthentication.Authenticate(username, password);

            // If the above is true ("true") then we add a cake.
            if (result)
            {
                // The "SetAuthCookie ()" method adds a cookie to the browser response so that the user does not have to authenticate with every request.
                FormsAuthentication.SetAuthCookie(username, false);
            }
            // Returning "true"
            return result;
        }
    }
}