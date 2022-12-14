using System.IO;
using T11_Football.Models;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
var ms = Match.matches;

// path to the csv file
string path = "files/football_results.csv";

string[] lines = File.ReadAllLines(path);
var i = 0;
foreach (string line in lines)
{
    if (i == 0)
    {
        i = 1;
        continue;
    }
    string[] columns = line.Split(',');
    Match match = new Match();
    match.Date = DateTime.Parse(columns[0]);
    match.HomeTeam = columns[1];
    match.AwayTeam = columns[2];
    string myStr = columns[3];
    Int16 a;
    bool res = Int16.TryParse(myStr, out a);
    if (res)
        match.HomeScore = a;
    else
        match.HomeScore = -1;

    myStr = columns[4];
    //match.AwayScore = Int16.Parse(columns[4]);
    res = Int16.TryParse(myStr, out a);
    if (res)
        match.AwayScore = a;
    else
        match.AwayScore = -1;
    match.Tournament = columns[5];
    match.City = columns[6];
    match.Country = columns[7];
    if (columns[8] == "TRUE")
        match.Neutral = true;
    else
        match.Neutral = false;
    Match.matches.Add(match);
}
Console.WriteLine(Match.matches.Count);