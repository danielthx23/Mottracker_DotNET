using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Mottracker.Application.Dtos.MlPrediction;
using Xunit;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Net.Http.Headers;

namespace Mottracker.Tests.IntegrationTests;

public class MlPredictionControllerTests : IClassFixture<WebApplicationFactory<Mottracker.Program>>
{
    private readonly WebApplicationFactory<Mottracker.Program> _factory;

    public MlPredictionControllerTests(WebApplicationFactory<Mottracker.Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("Testing");
        });
    }

    [Fact]
    public async Task PredictMotoDemand_WithoutAuth_ShouldReturnUnauthorized()
    {
        // Arrange
        var client = _factory.CreateClient();
        var request = new MotoDemandPredictionRequestDto
        {
            TotalMotos =50,
            Data = DateTime.Now
        };

        // Act
        var response = await client.PostAsJsonAsync("/api/v1/ml/prediction/moto-demand", request);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task PredictMotoDemand_WithInvalidData_ShouldReturnBadRequest()
    {
        // Arrange
        var client = _factory.CreateClient();
        var request = new MotoDemandPredictionRequestDto
        {
            TotalMotos = -1, // Valor inválido
            Data = DateTime.Now
        };

        // create JWT using default secret from appsettings fallback
        var secret = "YourSuperSecretKeyThatShouldBeAtLeast32CharactersLongForHS256Algorithm";
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(issuer: "MottrackerAPI", audience: "MottrackerClient", expires: DateTime.UtcNow.AddMinutes(30), signingCredentials: creds);
        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);

        // Act
        var response = await client.PostAsJsonAsync("/api/v1/ml/prediction/moto-demand", request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}

