namespace EducationPlatform.ViewModel
{
	public class PaginationFilter
	{
		public int maxPageElement = 50;

		private int pageSize = 6;
		public int PageNumber { get; set; } = 1;


		public int PageSize
		{
			get
			{
				return pageSize;

			}
			set
			{
				if (value < 1)
				{
					pageSize = pageSize;
				}
				else
				{
					pageSize = value > maxPageElement ? pageSize : value;
				}
			}
		}
	}
}


