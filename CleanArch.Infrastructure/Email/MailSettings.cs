using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Infrastructure.Email
{
	public class MailSettings
	{
		public string Email { get; set; }
		public string DisplayName { get; set; }
		public string password { get; set; }
		public string Host { get; set; }
		public int Port { get; set; }
	}
}
