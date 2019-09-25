using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirportDistanceForTest.Application;
using AirportDistanceForTest.Infra.Data.Rest;
using AirportDistanceForTest.Infra.Data.Rest.Interfaces;
using AirportDistanceForTest.Pres.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AirportDistanceForTest.Pres.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        private IAirportRest _airportRest;
        public AirportController(IAirportRest airportRest)
        {
            _airportRest = airportRest;
        }

        // GET api/values
        /// <summary>
        /// GET / Calc distance between two airports
        /// </summary>
        /// <param name="airportCodeA"></param>
        /// <param name="airportCodeB"></param>
        /// <returns></returns>
        [HttpGet("{airportCodeA}/{airportCodeB}")]
        public async Task<IActionResult> Get(string airportCodeA, string airportCodeB)
        {
            try
            {                
                var airportService = new AirportService(_airportRest);

                var airportDistanceResponse = await airportService.CalculeDistance(airportCodeA, airportCodeB);

                if (String.IsNullOrEmpty(airportDistanceResponse.Item2))
                {
                    var airportDistanceVM = new AirportDistanceViewModel
                    {
                        AirportCodeA = airportCodeA,
                        AirportCodeB = airportCodeB,
                        Distance = airportDistanceResponse.Item1
                    };

                    return Ok(airportDistanceVM);
                }
                else
                {
                    return NotFound(airportDistanceResponse.Item2);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
