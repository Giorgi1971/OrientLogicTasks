using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace CredoProject.Core.Db.Entity
{
    public class ExchangeEntity
    {
        public int Id { get; set; }
        public DateTime date { get; set; }
        public Currency currencyFrom { get; set; }
        public Currency currencyTo { get; set; }
        [Column(TypeName = "decimal(18,5)")]
        public decimal rate { get; set; }
    }
}

