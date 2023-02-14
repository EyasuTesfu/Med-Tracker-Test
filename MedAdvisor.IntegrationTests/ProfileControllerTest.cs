
using System.Text.Json;
using System.Net;


public class ProfileControllerTest : IClassFixture<TestingWebAppFactory<Program>>
{
    private readonly HttpClient _client;

    public ProfileControllerTest(TestingWebAppFactory<Program> factory)
        => _client = factory.CreateClient();


    [Fact]
    public async Task GetProfile_Returns_ok()
    {
        // arrange
        _client.DefaultRequestHeaders.Add("Authorization", "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJJZCI6IjA4ZGFmYTVmLWE4ZmItNGU3Ni04ZTkxLThiZGE4MWQ5M2JkMCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6InVzZXJAZXhhbXBsZS5jb20iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoic3RyaW5nIHN0cmluZyIsImV4cCI6MTY3ODkyMDY1NH0.bLrfme-jzDKoUtjTqNkK2qVcqH7r8XJs6oNfbm8U0bIqFhWnMZQdyv-HccvXsse5WMnAnZK3INl8bFISu1glMA");

        //Act
        var response = await _client.GetAsync("/api/Profile/get");

        //assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }


    [Fact]
    public async Task GetProfile_Returns_badRequest()
    {
        //arrange 
            // with no user authentication
        
        //Act
        var response = await _client.GetAsync("/api/Profile/get");

        //assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }


 


}



