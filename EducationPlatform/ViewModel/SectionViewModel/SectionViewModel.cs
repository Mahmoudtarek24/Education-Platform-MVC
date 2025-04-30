using EducationPlatform.Constants;
using System.ComponentModel.DataAnnotations;

namespace EducationPlatform.ViewModel.SectionViewModel
{
	public class SectionViewModel
	{
		public int? SectionId { get; set; }
		public int CourseId { get; set; }	
		public string? CourseName { get; set; }

		[Required(ErrorMessage = ValidationMessages.RequiredFiled)]
		[Display(Name ="Section Name"),MaxLength(150,ErrorMessage =ValidationMessages.MaxLength)]
		public string Name { get; set; }

		[Required(ErrorMessage = ValidationMessages.RequiredFiled)]
		[Display(Name ="Section Order")]
		[Range(1,100,ErrorMessage =ValidationMessages.Rang)]
		public byte Order { get; set; }
	}
}
