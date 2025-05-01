using Serilog;
using WeatherService.API.Configuration;
using WeatherService.API.Constants;

namespace WeatherService.API.Extensions;

public static class LoggingExtension
{
    public static void ConfigureSerilog(this WebApplicationBuilder builder)
    {
        var serilogConfiguration = builder.Configuration
            .GetSection(ConfigConstants.SerilogSectionName)
            .Get<SerilogConfiguration>()
            ?? throw new NullReferenceException("Serilog configuration not found");
        
        var logger = new LoggerConfiguration()
            .WriteTo.Console(outputTemplate: serilogConfiguration.LogFormat)
            .MinimumLevel.Is(serilogConfiguration.MinimumLevel)
            .CreateLogger();
        
        builder.Services.AddSerilog(logger);
    }
}