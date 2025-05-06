using EducationPlatform.Constants;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EducationPlatform.ViewModel.VideoViewModel
{
	public class CreateVideoViewModel
	{
		public int SectionId { get; set; }
		[Required(ErrorMessage = ValidationMessages.RequiredFiled)]
		public IFormFile Video {  get; set; }

		[Display(Name ="Video Name"),Required(ErrorMessage = ValidationMessages.RequiredFiled)]
		[Remote(action: "IsVideoNameTaken", controller: "RemoteValidation", AdditionalFields = "SectionId")]
		public string VideoName { get; set; }

		[Range(1,100,ErrorMessage = ValidationMessages.Rang)]
		[Display(Name ="Video order To Student")]
		[Remote(action: "IsOrderVideoNumberTaken" ,controller: "RemoteValidation", AdditionalFields = "SectionId")]
		public byte order { get; set; }
		[Display(Name = "Is public Video")]
		public bool IsFree { get; set; }	

		public string? SectionName { get; set; }	
		public string? CourseNmae { get; set; }	
	}
}
