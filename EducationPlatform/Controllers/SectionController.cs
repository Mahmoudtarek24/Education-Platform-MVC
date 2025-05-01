using CleanArch.Application.DTO_s.SectionDto_s;
using CleanArch.Application.Interfaces;
using CleanArch.Domain.Entity;
using EducationPlatform.Filters;
using EducationPlatform.ViewModel.SectionViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EducationPlatform.Controllers
{
	public class SectionController : Controller
	{
		private readonly ISectionServices sectionServices;
		private readonly ICourseServices courseServices;	
		public SectionController(ISectionServices sectionServices, ICourseServices courseServices)
		{
			this.sectionServices = sectionServices;
			this.courseServices = courseServices;	
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> Create(int CourseId) 
		{ 
		 var result= await courseServices.IsDeletedCourse(CourseId);

			if (!result)
				return NotFound();

			SectionViewModel  viewModel = new SectionViewModel() { 
			CourseId = CourseId,
			};	
			return PartialView("SectionForm", viewModel);
		}	
		[HttpPost]
		//[AjaxOnly]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(SectionViewModel model)
		{
			if(!ModelState.IsValid)	
				return BadRequest();	


			SectionDto dto = new SectionDto()
			{
				CourseId = model.CourseId,	
				Name = model.Name,
				Order = model.Order,	
			};

			var result=await sectionServices.CreateSectionAsync(dto);

			if (!result)	
				return BadRequest();

			return RedirectToAction("Details", "Course", new { Id =model.CourseId });
		}

		[HttpGet]
		public async Task<IActionResult> Update(int CourseId,int SectionId)
		{
			var Courseresult = await courseServices.IsDeletedCourse(CourseId);

			if (!Courseresult)
				return NotFound();

			var Section = sectionServices.IsDeletedSection(SectionId);

			if (Section is null)
				return NotFound();


			SectionViewModel viewModel = new SectionViewModel()
			{
				CourseId = Section.CourseId,
				Name= Section.Name,
				Order= Section.Order,
				SectionId = SectionId
			};
			return PartialView("SectionForm", viewModel);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Update(SectionViewModel model)
		{
			if (!ModelState.IsValid)	
				return BadRequest();


			SectionDto dto = new SectionDto()
			{
				CourseId = model.CourseId,
				Name = model.Name,
				Order = model.Order,
				SectionId = model.SectionId	
			};

			var result=await sectionServices.UpdateSectionAsync(dto);		

			if (!result) 
				return BadRequest();

			return RedirectToAction("Details", "Course", new { Id = model.CourseId });
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(int SectionId)
		{
			var Section =  sectionServices.IsDeletedSection(SectionId);

			if (Section is null)
				return NotFound();


			var result=await sectionServices.DeleteSectionAsync(SectionId);

			if(!result)	
				return BadRequest();	

			return Ok();	
		}	
	}
}
