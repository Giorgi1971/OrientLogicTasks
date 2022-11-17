using System;
namespace Task_08_Warehouse
{
    public static class Category
    {
        static string? desc;
        public static string? Desc 
        { 
            get
            {
                return desc;
            }
            set 
            {
                desc = value;
                // ეს ბევრგან მაქვს და რას აკეთებს წესიერად ვერ ვხვდები ???
                // Resolve nullable warnings ??? 
                categories.Add(desc ?? "");
            }
        }
        public static readonly List<string> categories = new() { "Food", "electrical equipment", "Sports equipment", "Books" };
    }
}

