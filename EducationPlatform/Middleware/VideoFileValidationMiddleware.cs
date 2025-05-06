using EducationPlatform.Settings;
using Microsoft.Extensions.Options;
using System.Web;

namespace EducationPlatform.Middleware
{
	public class VideoFileValidationMiddleware
	{
		private readonly VideoSetting videoSetting;
		private readonly RequestDelegate _next;
		public VideoFileValidationMiddleware(IOptions<VideoSetting> options, RequestDelegate next)
		{
			videoSetting = options.Value;
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			var path = context.Request.Path.Value?.ToLowerInvariant();


			if (context.Request.Method == HttpMethods.Post && videoSetting._targetPaths != null && videoSetting._targetPaths.Any(e => e == path))
			{
				if (context.Request.HasFormContentType && context.Request.Form.Files.Any())
				{
					var file = context.Request.Form.Files[0];
					if (file.Length > videoSetting.MaxFileSize)
					{
						context.Response.StatusCode = 400;
						await context.Response.WriteAsync($"File '{file.FileName}' exceeds the limit.");
						return;
					}

					var extensions = Path.GetExtension(file.FileName).ToLowerInvariant();
					if (!videoSetting.AllowedExtensions.Contains(extensions))
					{
						context.Response.StatusCode = 400;
						await context.Response.WriteAsync($"File '{file.FileName}' has invalid type. Allowed: MP4, AVI, MOV.");
						return;
					}

					if (!videoSetting.AllowedMimeTypes.Contains(file.ContentType))
					{
						context.Response.StatusCode = 400;
						await context.Response.WriteAsync($"File '{file.FileName}' has invalid MIME type.");
						return;
					}
				}
				else
				{
					context.Response.StatusCode = 400;
					await context.Response.WriteAsync("No files uploaded.");
					return;
				}
			}

			await _next(context);
		}
	}
}

