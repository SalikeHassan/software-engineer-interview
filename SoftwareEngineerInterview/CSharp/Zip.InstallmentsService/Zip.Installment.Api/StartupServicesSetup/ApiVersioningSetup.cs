namespace Zip.Installments.Api.StartupServicesSetup;

/// <summary>
/// Class defines extension method for api versioning configuration
/// </summary>
public static class ApiVersioningSetup
{
    /// <summary>
    /// Method adds service dependency for configuring API versioning 
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    public static void AddApiVersioningSetup(this IServiceCollection services)
    {
        services.AddApiVersioning(opt =>
        {
            //Code to setup api versioning
            opt.ReportApiVersions = true;
            opt.DefaultApiVersion = new ApiVersion(1, 0);
            opt.AssumeDefaultVersionWhenUnspecified = true;
        });

        //Code to fix the swagger while using multiple version of api
        //Install package Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer
        services.AddVersionedApiExplorer(opt =>
        {
            opt.GroupNameFormat = "'v'VVV";
            opt.SubstituteApiVersionInUrl = true;
        });
    }
}
