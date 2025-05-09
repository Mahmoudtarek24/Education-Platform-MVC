using EducationPlatform.Constants;
using EducationPlatform.Filters;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EducationPlatform.ViewModel.AccountViewModel
{
	public class RegisterViewModel
	{
		[Display(Name ="First Name") ,Required(ErrorMessage = ValidationMessages.RequiredFiled)]
		[StringLength(20,MinimumLength = 3,ErrorMessage =ValidationMessages.StringLent)]	
		public string FirstName { get; set; }
		[Display(Name = "Last Name"), Required(ErrorMessage = ValidationMessages.RequiredFiled)]
		[StringLength(20,MinimumLength = 3,ErrorMessage =ValidationMessages.StringLent)]
		public string LastName { get; set; }

		[Remote(action: "Register", controller: "Account"), Required(ErrorMessage = ValidationMessages.RequiredFiled)]
		[StringLength(14,MinimumLength =6 ,ErrorMessage =ValidationMessages.StringLent)]
		[RegularExpression(RegexPatterns.Username,ErrorMessage =ValidationMessages.InvalidUsername)]
		public string UserName { get; set; }

		[EmailAddress, Required(ErrorMessage = ValidationMessages.RequiredFiled)]
		[Remote(action: "Register",controller: "Account",AdditionalFields = "FirstName,LastName")]
		public string Email { get; set; }
	
	
		[Display(Name ="Date Of Birth"), Required(ErrorMessage = ValidationMessages.RequiredFiled)]
		[AgeRange(14)]
		public DateTime DateOfBirth { get; set; }
		[Required(ErrorMessage = ValidationMessages.RequiredFiled)]
		[StringLength(20, MinimumLength = 8, ErrorMessage = ValidationMessages.StringLent)]
		[RegularExpression(RegexPatterns.Password,ErrorMessage =ValidationMessages.WeakPassword)]
		public string Password { get; set; }
		
		[Compare("Password",ErrorMessage =ValidationMessages.ConfirmPasswordNotMatch)]
		[Display(Name = "Confirm Password"),Required(ErrorMessage = ValidationMessages.RequiredFiled)]	
		public string ConfirmPassword { get; set; }	
	}
}
