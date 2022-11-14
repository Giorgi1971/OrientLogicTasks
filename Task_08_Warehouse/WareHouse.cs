using System;

namespace Task_08_Warehouse

{
    public class WareHouse
    {
        public string NameWareHose { get; set; }
        public List<Product> Products { get; set; }

        public WareHouse()
        {
            NameWareHose = "House_001";
            Products = new List<Product>();
        }


    }
}

