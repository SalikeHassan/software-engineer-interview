namespace Zip.Installement.Api;

using Zip.Installments.Api.StartupServicesSetup;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration;
        var environment = builder.Environment;

        //Serilog service configuration
        builder.Logging.AddLoggingSetup(configuration);

        //MediatR service configuration
        builder.Services.AddMediatRSetup();

        //Api versioning service configuration
        builder.Services.AddApiVersioningSetup();

        //Fluent validation api service configuration
        builder.Services.AddFluentApiSetup();

        //Swagger service configuration
        builder.Services.AddSwaggerSetup();

        //Database context service configuration
        builder.Services.AddDatabaseSetup();

        //Problem details service configuration
        builder.Services.AddProblemDetailsSetup(environment);

        //Middleware configuration
        var app = builder.Build();

        app.UseProblemDetails();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}