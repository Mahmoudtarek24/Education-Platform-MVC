using CleanArch.Application.DTO_s;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.Interfaces
{
	public interface IImageServices
	{
		Task<string> UploadImage(IFormFile imagfile, string FolderPath);
		void DeleteImage(string imageName);
	}
}
