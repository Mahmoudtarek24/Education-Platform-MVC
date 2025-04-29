using CleanArch.Domain.Entity;
using CleanArch.Domain.Interfaces;
using CleanArch.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Infrastructure.Repository
{
	public class CourseRepository :GenaricRepository<Course>, ICourseRepository
 	{
		public CourseRepository(EducationPlatformDbContext context):base(context) { }		
		
	}
}
