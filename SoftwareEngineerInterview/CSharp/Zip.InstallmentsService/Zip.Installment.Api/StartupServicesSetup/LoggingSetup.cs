namespace Zip.Installments.Api.StartupServicesSetup;

using Serilog;

/// <summary>
/// Class defines extension method for serilog configuration
/// </summary>
public static class LoggingSetup
{
    /// <summary>
    /// Method adds service dependency for serilog
    /// </summary>
    /// <param name="logging"><see cref="ILoggingBuilder"/></param>
    /// <param name="configuration"><see cref="IConfiguration"/></param>
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
