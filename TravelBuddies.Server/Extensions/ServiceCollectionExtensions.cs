namespace TravelBuddies.Presentation.Extensions
{
	using Microsoft.AspNetCore.Identity;
	using TravelBuddies.Application.Common.Interfaces.AzureStorage;
	using TravelBuddies.Application.Common.Interfaces.MailSender;
	using TravelBuddies.Application.Common.Interfaces.Repository;
	using TravelBuddies.Application.Common.Interfaces.Stripe;
	using TravelBuddies.Application.Common.Interfaces;
	using TravelBuddies.Domain.Entities;
	using TravelBuddies.Infrastructure.ExternalVendors.AzureStorage;
	using TravelBuddies.Infrastructure.ExternalVendors.MailSender;
	using TravelBuddies.Infrastructure.ExternalVendors.Stripe;
	using TravelBuddies.Infrastructure.Identity;
	using TravelBuddies.Infrastructure.Repository;
	using TravelBuddies.Presentation.Filters;
	using Microsoft.AspNetCore.Mvc;
	using TravelBuddies.Presentation.Responses;
	using TravelBuddies.Application;
	using TravelBuddies.Infrastructure;
	using TravelBuddies.Domain.Common;
	using Stripe;
	using Microsoft.EntityFrameworkCore;

	public static class ServiceCollectionExtensions
	{
		public static void SetUpApplicationBuilder(this WebApplicationBuilder builder)
		{
			var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
			builder.Services.AddDbContext<TravelBuddiesDbContext>(options =>
				options.UseSqlServer(connectionString));

			builder.Services.ConfigureMediatR();
			builder.Services.ConfigureIdentity();
			builder.Services.ConfigureScopedServices();
			builder.Services.ConfigurePolicy();
			builder.Services.ConfigureCorses();
			builder.Services.ConfigureJwtAuthentication(builder.Configuration);
			builder.Services.ConfigureGlobalExceptionHandler();
			builder.Services.ConfigureApiBehavior();
			builder.Services.ConfigureSwagger();

			StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];
		}

		private static IServiceCollection ConfigureScopedServices(this IServiceCollection services)
		{
			services.AddScoped<UserManager<ApplicationUser>>();
			services.AddScoped<IRepository, Repository>();
			services.AddScoped<IBlobService, BlobService>();
			services.AddScoped<IMailSender, MailSender>();
			services.AddScoped<IAuthTokenService, AuthTokenService>();
			services.AddScoped<IStripeService, StripeService>();

			return services;
		}

		private static IServiceCollection ConfigureGlobalExceptionHandler(this IServiceCollection services)
		{
			services.AddControllers(options =>
			{
				options.Filters.Add(typeof(ExceptionHandler));
			});

			return services;
		}

		private static IServiceCollection ConfigureApiBehavior(this IServiceCollection services)
		{
			services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = ErrorResponse.GenerateErrorResponse;
			});

			return services;
		}

		private static IServiceCollection ConfigureMediatR(this IServiceCollection services)
		{
			services.AddMediatR(cf 
				=> cf.RegisterServicesFromAssembly(typeof(BaseHandler).Assembly));

			return services;
		}

		private static IServiceCollection ConfigureIdentity(this IServiceCollection services)
		{
			services
				.AddIdentity<ApplicationUser, IdentityRole>(options =>
			options.SignIn.RequireConfirmedAccount = false)
				.AddEntityFrameworkStores<TravelBuddiesDbContext>()
				.AddDefaultTokenProviders();

			return services;
		}

		private static IServiceCollection ConfigureCorses(this IServiceCollection service)
		{
			service.AddCors(options =>
			{
				options.AddPolicy(ApplicationCorses.AllowOrigin,
					builder => builder
					.WithOrigins("https://localhost:5173")
					.AllowAnyMethod()
					.AllowAnyHeader()
					.AllowCredentials());
			});

			return service;
		}

		private static IServiceCollection ConfigureSwagger(this IServiceCollection services)
		{
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen();

			return services;
		}
	}
}
