using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CredoProject.Core.Db.Entity
{
    public class AccountEntity
    {
        public int AccountEntityId { get; set; }
        public string IBAN { get; set; } = null!;
        [Column(TypeName = "decimal(18,5)")]
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public DateTime CreateAt { get; set; }

        public int CustomerEntityId { get; set; }
        public UserEntity CustomerEntity { get; set; } = null!;

        public List<CardEntity>? CardEntities { get; set; }

        public List<TransactionEntity>? FromTransactionEntities { get; set; }
        public List<TransactionEntity>? ToTransactionEntities { get; set; }
    }

    public enum Currency
    {
        [Display(Name = "Georgian Gel")]
        GEL,
        [Display(Name = "US Dollars")]
        USD,
        [Display(Name = "Euros")]
        EUR
    }
}

