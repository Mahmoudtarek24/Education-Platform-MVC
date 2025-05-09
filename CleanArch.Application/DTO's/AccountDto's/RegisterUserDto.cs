using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.DTO_s.AccountDto_s
{
	public class RegisterUserDto
	{
		public string Email { get; set; }	
		public string Username { get; set; }	
		public string FullName { get; set; }	
		public DateTime DateOfBirth { get; set; }
		public string Password { get; set; }	

	}
}
