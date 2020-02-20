using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



// Authentication process to be an administrator.
// Static methods are separated from the controller (for logging in as admin) because unit tests do not accept these methods.
// The "Authenicate ()" method allows you to check the user credentials (username and password contained in the "Web.config" file - things get different in life).
namespace PizzaRestaurant.WebUI.Infrastructure.Abstract
{
    public interface IAuthProvider
    {
        bool Authenticate(string username, string password);
    }
}
