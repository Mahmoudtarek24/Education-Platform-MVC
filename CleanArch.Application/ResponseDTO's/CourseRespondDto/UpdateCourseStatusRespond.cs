using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.ResponseDTO_s.CourseRespondDto
{
	public class UpdateCourseStatusRespond
	{
		public int CourseId { get; set; }		
		public string Status { get; set; }	

		public bool IsSuccessed { get; set; }	 
	}
}
