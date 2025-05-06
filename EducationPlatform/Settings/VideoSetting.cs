namespace EducationPlatform.Settings
{
	public class VideoSetting
	{
		public int MaxFileSize { get; set; }	
		public string[] AllowedExtensions { get; set; }	
		public string[] AllowedMimeTypes { get; set; }	
		public string[] _targetPaths { get; set; }	
	}
}
