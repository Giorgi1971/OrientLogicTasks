using System;
using CredoProject.Core.Db.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CredoProject.Core.Models.Responses
{
    public class CustomerAccountsResponse
    {
        public int AccountEntityId { get; set; }
        public string? IBAN { get; set; }
        [Column(TypeName = "decimal(18,5)")]
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }

        public ICollection<TransactionEntity>? Transactionss { get; set; }
    }
}

//DateTime now = DateTime.Now;
//string formattedDate = now.ToString("d");

