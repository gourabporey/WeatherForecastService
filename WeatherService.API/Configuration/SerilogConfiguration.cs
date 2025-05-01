using Serilog.Events;

namespace WeatherService.API.Configuration;

public class SerilogConfiguration
{
    public string LogFormat { get; init; } = "[{Timestamp:HH:mm:ss} {Level}] {Message}{NewLine}{Exception}";
    public LogEventLevel MinimumLevel { get; init; } = LogEventLevel.Information;
}