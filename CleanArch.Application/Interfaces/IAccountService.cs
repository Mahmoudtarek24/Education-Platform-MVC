using CleanArch.Application.DTO_s.AccountDto_s;
using CleanArch.Application.ResponseDTO_s;
using CleanArch.Application.ResponseDTO_s.UserRespondDto;
using CleanArch.Domain.Entity;

namespace CleanArch.Application.Interfaces
{
	public interface IAccountService
	{
		Task<ConfirmationResponseDTO> RegisterUserAsync(RegisterUserDto model);
		Task<ConfirmationResponseDTO> LoginUserAsync(LoginDto dto);
		Task<ConfirmationResponseDTO> ForgetPasswordAsync(string Email);
		Task<bool> ConfirmUpdateEmailAsync(string Email);
		Task<UserDetailsRespond> GetUserById(int userId);


		Task<bool> SendEmailConfirm(string email);/////
		Task<int> SendEmailConfirm(string Email, string FullName);
		Task<bool> IsEmailTakenAsync(string email);
		Task<bool> IsUserNameTakenAsync(string UserName);
	}
}
