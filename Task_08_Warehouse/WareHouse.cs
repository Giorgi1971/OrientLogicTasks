using System;

namespace Task_08_Warehouse

{
    public class WareHouse
    {
        public static string NameWareHose = "WareHouse_001";
        public List<Product>? Products { get; set; }

        //public WareHouse()
        //{
        //    var wareHouse = new WareHouse();
        //}

        public void DisplayAllProducts()
        {
            if (Products == null)
                Console.WriteLine("No items in WareHouse!");
            else
            {
                foreach (var item in Products)
                {
                    item.DisplayProduct();
                }
            }
        }
    }
}

