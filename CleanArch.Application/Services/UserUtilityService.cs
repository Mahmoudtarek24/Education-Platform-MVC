using CleanArch.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.Services
{
	public class UserUtilityService : IUserUtilityService
	{
		private readonly IAccountService accountService;
		public List<string> suggestions { get; }
		public UserUtilityService(IAccountService accountService)
		{
			this.accountService = accountService;
			suggestions = new List<string>();
		}

		public async Task<List<string>> GenerateUniqueEmailsAsync(string Email, string? FirstName, string? LastName)
		{
			string EmailName = Email.Split('@')[0];
			string domain = Email.Split('@')[1];

			if (FirstName is not null && LastName is not null)
			{
				while (suggestions.Count < 3)
				{
					var number = new Random().Next(100, 999);
					var checkEmail = $"{FirstName}.{LastName}{number}@{domain}";

					await AddGeneratedEmail(checkEmail, suggestions);

				}
				return suggestions;
			}
			else if (LastName is not null || FirstName is not null)
			{
				string name = LastName ?? FirstName;


				while (suggestions.Count < 3)
				{
					var number = new Random().Next(10, 99);
					var checkEmail = $"{EmailName}.{name}{number}@{domain}";
					await AddGeneratedEmail(checkEmail, suggestions);

				}
				return suggestions;
			}
			else
			{
				while (suggestions.Count < 3)
				{
					var number = new Random().Next(100, 999);
					var checkEmail = $"{Email}{number}@{domain}";

					await AddGeneratedEmail(checkEmail, suggestions);
				}
				return suggestions;

			}

		}

		private async Task AddGeneratedEmail(string checkEmail, List<string> suggestions)
		{

			var IsValidEmail = await accountService.IsEmailTakenAsync(checkEmail);
			if (!IsValidEmail || !suggestions.Contains(checkEmail))
			{
				suggestions.Add(checkEmail);
			}
		}


	}
}
