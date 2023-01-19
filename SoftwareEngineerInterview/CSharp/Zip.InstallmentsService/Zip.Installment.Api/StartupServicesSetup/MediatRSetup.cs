namespace Zip.Installments.Api.StartupServicesSetup;

public static class MediatRSetup
{
    public static void AddMediatRSetup(this IServiceCollection services)
    {

        services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

        //Register mediatR service for command classes
        services.AddMediatR(AppDomain.CurrentDomain.Load(Constants.CommandClassAssemblyName));

        //Register mediatR service for query classes
        services.AddMediatR(AppDomain.CurrentDomain.Load(Constants.QueryClassAssemblyName));
    }
}
