using NUnit.Framework;
using RestSharp;
using RestSharp.Authenticators;
using Services;

namespace ApiTestingExamples;

public class BaseTest
{
    protected RestClient AuthorizedRestClient;
    protected RestClient UnAuthorizedRestClient;

    protected readonly string ApiBaseUrl;

    public BaseTest()
    {
        ApiBaseUrl = "";
    }

    [OneTimeSetUp]
    public async Task SetupTests()
    {
        UnAuthorizedRestClient = new RestClient(ApiBaseUrl);
        AuthorizedRestClient = new RestClient(ApiBaseUrl) { Authenticator =  new JwtAuthenticator(new AuthService().GetAuthToken())};
    }
}