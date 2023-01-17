using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Newtonsoft.Json;
using OnlineClothes.Api.ConfigurationServices;
using OnlineClothes.Application.Middlewares;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerUI;

var aspnetEnv = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
var config = new ConfigurationBuilder()
	.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
	.AddJsonFile($"appsettings.{aspnetEnv}.json", optional: true)
	.AddEnvironmentVariables()
	.Build();

var builder = WebApplication.CreateBuilder(args);

// use SeriLog
Log.Logger = new LoggerConfiguration()
	.ReadFrom.Configuration(config)
	.CreateLogger();
builder.Host.UseSerilog();


// Add services to the container.
builder.Services.AddControllers()
	.AddNewtonsoftJson(mvcNewtonsoftJsonOptions =>
	{
		mvcNewtonsoftJsonOptions.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
		mvcNewtonsoftJsonOptions.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
	});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllersWithViews();

builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(swaggerUiOptions =>
	{
		swaggerUiOptions.DisplayOperationId();
		swaggerUiOptions.DisplayRequestDuration();
		swaggerUiOptions.DocExpansion(DocExpansion.None);
	});
}

app.MigrateDatabase();

app.UseCors(StartupExtension.CorsAnyOrigin);
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseSerilogRequestLogging(requestLoggingOptions =>
{
	requestLoggingOptions.MessageTemplate =
		"{IpAddress} {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";
	requestLoggingOptions.EnrichDiagnosticContext = (context, httpContext) =>
	{
		context.Set("IpAddress", httpContext.Connection.RemoteIpAddress);
	};
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapDefaultControllerRoute();

app.Start();
LogKestrelListeningAddress();
app.WaitForShutdown();

void LogKestrelListeningAddress()
{
	var server = app.Services.GetRequiredService<IServer>();
	var addressFeature = server.Features.Get<IServerAddressesFeature>();

	foreach (var addressFeatureAddress in addressFeature?.Addresses!)
	{
		Log.ForContext<Program>()
			.Information("Kestrel is listening on address: {address}", addressFeatureAddress);
	}
}
