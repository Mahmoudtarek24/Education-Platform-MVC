﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.Interfaces
{
	public interface IEmailSender
	{
		Task SendEmailAsync(string email, string subject, string htmlMessage);
	}
}
