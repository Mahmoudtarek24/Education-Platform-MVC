using CleanArch.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.ResponseDTO_s.CourseRespondDto
{
	public class CourseRespond
	{
		public int CourseId { get; set; }
		public string CourseName { get; set; }
		public decimal Price { get; set; }
		public bool IsFree { get; set; }
		public string CourseImage { get; set; }
		public string courseStatus { get; set; }
	    public DateTime CreateOn { get; set; }	
		public bool IsDeleted { get; set; }	
			 
	}
}
