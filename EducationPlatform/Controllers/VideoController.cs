using CleanArch.Application.DTO_s.VideoDto_s;
using CleanArch.Application.Interfaces;
using CleanArch.Application.Services;
using EducationPlatform.ViewModel.VideoViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EducationPlatform.Controllers
{
	public class VideoController : Controller
	{
		private readonly IVideoServices videoServices;
		private readonly ISectionServices sectionServices;	
		public VideoController(IVideoServices videoServices,ISectionServices sectionServices)
		{
			this.videoServices = videoServices;	
			this.sectionServices = sectionServices;	
		}
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public IActionResult Create(int Id ,string courseName ,string sectionName)
		{

			var Section = sectionServices.IsDeletedSection(Id);

			if (Section is null)
				return NotFound();

			CreateVideoViewModel viewModel = new CreateVideoViewModel()
			{
				SectionId = Id,
				SectionName = sectionName,
				CourseNmae = courseName ,
			};


			return View(viewModel);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		[RequestSizeLimit(1024 * 1024 * 600)]
		public async Task<IActionResult> Create(CreateVideoViewModel model) 
		{
			if(!ModelState.IsValid)
			{
				return View(model);		
			}

			VideoDto videoDto = new VideoDto()
			{
				IsFree = model.IsFree,
				SectionId = model.SectionId,
				Order=model.order,	
				Video=model.Video,
				VideoName=model.VideoName,	
			};	

			var result=await videoServices.AddVideoAsync(videoDto);
			if (result is null)
				return BadRequest();
			
			if(!result.IsSuccessed)
			{
				ModelState.AddModelError(string.Empty, result.Message);
				return View(model);	
			}


			ViewBag.Message = $@"Video '{model.VideoName}' added Successfully.";

			CreateVideoViewModel viewModel = new CreateVideoViewModel()
			{
				SectionId = model.SectionId,	
				SectionName = model.SectionName,	
				CourseNmae=model.CourseNmae,
			};	

			return View(viewModel);	
		}

		[HttpPost]
		[ValidateAntiForgeryToken]	
		public async Task<IActionResult> Delete(int Id)
		{
			var deleteResult=await videoServices.DeleteVideoAsync(Id);		

			if(!deleteResult.IsSuccessed) 
				return BadRequest();
			
			return Ok();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]	
		public async Task<IActionResult> ChangePreviewVideo(int Id)
		{
			var result=await videoServices.ChangePreviewVideoAsync(Id);

			if (!result.IsSuccessed)
				return BadRequest();


			return Ok(result.LastUpdatOn.ToString());
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int Id,int SectionId,string courseName ,string sectionName)
		{
			var videoDto=await videoServices.GetVideoByIdAsync(Id);

			EditVideoViewModel viewModel = new EditVideoViewModel()
			{
				VideoFileUrl = videoDto.VideoFileUrl,
				VideoOrder = videoDto.VideoOrder,
				VideoName = videoDto.VideoName,
				VideoId = videoDto.VideoId,
				SectionId= SectionId ,
				SectionName= sectionName,
				CourseName = courseName
			};

			return View(viewModel);	
		}
		[HttpPost]
		[ValidateAntiForgeryToken]	
		public async Task<IActionResult> Edit(EditVideoViewModel model)
		{
			if (!ModelState.IsValid)
				return View(model);

			EditVideoDto editVideoDto=new EditVideoDto()
			{
				VideoId = model.VideoId,
				VideoName=model.VideoName,
				VideoOrder=model.VideoOrder,
				VideoUpdateFile	= model.VideoUpdateFile,	
			};


			//var UpdateResult=await 

			// create esrvices,interface for Update vide and pass  EditVideoDto to it , view work okay

			return Ok();
		}

	}
}
