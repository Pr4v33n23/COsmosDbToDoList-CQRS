using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ToDoList.Shared.Services;

namespace ToDoList.API.Controllers
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
        private IOptions<ApplicationSettings> _applicationSettings;


        public WeatherForecastController(ILogger<WeatherForecastController> logger, IOptions<ApplicationSettings> applicationSettings)
        {
            _logger = logger;
            _applicationSettings = applicationSettings;
        }

        [HttpGet]
        public List<string> Get()
        {
            var cosmosDbSettings = _applicationSettings.Value.CosmosDbSettings;

            return new List<string>(){ cosmosDbSettings.Account, cosmosDbSettings.DatabaseName, cosmosDbSettings.Key};
        }
    }
}
