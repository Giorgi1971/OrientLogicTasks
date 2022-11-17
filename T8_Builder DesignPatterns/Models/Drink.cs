using System;
using System.Collections.Generic;
using System.Text;

namespace T8_Builder_DesignPatterns.Models
{
    public class Drink
    {
        public List<string> Cup { get; set; }
        public Drink()
        {
            Cup = new List<string>();
        }
    }
}
