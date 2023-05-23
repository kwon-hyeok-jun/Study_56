//using System;
//using System.Threading.Tasks;

//class AsyncMain
//{
//    static async Task Main()
//    {
//        await Task.Delay(1000);

//        Console.WriteLine("비동기 메인 메서드");
//    }
//}

//using System;
//using System.Net.Http;
//using System.Threading.Tasks;

//class AsyncAwaitSimple
//{
//    //[1] 비동기 메서드를 호출하는 DoAsync() 메서드 생성시 async 키워드 붙임
//    static async Task DoAsync()
//    {
//        using (var client = new HttpClient())
//        {
//            //[2] .NET API의 비동기 메서드 호출할 때 await 키워드 붙임
//            var r = await client.GetAsync("https://dotnetnote.azurewebsites.net/api/WebApiDemo");
//            var c = await r.Content.ReadAsStringAsync();

//            Console.WriteLine(c);
//        }
//    }

//    //[3] Main() 메서드를 async 키워드를 붙여 비동기 메서드로 변경
//    static async Task Main()
//    {
//        //[4] 비동기 메서드 호출할 때 await 키워드를 앞에 붙임
//        await DoAsync();
//    }
//}

using System;
using System.Linq;
using System.Threading.Tasks;

public class WeatherForecast
{
    public DateTime Date { get; set; }
    public int TemperatureC { get; set; }
    public int TemperatureF { get; set; }
    public string Summary { get; set; }
}

public class WeatherForecastService
{
    private static string[] summaries = new[]
    {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild",
            "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
    {
        var rng = new Random();
        return Task.FromResult(Enumerable.Range(1, 5).Select(idx => new WeatherForecast
        {
            Date = startDate.AddDays(idx),
            TemperatureC = rng.Next(-20, 55),
            Summary = summaries[rng.Next(summaries.Length)]
        }).ToArray());
    }
}

class WeatherForecastApp
{
    static async Task Main()
    {
        var service = new WeatherForecastService();
        var forecasts = await service.GetForecastAsync(DateTime.Now);

        Console.WriteLine("Date\tTemp. (C)\tTemp. (F)\tSummary");
        foreach (var f in forecasts)
        {
            Console.WriteLine($"{f.Date.ToShortDateString()}\t" +
                $"{f.TemperatureC}\t{f.TemperatureF}\t{f.Summary}");
        }
    }
}
