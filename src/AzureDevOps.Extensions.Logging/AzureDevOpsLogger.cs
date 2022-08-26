using Microsoft.Extensions.Logging;

namespace AzureDevOps.Extensions.Logging;

public class AzureDevOpsLogger : ILogger
{
    private readonly AzureDevOpsLoggerConfiguration configuration;
    private readonly IConsoleOutput consoleOutput;
    private readonly IAzureDevOpsLoggingCommandsFormattingMapper loggingCommandsFormattingMapper;

    public AzureDevOpsLogger(
        AzureDevOpsLoggerConfiguration configuration,
        IConsoleOutput consoleOutput,
        IAzureDevOpsLoggingCommandsFormattingMapper loggingCommandsFormattingMapper)
    {
        this.configuration = configuration;
        this.consoleOutput = consoleOutput;
        this.loggingCommandsFormattingMapper = loggingCommandsFormattingMapper;
    }

    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }

        var prefix = GetLogLevelPrefix(logLevel);

        if (prefix == null)
        {
            return;
        }

        var formattedOutput = formatter(state, exception);
        consoleOutput.WriteLine($"{prefix}{formattedOutput}");
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel != LogLevel.None;
    }

    public IDisposable BeginScope<TState>(TState state)
    {
        return default!;
    }

    private string? GetLogLevelPrefix(LogLevel logLevel)
    {
        var logLevelsMapping = configuration.LogLevelsMapping;

        if (!logLevelsMapping.TryGetValue(logLevel, out var formattingCommand))
        {
            return null;
        }

        return loggingCommandsFormattingMapper.MapToLoggingCommand(formattingCommand);
    }
}