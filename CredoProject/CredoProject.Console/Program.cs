using System;
using System.Net.Http;
using System.Threading.Tasks;
using CredoProject.Core.Db;
using CredoProject.Core.Db.Entity;
using CredoProject.Core.Repositories;
using CredoProject.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //using var httpClient = new HttpClient();
            //var response = await httpClient.GetAsync("https://api.example.com/hello");
            //var content = await response.Content.ReadAsStringAsync();
            //Console.WriteLine(content);

            //var per = new Period();
            //foreach (DataByPeriod period in Enum.GetValues(typeof(DataByPeriod)).Cast<DataByPeriod>().OrderBy(v => (int)v))
            //{
            //    foreach (DataByPeriod period in Enum.GetValues(typeof(DataByPeriod)).Cast<DataByPeriod>().OrderBy(v => (int)v))
            //    {
            //        for (int i = 0; i < 2; i++)
            //    {
            //        var dp = new DataPeriod();
            //        dp.Perodname = "Perodname";
            //        dp.Currency = "evr";
            //        var pp = new Period();
            //            pp.dataByPeriod = DataByPeriod.first;
            //            pp.dataPeriod = 
            //    }
            //}
        }
    }

    public class Period
    {
        public DataPeriod dataPeriod { get; set; }
        public DataByPeriod dataByPeriod { get; set; }
    }

    public enum DataByPeriod
    {
        first,
        second
    }

    public class DataPeriod
    {
        public string? Perodname { get; set; }
        public string? Currency { get; set; }
    }
}
