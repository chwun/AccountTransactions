using System.Reflection;
using System.Text.Json.Serialization;
using AccountTransactions.Api.Data;
using AccountTransactions.Api.Data.Repositories;
using AccountTransactions.Api.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace AccountTransactions.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration.AddEnvironmentVariables(prefix: "ACCOUNTTRANSACTIONS_");

        ConfigureServices(builder.Services, builder.Configuration);

        // builder.Services.AddAutoMapper(typeof(Program));

        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "AccountTransactions.Api",
                    Description = "Backend for AccountTransactions"
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

        bool enableDevCors = builder.Configuration.GetValue<bool>("EnableDevCors", false);
        if (enableDevCors)
        {
            builder.Services.AddCors(options => options.AddPolicy("DevPolicy", builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            }));
        }

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        if (enableDevCors)
        {
            app.UseCors("DevPolicy");
        }

        app.Run();
    }

    private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        ConfigureDbContext(services, configuration);
        ConfigureRepositories(services);

        ConfigureHelpers(services);
    }

    private static void ConfigureDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Db");
        var serverVersion = MariaDbServerVersion.AutoDetect(connectionString);

        services.AddDbContext<AppDbContext>(
            dbContextOptions => dbContextOptions
                .UseMySql(connectionString, serverVersion));
    }

    private static void ConfigureRepositories(IServiceCollection services)
    {
        services.AddScoped<ITransactionRepository, TransactionRepository>();
    }

    private static void ConfigureHelpers(IServiceCollection services)
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
    }
}