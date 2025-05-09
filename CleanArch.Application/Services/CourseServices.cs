using CleanArch.Application.DTO_s.CourseDto_s;
using CleanArch.Application.Interfaces;
using CleanArch.Application.ResponseDTO_s.CourseRespondDto;
using CleanArch.Domain.Entity;
using CleanArch.Domain.Interfaces;
using CleanArch.Domain.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using CourseStatus = CleanArch.Application.Enums.CourseStatus;
using CourseLevel = CleanArch.Application.Enums.CourseLevel;
using CleanArch.Application.DTO_s.SectionDto_s;
using CleanArch.Application.ResponseDTO_s.SectionRespondDto;

namespace CleanArch.Application.Services
{
	public class CourseServices : ICourseServices
	{
		private string CourseFolderName = @"/Course/";

		private readonly IUnitOfWork unitOfWork;
		private readonly IImageServices imageServices;
		private readonly IWebHostEnvironment webHostEnvironment;
		public CourseServices(IUnitOfWork unitOfWork, IImageServices imageServices,
							  IWebHostEnvironment webHostEnvironment)
		{
			this.unitOfWork = unitOfWork;	
			this.imageServices = imageServices;
		    this.webHostEnvironment = webHostEnvironment;	
		}

		public async Task<IEnumerable<CourseRespond>> GetCoursesAsync(int PageNumber, int PageSize)
		{

			var Courses = await unitOfWork.courseRepository.GetAllCourses(PageNumber, PageSize);

			List<CourseRespond> courseResponds = new List<CourseRespond>();
			foreach (var course in Courses) 
			{
				CourseRespond respond = new CourseRespond()
				{
					CourseId = course.CourseId,
					CourseName = course.CourseName,
					IsDeleted = course.IsDeleted,
					IsFree = course.IsFree,
					Price = course.Price,
					courseStatus = course.courseStatus.ToString(),
					CourseImage =$"{CourseFolderName}{course.CourseImage}",
				};
				courseResponds.Add(respond);	
			}		

			return courseResponds;
		}

		public async Task<CreateCourseRespond> CreateCourse(CreateCourseDto createCourseDto)
		{
			bool IsUpload = default;
			string ImageName=default;	
			if(createCourseDto == null)	
				return new CreateCourseRespond() { Message= "Course data is missing" };
			try
			{
				unitOfWork.CreateTransaction();
				Course course = new Course();

				var imageName = await imageServices.UploadImage(createCourseDto.CourseIamge, CourseFolderName);
				IsUpload = true;	
				ImageName = imageName;

				course.CourseImage = imageName;	
				course.CreateOn = DateTime.Now;
				course.CourseName = createCourseDto.CourseName;
				course.Description = createCourseDto.Description;
				course.Discount = createCourseDto.Discount;
				course.Price = createCourseDto.Price;
				course.IsFree = false;                           //make cours paid then free if he want
				course.IsSequentialWatch = createCourseDto.IsSequentialWatch;
				//course.courseStatus = CourseStatus.ComingSoon;
				course.CourseLevel = createCourseDto.CourseLevel;
				 
				await unitOfWork.courseRepository.AddAsync(course);
				await unitOfWork.Save();

			    unitOfWork.Commit();
				return new CreateCourseRespond() { Message = "Created Successfully", IsSuccessed = true };
			}
			catch
			{
				if (IsUpload)
				{
					imageServices.DeleteImage($"{CourseFolderName}/{ImageName}");
				}
				unitOfWork.RollBack();
				return new CreateCourseRespond() { Message = "Faild To create course"};
			}
		}



		

		public async Task<bool> DeleteCourseAsync(int Id)
		{
			var course= await unitOfWork.courseRepository.GetByIdEntity(Id);
	          if(course == null)
				return false;

			try
			{
				unitOfWork.CreateTransaction();
				course.IsDeleted=!course.IsDeleted;	
				course.LastUpdateOn = DateTime.Now;

				await unitOfWork.Save();
				unitOfWork.Commit();	
				return true;	
			}
			catch 
			{
				unitOfWork.RollBack();
				return false;
			}

		}

