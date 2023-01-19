namespace Zip.Installments.Api.StartupServicesSetup;

/// <summary>
/// Class defines extension method for swagger service configuration
/// </summary>
public static class SwaggerSetup
{
    /// <summary>
    /// Method adds service dependency for Swagger
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    public static void AddSwaggerSetup(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "V1",
                Title = "Payment Installment Plan API",
                Description = "API for creating and retriving payment installment plan",
                Contact = new OpenApiContact()
                {
                    Name = "Salike Hassan",
                    Email = "salikehassan93@gmail.com"
                }
            });
        });
    }
}
