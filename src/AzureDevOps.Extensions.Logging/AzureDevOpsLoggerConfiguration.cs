using Microsoft.Extensions.Logging;

namespace AzureDevOps.Extensions.Logging;

public class AzureDevOpsLoggerConfiguration
{
    /// <summary>
    /// Dictionary for mapping between LogLevel and AzureDevOpsFormattingCommand.
    /// Unspecified LogLevel values will not produce any log messages.
    /// </summary>
    public IDictionary<LogLevel, AzureDevOpsFormattingCommand> LogLevelsMapping { get; set; } =
        new Dictionary<LogLevel, AzureDevOpsFormattingCommand>
        {
            { LogLevel.Debug, AzureDevOpsFormattingCommand.Debug },
            { LogLevel.Information, AzureDevOpsFormattingCommand.Command },
            { LogLevel.Warning, AzureDevOpsFormattingCommand.Warning },
            { LogLevel.Error, AzureDevOpsFormattingCommand.Error },
            { LogLevel.Critical, AzureDevOpsFormattingCommand.Error }
        };
}