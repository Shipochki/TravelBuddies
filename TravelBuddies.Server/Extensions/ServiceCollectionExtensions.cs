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
		public static void AddServices(this WebApplicationBuilder builder)
		{
			var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
			builder.Services.AddDbContext<TravelBuddiesDbContext>(options =>
				options.UseSqlServer(connectionString));

			builder.Services.SetupMediatR();
			builder.Services.SetupIdentity();
			builder.Services.AddScopedServices();
			builder.Services.PolicyConfigure();
			builder.Services.CorsesConfigure();
			builder.Services.AddJwtAuthentication(builder.Configuration);
			builder.Services.AddGlobalExceptionHandler();
			builder.Services.ConfigureApiBehavior();

			StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];
		}

		private static IServiceCollection AddScopedServices(this IServiceCollection services)
		{
			services.AddScoped<UserManager<ApplicationUser>>();
			services.AddScoped<IRepository, Repository>();
			//services.AddScoped<IBlobService, BlobService>();
			services.AddScoped<IMailSender, MailSender>();
			services.AddScoped<IAuthTokenService, AuthTokenService>();
			services.AddScoped<IStripeService, StripeService>();

			return services;
		}

		private static IServiceCollection AddGlobalExceptionHandler(this IServiceCollection services)
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

		private static IServiceCollection SetupMediatR(this IServiceCollection services)
		{
			services.AddMediatR(cf 
				=> cf.RegisterServicesFromAssembly(typeof(BaseHandler).Assembly));

			return services;
		}

		private static IServiceCollection SetupIdentity(this IServiceCollection services)
		{
			services
				.AddIdentity<ApplicationUser, IdentityRole>(options =>
			options.SignIn.RequireConfirmedAccount = false)
				.AddEntityFrameworkStores<TravelBuddiesDbContext>()
				.AddDefaultTokenProviders();

			return services;
		}

		private static IServiceCollection CorsesConfigure(this IServiceCollection service)
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
	}
}
