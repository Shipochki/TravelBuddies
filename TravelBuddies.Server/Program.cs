namespace TravelBuddies.Server
{
    using TravelBuddies.Domain.Common;
    using TravelBuddies.Presentation.Extensions;

	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.SetUpApplicationBuilder();

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
