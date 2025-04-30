using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Domain.Entity
{
	public class Section
	{
		public int SectionId { get; set; }	

		public string Name { get; set; }	

		public TimeSpan SectionDuration { get; set; }	
		
		public byte Order { get; set; }
		public bool IsDeleted { get; set; }

		public int CourseId { get; set; }	

		public Course Course { get;set; }
	}
}
