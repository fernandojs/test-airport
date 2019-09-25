using AirportDistanceForTest.Application;
using AirportDistanceForTest.Domain;
using AirportDistanceForTest.Infra.Data.Rest.Interfaces;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace AirportDistanceForTest.Test.Unit
{
    public class AirportServiceTest
    {
        [Fact(DisplayName = "Airport Calculate Distance")]
        [Trait("AirportService", "Airport Service Test")]
        public async Task AirportService_CalculeDistance_ShoudBeReturnValid()
        {
            // Arrange
            var airportA = new Airport() {
                                Iata = "AMS",
                                Location = new Location
                                {
                                    Lat = 52.309069,
                                    Lon = 4.763385
                                }
                            };


            var airportB = new Airport(){
                                Iata = "CWB",
                                Location = new Location
                                {
                                    Lat = -25.535763,
                                    Lon = -49.173298
                                }
                            };

            var airportRest = new Mock<IAirportRest>();

            airportRest.Setup(r => r.GetAirport(airportA.Iata))
                                .ReturnsAsync(airportA);

            airportRest.Setup(r => r.GetAirport(airportB.Iata))
                                .ReturnsAsync(airportB);

            var airportService = new AirportService(airportRest.Object);

            // Act
            var distance = await airportService.CalculeDistance(airportA.Iata, airportB.Iata);

            // Assert
            Assert.NotEqual(0, distance.Item1);
            Assert.Empty(distance.Item2);
            Assert.Equal(6285.129234929529, distance.Item1);
            airportRest.Verify(r => r.GetAirport("AMS"), Times.Once);

        }

        [Fact(DisplayName = "Airport Calculate Distance Not Found A")]
        [Trait("AirportService", "Airport Service Test")]
        public async Task AirportService_CalculeDistance_ShoudBeReturnNotFoundAirPortA()
        {

            var airportA = new Airport()
            {
                Iata = "KKK",
            };

            // Arrange
            var airportB = new Airport()
            {
                Iata = "AMS",
                Location = new Location
                {
                    Lat = 52.309069,
                    Lon = 4.763385
                }
            };

            

            var airportRest = new Mock<IAirportRest>();

            airportRest.Setup(r => r.GetAirport(airportA.Iata))
                                 .ReturnsAsync((Airport)null);

            airportRest.Setup(r => r.GetAirport(airportB.Iata))
                                .ReturnsAsync((airportB));

            var airportService = new AirportService(airportRest.Object);

            // Act
            var distance = await airportService.CalculeDistance(airportA.Iata, airportB.Iata);

            // Assert
            Assert.Equal(0, distance.Item1);
            Assert.Equal("Airport A Not Found", distance.Item2);

        }

        [Fact(DisplayName = "Airport Calculate Distance Not Found B")]
        [Trait("AirportService", "Airport Service Test")]
        public async Task AirportService_CalculeDistance_ShoudBeReturnNotFoundAirPortB()
        {
            // Arrange
            var airportA = new Airport()
            {
                Iata = "AMS",
                Location = new Location
                {
                    Lat = 52.309069,
                    Lon = 4.763385
                }
            };

            var airportB = new Airport()
            {
                Iata = "TTTT",               
            };

            var airportRest = new Mock<IAirportRest>();

            airportRest.Setup(r => r.GetAirport(airportA.Iata))
                                .ReturnsAsync(airportA);

            airportRest.Setup(r => r.GetAirport(airportB.Iata))
                                .ReturnsAsync((Airport)null);

            var airportService = new AirportService(airportRest.Object);

            // Act
            var distance = await airportService.CalculeDistance(airportA.Iata, airportB.Iata);

            // Assert
            Assert.Equal(0, distance.Item1);
            Assert.Equal("Airport B Not Found", distance.Item2);            
        }
        
        [Trait("AirportService", "Airport Service Test")]
        [Theory(DisplayName = "Airport Calculate Distance Diferrent Than Excepted")]
        [InlineData(1000)]
        [InlineData(5)]
        [InlineData(6200.00)]
        [InlineData(500)]
        [InlineData(6285.229234929529)]        
        public async Task AirportService_CalculeDistance_ReturnDifferentThanExcepted(double expected)
        {
            // Arrange
            var airportA = new Airport()
            {
                Iata = "AMS",
                Location = new Location
                {
                    Lat = 52.309069,
                    Lon = 4.763385
                }
            };


            var airportB = new Airport()
            {
                Iata = "CWB",
                Location = new Location
                {
                    Lat = -25.535763,
                    Lon = -49.173298
                }
            };

            var airportRest = new Mock<IAirportRest>();

            airportRest.Setup(r => r.GetAirport(airportA.Iata))
                                .ReturnsAsync(airportA);

            airportRest.Setup(r => r.GetAirport(airportB.Iata))
                                .ReturnsAsync(airportB);

            var airportService = new AirportService(airportRest.Object);

            // Act
            var distance = await airportService.CalculeDistance(airportA.Iata, airportB.Iata);
            
            // Assert
            Assert.NotEqual(expected, distance.Item1);
            Assert.Empty(distance.Item2);            
            airportRest.Verify(r => r.GetAirport("AMS"), Times.Once);

        }
    }
}
