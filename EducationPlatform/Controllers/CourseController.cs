using CleanArch.Application.Interfaces;
using EducationPlatform.Filters;
using EducationPlatform.ViewModel.CourseViewModel;
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

		public IActionResult Index()
		{
			return View();
		}
		
		[HttpGet]
		public IActionResult Create()
		{

			CreateCourseViewModel viewModel = new CreateCourseViewModel()
			{
				AvailableLevels = (IEnumerable<SelectListItem>)courseServices.GetAvailableLevels(),
			};	


			return View(viewModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[ValidateImageAttribute("CourseImage", 2,new string[] { ".jpg", ".jpeg", ".png" })]
		public IActionResult Create(CreateCourseViewModel model) 
		{
			if(!ModelState.IsValid) 
				return View(model);	

			//convert viewModel to Dto


			return Ok();
		}


		[HttpPost]
		public async Task<IActionResult> Delete(int id) 
		{
			var result=await courseServices.DeleteCourseAsync(id);	

			if(!result)
				return NotFound();	
	
			return Ok();	
		}

		[HttpPost]
		public async Task<IActionResult> ChangeCourseAccess(int id)
		{
			var result = await courseServices.ChangeAccess(id);

			if (!result)
				return NotFound();

			return Ok();
		}
	}
}
