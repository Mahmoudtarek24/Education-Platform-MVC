using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Domain.Entity
{
	public class Video
	{
		public int VideoId { get; set; }	
		public string VideoName { get; set; }	
		public TimeSpan VideoDuration { get; set; }	
		public byte VideoOrder { get; set; }

		public string VideoFileUrl { get; set; }

		public bool IsFree { get; set; }	
		public DateTime CreateOn { get; set; }	
		public DateTime? LastUpdateOn { get; set; }	

		public int SectionId { get; set; }
		public Section Section { get; set; }
	}
}
