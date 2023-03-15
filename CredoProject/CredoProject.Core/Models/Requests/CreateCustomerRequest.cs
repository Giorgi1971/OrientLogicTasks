using System;
namespace CredoProject.Core.Models.Requests
{
    public class CreateCustomerRequest
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PersonalNumber { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
