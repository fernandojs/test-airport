using AirportDistanceForTest.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AirportDistanceForTest.Infra.Data.Rest.Interfaces
{
    public interface IAirportRest
    {
        Task<Airport> GetAirport(string code);
    }
}
 