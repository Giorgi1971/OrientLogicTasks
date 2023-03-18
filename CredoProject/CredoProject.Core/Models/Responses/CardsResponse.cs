using System;
using CredoProject.Core.Db.Entity;

namespace CredoProject.Core.Models.Responses
{
    public class CardsResponse
    {
        public string CardNumber { get; set; } = null!;
        public Status Status { get; set; }
        public string ExpiredDate { get; set; } = null!;
        public decimal CardAmount { get; set; }
        public Currency Currency { get; set; }
    }
}

