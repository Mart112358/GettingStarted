using Microsoft.Extensions.Logging;
using OpenTelemetry;
using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.ClearProviders();
    builder.AddOpenTelemetry(options =>
    {
        options
            .ConfigureResource(c => c.AddService("GettingStarted"))
            .AddConsoleExporter()
            .AddOtlpExporter(config =>
            {
                // config.Endpoint = new Uri("http://localhost:4318");
                config.Protocol = OtlpExportProtocol.HttpProtobuf;
            });
    });
});

var logger = loggerFactory.CreateLogger<Program>();

for (var i = 0; i < 100; i++)
{
    logger.LogError("{count}: Hello World!", i);
    await Task.Delay(1000);
}
