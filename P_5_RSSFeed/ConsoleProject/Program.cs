using System;
using System.Net.Http;
using APIProject.Db.Entity;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class Program
{
    static HttpClient client = new HttpClient();

    static async Task Main(string[] args)
    {
        var apiUrl = "https://localhost:7047/";
        var response = await client.GetAsync(apiUrl);
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<MyApiModel>(data);
            Console.WriteLine($"Result: {result}");
        }
        else
        {
            Console.WriteLine($"Error: {response.StatusCode}");
        }
    }

    static async Task Main2(string[] args)
    {
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("https://localhost:7047");
        httpClient.DefaultRequestHeaders.Accept.Clear();
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var response = await httpClient.GetAsync("api/Feed/");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var myModels = JsonConvert.DeserializeObject<List<MyApiModel>>(content);
            foreach (var myModel in myModels)
            {
                Console.WriteLine($"{myModel.Name} ({myModel.Age})");
            }
        }
        else
        {
            Console.WriteLine($"Failed to retrieve data: {response.ReasonPhrase}");
        }
    }
}

public class MyApiModel
{
    public string Name { get; set; }
    public int Age { get; set; }
}

