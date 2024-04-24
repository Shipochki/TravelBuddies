namespace TravelBuddies.Server
{
	using Microsoft.AspNetCore.Identity;
	using Microsoft.EntityFrameworkCore;
	using TravelBuddies.Application;
	using TravelBuddies.Application.Constants;
	using TravelBuddies.Application.Repository;
	using TravelBuddies.Domain.Entities;
	using TravelBuddies.Infrastructure;
	using TravelBuddies.Infrastructure.Repository;
	using TravelBuddies.Presentation.Configurations;

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
				.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<TravelBuddiesDbContext>()
				.AddDefaultTokenProviders()
				.AddDefaultUI();

			builder.Services.AddScoped<UserManager<ApplicationUser>>();
			builder.Services.AddScoped<IRepository, Repository>();
			//builder.Services.AddScoped<SignInManager<ApplicationUser>>();

			// Add services to the container.
			builder.Services.PolicyConfigure();
			
			builder.Services.CorsesConfigure();

			builder.Services.AddControllers();
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

			app.UseAuthorization();

			app.MapControllers();
			app.MapControllerRoute(
				name: "default",
				pattern: "{controller}/{action=Index}/{id?}");

			app.MapFallbackToFile("/index.html");
			app.MapIdentityApi<ApplicationUser>();
			app.MapSwagger()
				.RequireAuthorization();

			//app.SeedData().Wait();

			app.Run();
		}
	}
}
