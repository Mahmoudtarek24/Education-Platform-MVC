using CleanArch.Application.Interfaces;
using CleanArch.Application.Services;
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
			services.AddScoped<IImageServices, ImageServices>();
			
			return services;
		}
	}
}
