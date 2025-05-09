using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.Interfaces
{
	public interface IUserUtilityService
	{
		Task<List<string>> GenerateUniqueEmailsAsync(string Email, string? FirstName, string? LastName);
	}
}
