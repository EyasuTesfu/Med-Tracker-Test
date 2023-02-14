

using System.Net;
using System.Text.Json;

namespace MedAdvisor.IntegrationTests
{
    public class MedicineControllerTest : IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient _client;

        public MedicineControllerTest(TestingWebAppFactory<Program> factory)
             => _client = factory.CreateClient();


        [Fact]
        public async Task Addmedicine_returnsOk()
        {

            // Arrange
            var medicineId = "08db0878-887d-4b05-8bc6-e385a0e9d768";
            var content = new StringContent(
                JsonSerializer.Serialize(medicineId), System.Text.Encoding.UTF8, "application/json");

            _client.DefaultRequestHeaders.Add("Authorization", "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJJZCI6IjA4ZGFmYTVmLWE4ZmItNGU3Ni04ZTkxLThiZGE4MWQ5M2JkMCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6InVzZXJAZXhhbXBsZS5jb20iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoic3RyaW5nIHN0cmluZyIsImV4cCI6MTY3ODkyMDY1NH0.bLrfme-jzDKoUtjTqNkK2qVcqH7r8XJs6oNfbm8U0bIqFhWnMZQdyv-HccvXsse5WMnAnZK3INl8bFISu1glMA");

            // act
            var response = await _client.PostAsync("/api/Medicine/add/" + medicineId, content);

            //assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }


        [Fact]
        public async Task Addmedicine_returnBadRequest()
        {
            // Arrange
            var medicineId = "08db0878-887d-4b05-8bc6-e385a0e9d768";
            var content = new StringContent(
                JsonSerializer.Serialize(medicineId), System.Text.Encoding.UTF8, "application/json");
            // act
            var response = await _client.PostAsync("/api/Medicine/add/" + medicineId, content);

            //assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }


        [Fact]
        public async Task DeleteAllergy_ReturnsOk()
        {

            // Arrange
            var medicineId = "08db0878-887d-4b05-8bc6-e385a0e9d768";
            _client.DefaultRequestHeaders.Add("Authorization", "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJJZCI6IjA4ZGFmYTVmLWE4ZmItNGU3Ni04ZTkxLThiZGE4MWQ5M2JkMCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6InVzZXJAZXhhbXBsZS5jb20iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoic3RyaW5nIHN0cmluZyIsImV4cCI6MTY3ODkyMDY1NH0.bLrfme-jzDKoUtjTqNkK2qVcqH7r8XJs6oNfbm8U0bIqFhWnMZQdyv-HccvXsse5WMnAnZK3INl8bFISu1glMA");

            // act
            var response = await _client.DeleteAsync("/api/Medicine/" + medicineId);

            //assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteMedicine_ReturnsBadRequest()
        {

            // Arrange
            var medicineId = "08db0878-887d-4b05-8bc6-e385a0e9d768";
            // act
            var response = await _client.DeleteAsync("/api/Medicine/" + medicineId);

            //assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
