using System;
using System.Collections.Generic;
using System.Text;

namespace T8_Builder_DesignPatterns.Models
{
    static class Order
    {
        public static List<Object> Orders { get; set; }

        static Order()
        {
            Orders = new List<Object>();
        }
    }
}

