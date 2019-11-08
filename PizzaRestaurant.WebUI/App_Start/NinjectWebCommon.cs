[assembly: WebActivator.PreApplicationStartMethod(typeof(PizzaRestaurant.WebUI.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(PizzaRestaurant.WebUI.App_Start.NinjectWebCommon), "Stop")]

namespace PizzaRestaurant.WebUI.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// Poni¿ej tworzymy egzemplarz obiektu kernel Ninject, pozwalaj¹cego na rozwi¹zywanie zale¿noœci i tworzenie nowych obiektów.
        /// Kiedy potrzebny jest nowy obiekt, do jego utworzenia bêdziemy u¿ywaæ Ninject, a nie s³owa kluczowego new.
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            
            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>

        //Utworzenie pomostu miêdzy klas¹ NinjectDependencyResolver i oferowan¹ przez platformê MVC obs³ug¹ mechanizmu wstrzykiwania zale¿noœci
        private static void RegisterServices(IKernel kernel)
        {
            System.Web.Mvc.DependencyResolver.SetResolver(new PizzaRestaurant.WebUI.Infrastructure.NinjectDepedencyResolver(kernel));
        }        
    }
}
