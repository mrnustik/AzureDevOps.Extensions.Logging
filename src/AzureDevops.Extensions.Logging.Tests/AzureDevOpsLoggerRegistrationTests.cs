using System;
using System.IO;
using AzureDevOps.Extensions.Logging;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace AzureDevops.Extensions.Logging.Tests;

public class AzureDevOpsLoggerRegistrationTests
{
    [Fact]
    public void AddAzureDevOpsLoggerToLoggingBuilder_WithDefaultConfiguration_WorksCorrectly()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddLogging(builder => builder.AddAzureDevOpsLogger());
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        var logger = serviceProvider.GetRequiredService<ILogger<AzureDevOpsLoggerRegistrationTests>>();
        logger.LogError("Error message");


        stringWriter
            .ToString()
            .Should()
            .Be($"##[error]Error message{Environment.NewLine}");
    }
}