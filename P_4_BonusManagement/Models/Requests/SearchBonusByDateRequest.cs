using System;
namespace P_4_BonusManagement.Models.Requests
{
    public class SearchBonusByDateRequest
    {
        public DateTime FromDate { get; set; }
        public DateTime BeforeDate { get; set; }
    }
}