		public async Task<bool> ChangeAccess(int Id)
		{
			var course = await unitOfWork.courseRepository.GetByIdEntity(Id);
			if (course == null)
				return false;

			try
			{
				unitOfWork.CreateTransaction();
				course.IsFree=!course.IsFree;	
				course.LastUpdateOn = DateTime.Now;	

				await unitOfWork.Save();
				unitOfWork.Commit();	

				return true;	
			}
			catch
			{
				unitOfWork.RollBack();
				return false;
			}
		}
		public IEnumerable<SelectListItem> GetCourseStatus()
		{
			return Enum.GetValues(typeof(CourseStatus)).Cast<CourseStatus>()
							 .Select(e => new SelectListItem() { 
							 Text=e.ToString(),
							 Value=e.ToString()	
							 }).ToList();	
		}
		public IEnumerable<SelectListItem> GetAvailableLevels()
		{
			return Enum.GetValues(typeof(CourseLevel)).Cast<CourseLevel>()
					   .Select(e => new SelectListItem()
					   {
						   Value = e.ToString(),
						   Text = e.ToString()
					   }).ToList();
		}

		public  UpdateCourseStatusRespond GetCourseStatusAsync(int Id)
		{
			UpdateCourseStatusRespond respond = new UpdateCourseStatusRespond();	
			var course =  unitOfWork.courseRepository.GetCourseStatus(Id);
			if (course == null)
				return respond;

			respond.Status=course.courseStatus.ToString();
			respond.IsSuccessed=true;
			respond.CourseId=course.CourseId;

			return respond;	
		}

		public async Task<bool> UpdateCourseStatusAsync(int Id, string status)
		{
			var course = await unitOfWork.courseRepository.GetByIdEntity(Id);
			if (course == null)
				return false;

			try
			{
				unitOfWork.CreateTransaction();
				
				course.courseStatus = (Domain.Enums.CourseStatus)Enum.Parse(typeof(Domain.Enums.CourseStatus), status);
				course.LastUpdateOn = DateTime.Now;

				await unitOfWork.Save();
				unitOfWork.Commit();

				return true;
			}
			catch
			{
				unitOfWork.RollBack();
				return false;
			}
		}

		public async Task<bool> IsDeletedCourse(int Id)
		{
			var course = await unitOfWork.courseRepository.IsDeletedCourse(Id);

			if (course == null)
				return false;


			return true;
		}

		public async Task<CourseDataRespond> GetCourseDetailsAsync(int CourseId)
		{
			CourseDataRespond courseRespond = new CourseDataRespond();	

			var course =await unitOfWork.courseRepository.AllCourseData(CourseId);

			if (course == null)
				return courseRespond;

			courseRespond.Duration=course.Duration;
			courseRespond.Status=course.courseStatus.ToString();
			courseRespond.Discount=course.Discount;
			courseRespond.IsSequentialWatch=course.IsSequentialWatch;
			courseRespond.LastUpOn=course.LastUpdateOn;	
			courseRespond.Price=course.Price;
			courseRespond.IsDeleted=course.IsDeleted;	
			courseRespond.IsFree=course.IsFree;	
			courseRespond.Rating=course.Rating;	
			courseRespond.CourseLevel=course.CourseLevel;	
			courseRespond.CourseId=course.CourseId;
			courseRespond.CourseImage = $"{CourseFolderName}{course.CourseImage}";
			courseRespond.CreateOn=course.CreateOn;	
			courseRespond.CourseName=course.CourseName;
			courseRespond.Description=course.Description;

			foreach(var section in course.Sections)
			{
				courseRespond.SectionDataResponds.Add(new SectionDataRespond
				{
					order = section.Order,
					SectionName = section.Name,
					SectionId = section.SectionId,
					IsDelted=section.IsDeleted,
				});
			}

			return courseRespond;
		}
	}
}
