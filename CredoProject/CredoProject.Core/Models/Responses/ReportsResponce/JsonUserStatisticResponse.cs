using System;
namespace CredoProject.Core.Models.Responses
{
    public class JsonUserStatisticResponse
    {
        public string DatePeriod { get; set; } = null!;
        public int NumberOfUsers { get; set; }
    }
}

