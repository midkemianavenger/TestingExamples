using Common;
using NUnit.Framework;
using RestSharp;
using RestSharp.Authenticators;
using Services;

namespace ApiTestingExamples;

public class BaseTest : CommonBaseTest
{
    protected RestClient AuthorizedRestClient;
    protected RestClient UnauthorizedRestClient;

    protected readonly string ApiBaseUrl;

    public BaseTest() : base(TestProjects.ApiTesting)
    {
        ApiBaseUrl = GetConfigurationValue("ApiTesting:BaseUrl", true);
    }

    [OneTimeSetUp]
    public async Task SetupTests()
    {
        UnauthorizedRestClient = new RestClient(ApiBaseUrl);
        AuthorizedRestClient = new RestClient(ApiBaseUrl) { Authenticator =  new JwtAuthenticator(new AuthService().GetAuthToken())};
    }
}