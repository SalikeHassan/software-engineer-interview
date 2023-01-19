namespace Zip.Installments.Api.StartupServicesSetup;

using FluentValidation.AspNetCore;

/// <summary>
/// Class defines extension method for fluentvalidation api configuration
/// </summary>
public static class FluentApiValidationSetup
{
    /// <summary>
    /// Method adds service dependency for fluentvalidation api
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
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
