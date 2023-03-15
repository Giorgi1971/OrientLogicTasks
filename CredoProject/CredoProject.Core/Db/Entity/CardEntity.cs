using System;
namespace CredoProject.Core.Db.Entity
{
    public class CardEntity
    {
        public int CardEntityId { get; set; }
        public string CardNumber { get; set; } = null!;
        public string CVV { get; set; } = null!;
        public string PIN { get; set; } = null!;
        public string OwnerName { get; set; } = null!;
        public string OwnerLastName { get; set; } = null!;
        public Status Status { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime ExpiredDate { get; set; }

        //public int UserId { get; set; }
        //public CustomerEntity CustomerEntity { get; set; }

        public int AccountEntityId { get; set; }
        public AccountEntity AccountEntity { get; set; } = null!;
    }

    public enum Status
    {
        Active,
        Blocked,
        Expired
    }
}

