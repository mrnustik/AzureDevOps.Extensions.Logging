namespace AzureDevOps.Extensions.Logging;

public class AzureDevOpsLoggingCommandsFormattingMapper : IAzureDevOpsLoggingCommandsFormattingMapper
{
    public string MapToLoggingCommand(AzureDevOpsFormattingCommand command)
    {
        var commandName =
            command
                .ToString()
                .ToLower();
        return $"##[{commandName}]";
    }
}