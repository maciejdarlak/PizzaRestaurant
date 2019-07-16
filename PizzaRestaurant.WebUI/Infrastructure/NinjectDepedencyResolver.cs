using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using PizzaRestaurant.Domain.Concrete;
using PizzaRestaurant.Domain.Abstract;
using PizzaRestaurant.Domain.Entities;
using Moq;



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

        //Tu miejsce na powiązania
        private void AddBindings()
        {
            kernel.Bind<IProductRepository>().To<EFProductRepository>();
        }
    }
}