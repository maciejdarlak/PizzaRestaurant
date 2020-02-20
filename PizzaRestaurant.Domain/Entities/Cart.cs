using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



//Shopping cart - here are the basic activities, not in the controller.
namespace PizzaRestaurant.Domain.Entities
{
    public class Cart
    {
        //"lineCollection" - list of ALL products, "CartLine" - product * quantity
        private List<CartLine> lineCollection = new List<CartLine>();

        // Add product x to file with ID x (even if it's empty)
        // NP. IS PIZZA DIAVOLLA?
        public void AddItem(Product product, int quantity) 
        {
            // "Line" - a single copy with an ID consistent with the parameter. Search in the product list whose ID matches the one in the parameter.
            // "FirstOrDefault ()" allows to take from the group of the same ID (as there are> 1) one copy which we assign the reference "line".
            // CHECKING IS DIAVOLLA.
            CartLine line = lineCollection
                //Finding a product category according to this parameter (same ID) - DIAVOLLA TO DIAVOLLA.
                .Where(p => p.Product.ProductID == product.ProductID)
                //Returns the first item (of x quantity because there may be > 1 of the same product) or the default value if no item was found
                .FirstOrDefault();

            //THERE IS NO DIAVOLLA
            if (line == null)
                //Adding the "CartLine" object
                lineCollection.Add(new CartLine { Product = product, Quantity = quantity });
            //If it is, then to the "line" quantity we add the quantity from the parameter IS DIAVOLLA
            else
                line.Quantity += quantity;
        }

        //Subtract list of all products
        public void RemoveLine(Product product)  
        {
            lineCollection.RemoveAll(l => l.Product.ProductID == product.ProductID);
        }

        //Amount to be paid
        public decimal ComputeTotalValue()  
        {
            //Adding a list for each product (one product = what's in brackets)
            return lineCollection.Sum(e => e.Product.Price * e.Quantity);  
        }

        //Clearing the cart contents
        public void Clear()  
        {
            lineCollection.Clear();
        }

        //Access to the contents of the cart - IEnumerable can view products in turn
        public IEnumerable<CartLine> Lines  
        {
            get { return lineCollection; }
        }

        //One product * quantity
        public class CartLine  
        {
            public Product Product { get; set; }
            public int Quantity { get; set; }
        }
    }
}