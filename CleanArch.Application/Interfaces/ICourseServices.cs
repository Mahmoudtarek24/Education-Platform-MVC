using CleanArch.Application.DTO_s.CourseDto_s;
using CleanArch.Application.ResponseDTO_s.CourseRespondDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CleanArch.Application.Interfaces
{
	public interface ICourseServices
	{
		Task<CreateCourseRespond> CreateCourse(CreateCourseDto createCourseDto);
		Task<bool> DeleteCourseAsync(int Id);
		Task<bool> ChangeAccess(int Id);	

		IEnumerable<SelectListItem> GetAvailableLevels();
	}
}
