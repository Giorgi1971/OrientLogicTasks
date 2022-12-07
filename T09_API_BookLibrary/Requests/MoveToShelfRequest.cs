using System;
namespace T09_API_BookLibrary.Requests
{
    public class MoveToShelfRequest
    {
        public int BookId { get; set; }
        public int NewShelfId { get; set; }
    }
}