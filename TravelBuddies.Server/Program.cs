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

			builder.Services.AddScoped<UserManager<ApplicationUser>>();
			builder.Services.AddScoped<IRepository, Repository>();
			builder.Services.AddScoped<IBlobService, BlobService>();
			//builder.Services.AddScoped<SignInManager<ApplicationUser>>();

			// Add services to the container.
			builder.Services.PolicyConfigure();
			
			builder.Services.CorsesConfigure();

			builder.Services.AddToken(builder.Configuration);

			builder.Services.AddControllers(cfg =>
			{
				cfg.Filters.Add(typeof(ExceptionHandler));
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

			app.MapSwagger()
				.RequireAuthorization();

			//app.SeedData().Wait();

			app.Run();
		}
	}
}
