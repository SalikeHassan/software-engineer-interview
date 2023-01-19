namespace Zip.Installments.Api.StartupServicesSetup;

/// <summary>
/// Class defines extension method for database context configuration
/// </summary>
public static class DatabaseSetup
{
    /// <summary>
    /// Method adds service dependency for database context
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    public static void AddDatabaseSetup(this IServiceCollection services)
    {
        services.AddDbContext<ZipPayContext>(option => option.UseInMemoryDatabase("TestDb"));
    }
}
