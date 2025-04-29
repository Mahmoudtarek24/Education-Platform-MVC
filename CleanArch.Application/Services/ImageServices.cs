using CleanArch.Application.DTO_s;
using CleanArch.Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.Services
{
	public class ImageServices : IImageServices
	{
		private readonly IWebHostEnvironment webHostEnvironment;
		public ImageServices(IWebHostEnvironment webHostEnvironment)
		{
			this.webHostEnvironment = webHostEnvironment;
		}

		
		public async Task<string> UploadImage(IFormFile imagefile, string folderPath)
		{
			var FolderPath = Path.Combine(webHostEnvironment.WebRootPath, folderPath.TrimStart('/'));
			if (!Directory.Exists(FolderPath))
			{ 
				Directory.CreateDirectory(FolderPath);
			}

			var extension=Path.GetExtension(imagefile.FileName);	
			var imageName= $"{Guid.NewGuid().ToString()}{extension}";
			var ImageFolderPath= Path.Combine(FolderPath,imageName);	

			using(var fileStream=new FileStream(ImageFolderPath, FileMode.Create, FileAccess.Write))
			{
				await imagefile.CopyToAsync(fileStream);
			}

			return imageName;	
		}

		public void DeleteImage(string imagePath)
		{
			var oldImage = $"{webHostEnvironment}{imagePath}";

			if (File.Exists(oldImage)) 
				File.Delete(oldImage);
		}

	}
}
