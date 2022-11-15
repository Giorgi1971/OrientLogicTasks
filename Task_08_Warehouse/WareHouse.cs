using System;

namespace Task_08_Warehouse

{
    public class WareHouse
    {
        public static string NameWareHose = "WareHouse_001";
        public List<Product> Products { get; set; }

        public WareHouse()
        {
            Products = new List<Product>();
        }


    }
}

