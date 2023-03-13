using System;
using CredoProject.Core.Db.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace CredoProject.Core.Models.Requests
{
    public class CreateAccountRequest
    {
        public int CustomerId { get; set; }
        public string? IBAN { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
    }
}
