using Azure.Monitor.OpenTelemetry.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// configuration
builder.Configuration.AddJsonFile("appsettings.json", true, true);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true);
builder.Configuration.AddEnvironmentVariables();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var applicationInsightsKey = builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"];
if (applicationInsightsKey == null)
{
    throw new Exception("ApplicationInsights InstrumentationKey is missing or invalid");
}
builder.Services.AddOpenTelemetry().UseAzureMonitor(options => options.ConnectionString = applicationInsightsKey);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
