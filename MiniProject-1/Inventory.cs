using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MiniProject_1
{

    public class Inventory
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string PurchaseDate { get; set; }
        public float Price { get; set; }
        public string Currency { get; set; }
        public string Office { get; set; }

        public Inventory()
        {

        }
    }
}
