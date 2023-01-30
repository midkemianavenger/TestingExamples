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

        response.Data?.StructuredData.Select(x=>x.Id).Should().Equal(dataIndicator);
        response.Data?.StructuredData.Select(x => x.Name).Should().BeEquivalentTo(dataName);
        response.Data?.StructuredData.Select(x => x.EndDate).Should().Equal(endDateTime);
        response.Data?.StructuredData.Select(x => x.StartDate).Should().Equal(endDateTime.AddMinutes(-15));
    }
}