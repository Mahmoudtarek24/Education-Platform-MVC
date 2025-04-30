using CleanArch.Application.DTO_s.SectionDto_s;
using CleanArch.Application.Interfaces;
using EducationPlatform.ViewModel.SectionViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EducationPlatform.Controllers
{
	public class SectionController : Controller
	{
		private readonly ISectionServices sectionServices;
		private readonly ICourseServices courseServices;	
		public SectionController(ISectionServices sectionServices)
		{
			this.sectionServices = sectionServices;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> Create(int CourseId ,string CourseName) 
		{ 
		 var result= await courseServices.IsDeletedCourse(CourseId);

			if (!result)
				return NotFound();

			SectionViewModel  viewModel = new SectionViewModel() { 
			CourseId = CourseId,
			CourseName = CourseName	
			};	
			return View(viewModel);
		}	
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(SectionViewModel model)
		{
			if(!ModelState.IsValid)	
				return View(model);


			SectionDto dto = new SectionDto()
			{
				CourseId = model.CourseId,	
				Name = model.Name,
				Order = model.Order,	
			};

			var result=await sectionServices.CreateSectionAsync(dto);

			if (!result)	
				return BadRequest();

			return Ok();
		}

		[HttpGet]
		public async Task<IActionResult> Update(int CourseId,int SectionId)
		{
			var Courseresult = await courseServices.IsDeletedCourse(CourseId);

			if (!Courseresult)
				return NotFound();

			var Section = await sectionServices.IsDeletedSection(CourseId);

			if (Section is null)
				return NotFound();


			SectionViewModel viewModel = new SectionViewModel()
			{
				CourseId = Section.CourseId,
				Name= Section.Name,
				Order= Section.Order,
				SectionId = SectionId
			};
			return View(viewModel);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Update(SectionViewModel model)
		{
			if (!ModelState.IsValid)	
				return View();


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


			return Ok();
		}


	}
}
