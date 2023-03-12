using System;

namespace CredoProject.Core.Db.Entity
{
    public class AccountEntity
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string? IBAN { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public DateTime CreateAt { get; set; }
    }

    public enum Currency
    {
        GEL,
        USD,
        EUR
    }
}

