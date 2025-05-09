using CleanArch.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Domain.Interfaces
{
	public interface IApplicationUserRepository :IGenaricRepository<ApplicationUser>
	{
		Task<ApplicationUser?> FindByEmailAsync(string email);
		Task<ApplicationUser?> FindByNameAsync(string userName);
		Task<bool> IsEmailConfirmed(string email);
		Task<bool> IsDeletedUser(string email);

		//Task<bool> UsernameExistsAsync(string username);
		//	Task<bool> EmailExistsAsync(string email);
	}
}
