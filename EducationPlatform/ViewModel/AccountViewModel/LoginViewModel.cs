using EducationPlatform.Constants;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EducationPlatform.ViewModel.AccountViewModel
{
	public class LoginViewModel
	{
		[EmailAddress, Required(ErrorMessage = ValidationMessages.RequiredFiled)]
		public string Email { get; set; }

		[Required(ErrorMessage = ValidationMessages.RequiredFiled)]
		public string Password { get; set; }

		public bool RememberMe { get; set; }	
	}
}
