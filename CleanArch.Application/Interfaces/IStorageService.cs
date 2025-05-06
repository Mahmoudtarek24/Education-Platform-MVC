using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.Interfaces
{
	public interface IStorageService
	{
		Task<string> UploadToSupabaseAsync(IFormFile file);
		Task<bool> DeleteFromSupabaseAsync(string fileName);
	}
}
