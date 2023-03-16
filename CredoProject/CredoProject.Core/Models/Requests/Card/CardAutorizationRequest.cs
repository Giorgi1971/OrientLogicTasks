using System;
namespace CredoProject.Core.Models.Requests.Card
{
    public class CardAutorizationRequest
    {
        public string CardNumber { get; set; } = null!;
        public string PIN { get; set; } = null!;
    }
}

