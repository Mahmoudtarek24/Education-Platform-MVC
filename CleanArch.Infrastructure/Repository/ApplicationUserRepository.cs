using CleanArch.Domain.Entity;
using CleanArch.Domain.Interfaces;
using CleanArch.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Infrastructure.Repository
{
	public class ApplicationUserRepository : GenaricRepository<ApplicationUser>, IApplicationUserRepository
	{
		public ApplicationUserRepository(EducationPlatformDbContext context) : base(context) { }

		public async Task<ApplicationUser?> FindByEmailAsync(string email)
		{
			return await context.applicationUsers.Where(e=>!e.IsDeleted).SingleOrDefaultAsync(e => e.Email == email);
		}



		public async Task<ApplicationUser?> FindByNameAsync(string userName)
		{
			return await context.applicationUsers.SingleOrDefaultAsync(e => e.UserName == userName);
		}

		public async Task<bool> IsEmailConfirmed(string email)
		{
			return await context.applicationUsers.Where(e => e.Email == email)
												.Select(e => e.EmailConfirmed)
												.FirstOrDefaultAsync();
		}
		public async Task<bool> IsDeletedUser(string email)
		{
			return await context.applicationUsers.Where(e => e.Email == email)
												.Select(e => !e.IsDeleted)
												.FirstOrDefaultAsync();
		}

	}
}
