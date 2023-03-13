using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace CredoProject.Core.Db.Entity
{
    public class AccountEntity
    {
        public int Id { get; set; }
        public string? IBAN { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public DateTime CreateAt { get; set; }

        public int CustomerId { get; set; }
        public CustomerEntity CustomerEntity { get; set; }

        public ICollection<CardEntity> CardEntities { get; set; }

        public ICollection<TransactionEntity> FromTransactionEntities { get; set; }
        public ICollection<TransactionEntity> ToTransactionEntities { get; set; }
    }

    public enum Currency
    {
        GEL,
        USD,
        EUR
    }
}

