namespace Zip.Installments.Api.StartupServicesSetup;

using FluentValidation.AspNetCore;

public static class FluentApiValidationSetup
{
    public static void AddFluentApiSetup(this IServiceCollection services)
    {
        services.AddControllers()
            .AddFluentValidation(options =>
            {
                options.ImplicitlyValidateChildProperties = true;
                options.ImplicitlyValidateRootCollectionElements = true;

                options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            });
    }
}
