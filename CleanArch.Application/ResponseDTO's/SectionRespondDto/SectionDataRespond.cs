using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.ResponseDTO_s.SectionRespondDto
{
	public class SectionDataRespond
	{
		public int SectionId { get; set; }	
		public string SectionName { get; set; } 
		public int order {  get; set; }	
		public bool IsDelted { get; set; }	
	}
}
