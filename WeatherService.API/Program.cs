using Serilog;
using WeatherService.API.Configuration;
using WeatherService.API.Extensions;

var serilogConfiguration = new SerilogConfiguration();
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Is(serilogConfiguration.MinimumLevel)
    .WriteTo.Console(outputTemplate: serilogConfiguration.LogFormat)
    .CreateLogger();

try
{
    Log.Information("Starting the application");
    var builder = WebApplication.CreateBuilder(args);

    builder.ConfigureSerilog();
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var applicationInsightsKey = builder.Configuration["Azure:ApplicationInsights:InstrumentationKey"];
    if (applicationInsightsKey == null)
    {
        throw new Exception("ApplicationInsights InstrumentationKey is missing or invalid");
    }

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.Information("Stopping the application");
    Log.CloseAndFlush();
}
