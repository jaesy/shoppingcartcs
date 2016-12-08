using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_1_DRAFT
{
    class Options
    {
        public ShoppingCart cata = new ShoppingCart();

        public void FileReadM()
        {
            cata.FileRead();
        }
        public void ShowCatalogM()
        {
            cata.ShowCatalog();
        }

        public void UpdateXMLM()
        {
            cata.UpdateXML();
        }

        /* Shopping Cart and Catalog Cart are updated in real-time to make sure the user never adds more than
         * whats available in the stock. Although the catalog is temporary stored in an array list and not
         * written in its XML file until the order has completed the CheckOutAndPay method.      
         */
        public void AddItem()
        {
            
            int tempID = 0;
            int qty = 0;
            try
            {
                bool option = false; //Used to break from 'do' loop. break-out of loop if true.
                do
                {
                    Console.WriteLine("Please enter ID of the Product from the Catalog: ");
                    tempID = int.Parse(Console.ReadLine());

                    /* idA stores the array number of the tempID. -1 used as default because array starts at 0.
                     * findIDE method will loop through the stock arrays to find if product of tempID exists.
                     * if product not found, idA will stay as -1 value avoiding the following 'if' statment.*/
                    
                    int idA = -1;
                    idA = cata.FindIDCatalog(tempID);
                    
                    if (idA != -1)
                    {

                        Console.WriteLine("Please enter Quantity of the Product: ");
                        qty = int.Parse(Console.ReadLine());
                        //Validation for correct qty inputs. 
                        //Updating and checking stock so user is never adding more than stock
                        if (qty > 0)
                        {
                            if (cata.CheckStock(idA, qty) == true)
                            {
                                cata.AddProd(idA, qty);
                                cata.UpdateStock(idA, qty, 0);
                                Console.WriteLine("Product Added.");
                                option = true;
                                Console.WriteLine("\nPress any key to continue...");
                                Console.ReadKey();
                            }
                            else {Console.WriteLine("Not enough Stock."); option = true;}
                        }
                        else
                        {
                            Console.WriteLine("You must select one or more qty. Try again.");
                        }
                    }
                    else 
                    { 
                        Console.WriteLine("Could not find Product ID.");
                        Console.WriteLine("Press any key to go back to menu...");
                        Console.ReadKey();
                        option = true;
                    }
                } while (option == false);

            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid Option\n");
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine("Error: " + e);
            }           
        }
      
        public void RemoveItem()
        {
            cata.ShowCart();
            int tempID;
            try
            {
                Console.WriteLine("Enter ID you want to Remove: ");
                tempID = int.Parse(Console.ReadLine());
                int id = -1;
                id = cata.FindIDCart(tempID);
              
                if (id != -1)
                {
                    Console.WriteLine("Would you like to remove all quantity (Y/N)?");
                   bool x = false;
                   //do-loop used to valid and make use user is entering the right information
                   //e.g. entered integers that are within range (remove integer <= than cart qty).
                   do{
                    try
                    {
                        String tempR = Console.ReadLine();
                        char resp = Convert.ToChar(tempR);
                        resp = Char.ToUpper(resp); //user can enter lower or upper case

                        switch (resp)
                        {
                            case 'Y':
                                try
                                {
                                    /* FindIDcatalog method finds array[n] of ID in the Catalog Cart so
                                     * can transfer integer of qty in shopping cart back to catalog stock
                                     */
                                    int catID = -1;
                                    catID = cata.FindIDCatalog(tempID);
                                    cata.AllQtyToStock(id, catID);
                                    cata.RemoveProd(id);
                                    Console.WriteLine("Product removed from the Cart.\n");
                                }
                                catch (IndexOutOfRangeException)
                                {
                                    Console.WriteLine("Product from catalog missing.");
                                }
                                x=true ;
                                break;

                            case 'N': 
                                Console.WriteLine("Enter qty you want to remove: ");
                                int q = int.Parse(Console.ReadLine());

                                if (cata.UpdateQty(q, id) == true)//returns bool if int returns less than zero other wise int is higher that qty available
                                {
                                    try
                                    {
                                        int catID = -1;
                                        catID = cata.FindIDCatalog(tempID);
                                        cata.UpdateStock(catID, q, 1);
                                        Console.WriteLine("Product removed from the Cart.\n");
                                    }
                                    catch (IndexOutOfRangeException)
                                    {
                                        Console.WriteLine("Product from catalog missing.");
                                    }
                                    x = true;
                                }
                                else 
                                { 
                                    Console.WriteLine("You Entered Higher than your Qty amount.\n");
                                    Console.WriteLine("Press any key to go back to menu...");
                                    Console.ReadKey();
                                    x = true;                                 
                                }
                               
                                break;

                            default: 
                                Console.WriteLine("Invalid Input. Press Y (for Yes) or N (for No) on your keyboard. ");
                                break;                        
                        }

                    }catch(FormatException){
                        Console.WriteLine("Invalid Input. Try again");
                    }

                   }while(x == false);
                }
                else
                {
                    Console.WriteLine("Could not find ID.");
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine(e);
            }
        }


        public void ViewCart()
        {
            cata.ShowCart();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public bool CheckoutAndPay()
        {
            bool choice = false;
            cata.ShowCart();
            int count = cata.cartCount;
            
            if (count >= 1) //To make sure cart is not empty.
            {
                
                double total = cata.TotalPrice();
                Console.WriteLine("Your total price is: " + total);
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.WriteLine("YOUR ORDER HAS BEEN PROCESSED.THANK YOU FOR SHOPPING WITH US.\n\n\n");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                choice = true;
            }
            else 
            {
                Console.WriteLine("Your Cart Is Empty.\n\n");
            }
            return choice;
        
        }

    }
}
