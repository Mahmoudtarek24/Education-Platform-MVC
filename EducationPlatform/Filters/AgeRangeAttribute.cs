using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace EducationPlatform.Filters
{
	public class AgeRangeAttribute : ValidationAttribute
	{
		private readonly int minimumAge;
		public AgeRangeAttribute(int minAge)
		{
			this.minimumAge = minAge;
			ErrorMessage = $"The minimum age to join is {minAge} years";
		}                                           //value come from user
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			if (value is DateTime dateOfBirth)
			{
				if (dateOfBirth > DateTime.Now)
				{
					return new ValidationResult("Date of birth cannot be in the future.");

				}
				int birthYear = dateOfBirth.Year;
				int currentAge = DateTime.Now.Year - birthYear;

				if (currentAge < minimumAge)
				{
					return new ValidationResult(ErrorMessage);
				}
				

				return ValidationResult.Success;
			}

			return new ValidationResult("Invalid date format.");
		}
	}
}

