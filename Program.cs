using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Assignment_1_DRAFT
{
    class Program
    {
        public static int choice;
        public static Options pointer = new Options();
      
        static void Main(string[] args)
        {
            Console.WriteLine("WELCOME TO CONSOLE-BASED SHOPPING CART\n");
            choice = 0;
            
            //Try-block to handle any errors and terminate program when error occurs (e.g. missing xml file)
            try
            {
                pointer.FileReadM();
            do
            {

                pointer.ShowCatalogM();            
                Console.WriteLine("\nFollowing options are available:\n");
                Console.WriteLine("1. Add an item to cart");
                Console.WriteLine("2. Remove an item from the cart");
                Console.WriteLine("3. View the cart");
                Console.WriteLine("4. Checkout and Pay");
                Console.WriteLine("5. Exit\n");

                try
                {
                    choice = int.Parse(Console.ReadLine());
                    //Valdation input of only an integer between 1 and 5.
                    if (choice > 5 || choice <= 0)
                    {
                        Console.WriteLine("Invalid Choice. Please Choose a Valid Option");
                    }
                    else
                    {
                        
                        switch (choice)
                        {
                            case 1: 
                                pointer.AddItem(); 
                                break;
                            case 2: 
                                pointer.RemoveItem(); 
                                break;
                            case 3: 
                                pointer.ViewCart(); 
                                break;
                            case 4:
                                //CheckoutAndPay returns bool to avoid processing empty cart.
                                if
                                (pointer.CheckoutAndPay() == true)
                                {
                                    choice = 5;//exit program once processing is complete.
                                }
                                break;              
                        }
                    }
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Invalid Type. Please Try Again." + e);
                }
                catch (IndexOutOfRangeException e)
                {
                    Console.WriteLine(e);
                }             
            } while (choice != 5);
            pointer.UpdateXMLM();
            Console.Beep();

        }catch (Exception) 
        {
            Console.WriteLine("Something went wrong.");
            Console.WriteLine("Press any key to terminate program...");
            Console.ReadKey();

        }

        }
    }
}
