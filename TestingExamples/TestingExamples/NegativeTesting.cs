using NUnit.Framework;
using RestSharp;
using System.Net;
using FluentAssertions;

namespace ApiTestingExamples;

public class NegativeTesting : BaseTest
{
    [Test]
    [Property("Test", "TestNumber")]
    public async Task HappyPathTest()
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