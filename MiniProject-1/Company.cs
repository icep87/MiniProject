using System;
using System.Collections.Generic;

namespace MiniProject_1
{
    public class Company
    {
        public List<Office> Offices = new List<Office>();
        public List<Currency> Currencies = new List<Currency>()
        {
            new Currency { Name = "SEK", Price = 8.49f},
            new Currency { Name = "PLN", Price = 3.79f}
        };

        public List<Devicetype> DeviceType = new List<Devicetype>()
        {
            new Devicetype { Name = "Mobile Device", shortName = "mobile" },
            new Devicetype { Name = "Laptop Computers", shortName = "laptop" }
        };

        public Company()
        {
            Offices.Add(new Office { Name = "Malmoe", Country = "Sweden", Currency = "SEK" });
            Offices.Add(new Office { Name = "Warsaw", Country = "Poland", Currency = "PLN" });
            Offices.Add(new Office { Name = "New York", Country = "USA", Currency = "USD" });
        }

        public void GetOfficeList()
        {

            Console.WriteLine("Select office location for the product");
            Console.WriteLine("");

            for (int i = 0; i < Offices.Count; i++)
            {
                Console.WriteLine("{0}) {1}", i, Offices[i].Name);
            }
        }

        public float CurrencyConverter(string Currency, float Price)
        {
            var currencyInfo = Currencies.Find(x => x.Name == Currency);
            float localCurrency = Price * currencyInfo.Price;

            return localCurrency;
        }

        public void GetDeviceTypeList()
        {

            Console.WriteLine("Select device type for the inventory item");
            Console.WriteLine("");

            for (int i = 0; i < DeviceType.Count; i++)
            {
                Console.WriteLine("{0}) {1}", i, DeviceType[i].Name);
            }
        }
    }
}
