using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using Xunit;

public class HealthTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task Healthz_ReturnsOk()
    {
        var resp = await _client.GetFromJsonAsync<Dictionary<string, string>>("/healthz");
        Assert.Equal("ok", resp!["status"]);
    }
}
