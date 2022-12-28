using NUnit.Framework;
using RestSharp;
using System.Net;
using FluentAssertions;

namespace ApiTestingExamples.RequestTests;

public class NegativeTesting : BaseTest
{
    [Test]
    [Property("Test", "TestNumber")]
    public async Task NegativeTesting_NonExistentUser_ReturnsBadRequest()
    {
        await ApiRequestTest("testPath",
            "nonExistentUser",
            HttpStatusCode.BadRequest);
    }

    private async Task ApiRequestTest(string updatableInfo, string username, HttpStatusCode validationCode)
    {
        // ARRANGE
        var request = new RestRequest($"/apiEndpoint/{updatableInfo}")
            .AddJsonBody(new
            {
                Username = username
            });

        // ACT
        var response = await AuthorizedRestClient.ExecutePostAsync(request);

        // ASSERT
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(validationCode);
    }
}