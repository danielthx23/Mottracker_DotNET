using Mottracker.Application.Services;
using Moq;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Mottracker.Tests.UnitTests;

public class JwtServiceTests
{
    [Fact]
    public void GenerateToken_ShouldReturnValidToken()
    {
        // Arrange
        var mockConfig = new Mock<IConfiguration>();
        mockConfig.SetupGet(c => c["Jwt:SecretKey"]).Returns("YourSuperSecretKeyThatShouldBeAtLeast32CharactersLongForHS256Algorithm");
        mockConfig.SetupGet(c => c["Jwt:Issuer"]).Returns("TestIssuer");
        mockConfig.SetupGet(c => c["Jwt:Audience"]).Returns("TestAudience");

        var jwtService = new JwtService(mockConfig.Object);
        var userId =1;
        var email = "test@example.com";
        var nome = "Test User";

        // Act
        var token = jwtService.GenerateToken(userId, email, nome);

        // Assert
        Assert.NotNull(token);
        Assert.NotEmpty(token);
        Assert.Contains(".", token); // JWT tem formato: header.payload.signature
    }

    [Fact]
    public void GenerateToken_ShouldReturnDifferentTokensForDifferentUsers()
    {
        // Arrange
        var mockConfig = new Mock<IConfiguration>();
        mockConfig.SetupGet(c => c["Jwt:SecretKey"]).Returns("YourSuperSecretKeyThatShouldBeAtLeast32CharactersLongForHS256Algorithm");
        mockConfig.SetupGet(c => c["Jwt:Issuer"]).Returns("TestIssuer");
        mockConfig.SetupGet(c => c["Jwt:Audience"]).Returns("TestAudience");

        var jwtService = new JwtService(mockConfig.Object);

        // Act
        var token1 = jwtService.GenerateToken(1, "user1@example.com", "User1");
        var token2 = jwtService.GenerateToken(2, "user2@example.com", "User2");

        // Assert
        Assert.NotEqual(token1, token2);
    }
}

