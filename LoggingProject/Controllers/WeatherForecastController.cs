namespace LoggingProject.Controllers
{
    using LoggingLibrary.Domain;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILoggerSerilog log;

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ILoggerSerilog serilog)
        {
            _logger = logger;
            log = serilog;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            List<WeatherForecast> result = new List<WeatherForecast>();

            try
            {
                var rng = new Random();

                int request = rng.Next(1, 5);

                if(request == 1 || request == 5)
                    throw new NotImplementedException();

                result = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                }).ToList();

                log.Info("Logging request");
            }
            catch (Exception ex)
            {

                log.Error(ex, ex.ToString());

            }

            return result;
        }
    }
}
