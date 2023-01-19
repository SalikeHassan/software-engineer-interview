namespace Zip.Installments.Api.StartupServicesSetup;

public static class ProblemDetailsSetup
{
    public static void AddProblemDetailsSetup(this IServiceCollection services, IWebHostEnvironment environment)
    {
        services.AddProblemDetails(options =>
        {
            //Include error detail based for Development and Staging environment
            options.IncludeExceptionDetails = (ctx, env) => environment.IsDevelopment() || environment.IsStaging();
        });
    }
}
