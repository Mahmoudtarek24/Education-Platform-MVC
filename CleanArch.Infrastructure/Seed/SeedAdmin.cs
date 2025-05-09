using CleanArch.Domain.Entity;
using CleanArch.Domain.Enums;
using CleanArch.Domain.Interfaces;
using CleanArch.Infrastructure.Context;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Infrastructure.Seed
{
	public static class SeedAdmin
	{
		public static async Task SeedAdminAsync(IServiceProvider serviceProvider)
		{
			var scope = serviceProvider.CreateScope();
			var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

			var admin = new ApplicationUser()
			{
				Email = "Admin.ad123@gmail.com",
				UserName = "Admin1",
				FullName = "Admin",
				DateOfBirth = default(DateTime),
				EmailConfirmed = true,
				Role = (UserRole)Enum.Parse(typeof(UserRole), "Admin"),
				CreateOn = DateTime.Now,
				PasswordHash = "12345678"
			};

			var user=await unitOfWork.UserManager.FindByEmailAsync(admin.Email);	

			if(user is null)
			{
				await unitOfWork.UserManager.AddAsync(admin);
				await unitOfWork.Save();
			}
				
		}
	}
}
