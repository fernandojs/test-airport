using AirportDistanceForTest.Application.Helpers;
using AirportDistanceForTest.Domain;
using AirportDistanceForTest.Infra.Data.Rest.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AirportDistanceForTest.Application
{
    public class AirportService
    {
        private IAirportRest _airportRest;
        public AirportService(IAirportRest airportRest)
        {
            _airportRest = airportRest;
        }
        /// <summary>
        /// Calc distance between two airports 
        /// Business Logic Service Layer
        /// </summary>
        /// <param name="airportCodeA"></param>
        /// <param name="airportCodeB"></param>
        /// <returns></returns>
        public async Task<Tuple<double, string>> CalculeDistance(string airportCodeA, string airportCodeB)
        {
            var errorMessage = string.Empty;
            var airportA = await _airportRest.GetAirport(airportCodeA);

            if (airportA != null)
            {
                var airportB = await _airportRest.GetAirport(airportCodeB);

                if (airportB != null)
                {
                    var tempDistance = Haversine.Distance(airportA.Location, airportB.Location);
                    return new Tuple<double, string>(tempDistance, errorMessage);
                }
                else
                {
                    errorMessage = "Airport B Not Found";
                }
            }
            else
            {
                errorMessage = "Airport A Not Found";
            }


            return new Tuple<double, string>(0, errorMessage); ;
        }
    }
}
