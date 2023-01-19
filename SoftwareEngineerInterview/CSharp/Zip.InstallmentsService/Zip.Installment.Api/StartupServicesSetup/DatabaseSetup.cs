namespace Zip.Installments.Api.StartupServicesSetup;

public static class DatabaseSetup
{
    public static void AddDatabaseSetup(this IServiceCollection services)
    {
        services.AddDbContext<ZipPayContext>(option => option.UseInMemoryDatabase("TestDb"));
        services.AddTransient<IPaymentInstallementPlan, PaymentInstallmentPlan>();
    }

}
