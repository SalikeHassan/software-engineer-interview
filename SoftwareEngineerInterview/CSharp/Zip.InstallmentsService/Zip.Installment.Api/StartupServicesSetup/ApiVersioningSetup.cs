namespace Zip.Installments.Api.StartupServicesSetup;

public static class ApiVersioningSetup
{
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
