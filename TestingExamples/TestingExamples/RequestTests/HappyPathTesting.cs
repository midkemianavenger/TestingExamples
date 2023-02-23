using System.Net;
using FluentAssertions;
using NUnit.Framework;
using RestSharp;

namespace ApiTestingExamples.RequestTests;

public class HappyPathTesting : BaseTest
{
    [Test]
    [Property("Test", "TestNumber")]
    public async Task HappyPathTesting_ValidDateTime_ReturnsOk()
    {
        await ApiRequestTest("testPath",
            DateTime.Now,
            HttpStatusCode.OK);
    }

    private async Task ApiRequestTest(string updatableInfo, DateTime queryDate, HttpStatusCode validationCode)
    {
        // ARRANGE
        var request = new RestRequest($"/apiEndpoint/{updatableInfo}")
            .AddJsonBody(new
            {
                Date = queryDate
            });

        // ACT
        var response = await AuthorizedRestClient.ExecutePostAsync(request);

        // ASSERT
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(validationCode);
    }
}