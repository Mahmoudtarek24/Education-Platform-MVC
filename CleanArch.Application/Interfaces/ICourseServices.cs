using CleanArch.Application.DTO_s.CourseDto_s;
using CleanArch.Application.ResponseDTO_s.CourseRespondDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CleanArch.Application.Interfaces
{
	public interface ICourseServices
	{
		Task<IEnumerable<CourseRespond>> GetCoursesAsync(int PageNumber,int PageSize);
		Task<CreateCourseRespond> CreateCourse(CreateCourseDto createCourseDto);
		Task<bool> DeleteCourseAsync(int Id);
		Task<bool> ChangeAccess(int Id);	
		Task<bool> UpdateCourseStatusAsync(int Id,string status);
		Task<bool> IsDeletedCourse(int Id);
		UpdateCourseStatusRespond GetCourseStatusAsync(int Id);
		
		IEnumerable<SelectListItem> GetAvailableLevels();
		IEnumerable<SelectListItem> GetCourseStatusAsync();	

	}
}
