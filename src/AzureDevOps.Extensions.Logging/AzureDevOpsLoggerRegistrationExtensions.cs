using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;

namespace AzureDevOps.Extensions.Logging;

public static class AzureDevOpsLoggerRegistrationExtensions
{
    public static ILoggingBuilder AddAzureDevOpsLogger(
        this ILoggingBuilder builder)
    {
        builder.AddConfiguration();

        builder.Services
            .AddTransient<IAzureDevOpsLoggingCommandsFormattingMapper, AzureDevOpsLoggingCommandsFormattingMapper>();

        builder.Services
            .AddTransient<IConsoleOutput, StandardConsoleOutput>();

        builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, AzureDevOpsLoggerProvider>());

        LoggerProviderOptions.RegisterProviderOptions
            <AzureDevOpsLoggerConfiguration, AzureDevOpsLoggerProvider>(builder.Services);

        return builder;
    }

    public static ILoggingBuilder AddAzureDevOpsLogger(
        this ILoggingBuilder builder,
        Action<AzureDevOpsLoggerConfiguration> configure)
    {
        builder.AddAzureDevOpsLogger();
        builder.Services.Configure(configure);

        return builder;
    }
}