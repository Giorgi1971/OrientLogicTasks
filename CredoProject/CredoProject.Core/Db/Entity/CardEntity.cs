using System;
namespace CredoProject.Core.Db.Entity
{
    public class CardEntity
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string CVV { get; set; }
        public string PIN { get; set; }
        public Status Status { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime ExpiredDate { get; set; }

        //public int UserId { get; set; }
        //public CustomerEntity CustomerEntity { get; set; }

        public int AccountId { get; set; }
        public AccountEntity AccountEntity { get; set; }
    }

    public enum Status
    {
        Active,
        Blocked,
        Expired
    }
}

