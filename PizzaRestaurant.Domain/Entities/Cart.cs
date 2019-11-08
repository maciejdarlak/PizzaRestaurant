using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



//Koszyk zakupów - tu są podstawowe działania, nie w kontrolerze.
namespace PizzaRestaurant.Domain.Entities
{
    public class Cart
    {
        //"lineCollection" - lista WSZYSTKICH produktów, "CartLine" - produkt x ilość
        private List<CartLine> lineCollection = new List<CartLine>();

        //Dodawanie  produktu x do zbioru z ID x (nawet jak jest pusty) 
        //NP. CZY JEST PIZZA DIAVOLLA?
        public void AddItem(Product product, int quantity) 
        {
            //"Line" - pojedyńczy egzemplarz z ID zgodnym z parametru. Szukamy w liście produktu, którego ID jest zgodne z tym w parametrze.
            //"FirstOrDefault()" pozwala wziąść z grupy tych samych ID (jak jest ich > 1) jeden egzemplarz który przypisujemy referencji "line". 
            //SPRAWDZAMY CZY JEST DIAVOLLA.
            CartLine line = lineCollection
                //Znalezienie kategorii produktu zgodnego z tym z parametru (to samo ID) - DIAVOLLA DO DIAVOLLI.
                .Where(p => p.Product.ProductID == product.ProductID)
                //Zwraca pierwszy element (z x ilości bo może być > 1 tego samego produktu) lub wartość domyślną jeżeli żaden element nie został odnaleziony
                .FirstOrDefault();

            //NIE MA DIAVOLLI
            if (line == null)
                //Dodanie obiektu "CartLine"
                lineCollection.Add(new CartLine { Product = product, Quantity = quantity });
            //Jeśli jest to do ilości "line" dodajemy ilość z parametru JEST DIAVOLLA
            else
                line.Quantity += quantity;
        }

        //Odejmowanie listy wszystkich produktów
        public void RemoveLine(Product product)  
        {
            lineCollection.RemoveAll(l => l.Product.ProductID == product.ProductID);
        }

        //Suma do zapłaty
        public decimal ComputeTotalValue()  
        {
            //Sumowanie listy dla każdego produktu  (jeden produkt = to co w nawiasie)
            return lineCollection.Sum(e => e.Product.Price * e.Quantity);  
        }

        //Czyszczenie zawartości koszyka
        public void Clear()  
        {
            lineCollection.Clear();
        }

        //Dostęp do zawartości koszyka - IEnumerable może po kolei przeglądać produkty
        public IEnumerable<CartLine> Lines  
        {
            get { return lineCollection; }
        }

        //Jeden produkt x ilość
        public class CartLine  
        {
            public Product Product { get; set; }
            public int Quantity { get; set; }
        }
    }
}