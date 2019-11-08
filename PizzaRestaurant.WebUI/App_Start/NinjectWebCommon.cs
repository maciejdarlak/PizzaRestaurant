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

        /// Poni�ej tworzymy egzemplarz obiektu kernel Ninject, pozwalaj�cego na rozwi�zywanie zale�no�ci i tworzenie nowych obiekt�w.
        /// Kiedy potrzebny jest nowy obiekt, do jego utworzenia b�dziemy u�ywa� Ninject, a nie s�owa kluczowego new.
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

        //Utworzenie pomostu mi�dzy klas� NinjectDependencyResolver i oferowan� przez platform� MVC obs�ug� mechanizmu wstrzykiwania zale�no�ci
        private static void RegisterServices(IKernel kernel)
        {
            System.Web.Mvc.DependencyResolver.SetResolver(new PizzaRestaurant.WebUI.Infrastructure.NinjectDepedencyResolver(kernel));
        }        
    }
}
