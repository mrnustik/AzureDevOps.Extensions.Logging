using System.Collections.Generic;
using AzureDevOps.Extensions.Logging;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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

    [Fact]
    public void LogOnAllLevels_WithSimpleMessage_OutputsMessageProperFormat()
    {
        //Arrange
        var logger = CreateLogger();

        //Act 
        logger.LogDebug("Debug message");
        logger.LogInformation("Information message");
        logger.LogWarning("Warning message");
        logger.LogError("Error message");
        logger.LogCritical("Critical message");

        //Assert
        var outputLines = fakeConsoleOutput.GetOutputLines();
        outputLines
            .Should()
            .HaveElementAt(0, "##[debug]Debug message")
            .And
            .HaveElementAt(1, "##[command]Information message")
            .And
            .HaveElementAt(2, "##[warning]Warning message")
            .And
            .HaveElementAt(3, "##[error]Error message")
            .And
            .HaveElementAt(4, "##[error]Critical message");
    }

    [Fact]
    public void Log_WithOverridenConfiguration_WillUserOverridenLoggingCommand()
    {
        //Arrange
        var logger = CreateLogger(new AzureDevOpsLoggerConfiguration
        {
            LogLevelsMapping = new Dictionary<LogLevel, AzureDevOpsFormattingCommand>()
            {
                { LogLevel.Information, AzureDevOpsFormattingCommand.Error }
            }
        });

        //Act
        logger.LogInformation("Information message that should look like an error");

        //Assert
        var outputLines = fakeConsoleOutput.GetOutputLines();
        outputLines
            .Should()
            .HaveElementAt(0, "##[error]Information message that should look like an error");
    }

    private ILogger CreateLogger(AzureDevOpsLoggerConfiguration? configuration = null)
    {
        var provider =
            new AzureDevOpsLoggerProvider(
                new OptionsWrapper<AzureDevOpsLoggerConfiguration>(
                    configuration ?? new AzureDevOpsLoggerConfiguration()),
                fakeConsoleOutput,
                new AzureDevOpsLoggingCommandsFormattingMapper());
        return provider.CreateLogger(nameof(AzureDevOpsLoggerTests));
    }
}