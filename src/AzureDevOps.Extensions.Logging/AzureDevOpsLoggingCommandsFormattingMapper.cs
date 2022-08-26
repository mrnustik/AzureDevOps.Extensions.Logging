namespace AzureDevOps.Extensions.Logging;

public class AzureDevOpsLoggingCommandsFormattingMapper : IAzureDevOpsLoggingCommandsFormattingMapper
{
    public string MapToLoggingCommand(AzureDevOpsFormattingCommands command)
    {
        var commandName =
            command
                .ToString()
                .ToLower();
        return $"##[{commandName}]";
    }
}