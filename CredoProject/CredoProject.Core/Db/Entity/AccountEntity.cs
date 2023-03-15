using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace CredoProject.Core.Db.Entity
{
    public class AccountEntity
    {
        public int AccountEntityId { get; set; }
        public string? IBAN { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public DateTime CreateAt { get; set; }

        public int CustomerEntityId { get; set; }
        public UserEntity CustomerEntity { get; set; } = null!;

        public ICollection<CardEntity>? CardEntities { get; set; }

        // ამას რამე აზრი აქვს????
        public ICollection<TransactionEntity>? FromTransactionEntities { get; set; }
        public ICollection<TransactionEntity>? ToTransactionEntities { get; set; }
    }

    public enum Currency
    {
        GEL,
        USD,
        EUR
    }
}

