using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using PizzaRestaurant.Domain.Concrete;
using PizzaRestaurant.Domain.Abstract;
using PizzaRestaurant.Domain.Entities;
using Moq;
using PizzaRestaurant.WebUI.Infrastructure.Abstract;
using PizzaRestaurant.WebUI.Infrastructure.Concrete;



namespace PizzaRestaurant.WebUI.Infrastructure
{
    public class NinjectDepedencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDepedencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        //Here is the place for specific connections     
        private void AddBindings()
        {
            // Below, each reference to "IProductRepository" (it has a class controller constructor with this interface) gives you a connection to "EFProductRepository" and this to the database.
            // A copy of the "EFProductRepository" class is created in response to each request to provide access to the "IProductRepository" interface.
            kernel.Bind<IProductRepository>().To<EFProductRepository>();

            // An "EmailSettings" object is created, which is used in the "Ninject" method "WithConstructorArgument ()"        
            EmailSettings emailSettings = new EmailSettings
            {
                // The form is completed or not (false).
                // The value has been defined only for one property "EmailSettings: WriteAsFile".
                // The value of this property is read using "ConfigurationManager.AppSettings", which allows you to refer to the application settings located in the "Web.config" file 
                //(the one in the main project directory).
                WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            };
            // The "emailSettings" object (created above) is injected into the "EmailOrderProcessor" constructor when creating a new one copy 
            //in response to a "IOrderProcessor" interface request.
            kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>().WithConstructorArgument("settings", emailSettings);

            // A copy of the "FormsAuthProvider" class is created in response to each request to provide access to the "IAuthProvider" interface.
            kernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
        }
    }
}