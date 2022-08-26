using Microsoft.Extensions.Logging;

namespace AzureDevOps.Extensions.Logging;

public class AzureDevOpsLoggerConfiguration
{
    public IDictionary<LogLevel, AzureDevOpsFormattingCommands> LogLevelsMapping { get; set; } =
        new Dictionary<LogLevel, AzureDevOpsFormattingCommands>
        {
            { LogLevel.Debug, AzureDevOpsFormattingCommands.Debug },
            { LogLevel.Information, AzureDevOpsFormattingCommands.Command },
            { LogLevel.Warning, AzureDevOpsFormattingCommands.Warning },
            { LogLevel.Error, AzureDevOpsFormattingCommands.Error }
        };
}