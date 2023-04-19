using Microsoft.AspNetCore.Mvc;
using static System.Collections.Specialized.BitVector32;

namespace Evoting_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        //[HttpGet(Name = "GetWeatherForecast")]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = Random.Shared.Next(-20, 55),
        //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}
        [HttpGet(Name = “GetElections”)]
        public IEnumerable<Election> Get()
        {
            List<Rows> result = databaseContext.execute(“SELECT * FROM Elections;”);
            foreach (Row dbRow in result)
            {
                // Convert dbRow into Election class, like new Election(dbRow.getInt(0), dbRow.getString(1), dbRow.getString(2))…
            }
            return electionList;
        }
    }
}