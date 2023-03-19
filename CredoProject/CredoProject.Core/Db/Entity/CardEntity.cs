using System;
using System.Globalization;

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
        public string ExpiredDate { get; set; } = null!;

        //public int UserId { get; set; }
        //public CustomerEntity CustomerEntity { get; set; }
        public ICollection<TransactionEntity>? TransactionEntities { get; set; }

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


//string yearMonthString = "03-2023";
//DateTime myDateTime = DateTime.ParseExact(yearMonthString, "MM-yyyy", CultureInfo.InvariantCulture);

