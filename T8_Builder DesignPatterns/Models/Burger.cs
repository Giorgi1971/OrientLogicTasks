using System;
using System.Collections.Generic;
using System.Text;

namespace T8_Builder_DesignPatterns.Models
{
    public class Burger
    {
        public List<string> Ingredients { get; set; }
        public Burger()
        {
            Ingredients = new List<string>();
        }
    }
}
