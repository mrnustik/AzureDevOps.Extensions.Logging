namespace AzureDevOps.Extensions.Logging;

public interface IAzureDevOpsLoggingCommandsFormattingMapper
{
    string MapToLoggingCommand(AzureDevOpsFormattingCommands command);
}