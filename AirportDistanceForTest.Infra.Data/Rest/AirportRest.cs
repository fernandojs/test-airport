using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AirportDistanceForTest.Domain;
using Newtonsoft.Json;
using Polly;
using Serilog;
using Serilog.Core;
using AirportDistanceForTest.Infra.Data.Rest.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;

namespace AirportDistanceForTest.Infra.Data.Rest
{
    public class AirportRest : IAirportRest
    {                
        private string _baseUrl;
        private Logger _logger;
        private const int _retryPolice = 3;

        public AirportRest(IConfiguration configuration)
        {            
            _baseUrl = configuration["BaseAPI"];
            _logger = new LoggerConfiguration()
                            .MinimumLevel.Verbose()
                            .Enrich.FromLogContext()
                            .WriteTo.ColoredConsole()
                            .CreateLogger();
        }

        /// <summary>
        /// GET airport infos by API
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<Airport> GetAirport(string code)
        {   
            var httpClient = new HttpClient();
            var airportAPIUrl = String.Format("{0}{1}/{2}", _baseUrl, "airports", code);

            try
            {
                var response = await Policy
                    .HandleResult<HttpResponseMessage>(message => !message.IsSuccessStatusCode)
                    .WaitAndRetryAsync(_retryPolice, i => TimeSpan.FromSeconds(2), (result, timeSpan, retryCount, context) =>
                    {
                        _logger.Warning($"Request failed with {result.Result.StatusCode}. Waiting {timeSpan} before next retry. Retry attempt {retryCount}");
                    })
                    .ExecuteAsync(() => httpClient.GetAsync(airportAPIUrl));

                if (response.IsSuccessStatusCode)
                {
                    _logger.Information("Response was successful.");
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var airport = JsonConvert.DeserializeObject<Airport>(jsonString);

                    //Check if information is precisely
                    //When the airport name is empty, the location is different
                    if (!String.IsNullOrEmpty(airport.Name))
                        return airport;
                    else
                    {
                        _logger.Error($"Response failed. Status code {response.StatusCode}");
                    }
                }
                else
                {
                    _logger.Error($"Response failed. Status code {response.StatusCode}");
                 
                }
            }catch(Exception ex)
            {
                _logger.Error($"Process failed. Exception: {ex.ToString()}");

            }
            return null;

        }
    }
}
