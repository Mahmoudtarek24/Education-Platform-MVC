using CleanArch.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.DTO_s.CourseDto_s
{
	public class CreateCourseDto
	{
		public string CourseName { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public decimal Discount { get; set; }
		public bool IsFree { get; set; }
		public bool IsSequentialWatch { get; set; }
		public string CourseLevel { get; set; }

		public IFormFile CourseIamge { get; set; }
	}
}
