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
    public void AzureDevOpsLogger_RegisteredViaServiceCollection_OutputsProperlyFormattedMessages()
    {
        // Arrange
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);

        var serviceCollection = new ServiceCollection();
        serviceCollection.AddLogging(builder => builder.AddAzureDevOpsLogger());
        var serviceProvider = serviceCollection.BuildServiceProvider();

        // Act
        var logger = serviceProvider.GetRequiredService<ILogger<AzureDevOpsLoggerRegistrationTests>>();
        logger.LogError("Error message");


        // Assert
        stringWriter
            .ToString()
            .Should()
            .Be($"##[error]Error message{Environment.NewLine}");
    }
}