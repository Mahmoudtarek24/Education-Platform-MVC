using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.DTO_s.VideoDto_s
{
	public class VideoDto
	{
		public int SectionId { get; set; }
		public string VideoName { get; set; }
		public byte Order { get; set; }
		public IFormFile Video { get; set; }
		public bool IsFree { get; set; }	

	}
}
