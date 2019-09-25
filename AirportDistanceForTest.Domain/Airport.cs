namespace AirportDistanceForTest.Domain
{
    public class Airport
    {
        public string Country { get; set; }
        public string City_iata { get; set; }
        public string Iata { get; set; }
        public string City { get; set; }
        public string Timezone_region_name { get; set; }
        public string Country_iata { get; set; }
        public int Rating { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public string Type { get; set; }
        public int Hubs { get; set; }
    }

    public class Location
    {
        public double Lon { get; set; }
        public double Lat { get; set; }
    }    
}
