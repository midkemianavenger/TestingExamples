using System.Net;
using FluentAssertions;
using RestSharp;

namespace ApiTestingExamples;

public class HappyPathTesting : BaseTest
{
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