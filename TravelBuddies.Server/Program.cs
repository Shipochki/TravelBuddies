namespace TravelBuddies.Server
{
	using Microsoft.AspNetCore.Identity;
	using Microsoft.EntityFrameworkCore;
	using TravelBuddies.Application;
	using TravelBuddies.Domain.Common;
	using TravelBuddies.Application.Repository;
	using TravelBuddies.Domain.Entities;
	using TravelBuddies.Infrastructure;
	using TravelBuddies.Infrastructure.Repository;
	using TravelBuddies.Presentation.Configurations;
	using TravelBuddies.Application.Interfaces.AzureStorage;
	using TravelBuddies.Infrastructure.ExternalVendors.AzureStorage;
	using TravelBuddies.Presentation.Filters;
	using Microsoft.AspNetCore.Mvc;
	using TravelBuddies.Presentation.Contract;
	using TravelBuddies.Application.Interfaces.MailSender;
	using TravelBuddies.Infrastructure.ExternalVendors.MailSender;
	using TravelBuddies.Application.Interfaces.Stripe;
	using TravelBuddies.Infrastructure.ExternalVendors.Stripe;
	using Stripe;

	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
			builder.Services.AddDbContext<TravelBuddiesDbContext>(options =>
				options.UseSqlServer(connectionString));

			builder.Services.AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(BaseHandler).Assembly));

			builder.Services
				.AddIdentity<ApplicationUser, IdentityRole>(options =>
			options.SignIn.RequireConfirmedAccount = false)
				.AddEntityFrameworkStores<TravelBuddiesDbContext>()
				.AddDefaultTokenProviders();

			StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];

			builder.Services.AddScoped<UserManager<ApplicationUser>>();
			builder.Services.AddScoped<IRepository, Repository>();
			builder.Services.AddScoped<IBlobService, BlobService>();
			builder.Services.AddScoped<IMailSender, MailSender>();
			builder.Services.AddScoped<IStripeService, StripeService>();

			// Add services to the container.
			builder.Services.PolicyConfigure();
			
			builder.Services.CorsesConfigure();

			builder.Services.AddToken(builder.Configuration);

			builder.Services.AddControllers(options =>
			{
				options.Filters.Add(typeof(ExceptionHandler));
			});

			builder.Services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = ErrorResponse.GenerateErrorResponse;
			});

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			app.UseDefaultFiles();
			app.UseStaticFiles();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseRouting();

			app.UseCors(ApplicationCorses.AllowOrigin);

			app.UseHttpsRedirection();

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllers();
			app.MapControllerRoute(
				name: "default",
				pattern: "{controller}/{action=Index}/{id?}");

			app.MapFallbackToFile("/index.html");

			app.MapSwagger()
				.RequireAuthorization();

			app.Run();
		}
	}
}
