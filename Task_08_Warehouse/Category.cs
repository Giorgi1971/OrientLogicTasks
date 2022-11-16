using System;
namespace Task_08_Warehouse
{
    public class Category
    {
        private string desc;
        public string? Desc 
        { 
            get
            {
                return desc;
            }
            set
            {
                desc = value;
                categories.Add(desc);
            }
        }

        public List<string> categories = new List<string> { "Food", "electrical equipment", "Sports equipment", "Books" };
    }
}

