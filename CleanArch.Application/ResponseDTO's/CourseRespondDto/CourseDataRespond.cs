using CleanArch.Application.ResponseDTO_s.SectionRespondDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.ResponseDTO_s.CourseRespondDto
{
	public class CourseDataRespond
	{
		public int CourseId { get; set; }
		public string CourseName { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public decimal Discount { get; set; }
		public double? Rating { get; set; }
		public bool IsFree { get; set; }
		public bool IsSequentialWatch { get; set; }
		public string CourseImage { get; set; }
		public string CourseLevel { get; set; }
		public string Status { get; set; }
		public TimeSpan Duration { get; set; }
        
		public DateTime CreateOn { get; set; }	
		public DateTime? LastUpOn { get; set; }	
		public bool IsDeleted {  get; set; }		

		public List<SectionDataRespond> SectionDataResponds { get; set; }	 = new List<SectionDataRespond>();	

	}
}
