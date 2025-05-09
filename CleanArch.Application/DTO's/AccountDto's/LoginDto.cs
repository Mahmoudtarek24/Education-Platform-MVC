using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.DTO_s.AccountDto_s
{
	public class LoginDto
	{
		public string Email {  get; set; }	
		public string password {  get; set; }
		public bool RememberMe { get; set; }
		public HttpContext httpContext { get; set; }	
	}
}
