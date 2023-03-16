using System;
namespace CredoProject.Core.Models.Requests.Card
{
    public class ChangeCardPinRequest
    {
        public string CardNumber { get; set; } = null!;
        public string PIN { get; set; } = null!;
        public string newPIN { get; set; } = null!;
    }
}
