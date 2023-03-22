using System;
using System.ComponentModel.DataAnnotations.Schema;
using CredoProject.Core.Db.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CredoProject.Core.Models.Requests.Card
{
    public class WithdrawManyFromCardRequest
    {
        public string CardNumber { get; set; } = null!;
        public string PIN { get; set; } = null!;
        [Column(TypeName = "decimal(18,5)")]
        public decimal WithdrawAmount { get; set; }
    }
}

