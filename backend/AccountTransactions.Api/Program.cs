using AccountTransactions.Api.Data;
using AccountTransactions.Api.Data.Repositories;
using AccountTransactions.Api.Helpers;
using AccountTransactions.Api.Models.Updater;
using AccountTransactions.Api.Services;
using AccountTransactions.Api.Services.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json.Serialization;

namespace AccountTransactions.Api;

public class Program
{
	public static void Main(string[] args)
	{
		WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

		builder.Configuration.AddEnvironmentVariables("ACCOUNTTRANSACTIONS_");

		ConfigureServices(builder.Services, builder.Configuration);

		builder.Services.AddControllers().AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });

		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen(options =>
		{
			options.SwaggerDoc("v1", new()
			{
				Version = "v1",
				Title = "AccountTransactions.Api",
				Description = "Backend for AccountTransactions"
			});

			string xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
			options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
		});

		bool enableDevCors = builder.Configuration.GetValue("EnableDevCors", false);
		if (enableDevCors)
		{
			builder.Services.AddCors(options => options.AddPolicy("DevPolicy", builder =>
			{
				builder.AllowAnyOrigin();
				builder.AllowAnyHeader();
				builder.AllowAnyMethod();
			}));
		}

		WebApplication app = builder.Build();

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

		app.MapControllers();

		app.Run();
	}

	private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
	{
		ConfigureDbContext(services, configuration);
		ConfigureRepositories(services);

		ConfigureHelpers(services);

		services.AddTransient<ITransactionAccess, TransactionAccess>();
		services.AddTransient<ITransactionImportFileAccess, TransactionImportFileAccess>();
		services.AddTransient<ICategoryAccess, CategoryAccess>();

		services.AddTransient<ITransactionFileImport, TransactionFileImport>();
		services.AddTransient<IIngCsvImport, IngCsvImport>();
		services.AddTransient<IBarclaysExcelImport, BarclaysExcelImport>();
		services.AddTransient<ICategoryAssignment, CategoryAssignment>();
		services.AddTransient<ICategoryConditionMatcher, CategoryConditionMatcher>();

		services.AddTransient<ITransactionUpdater, TransactionUpdater>();
		services.AddTransient<ICategoryUpdater, CategoryUpdater>();
		services.AddTransient<ICategoryConditionUpdater, CategoryConditionUpdater>();
	}

	private static void ConfigureDbContext(IServiceCollection services, IConfiguration configuration)
	{
		string? connectionString = configuration.GetConnectionString("Db");
		ServerVersion? serverVersion = ServerVersion.AutoDetect(connectionString);

		services.AddDbContext<AppDbContext>(
			dbContextOptions => dbContextOptions
				.UseMySql(connectionString, serverVersion));
	}

	private static void ConfigureRepositories(IServiceCollection services)
	{
		services.AddScoped<ITransactionRepository, TransactionRepository>();
		services.AddScoped<ITransactionImportFileRepository, TransactionImportFileRepository>();
		services.AddScoped<ICategoryRepository, CategoryRepository>();
	}

	private static void ConfigureHelpers(IServiceCollection services)
	{
		services.AddTransient<IDateTimeProvider, DateTimeProvider>();
	}
}