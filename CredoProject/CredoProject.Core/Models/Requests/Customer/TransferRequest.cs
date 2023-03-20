using System;
using System.ComponentModel.DataAnnotations.Schema;
using CredoProject.Core.Db.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CredoProject.Core.Models.Requests.Customer
{
    public class TransferRequest
    {
        public string AccountFrom { get; set; } = null!;
        public string AccountTo { get; set; } = null!;
        [Column(TypeName = "decimal(18,5)")]
        public decimal TransferAmount { get; set; }
        public TransType TransType { get; set; }
    }
}

