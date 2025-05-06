using CleanArch.Application.DTO_s.SectionDto_s;
using CleanArch.Application.Interfaces;
using CleanArch.Domain.Entity;
using EducationPlatform.Filters;
using EducationPlatform.ViewModel.SectionViewModel;
using EducationPlatform.ViewModel.VideoViewModel;
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

		public async Task<IActionResult> Index(int Id ,string courseName,string sctionName)
		{

			var sectionDetails = await sectionServices.SectionVideosAsync(Id);


			SectionDetailsViewModel viewModel =new SectionDetailsViewModel()
			{
				SectionId = Id ,
				CourseNmae = courseName,
				SectionName = sctionName,
				SectionDuration = sectionDetails.SectionDuration,	
			};

			foreach(var videoDetails in sectionDetails.VideoDetailsResponds)
			{
				VideoDetailsViewModel videoDetailsViewModel = new VideoDetailsViewModel()
				{
					VideoDuration = videoDetails.VideoDuration,
					CreateOn = videoDetails.CreateOn,	
					IsFree= videoDetails.IsFree,	
					LastUpdateOn= videoDetails.LastUpdateOn,	
					VideoId = videoDetails.VideoId,
					VideoName = videoDetails.VideoName,
					VideoOrder = videoDetails.VideoOrder,		
					VideoFileUrl = videoDetails.VideoFileUrl,		
				};
				viewModel.videoDetailsViewModels.Add(videoDetailsViewModel);	
			}	
			return View(viewModel);
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
		[HttpGet]
		public async Task<IActionResult> GetCourseSections(int Id,string courseName)
		{
			var result =await courseServices.IsDeletedCourse(Id);

			if (!result)
				return NotFound();

			var Sections = sectionServices.GetSectionByCourseId(Id);

			if (Sections is null)
				return NotFound();

			List<SectionDataViewModel> viewModels = new List<SectionDataViewModel>();

			foreach (var section in Sections)
			{
				SectionDataViewModel sectionData = new SectionDataViewModel()
				{
					SectionName = section.Name,
					SectionId = section.SectionId,
					order = section.Order,
				};
				viewModels.Add(sectionData);
			}

			ViewBag.courseName=courseName;	
			return PartialView("SectonsCourse", viewModels);
		}

	}
}


