using Microsoft.Extensions.Logging;

namespace AzureDevOps.Extensions.Logging;

public class AzureDevOpsLogger : ILogger
{
    private readonly AzureDevOpsLoggerConfiguration configuration;
    private readonly IConsoleOutput consoleOutput;

    public AzureDevOpsLogger(AzureDevOpsLoggerConfiguration configuration, IConsoleOutput consoleOutput)
    {
        this.configuration = configuration;
        this.consoleOutput = consoleOutput;
    }

    public void Log<TState>(LogLevel logLevel,
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

    private string GetLogLevelPrefix(LogLevel logLevel)
    {
        return "##[error]";
    }
}