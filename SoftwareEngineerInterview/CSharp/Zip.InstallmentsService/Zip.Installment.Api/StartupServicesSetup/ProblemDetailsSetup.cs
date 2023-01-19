namespace Zip.Installments.Api.StartupServicesSetup;

/// <summary>
/// Class defines extension method for problem details configuration
/// </summary>
public static class ProblemDetailsSetup
{
    /// <summary>
    /// Method adds service dependency for problem details
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    /// <param name="environment"><see cref="IWebHostEnvironment"/></param>
    public static void AddProblemDetailsSetup(this IServiceCollection services, IWebHostEnvironment environment)
    {
        services.AddProblemDetails(options =>
        {
            //Include error detail based for Development and Staging environment
            options.IncludeExceptionDetails = (ctx, env) => environment.IsDevelopment() || environment.IsStaging();
        });
    }
}
