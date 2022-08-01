using Microsoft.Extensions.Logging;

namespace AzureDevOps.Extensions.Logging;

public class AzureDevOpsLoggerProvider : ILoggerProvider
{
    private readonly AzureDevOpsLoggerConfiguration configuration;
    private readonly IConsoleOutput consoleOutput;

    private ILogger? logger;

    public AzureDevOpsLoggerProvider(AzureDevOpsLoggerConfiguration configuration, IConsoleOutput consoleOutput)
    {
        this.configuration = configuration;
        this.consoleOutput = consoleOutput;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return logger ??= new AzureDevOpsLogger(configuration, consoleOutput);
    }

    public void Dispose()
    {
    }
}