using System;
using System.Collections.Generic;

namespace T8_Builder_DesignPatterns.Models
{
    public class ReadyDrink
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public List<string> Cup { get; set; }

        public static List<Object> ReadyDrinks { get; set; }

        static ReadyDrink()
        {
            ReadyDrinks = new List<Object>();
        }
        public ReadyDrink()
        {
            Cup = new List<string>();
            ReadyDrinks.Add(this);
        }
    }
}

    