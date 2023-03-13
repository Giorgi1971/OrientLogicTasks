using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CredoProject.Core.Db.Entity
{
    public class TransactionEntity
    {
        public long Id { get; set; }
        public Currency Currency { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal AmountTransaction { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExecutionAt { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Fee { get; set; }

        public int AccountFromId { get; set; }
        public AccountEntity AccountEntityFrom { get; set; }

        public int AccountToId { get; set; }
        public AccountEntity AccountEntityTo { get; set; }
    }
}

