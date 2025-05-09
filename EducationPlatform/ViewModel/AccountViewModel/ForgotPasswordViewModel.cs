using EducationPlatform.Constants;
using System.ComponentModel.DataAnnotations;

namespace EducationPlatform.ViewModel.AccountViewModel
{
	public class ForgotPasswordViewModel
	{
		[Required(ErrorMessage = ValidationMessages.RequiredFiled)]
		[EmailAddress(ErrorMessage = ValidationMessages.EmailAddress)]
		public string Email { get; set; }
	}
}
