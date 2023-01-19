namespace Zip.Installments.Api.StartupServicesSetup;

using Serilog;

public static class LoggingSetup
{
    public static void AddLoggingSetup(this ILoggingBuilder logging, IConfiguration configuration)
    {
        //Configuration of Serilog
        var logger = new LoggerConfiguration()
                            .ReadFrom
                            .Configuration(configuration)
                            .Enrich.FromLogContext()
                            .CreateLogger();

        logging.ClearProviders();
        logging.AddSerilog(logger);
    }
}
