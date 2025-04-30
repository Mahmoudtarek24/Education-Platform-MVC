using Microsoft.AspNetCore.Mvc.Rendering;

namespace EducationPlatform.ViewModel.CourseViewModel
{
	public class UpdateCourseStatusViewModel
	{
		public int CourseId { get; set; }	
		public string Status { get; set; }
		public IEnumerable<SelectListItem>? CourseStates { get; set; }
	}
}
