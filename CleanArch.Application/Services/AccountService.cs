using CleanArch.Application.DTO_s.AccountDto_s;
using CleanArch.Application.Interfaces;
using CleanArch.Application.ResponseDTO_s;
using CleanArch.Application.ResponseDTO_s.UserRespondDto;
using CleanArch.Domain.Entity;
using CleanArch.Domain.Enums;
using CleanArch.Domain.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.Services
{
	public class AccountService : IAccountService
	{
		private string AccountFolderName = @"/Account/";
		private readonly IEmailSender emailSender;
		private readonly IUnitOfWork unitOfWork;
		private readonly IWebHostEnvironment webHostEnvironment;
		private readonly IHttpContextAccessor httpContextAccessor;
		private readonly IImageServices imageServices;	

		public AccountService(IUnitOfWork unitOfWork, IEmailSender emailSender, IWebHostEnvironment webHostEnvironment
							 , IHttpContextAccessor httpContextAccessor, IImageServices imageServices	)
		{
			this.unitOfWork = unitOfWork;
			this.emailSender = emailSender;
			this.webHostEnvironment = webHostEnvironment;
			this.httpContextAccessor = httpContextAccessor;
			this.imageServices = imageServices;
		}
		public async Task<ConfirmationResponseDTO> RegisterUserAsync(RegisterUserDto model)
		{
			ConfirmationResponseDTO response = new ConfirmationResponseDTO();

			var IsValidEmail = await unitOfWork.UserManager.FindByEmailAsync(model.Email);
			if (IsValidEmail != null)
			{
				response.Message = "Email is already in use.";
				return response;
			}
			var IsValidUseName = await unitOfWork.UserManager.FindByNameAsync(model.Username);
			if (IsValidEmail != null)
			{
				response.Message = "User Name is already in use.";
				return response;
			}
			var HashPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);

			var User = new ApplicationUser()
			{
				CreateOn = DateTime.Now,
				DateOfBirth = model.DateOfBirth,
				Email = model.Email,
				FullName = model.FullName,
				UserName = model.Username,
				Role = (UserRole)Enum.Parse(typeof(UserRole), "Student"),
				PasswordHash = HashPassword,
			};

			try
			{
				unitOfWork.CreateTransaction();
				await unitOfWork.UserManager.AddAsync(User);
				await unitOfWork.Save();
				unitOfWork.Commit();

				response.IsSuccessed = true;
				return response;
			}
			catch
			{
				unitOfWork.RollBack();
				return response;
			}
		}

		public async Task<bool> IsEmailTakenAsync(string email)
		{
			var IsValidEmail = await unitOfWork.UserManager.FindByEmailAsync(email);

			if (IsValidEmail is not null)
				return true;

			return false;
		}
		public async Task<bool> IsUserNameTakenAsync(string UserName)
		{
			var IsValidEmail = await unitOfWork.UserManager.FindByNameAsync(UserName);

			if (IsValidEmail is not null)
				return true;

			return false;
		}

		public async Task<int> SendEmailConfirm(string Email,string FullName)
		{
			try
			{
				var code = new Random().Next(123000, 999123);
				await EmailVerificationCode(FullName, code.ToString(), Email);
				return code;
			}
			catch
			{
				return default(int);	
			}
		}


		public async Task<bool> SendEmailConfirm(string email)
		{
			var user = await unitOfWork.UserManager.FindByEmailAsync(email);

			var code = new Random().Next(123000, 999123);

			try
			{
				unitOfWork.CreateTransaction();

				user.ConfirmationCode = code;
				user.ExpirationTimeCode = DateTime.Now.AddMinutes(3);

				await unitOfWork.Save();
				unitOfWork.Commit();

				await EmailVerificationCode(user.FullName, code.ToString(), user.Email);

				return true;
			}
			catch
			{
				unitOfWork.RollBack();
				return false;
			}
		}

		public async Task<ConfirmationResponseDTO> LoginUserAsync(LoginDto dto)
		{
			ConfirmationResponseDTO response = new ConfirmationResponseDTO();

			try
			{
				var IsConfirmedEmail = await unitOfWork.UserManager.IsEmailConfirmed(dto.Email);
				if (!IsConfirmedEmail)
				{

					response.Message = "our email is not confirmed yet. Please check your email inbox to confirm your email address.";
					return response;
				}

				var IsDeletedUser = await unitOfWork.UserManager.IsDeletedUser(dto.Email);
				if (!IsDeletedUser)
				{

					response.Message = "Account deleted. Access denied.";
					return response;
				}

				var user = await unitOfWork.UserManager.FindByEmailAsync(dto.Email);

				var IsCorrectPassword = BCrypt.Net.BCrypt.Verify(dto.password, user.PasswordHash);

				if (user is null || !IsCorrectPassword)
				{
					response.Message = "Invalid Email or password.";
					return response;
				}

				//create claim to user represnt individual information
				List<Claim> claims = new List<Claim>();

				claims.Add(new Claim(ClaimTypes.Email, user.Email));
				claims.Add(new Claim(ClaimTypes.Role, user.Role.ToString()));
				claims.Add(new Claim("UserId", user.ApplicationUserId.ToString()));

				var idntity = new ClaimsIdentity(claims, "ElearnPlatformAuth");
				var principal = new ClaimsPrincipal(idntity);

				await dto.httpContext.SignInAsync("ElearnPlatformAuth", principal
					, new AuthenticationProperties()
					{
						ExpiresUtc = dto.RememberMe ? DateTimeOffset.UtcNow.AddDays(30) : DateTimeOffset.UtcNow.AddDays(7),
						IsPersistent = true,
					});

				response.IsSuccessed = true;
				return response;
			}
			catch
			{
				response.IsSuccessed = false;
				return response;
			}
		}
		public async Task<ConfirmationResponseDTO> ForgetPasswordAsync(string Email)
		{
			ConfirmationResponseDTO response = new ConfirmationResponseDTO();
			try
			{

				var IsConfirmedEmail = await unitOfWork.UserManager.IsEmailConfirmed(Email);
				if (!IsConfirmedEmail)
				{

					response.Message = "our email is not confirmed yet. Please check your email inbox to confirm your email address.";
					return response;
				}
				var IsDeletedUser = await unitOfWork.UserManager.IsDeletedUser(Email);
				if (!IsDeletedUser)
				{

					response.Message = "Account deleted. Access denied.";
					return response;
				}

				var user = await unitOfWork.UserManager.FindByEmailAsync(Email);
				if (user is null)
				{
					response.Message = "Invalid	Email";
					return response;
				}

				var code = new Random().Next(123000, 999123);
				var baseUri = GetBaseUri(); 
				var endPointUri = new Uri(string.Concat(baseUri,@"/Accout/ResetPassword"));
				var modifiedUri = QueryHelpers.AddQueryString(endPointUri.ToString(), "VerificationCode", code.ToString());
				modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "Email", user.Email);

				await ResetPasswordCode(user.FullName, modifiedUri, user.Email);


				/////////need to save code to comapere it


				response.IsSuccessed = true;
				return response;
			}
			catch
			{
				response.IsSuccessed = false;
				return response;
			}
		}

		private async Task EmailVerificationCode(string userName, string code, string email)
		{
			var filePath = $"{webHostEnvironment.WebRootPath}/EmailTemplat/ConfirmEmailCode.html";
			var emailBody = await System.IO.File.ReadAllTextAsync(filePath);
			emailBody = emailBody.Replace("{AppName}", "Education Platform")
							.Replace("{UserName}", userName)
							.Replace("{Code}", code);

			var subject = "Email Verification Code";
			await emailSender.SendEmailAsync(email, subject, emailBody);

		}
		private async Task ResetPasswordCode(string userName, string ResetLink, string email)
		{
			var filePath = $"{webHostEnvironment.WebRootPath}/EmailTemplat/ResetPassword.html";
			var emailBody = await System.IO.File.ReadAllTextAsync(filePath);
			emailBody = emailBody.Replace("{AppName}", "Education Platform")
							.Replace("{UserName}", userName)
							.Replace("{ResetLink}", ResetLink);

			var subject = "Password Reset Request";
			await emailSender.SendEmailAsync(email, subject, emailBody);

		}

		private string GetBaseUri()
		{
			var request = httpContextAccessor.HttpContext.Request;
			var schema = request.Scheme;
			var Host = request.Host.ToUriComponent();
			string BaseUri = string.Concat(schema, "://", Host);
			return BaseUri;

		}

		public async Task<bool> ConfirmUpdateEmailAsync(string Email)
		{
			try
			{
				var user=await unitOfWork.UserManager.FindByEmailAsync(Email);

				unitOfWork.CreateTransaction();
				user.EmailConfirmed=true;

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


		public async Task<UserDetailsRespond> GetUserById(int userId)
		{
			UserDetailsRespond respond= new UserDetailsRespond();		
			var user = await unitOfWork.UserManager.GetByIdEntity(userId);
			respond.FirstName=user.FullName.Split(' ')[0];
			respond.LastName=user.FullName.Split(' ')[1];
			respond.ImagePath = $"{AccountFolderName}{user.ImageUrl}";

			return respond;		
		}
	}
}

