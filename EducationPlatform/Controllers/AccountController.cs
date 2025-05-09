using CleanArch.Application.DTO_s.AccountDto_s;
using CleanArch.Application.Interfaces;
using EducationPlatform.ViewModel.AccountViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Threading.Tasks;

namespace EducationPlatform.Controllers
{
	public class AccountController : Controller
	{
		private readonly IAccountService accountService;
		public AccountController(IAccountService accountService)
		{
			this.accountService = accountService;
		}
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public IActionResult Register() => View();

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (!ModelState.IsValid)
				return View(model);

			RegisterUserDto userDto = new RegisterUserDto()
			{
				DateOfBirth = model.DateOfBirth,
				Email = model.Email,
				FullName = $"{model.FirstName} {model.LastName}",
				Password = model.Password,
				Username = model.UserName,
			};


			var RegisterResult = await accountService.RegisterUserAsync(userDto);

			if (!RegisterResult.IsSuccessed && !string.IsNullOrEmpty(RegisterResult.Message))
			{
				ModelState.AddModelError(string.Empty, RegisterResult.Message);
				return View(model);
			}
			var code=await accountService.SendEmailConfirm(model.Email,userDto.FullName);

			var expireDate= DateTime.Now.AddMinutes(1);	
			HttpContext.Session.SetString("Email", model.Email);
			HttpContext.Session.SetString("ExpireDate", expireDate.ToString("o"));
			HttpContext.Session.SetInt32("Code",code);
			HttpContext.Session.SetInt32("AttemptNumber", 3);

			return RedirectToAction("ConfirmEmail"); 
		}
		[HttpGet]
		public IActionResult ConfirmEmail() => View();

		[HttpGet]
		public IActionResult CheckVerificationCode() => View();

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CheckVerificationCode(int code)
		{
			var Email = HttpContext.Session.GetString("Email");
			var ExpireDate = DateTime.ParseExact(HttpContext.Session.GetString("ExpireDate"),"o",CultureInfo.InvariantCulture);
			var Code = HttpContext.Session.GetInt32("Code");
			int? attemptNumber = HttpContext.Session.GetInt32("AttemptNumber");

			if (string.IsNullOrEmpty(Email))
				return RedirectToAction(nameof(Register));

			if (DateTime.Now < ExpireDate&&attemptNumber>0)
			{
				if (code == Code)
				{
					var result = await accountService.ConfirmUpdateEmailAsync(Email);
					if (!result)
						return BadRequest();

					HttpContext.Session.Remove("Email");
					HttpContext.Session.Remove("ExpireDate");
					HttpContext.Session.Remove("Code");
					HttpContext.Session.Remove("AttemptNumber");

					return RedirectToAction("Register");
				}
				else
				{
					HttpContext.Session.SetInt32("AttemptNumber", attemptNumber.Value - 1);
					ModelState.AddModelError("Code", "Incorrect code. Please try again.");
					return View();
				}
			}
			else
			{
				attemptNumber = 3;
				ModelState.AddModelError("Code", "The code has expired or you have used all your attempts. Click 'Resend Code' to get a new one.");
				return View();
			}
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ResetCode()
		{
			var Email = HttpContext.Session.GetString("Email");
			var code = await accountService.SendEmailConfirm(Email, "ali");
			var expireDate = DateTime.Now.AddMinutes(1);
			HttpContext.Session.SetString("Email", Email);
			HttpContext.Session.SetString("ExpireDate", expireDate.ToString("o"));
			HttpContext.Session.SetInt32("Code", code);
			HttpContext.Session.SetInt32("AttemptNumber", 3);
			return Ok();
		}

		[HttpGet]
		public IActionResult Login() => View();


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (!ModelState.IsValid)
				return View(model);

			LoginDto dto = new LoginDto()
			{
				Email = model.Email,
				password = model.Password,
				httpContext = HttpContext,
				RememberMe = model.RememberMe,
			};

			var LoginResult = await accountService.LoginUserAsync(dto);

			if (!LoginResult.IsSuccessed && !string.IsNullOrEmpty(LoginResult.Message))
			{
				ModelState.AddModelError(string.Empty, LoginResult.Message);
				return View(model);
			}

			return RedirectToAction("Create", "Course");
		}









		[HttpGet]
		public IActionResult ForgetPassword() => View();

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ForgetPassword(ForgotPasswordViewModel model)
		{
			if (!ModelState.IsValid)
				return View(model);


			var ForgetPasswordResult = await accountService.ForgetPasswordAsync(model.Email);
			if (!ForgetPasswordResult.IsSuccessed && !string.IsNullOrEmpty(ForgetPasswordResult.Message))
			{
				ModelState.AddModelError(string.Empty, ForgetPasswordResult.Message);
				return View(model);
			}
			return Ok(); 
		}

		[HttpGet]
		public IActionResult ResetPassword([FromQuery] string VerificationCode, string Email)
		{

			return View();
		}
		/////////////////////////////////////////////////////////////////////////////////////////////

		[HttpGet]
		public async Task<IActionResult> AccountSetting()
		{
			var UserId = User.FindFirst("UserId")!.Value;

			int userId=int.Parse(UserId.ToString());	

			var UserRespond = await accountService.GetUserById(userId);

			UpdateAccountViewModel viewModel = new UpdateAccountViewModel()
			{
				FirstName = UserRespond.FirstName,
				LastName = UserRespond.LastName,
				ImagePath = UserRespond.ImagePath,
			};

			return View(viewModel);  //THIS method completed work on post ,and her view
		}



	}
}
