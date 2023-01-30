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

    [Test]
    [Property("Test", "TestNumber")]
    public async Task NegativeTesting_EmptyUser_ReturnsBadRequestAndErrorMessage()
    {
        await ApiRequestTest("testPath",
            "",
            HttpStatusCode.BadRequest,
            "This is a very bad thing to do why did you do it?");
    }

    private async Task ApiRequestTest(string updatableInfo, string username, HttpStatusCode validationCode, string errorMessage = null)
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
        if (errorMessage != null)
        {
            response.ErrorMessage.Should().Be(errorMessage);
        }
    }
}