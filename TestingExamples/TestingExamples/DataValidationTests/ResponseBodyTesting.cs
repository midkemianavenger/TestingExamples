using System.Net;
using FluentAssertions;
using NUnit.Framework;
using RestSharp;
using Services.Models;
using Services.RequestModels;

namespace ApiTestingExamples.DataValidationTests;

public class ResponseBodyTesting : BaseTest
{
    [Test]
    [Property("Test", "DataValidationTesting")]
    public async Task ResponseBodyTesting_DataMatchesExpectedValues()
    {
        await ValidateResponseData("TestName", 42, DateTime.Now);
    }

    private async Task ValidateResponseData(string dataName, int dataIndicator, DateTime endDateTime)
    {
        // ARRANGE
        var request = new RestRequest($"/api/testing/return/the/data/endpoint")
            .AddJsonBody(new DataRequest
            {
                StartDate = endDateTime.AddMinutes(-15),
                EndDate = endDateTime,
                DataName = dataName,
                DataIndicator = dataIndicator
            });

        // ACT
        var response = await AuthorizedRestClient.ExecutePostAsync<GetDataRequestResponse>(request);

        // ASSERT
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        response.Data?.StructuredData.Any(x => x.Id == dataIndicator).Should().BeTrue();
        response.Data?.StructuredData.Any(x => x.Name == dataName).Should().BeTrue();
        response.Data?.StructuredData.Any(x => x.EndDate == endDateTime).Should().BeTrue();
        response.Data?.StructuredData.Any(x => x.StartDate == endDateTime.AddMinutes(-15)).Should().BeTrue();
    }
}