using Newtonsoft.Json;
using OnlineClothes.Api.Extensions;
using OnlineClothes.Application.Middlewares;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
	.AddNewtonsoftJson(mvcNewtonsoftJsonOptions =>
	{
		mvcNewtonsoftJsonOptions.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
		mvcNewtonsoftJsonOptions.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
	});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.ConfigStartup(builder.Configuration);

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

app.UseCors(StartupExtension.CorsAnyOrigin);
app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
