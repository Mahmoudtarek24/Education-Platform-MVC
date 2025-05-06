using CleanArch.Application.Interfaces;
using CleanArch.Application.Services;
using CleanArch.Application.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<ICourseServices,CourseServices>();
			services.AddScoped<ISectionServices,SectionServices>();
			services.AddScoped<IImageServices, ImageServices>();
			services.Configure<SupabaseSettings>(configuration.GetSection("supabaseSettings"));
			services.AddScoped<IStorageService, StorageServices>();
			services.AddScoped<IVideoServices, VideoServices>();

			return services;
		}
	}
}
