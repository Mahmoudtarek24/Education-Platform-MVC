using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.DTO_s.SectionDto_s
{
	public class SectionDto
	{
		public int? SectionId { get; set; }	
		public string Name { get; set; }
		public byte Order { get; set; }
		public int CourseId { get; set; } 
	}
}
