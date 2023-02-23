using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace Common;

public abstract class CommonBaseTest
{
    // To change the environment for a local run update the value of DefaultEnvironment.
    private const string DefaultEnvironment = "env-dev";

    private readonly string _runEnvironment;

    private readonly IConfigurationRoot _configuration;

    protected CommonBaseTest(string testProject)
    {
        var configuredEnvironment = Environment.GetEnvironmentVariable("ENVIRONMENT");

        if (string.IsNullOrEmpty(configuredEnvironment))
        {
            configuredEnvironment = DefaultEnvironment;
        }

        if (configuredEnvironment.StartsWith("env-"))
        {
            var environmentDefinitions = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("AppSettings/environment-definitions.json", false, true).Build();
            _runEnvironment = environmentDefinitions[$"{testProject}:{configuredEnvironment}"];

            if (string.IsNullOrEmpty(_runEnvironment))
            {
                Assert.Ignore($"No environment is defined in the environment-definitions.json file for {testProject} in {configuredEnvironment}");
            }
        }
        else
        {
            _runEnvironment = configuredEnvironment;
        }

        _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"AppSettings/appsettings.{_runEnvironment.ToLowerInvariant()}.json")
            .Build();
    }

    protected string GetConfigurationValue(string configKey, bool required = false)
    {
        var configValue = _configuration[configKey];

        if (required && string.IsNullOrEmpty(configValue))
        {
            Assert.Fail($"Configuration value is required to run. Configuration Value {configKey} is not defined for this environment {_runEnvironment}");
        }

        return configValue;
    }
}