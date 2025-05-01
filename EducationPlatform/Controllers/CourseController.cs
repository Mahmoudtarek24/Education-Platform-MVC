using CleanArch.Application.DTO_s.CourseDto_s;
using CleanArch.Application.Interfaces;
using CleanArch.Domain.Entity;
using EducationPlatform.Filters;
using EducationPlatform.ViewModel;
using EducationPlatform.ViewModel.CourseViewModel;
using EducationPlatform.ViewModel.SectionViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Threading.Tasks;
namespace EducationPlatform.Controllers
{
	public class CourseController : Controller
	{
		private readonly ICourseServices courseServices;
		public CourseController(ICourseServices courseServices)
		{
			this.courseServices = courseServices;	
		}

		public async Task<IActionResult> Index(CoursesViewModel model)
		{
		

			var result = await courseServices.GetCoursesAsync(model.Pagination.PageNumber, model.Pagination.PageSize);

			var viewModel = new CoursesViewModel
			{
				Courses = result,
				Pagination = model.Pagination,
			};


			return View(viewModel);
		}
		
		[HttpGet]
		public IActionResult Create()
		{

			CreateCourseViewModel viewModel = new CreateCourseViewModel()
			{
				AvailableLevels =courseServices.GetAvailableLevels(),
			};


			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[ValidateImageAttribute("CourseImage", 2,new [] { ".jpg", ".jpeg", ".png" })]
		public async Task<IActionResult> Create(CreateCourseViewModel model) 
		{
			if (!ModelState.IsValid)
			{
				model.AvailableLevels = courseServices.GetAvailableLevels();

				return View(model);
			}

			CreateCourseDto courseDto = new CreateCourseDto()
			{
				CourseName = model.CourseName,
				Description = model.Description,
				CourseIamge = model.CourseImage,
				Price = model.Price,
				Discount = model.Discount,
				IsSequentialWatch = model.IsSequentialWatch,
				CourseLevel = string.Join(",", model.SelectedLevel),
			};

			var result=await courseServices.CreateCourse(courseDto);

			if(!result.IsSuccessed)
				return BadRequest();


			return RedirectToAction(nameof(Index));
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(int id) 
		{
			var result=await courseServices.DeleteCourseAsync(id);	

			if(!result)
				return NotFound();	
	
			return Ok();	
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ChangeCourseAccess(int id)
		{
			var result = await courseServices.ChangeAccess(id);

			if (!result)
				return NotFound();

			return Ok();
		}

		[HttpGet]
		public IActionResult UpdateCourseStatus(int Id)
		{
			var result =  courseServices.GetCourseStatusAsync(Id);

			if (!result.IsSuccessed)
			{
				return NotFound();	
			}
			var courseStates = courseServices.GetCourseStatus();

			var viewmode = new UpdateCourseStatusViewModel()
			{
				Status = result.Status,
				CourseStates = courseStates,
				CourseId = Id
			};
		
			return PartialView("UpdateCourseStatus",viewmode);
		}


		[HttpPost]
		//[AjaxOnly]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> UpdateCourseStatus(UpdateCourseStatusViewModel model)
		{
			if(!ModelState.IsValid)	
				return BadRequest();


			var result = await courseServices.UpdateCourseStatusAsync(model.CourseId, model.Status);

			if(!result)
				return BadRequest();

			return RedirectToAction(nameof(Index));
		}

		public async Task<IActionResult> Details(int Id)
		{
			var result=await courseServices.GetCourseDetailsAsync(Id);

			if (result is null)
				return BadRequest();

			CourseDetailsViewModel viewModel = new CourseDetailsViewModel()
			{
				CourseId=result.CourseId,	
				CourseImage = result.CourseImage,	
				IsSequentialWatch=result.IsSequentialWatch,	
				CourseLevel = result.CourseLevel,
				CourseName = result.CourseName,
				CreateOn=result.CreateOn,	
				Description=result.Description,
				Discount=result.Discount,	
				Duration=result.Duration,
				IsDeleted=result.IsDeleted,
				IsFree=result.IsFree,	
				LastUpOn=result.LastUpOn,	
				Price=result.Price,
				Rating=result.Rating,
				Status=result.Status,	
			};	

			foreach(var sectionDto in result.SectionDataResponds)
			{
				viewModel.sectionDataViewModels.Add(new SectionDataViewModel
				{
					order = sectionDto.order,
					SectionId = sectionDto.SectionId,
					SectionName = sectionDto.SectionName,
					IsDeleted = sectionDto.IsDelted
				});

			}

			return View(viewModel);
		}



	}
}
