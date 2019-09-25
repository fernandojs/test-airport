using AirportDistanceForTest.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirportDistanceForTest.Application.Helpers
{
    public static class Haversine {

        /// <summary>
        /// Returns the distance in miles between two points
        /// latitude / longitude points.
        /// </summary>
        /// <param name="location1">Location 1</param>    
        /// <param name="location2">Location 2</param>    
        /// <returns>Distance in miles</returns>
        public static double Distance(Location location1, Location location2)
        {
            double R = 3960;
            var lat = (location2.Lat - location1.Lat).ToRadians();
            var lng = (location2.Lon - location1.Lon).ToRadians();
            var h1 = Math.Sin(lat / 2) * Math.Sin(lat / 2) +
                          Math.Cos(location1.Lat.ToRadians()) * Math.Cos(location2.Lat.ToRadians()) *
                          Math.Sin(lng / 2) * Math.Sin(lng / 2);
            var h2 = 2 * Math.Asin(Math.Min(1, Math.Sqrt(h1)));
            return R * h2;
        }
    }   


}
