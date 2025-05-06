using CleanArch.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EducationPlatform.Controllers
{
	public class RemoteValidationController : Controller
	{
		private readonly IVideoServices videoServices;
		public RemoteValidationController(IVideoServices videoServices)
		{
			this.videoServices = videoServices;	
		}

		public IActionResult Index()
		{
			return View();
		}

		public async Task<IActionResult> IsOrderVideoNumberTaken(byte order, int sectionId)
		{

			var TakenOrderNumber = await videoServices.OrderVideoNumberAsync(sectionId);

			foreach (var number in TakenOrderNumber) 
			{
				if(number == order)
				{
					return Json($"Number is already in use. Number Taken {string.Join(" , ", TakenOrderNumber)}");
				}
			}	
			return Json(true);	
		}

		public async Task<IActionResult> IsVideoNameTaken(string VideoName, int sectionId)
		{

			var IsNotValidName = await videoServices.ValidVideoNameAsync(sectionId, VideoName);

			if (IsNotValidName)
			{
				return Json($"Name is already in use.");

			}
			return Json(true);
		}
	}
}
