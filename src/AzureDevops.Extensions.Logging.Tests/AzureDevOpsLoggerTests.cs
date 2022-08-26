using AzureDevOps.Extensions.Logging;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Xunit;

namespace AzureDevops.Extensions.Logging.Tests;

public class AzureDevOpsLoggerTests
{
    private readonly FakeConsoleOutput fakeConsoleOutput = new FakeConsoleOutput();

    [Fact]
    public void LogError_WithSimpleMessage_OutputsMessageProperFormat()
    {
        //Arrange
        var logger = CreateLogger();

        //Act
        var message = "Test error message";
        logger.LogError(message);

        //Assert
        var outputLines = fakeConsoleOutput.GetOutputLines();
        outputLines
            .Should()
            .HaveElementAt(0, "##[error]Test error message");
    }

    [Fact]
    public void LogError_WithFormattedMessage_OutputsMessageProperFormat()
    {
        //Arrange
        var logger = CreateLogger();

        //Act
        const int parameter = 42;
        logger.LogError("Test error message with parameter {Parameter}", parameter);

        //Assert
        var outputLines = fakeConsoleOutput.GetOutputLines();
        outputLines
            .Should()
            .HaveElementAt(0, "##[error]Test error message with parameter 42");
    }

    private ILogger CreateLogger(AzureDevOpsLoggerConfiguration? configuration = null)
    {
        var provider =
            new AzureDevOpsLoggerProvider(
                configuration ?? new AzureDevOpsLoggerConfiguration(),
                fakeConsoleOutput,
                new AzureDevOpsLoggingCommandsFormattingMapper());
        return provider.CreateLogger(nameof(AzureDevOpsLoggerTests));
    }
}