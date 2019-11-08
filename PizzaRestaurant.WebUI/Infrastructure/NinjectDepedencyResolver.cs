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

        //Tu miejsce na konkretne powiązania.      
        private void AddBindings()
        {
            //Poniżej każdorazowe odwołanie sie do "IProductRepository" (a ma go konstruktor kontrolera klasy z tym interfejsem) daje podłączenie 
            //do "EFProductRepository" a ten do bazy danych.
            //Tworzymy egzemplarz klasy "EFProductRepository" w odpowiedzi na każde żądania udostępnienia interfejsu "IProductRepository".
            kernel.Bind<IProductRepository>().To<EFProductRepository>();

            //Tworzymy obiekt "EmailSettings", który wykorzystujemy w metodzie "Ninject" "WithConstructorArgument()"          
            EmailSettings emailSettings = new EmailSettings
            {
                //Formularz jest wypełniony lub nie (false).
                //zdefiniowaliśmy wartość wyłącznie dla jednej właściwości "EmailSettings: WriteAsFile".
                //Odczytujemy wartość tej właściwości za pomocą "ConfigurationManager.AppSettings", 
                //która pozwala odwoływać się do ustawień aplikacji umieszczonych w pliku "Web.config"(tego w głównym katalogu projektu).
                WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            };
            //Obiekt "emailSettings" (utworzony powyżej) wstrzykujemy do konstruktora "EmailOrderProcessor" w momencie tworzenia nowego 
            //egzemplarza w odpowiedzi na żądanie interfejsu "IOrderProcessor".
            kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>().WithConstructorArgument("settings", emailSettings);

            //Tworzymy egzemplarz klasy "FormsAuthProvider" w odpowiedzi na każde żądania udostępnienia interfejsu "IAuthProvider".
            kernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
        }
    }
}