using System;
using System.IO;

namespace T11_Football.Models
{
    public class Match
    {
        public DateTime Date { get; set; }
        public string? HomeTeam { get; set; }
        public string? AwayTeam { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
        public string? Tournament { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public bool Neutral { get; set; }

        public static List<Match> matches;

        static Match()
        {
            matches = new List<Match>();
        }
    }
}
