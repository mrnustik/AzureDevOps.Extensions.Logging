namespace AzureDevOps.Extensions.Logging;

public interface IAzureDevOpsLoggingCommandsFormattingMapper
{
    string MapToLoggingCommand(AzureDevOpsFormattingCommand command);
}