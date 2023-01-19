namespace Zip.Installments.Api.StartupServicesSetup;

public static class SwaggerSetup
{
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
