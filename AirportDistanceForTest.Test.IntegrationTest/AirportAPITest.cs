using AirportDistanceForTest.Pres.Api;
using AirportDistanceForTest.Pres.Api.ViewModels;
using AirportDistanceForTest.Test.IntegrationTest.Fixtures;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace AirportDistanceForTest.Test.IntegrationTest
{
    public class AirportAPITest : IClassFixture<TestFixture<Startup>>
    {

        private HttpClient Client;

        public AirportAPITest(TestFixture<Startup> fixture)
        {
            Client = fixture.Client;
        }

        [Fact(DisplayName = "Airport Simple GET")]
        [Trait("AirportService", "Airport API Test")]
        public async Task AirportAPI_GET_ShoudBeReturnStatusOK()
        {
            // Arrange
            var request = "/api/airport/CWB/AMS";

            // Act
            var response = await Client.GetAsync(request);

            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<AirportDistanceViewModel>(responseContent);

            // Assert
            response.EnsureSuccessStatusCode();
        }


        [Fact(DisplayName = "Airport Calculate Distance API")]
        [Trait("AirportService", "Airport API Test")]
        public async Task AirportAPI_GET_ShoudBeReturnStatusOKAndReturnValidCalc()
        {
            // Arrange
            var request = "/api/airport/CWB/AMS";

            // Act
            var response = await Client.GetAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();
            var distance = JsonConvert.DeserializeObject<AirportDistanceViewModel>(responseContent);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.NotEqual(0, distance.Distance);            
            Assert.Equal(6285.129234929529, distance.Distance);            
        }
               

        [Theory(DisplayName = "Airport Calculate Distance API Not found")]
        [Trait("AirportService", "Airport API Test")]
        [InlineData("CWB", "ZZZ")]
        [InlineData("ZZZ", "ZZZ")]
        [InlineData("ZZZ", "CWB")]
        public async Task AirportAPI_GET_ShoudBeReturnStatusOKAndReturnNotFound(string a, string b)
        {
            // Arrange
            var request = String.Format("/api/airport/{0}/{1}",a,b);

            // Act
            var response = await Client.GetAsync(request);

            // Assert            
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
