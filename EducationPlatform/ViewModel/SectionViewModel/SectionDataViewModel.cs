using System.ComponentModel.DataAnnotations;

namespace EducationPlatform.ViewModel.SectionViewModel
{
	public class SectionDataViewModel
	{
		public int SectionId { get; set; }
		[Display(Name = "Section Name")]
		public string SectionName { get; set; }
		[Display(Name = "Section Order")]
		public int order { get; set; }
		public bool IsDeleted { get; set; }	
	}
}
