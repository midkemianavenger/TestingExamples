using System.Net;
using FluentAssertions;
using NUnit.Framework;
using RestSharp;

namespace ApiTestingExamples;

public class AuthenticationTests : BaseTest
{
    [Test]
    [Property("Test", "AuthTest")]
    public async Task AuthenticationTest_WithoutAuthorizationHeader_ReturnsUnauthorized()
    {
        // ARRANGE
        var request = new RestRequest("/api/authorization/required");

        // ACT
        var response = await UnauthorizedRestClient.ExecutePostAsync(request);

        // ASSERT
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Test]
    [Property("Test", "AuthTestInvalidToken")]
    public async Task AuthenticationTest_WithInvalidAuthToken_ReturnsUnauthorized()
    {
        // ARRANGE
        var request = new RestRequest("/api/authorization/required")
            .AddHeader("Authorization", $"Bearer {Guid.NewGuid()}");

        // ACT
        var response = await UnauthorizedRestClient.ExecutePostAsync(request);

        // ASSERT
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}