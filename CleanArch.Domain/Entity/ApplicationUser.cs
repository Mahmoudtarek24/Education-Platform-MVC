using CleanArch.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Domain.Entity
{
	public class ApplicationUser :BaseModel
	{
		public int ApplicationUserId { get; set; }	
		public string FullName { get; set; }	
		public string Email { get; set; }	
		public string UserName { get; set; }	
		public string PasswordHash { get; set; }	
		public string? ImageUrl { get; set; }	
		public bool EmailConfirmed { get; set; }	
		public DateTime DateOfBirth { get; set; }
		public UserRole Role { get; set; }

		public int ConfirmationCode { get; set; }	
		public DateTime ExpirationTimeCode { get; set; }
		public byte AttemptCount { get; set; } 
	}
}
