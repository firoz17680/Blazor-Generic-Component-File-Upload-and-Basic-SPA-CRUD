using Day3Training24032021.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day3Training24032021.Server.Controllers
{
    [Authorize]
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

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("count/{cnt}")]
        public IEnumerable<WeatherForecast> GetData(int cnt)
        {
            try
            {
                
                var rng = new Random();
                int duration = rng.Next(5, 55) * -1;

                List<WeatherForecast> list = new List<WeatherForecast>();
                for (int i = 1; i <= cnt; i++)
                {
                    list.Add(new WeatherForecast
                    {
                        Sno = i,
                        Date = DateTime.Now.AddYears(duration),
                        TemperatureC = rng.Next(-20, 55),
                        Summary = Summaries[rng.Next(Summaries.Length)]

                    });
                }

                return list;

            }
            catch(Exception e)
            {
                Log.Logger.Error(e.InnerException.Message);

                return new List<WeatherForecast>();
            }
        
        }



    }
}
