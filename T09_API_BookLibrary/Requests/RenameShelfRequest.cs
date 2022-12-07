using System;
namespace T09_API_BookLibrary.Requests
{
    public class RenameShelfRequest
    {
        public int ShelfdId { get; set; }
        public string? Name { get; set; }
    }
}

