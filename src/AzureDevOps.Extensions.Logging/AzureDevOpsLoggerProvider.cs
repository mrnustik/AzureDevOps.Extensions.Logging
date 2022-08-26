using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AzureDevOps.Extensions.Logging;

public class AzureDevOpsLoggerProvider : ILoggerProvider
{
    private readonly AzureDevOpsLoggerConfiguration configuration;
    private readonly IConsoleOutput consoleOutput;
    private readonly IAzureDevOpsLoggingCommandsFormattingMapper loggingCommandsFormattingMapper;

    private ILogger? logger;

    public AzureDevOpsLoggerProvider(
        IOptions<AzureDevOpsLoggerConfiguration> getCurrentConfiguration,
        IConsoleOutput consoleOutput,
        IAzureDevOpsLoggingCommandsFormattingMapper loggingCommandsFormattingMapper)
    {
        this.configuration = getCurrentConfiguration.Value;
        this.consoleOutput = consoleOutput;
        this.loggingCommandsFormattingMapper = loggingCommandsFormattingMapper;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return logger ??= new AzureDevOpsLogger(configuration, consoleOutput, loggingCommandsFormattingMapper);
    }

    public void Dispose()
    {
    }
}