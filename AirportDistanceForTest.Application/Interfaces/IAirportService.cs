using AirportDistanceForTest.Application.Helpers;
using AirportDistanceForTest.Domain;
using AirportDistanceForTest.Infra.Data.Rest.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AirportDistanceForTest.Application
{
    public interface IAirportService
    {
        Task<Tuple<double, string>> CalculeDistance(string airportCodeA, string airportCodeB);
        
    }
}
