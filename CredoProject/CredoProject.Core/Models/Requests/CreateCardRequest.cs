using System;
using CredoProject.Core.Db.Entity;

namespace CredoProject.Core.Models.Requests
{
    public class CreateCardRequest
    {
        public string CardNumber { get; set; } = null!;
        public string CVV { get; set; } = null!;
        public string PIN { get; set; } = null!;
        public int AccountEntityId { get; set; }
    }
}
