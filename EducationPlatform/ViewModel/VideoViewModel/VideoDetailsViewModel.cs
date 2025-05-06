namespace EducationPlatform.ViewModel.VideoViewModel
{
	public class VideoDetailsViewModel
	{
		public int VideoId { get; set; }
		public string VideoName { get; set; }
		public TimeSpan VideoDuration { get; set; }
		public byte VideoOrder { get; set; }

		public string VideoFileUrl { get; set; }
		public bool IsFree { get; set; }
		public DateTime CreateOn { get; set; }
		public DateTime? LastUpdateOn { get; set; }
	}
}
