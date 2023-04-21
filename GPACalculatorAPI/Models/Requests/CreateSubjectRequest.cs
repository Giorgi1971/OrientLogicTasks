using System;
namespace GPACalculatorAPI.Models.Requests
{
    public class CreateSubjectRequest
    {
        public string Name { get; set; }
        public int Credit { get; set; }
    }
}
