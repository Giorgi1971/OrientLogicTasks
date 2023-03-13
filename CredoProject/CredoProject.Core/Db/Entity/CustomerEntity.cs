using System;

namespace CredoProject.Core.Db.Entity
{
    public class CustomerEntity
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PersonalNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Email { get; set; }
        public string? Hash { get; set; }

        public ICollection<AccountEntity> AccountEntities { get; set; }
        //public ICollection<CardEntity> CardEntities { get; set; }
    }
}
