using EducationPlatform.Constants;
using System.ComponentModel.DataAnnotations;

namespace EducationPlatform.ViewModel.AccountViewModel
{
	public class UpdateAccountViewModel
	{
		public IFormFile? ImageData { get; set; }
		[Display(Name = "First Name")]
		[StringLength(20, MinimumLength = 3, ErrorMessage = ValidationMessages.StringLent)]
		public string FirstName { get; set; }
		[Display(Name = "Last Name")]
		[StringLength(20, MinimumLength = 3, ErrorMessage = ValidationMessages.StringLent)]
		public string LastName { get; set; }	

        public string? ImagePath { get; set; }	
	}
}
