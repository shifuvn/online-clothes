using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OnlineClothes.Application;
using OnlineClothes.Application.Services.Auth.Models;
using OnlineClothes.Infrastructure;
using OnlineClothes.Persistence;

namespace OnlineClothes.Api.Extensions;

public static class StartupExtension
{
	public const string CorsAnyOrigin = "AnyOrigin";

	public static void ConfigStartup(this IServiceCollection services, IConfiguration configuration)
	{
		services.ConfigSwagger();
		services.ConfigAuth(configuration);
		services.ConfigCors();
		services.ConfigAutoMapper();

		services.RegisterPersistenceLayer(configuration);
		services.RegisterApplicationLayer(configuration);
		services.RegisterInfrastructureLayer(configuration);

		services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
	}

	/// <summary>
	/// Config swagger
	/// </summary>
	private static void ConfigSwagger(this IServiceCollection services)
	{
		services.AddSwaggerGen(swaggerGenOptions =>
		{
			swaggerGenOptions.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
			var jwtSchema = new OpenApiSecurityScheme
			{
				Scheme = JwtBearerDefaults.AuthenticationScheme,
				BearerFormat = "Jwt",
				Name = "Authorization",
				In = ParameterLocation.Header,
				Type = SecuritySchemeType.Http,
				Description = "Enter ONLY `JWT token` here",
				Reference = new OpenApiReference
				{
					Id = JwtBearerDefaults.AuthenticationScheme,
					Type = ReferenceType.SecurityScheme
				}
			};
			swaggerGenOptions.AddSecurityDefinition(jwtSchema.Reference.Id, jwtSchema);
			swaggerGenOptions.AddSecurityRequirement(new OpenApiSecurityRequirement
			{
				{ jwtSchema, ArraySegment<string>.Empty }
			});
			swaggerGenOptions.CustomSchemaIds(type => type.ToString());
		});
	}

	/// <summary>
	/// Config authentication
	/// </summary>
	private static void ConfigAuth(this IServiceCollection services, IConfiguration configuration)
	{
		var jwtAuthConfigureSection = configuration.GetSection("DefaultJwtAuthentication");
		services.Configure<AuthConfiguration>(jwtAuthConfigureSection);

		services
			.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				var authConfiguration = jwtAuthConfigureSection.Get<AuthConfiguration>();

				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfiguration.Secret)),
					ValidateIssuer = authConfiguration.ValidateIssuer,
					ValidateAudience = authConfiguration.ValidateAudience,
					ValidIssuers = authConfiguration.ValidIssuers,
					ValidAudiences = authConfiguration.ValidAudiences
				};
			});

		services.AddAuthorization();
	}

	/// <summary>
	/// Config cors
	/// </summary>
	private static void ConfigCors(this IServiceCollection services)
	{
		services.AddCors(opt =>
		{
			opt.AddPolicy(CorsAnyOrigin, builder => builder
				.AllowAnyHeader()
				.AllowAnyMethod()
				.AllowAnyOrigin());
		});
	}

	private static void ConfigAutoMapper(this IServiceCollection services)
	{
		services.AddAutoMapper(
			Assembly.GetExecutingAssembly(),
			InfrastructureAssembly.ExecutingAssembly,
			ApplicationLayerAssembly.ExecutingAssembly,
			PersistenceAssembly.ExecutingAssembly);
	}
}
