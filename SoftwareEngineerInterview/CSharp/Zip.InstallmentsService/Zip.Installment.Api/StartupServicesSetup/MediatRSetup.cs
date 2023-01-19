namespace Zip.Installments.Api.StartupServicesSetup;

/// <summary>
/// Class defines extension method for MediatR configuration
/// </summary>
public static class MediatRSetup
{
    /// <summary>
    /// Method adds service dependency for MediatR
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    public static void AddMediatRSetup(this IServiceCollection services)
    {

        services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

        //Register mediatR service for command classes
        services.AddMediatR(AppDomain.CurrentDomain.Load(Constants.CommandClassAssemblyName));

        //Register mediatR service for query classes
        services.AddMediatR(AppDomain.CurrentDomain.Load(Constants.QueryClassAssemblyName));
    }
}
