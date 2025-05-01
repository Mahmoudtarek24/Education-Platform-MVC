using CleanArch.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Domain.Interfaces
{
	public interface ICourseRepository :IGenaricRepository<Course>
	{
		Task<List<Course?>> GetAllCourses(int PageNumber, int PageSize);
		Course? GetCourseStatus(int Id);
		Task<Course?> IsDeletedCourse(int Id);
		Task<Course?> AllCourseData(int Id);
	}
}
