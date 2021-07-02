using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace MiniProject_1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Load the inventory list
            List<Inventory> InventoryList = new List<Inventory>
            {
                new Laptop { Type = "Laptop Computers", Name = "Macbook Pro", Currency = "SEK", Price = 12500f, Office = "Malmoe", PurchaseDate = "20/07/2018" },
                new Laptop { Type = "Laptop Computers", Name = "Macbook Air", Currency = "PLN", Price = 4500f, Office = "Warsaw", PurchaseDate = "20/10/2018" },
                new Mobilephone { Type = "Mobile Phones", Name = "iPhone 11", Currency = "SEK", Price = 14000f, Office = "Malmoe", PurchaseDate = "29/04/2021" },
                new Mobilephone { Type = "Mobile Phones", Name = "iPhone 11 Max", Currency = "SEK", Price = 18990f, Office = "Malmoe", PurchaseDate = "30/11/2020" },
                new Mobilephone { Type = "Mobile Phones", Name = "iPhone 12", Currency = "PLN", Price = 5000f, Office = "Warsaw", PurchaseDate = "15/12/2020" }
            };

            Company company = new();

            Console.WriteLine("Welcome to the inventory managment application");
            Console.WriteLine();
            while (true)
            {
                Console.WriteLine("1) Inventory");
                Console.WriteLine("2) List Offices");
                Console.WriteLine("3) Exchange Rates");
                ConsoleMessage("Write \"q\" to exit", "info");
                Console.WriteLine();
                Console.Write("Please choose action: ");

                string actionMethod = Console.ReadLine();

                if (actionMethod == "1")
                {
                    while (true)
                    {
                        Console.WriteLine();
                        Console.WriteLine("1) Add to Inventory");
                        Console.WriteLine("2) List Inventory");
                        ConsoleMessage("Write \"q\" to exit", "info");
                        Console.WriteLine();
                        Console.Write("Please choose action: ");

                        string inventoryAction = Console.ReadLine();

                        if (inventoryAction == "1")
                        {
                            Console.WriteLine();
                            Console.WriteLine("Adding to inventory");
                            Console.WriteLine();
                            Console.WriteLine();

                            AddItem(InventoryList);
                            Console.WriteLine();
                        }
                        else if (inventoryAction == "2")
                        {
                            Console.WriteLine();
                            Console.WriteLine("Listing Inventory");
                            ConsoleMessage("Sorted by office and then by purchase date", "info");
                            ListInventory(InventoryList);
                            Console.WriteLine();
                        }
                        else if (inventoryAction.ToLower() == "q")
                        {
                            ListInventory(InventoryList);
                            Console.WriteLine();
                            break;
                        }
                        else
                        {
                            IncorrectInputWarning();
                        }
                    }
                }
                else if (actionMethod == "2")
                {
                    Console.WriteLine("");
                    Console.WriteLine("Offices:");
                    Console.WriteLine();

                    foreach (var item in company.Offices)
                    {
                        Console.WriteLine("\nName: {0}\nCountry: {1}\nCurrency: {2}", item.Name, item.Country, item.Currency);
                    }
                    Console.WriteLine();
                }
                else if (actionMethod == "3")
                {
                    Console.WriteLine("");
                    Console.WriteLine("Currency Exchange:");
                    ConsoleMessage("Exchange rates towards USD", "info");
                    Console.WriteLine();

                    foreach (var item in company.Currencies)
                    {
                        Console.WriteLine("\nName: {0}\nRate: {1}", item.Name, item.Price);
                    }
                    Console.WriteLine();
                }
                else if (actionMethod.ToLower() == "q")
                {
                    Console.WriteLine();
                    break;
                }
                else
                {
                    IncorrectInputWarning();
                }
            }
        }

        private static void ListInventory(List<Inventory> InventoryList)
        {
            foreach (var item in InventoryList.OrderBy(x => x.Office).ThenBy(x => DateTime.ParseExact(x.PurchaseDate, "d/M/yyyy", CultureInfo.InvariantCulture)))
            {
                //Check warranty end period and color the item if necessary, yellow for 6 months, red for 3 months.
                DateTime purchaseDate = DateTime.ParseExact(item.PurchaseDate, "d/M/yyyy", CultureInfo.InvariantCulture);
                DateTime warrantyEndDate = purchaseDate.AddYears(3);

                if (warrantyEndDate.AddMonths(-3) < DateTime.Now)
                {
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (warrantyEndDate.AddMonths(-6) < DateTime.Now)
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                };

                Console.WriteLine("\nName: {0} \nType: {1} \nPrice: {2} \nCurrency: {3} \nPurchase date: {4} \nOfficename: {5}",
                                  item.Name,
                                  item.Type,
                                  item.Price.ToString(),
                                  item.Currency,
                                  item.PurchaseDate,
                                  item.Office);
                Console.ResetColor();
            }
        }

        private static void ConsoleMessage(string message, string messageType = null)
        {
            switch (messageType)
            {
                case "info":
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
                case "warning":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                default:
                    break;
            }

            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void IncorrectInputWarning()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Incorrect input, please choose correction action");
            Console.ResetColor();
        }

        public static void AddItem(List<Inventory> InventoryList)
        {
            Console.WriteLine("Please provide details about the item you like to add to inventory.");
            Console.WriteLine("");

            Company companyDetails = new Company();

            //
            //Office section
            //
            //Get a list of offices 
            companyDetails.GetOfficeList();

            string OfficeName = null;
            string OfficeCurrency = null;

            while (OfficeName == null)
            {
                try
                {
                    //Ask for officeID 
                    Console.WriteLine("");
                    Console.Write("Office ID: ");
                    int officeIndex = int.Parse(Console.ReadLine());
                    OfficeName = companyDetails.Offices[officeIndex].Name;
                    OfficeCurrency = companyDetails.Offices[officeIndex].Currency;

                }
                catch (Exception ex)
                {
                    ConsoleMessage("Incorrect Office ID, please try again", "warning");
                    Console.WriteLine(ex.Message);
                }
            }
            //
            //Type section
            //
            //Get a list of device types
            companyDetails.GetDeviceTypeList();

            string Type = null;

            while (Type == null)
            {
                try
                {
                    //Ask for typeID 
                    Console.WriteLine("");
                    Console.Write("Type ID: ");
                    int typeIndex = int.Parse(Console.ReadLine());
                    Type = companyDetails.DeviceType[typeIndex].Name;

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Incorrect Office ID, please try again");
                    Console.WriteLine(ex.Message);
                }
            }

            //
            //Name section
            //
            Console.Write("Name: ");
            string Name = Console.ReadLine();

            //
            //Price section
            //
            float Price = 0;
            bool PriceCheck = true;

            while (PriceCheck)
            {
                Console.Write("Please provide product price with decimals (999.99) in USD: ");
                string PriceString = Console.ReadLine();
                if (!Regex.IsMatch(PriceString, @"^\d+(\.\d{1,2})?$"))
                {
                    ConsoleMessage("Incorrect price format, following formats are accepted: 9.99, 9", "warning");
                }
                else
                {
                    //Console ReadLine returns a string and we want to store the price as a float. Convert to float. 
                    Price = float.Parse(PriceString);

                    //Check what currency has and if conversion is necessary
                    if (OfficeCurrency != "USD")
                    {
                        var localCurrency = companyDetails.CurrencyConverter(OfficeCurrency, Price);
                        Price = localCurrency;
                    }
                    //Exit the PriceCheck loop.
                    PriceCheck = false;
                }
            }

            //
            //Purchase Date
            //
            string PurchaseDate = "";
            bool DateCheck = true;

            while (DateCheck)
            {
                Console.Write("Please provide purchase date in following format DD/MM/YYYY: ");
                string DateString = Console.ReadLine();
                if (!validateDate(DateString))
                {
                    ConsoleMessage("Incorrect date", "warning");
                }
                else
                {
                    PurchaseDate = DateString;
                    //Exit the DateCheck loop.
                    DateCheck = false;
                }
            }

            AddItem(InventoryList, Name, Type, Price, PurchaseDate, OfficeName, OfficeCurrency);
        }

        public static void AddItem(List<Inventory> InventoryList, string Name, string Type, float Price, string PurchaseDate, string OfficeName, string OfficeCurrency)
        {
            try
            {
                if (Type == "Mobile Device")
                {
                    InventoryList.Add(new Mobilephone { Type = Type, Name = Name, Currency = OfficeCurrency, Price = Price, Office = OfficeName, PurchaseDate = PurchaseDate });
                }
                else if (Type == "Laptop Computers")
                {
                    InventoryList.Add(new Laptop { Type = Type, Name = Name, Currency = OfficeCurrency, Price = Price, Office = OfficeName, PurchaseDate = PurchaseDate });
                }

                Console.WriteLine();
                Console.WriteLine("Added following product");
                Console.WriteLine("\nName: {0} \nType: {1} \nPrice: {2} \nCurrency: {3} \nPurchaseDate: {4} \nOfficeName: {5}",
                                  Name,
                                  Type,
                                  Price.ToString(),
                                  OfficeCurrency,
                                  PurchaseDate,
                                  OfficeName);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                ConsoleMessage("Could not add product to the list", "Warning");
            }



        }

        public static bool validateDate(string date)
        {
            string pattern = @"^(3[01]|[12][0-9]|0?[1-9])\/(1[0-2]|0?[1-9])\/(?:[0-9]{2})?[0-9]{2}$";
            if (Regex.IsMatch(date, pattern))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}