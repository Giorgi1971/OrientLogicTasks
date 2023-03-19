using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CredoProject.Core.Db.Entity
{
    public class TransactionEntity
    {
        [Key]
        public long TransactionEntityId { get; set; }
        public Currency CurrencyFrom { get; set; }
        public Currency CurrencyTo { get; set; }
        [Column(TypeName = "decimal(18,5)")]
        public decimal AmountTransaction { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExecutionAt { get; set; }
        [Column(TypeName = "decimal(18,5)")]
        public decimal Fee { get; set; }
        public string? TransType { get; set; }
        [Column(TypeName = "decimal(18,5)")]
        public decimal CurrentRate { get; set; }

        public int? CardId { get; set; }
        public CardEntity? cardEntity { get; set; } = null!;

        public int AccountFromId { get; set; }
        public AccountEntity AccountEntityFrom { get; set; } = null!;

        public int AccountToId { get; set; }
        public AccountEntity? AccountEntityTo { get; set; } = null!;
    }
}

