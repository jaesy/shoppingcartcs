using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Assignment_1_DRAFT
{
    partial class  ShoppingCart
    {
        private int recNo, stock, price;
        private string name;
        private List<Product> catalog = new List<Product>();
        private List<Product> cart = new List<Product>();
        public int cartCount;


          public void ShowCatalog(){
              Console.WriteLine("PRODUCT CATALOG:");
              Console.WriteLine("---------------------------------------");
              Console.WriteLine(" ID\t  Name\t\tStock\t  Price");
              Console.WriteLine("---------------------------------------");

              for (int i = 0; i < catalog.Count ; i++)
              {
                 Console.WriteLine("{0,3}{1,15}{2,10}{3,10}", catalog[i].ID, catalog[i].prodName, catalog[i].stock, catalog[i].price);
              }
              Console.WriteLine("---------------------------------------\n");
          }       

        public void ShowCart()
        {
            /* Before we show the Cart, methods are used to to make the cart look more clean. We first find duplicated products in the array and 
             * combine their qty amountand also another method to remove products that have a zero value(User might remove all qty in cart of a product
             * but the array is still there)
             */

            List<int> remove = new List<int>();//list to store locations of duplicated ID's we need to remove.

            //Cart obj are compared with other cart obj if a cart array number doesnt equal to the same cart array(To avoid comparing same array numbers).
            for (int i = 0; i < cart.Count; i++)
            {
                for (int k = 0; i < cart.Count; i++)
                {

                    if (k != i) {
                        if (cart[i].ID == cart[k].ID)
                        {
                            cart[i].qty += cart[k].qty;
                            remove.Add(k);//if same id exists, stored that array number to use later
                        }
                    }
                }
            }

            //Remove obj using the locations of the remove array list.
            for (int i = 0; i < remove.Count; i++)
            {
                cart.RemoveAt(remove[i]);
            }

                //remove all products with zero qty
                for (int i = 0; i < cart.Count; i++)
                {
                    if (cart[i].qty == 0)
                    {
                        RemoveProd(i);
                    }
                }

            Console.WriteLine("YOUR SHOPPING CART:");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine(" ID\t  Name\t\tQty\t  Sub-price");
            Console.WriteLine("---------------------------------------");

            for (int i = 0; i < cart.Count ; i++)
            {
                Console.WriteLine("{0,3}{1,15}{2,10}{3,10}", cart[i].ID, cart[i].prodName, cart[i].qty, cart[i].price);
            }
            Console.WriteLine("---------------------------------------\n");
            cartCount = cart.Count;
        }

        //return array numbers from an ID number paramenter.
        public int FindIDCatalog(int find)
        {

            int x = -1;
            for (int i = 0; i < catalog.Count ; i++)
            {
                if (catalog[i].ID == find)
                {             
                    x = i;
                }
            }
           return x;
        }

        public int FindIDCart(int find)
        {
            int x = -1;
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].ID == find)
                {
                    x = i;
                }
            }
            return x;
        }


        public void AddProd(int id, int rQty)//id parameter passes the array number of a product from catalog. 
        {

            int i = id;
                   cart.Add(new Product()
                       {
                           ID = catalog[i].ID,
                           prodName = catalog[i].prodName,
                           stock = catalog[i].stock,
                           qty = rQty,
                           price = (rQty * catalog[i].price)
                       }
                       );                     
        }

        public void RemoveProd(int id)
        {            
            cart.RemoveAt(id);
        }


        public bool UpdateQty(int a, int id)
        {
            bool x = false;
            if ((cart[id].qty - a) > -1)
            {
                cart[id].qty -= a;
                x = true;
            }
            return x;
        }

        //returns true if b(qty parameter) is less or equal to the stock number
        public bool CheckStock(int a, int b)
        {
            int qty = b;
            int stock = catalog[a].stock;

            bool dw = false;

            if ((stock - qty) >= 0)
            {
                dw = true;
            }
            return dw;
        }

        //ch parameter to choice whether to increase or decrease stock (0 - increase, 1 - decrease)
        public void UpdateStock(int a, int b, int ch)
        {
            if (ch == 0)
            {
                catalog[a].stock -= b;
            }
            else
            {
                catalog[a].stock += b;
            }
        }

        //Removes everything from the Qty and places it back on catalog stock (Used when user removes a product)
        public void AllQtyToStock(int a, int b)
        {
            catalog[b].stock += cart[a].qty;
        }

        public double TotalPrice()
        {
            double totPrice = 0;
            for (int i = 0; i < cart.Count; i++)
            {
                totPrice += cart[i].price;
            }
            return totPrice;
        }
    }
}
