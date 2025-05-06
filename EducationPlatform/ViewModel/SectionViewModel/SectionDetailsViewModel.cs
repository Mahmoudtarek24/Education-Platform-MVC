using EducationPlatform.ViewModel.VideoViewModel;

namespace EducationPlatform.ViewModel.SectionViewModel
{
	public class SectionDetailsViewModel
	{
		public int SectionId { get; set; }	
		public string SectionName { get; set; }	
		public string CourseNmae { get; set; }		
		public TimeSpan SectionDuration { get; set; }	
			 
     	public List<VideoDetailsViewModel> videoDetailsViewModels { get; set; }	=new List<VideoDetailsViewModel>();

	}
}
