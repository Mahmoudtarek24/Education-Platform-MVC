using CleanArch.Infrastructure;
using CleanArch.Application;
using EducationPlatform.Settings;
using EducationPlatform.Middleware;
using System.Threading.Tasks;
using CleanArch.Infrastructure.Seed;
namespace EducationPlatform
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();

			builder.Services.Configure<VideoSetting>(builder.Configuration.GetSection("VideoSettings"));
			builder.Services.AddInfrastructure(builder.Configuration);
			builder.Services.AddApplication(builder.Configuration);

			//builder.WebHost.ConfigureKestrel(serverOptions =>
			//{
			//    serverOptions.Limits.MaxRequestBodySize = long.MaxValue;
			//});

			builder.Services.AddAuthentication("ElearnPlatformAuth")
				.AddCookie("ElearnPlatformAuth", options =>
				{
					options.LoginPath = "/Account.Login";
					options.Cookie.HttpOnly = true;
					options.SlidingExpiration = true;
				});

			builder.Services.AddDistributedMemoryCache();

			builder.Services.AddSession(options =>
			{
				options.Cookie.HttpOnly = true;
				options.Cookie.IsEssential = true;
			});


			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			using (var scope = app.Services.CreateScope())
			{
				var service = scope.ServiceProvider;
				await SeedAdmin.SeedAdminAsync(service);
			}


			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseRouting();
			app.UseSession();	

		    app.UseAuthentication();	
			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}
