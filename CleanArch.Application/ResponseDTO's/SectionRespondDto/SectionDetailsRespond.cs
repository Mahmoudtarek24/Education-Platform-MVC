using CleanArch.Application.ResponseDTO_s.VideoRespondDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.ResponseDTO_s.SectionRespondDto
{
	public class SectionDetailsRespond
	{
		public TimeSpan SectionDuration { get; set; }
		public List<VideoDetailsRespond> VideoDetailsResponds {  get; set; }	=new List<VideoDetailsRespond>();	
	}
}