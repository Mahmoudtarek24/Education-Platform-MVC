using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.DTO_s.VideoDto_s
{
	public class EditVideoDto
	{
		public int VideoId { get; set; }
		public string VideoName { get; set; }	
		public IFormFile? VideoUpdateFile {  get; set; }
		public byte VideoOrder { get; set; }
	}
}
