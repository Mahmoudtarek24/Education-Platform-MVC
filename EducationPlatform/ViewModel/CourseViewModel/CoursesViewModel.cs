using CleanArch.Application.ResponseDTO_s.CourseRespondDto;

namespace EducationPlatform.ViewModel.CourseViewModel
{
	public class CoursesViewModel
	{
		public IEnumerable<CourseRespond> Courses { get; set; }
		public PaginationFilter Pagination { get; set; } = new PaginationFilter();
	}
}
