using EducationPlatform.Constants;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EducationPlatform.ViewModel.VideoViewModel
{
	public class EditVideoViewModel
	{
		public int VideoId { get; set; }
		[Display(Name = "Video Name"), Required(ErrorMessage = ValidationMessages.RequiredFiled)]
		[Remote(action: "IsVideoNameTaken", controller: "RemoteValidation", AdditionalFields = "SectionId")]
		public string VideoName { get; set; }

		[Range(1, 100, ErrorMessage = ValidationMessages.Rang)]
		[Display(Name = "Video order To Student")]
		[Remote(action: "IsOrderVideoNumberTaken", controller: "RemoteValidation", AdditionalFields = "SectionId")]
		public byte VideoOrder { get; set; }

		public string VideoFileUrl { get; set; }
		public IFormFile? VideoUpdateFile { get; set; } 

		public int SectionId { get; set; }		
		public string SectionName { get; set; }	
		public string CourseName { get; set; }	

	}
}
