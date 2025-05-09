using CleanArch.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EducationPlatform.Controllers
{
	public class RemoteValidationController : Controller
	{
		private readonly IVideoServices videoServices;
		private readonly IAccountService accountService;	
		private readonly IUserUtilityService userUtilityService;
		public RemoteValidationController(IVideoServices videoServices,IAccountService accountService
					, IUserUtilityService userUtilityService)
		{
			this.videoServices = videoServices;
			this.accountService = accountService;
			this.userUtilityService = userUtilityService;
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

		public async Task<IActionResult> IsUniqueEmail(string Email ,string? FirstName,string? LastName)
		{
			if (string.IsNullOrEmpty(Email)) {
				return Json("Please enter a valid email address.");
			}

			var emailAttribute = new EmailAddressAttribute();
			if (!emailAttribute.IsValid(Email))
			{
				return Json("Please enter a valid email address.");
			}

			var IsEmailValid=await accountService.IsEmailTakenAsync(Email);

			if (IsEmailValid)
			{
				var EmailSuggestionsResult = await userUtilityService.GenerateUniqueEmailsAsync(Email,FirstName,LastName);
				var suggestions = string.Join(", ", EmailSuggestionsResult);
				return Json($"This email address is already in use.You Can used one of them {suggestions} ");
			}
			return Json(true);
		}
		public async Task<IActionResult> IsUniqueName(string UserName)
		{
			if (string.IsNullOrEmpty(UserName))
			{
				return Json("Please enter a valid email address.");
			}
			var IsUserNameValid = await accountService.IsUserNameTakenAsync(UserName);

			if (IsUserNameValid)
			{
		
				return Json($"This User Name is already in use.");
			}
			return Json(true);

		}
	}
}
