using CleanArch.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Domain.Entity
{
	public class Course : BaseModel
	{
		public int CourseId { get; set; }	
		public string CourseName { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }	
		public decimal Discount { get; set; }	
		public double? Rating { get; set; }	
		public bool  IsFree { get; set; }	
		public bool IsSequentialWatch {  get; set; }	
		public string CourseImage { get; set; }	
		public string CourseLevel { get; set; }	
		public CourseStatus courseStatus { get; set; }
		public TimeSpan Duration { get; set; }	
	}
}
