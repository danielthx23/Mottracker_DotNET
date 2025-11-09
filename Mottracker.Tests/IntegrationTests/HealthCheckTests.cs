using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Xunit;

namespace Mottracker.Tests.IntegrationTests;

public class HealthCheckTests : IClassFixture<WebApplicationFactory<Mottracker.Program>>
{
    private readonly WebApplicationFactory<Mottracker.Program> _factory;

    public HealthCheckTests(WebApplicationFactory<Mottracker.Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("Testing");
        });
    }

    [Fact]
    public async Task HealthCheck_ShouldReturnOk()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/health");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var content = await response.Content.ReadAsStringAsync();
        Assert.Contains("status", content);
    }
}

