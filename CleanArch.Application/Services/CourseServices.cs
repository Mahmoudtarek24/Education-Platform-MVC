using CleanArch.Application.DTO_s.CourseDto_s;
using CleanArch.Application.Interfaces;
using CleanArch.Application.ResponseDTO_s.CourseRespondDto;
using CleanArch.Domain.Entity;
using CleanArch.Domain.Interfaces;
using CleanArch.Domain.Enums;
using System.Web.Mvc;

namespace CleanArch.Application.Services
{
	public class CourseServices : ICourseServices
	{
		private string CourseFolderName = @"/Course/";

		private readonly IUnitOfWork unitOfWork;
		private readonly IImageServices imageServices;
		public CourseServices(IUnitOfWork unitOfWork,ImageServices imageServices)
		{
			this.unitOfWork = unitOfWork;	
			this.imageServices = imageServices;
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
				course.courseStatus = CourseStatus.InProgress;
				course.CourseLevel = createCourseDto.CourseLevel;

				await unitOfWork.courseRepository.AddAsync(course);
				await unitOfWork.Save();

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



		public IEnumerable<SelectListItem> GetAvailableLevels()
		{
			return Enum.GetValues(typeof(CourseLevel)).Cast<CourseLevel>()
					   .Select(e => new SelectListItem()
					   {
						   Value = e.ToString(),
						   Text = e.ToString()
					   });
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
				course.UpdateOn=DateTime.Now;

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
				course.UpdateOn=DateTime.Now;	

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
	}
}
