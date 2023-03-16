using System;
using CredoProject.Core.Db.Entity;

namespace CredoProject.Core.Models.Responses
{
    public class UserEntityResponce
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PersonalNumber { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string? Email { get; set; }
        public string? Hash { get; set; }

        public ICollection<AccountEntity>? AccountEntities { get; set; }
    }
}

