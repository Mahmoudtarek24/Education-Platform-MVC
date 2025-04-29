using EducationPlatform.Constants;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace EducationPlatform.ViewModel.CourseViewModel
{
	public class CreateCourseViewModel
	{
		[Display(Name = "Course Name"),Required(ErrorMessage = ValidationMessages.RequiredFiled)]
		[StringLength(100,MinimumLength =2,ErrorMessage =ValidationMessages.StringLent)]
		public string CourseName { get; set; }

		[Required(ErrorMessage = ValidationMessages.RequiredFiled),MaxLength(1000,ErrorMessage =ValidationMessages.MaxLength)]

		public string Description { get; set; }
		[Required(ErrorMessage = ValidationMessages.RequiredFiled),Range(1,20000,ErrorMessage =ValidationMessages.Rang)]
		public decimal Price { get; set; }

		[Required(ErrorMessage = ValidationMessages.RequiredFiled)]
		[Range(1,100,ErrorMessage =ValidationMessages.RangValue)]
		public decimal Discount { get; set; }
		 
		public bool IsSequentialWatch { get; set; }

		[Display(Name ="Course Image")]
		public IFormFile CourseImage { get; set; }


		public List<string> SelectedLevel { get; set; }	= new List<string>();	

		public IEnumerable<SelectListItem> AvailableLevels { get; set; }	

	}
}
