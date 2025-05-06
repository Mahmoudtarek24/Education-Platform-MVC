using CleanArch.Application.Interfaces;
using CleanArch.Application.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.Services
{
	public class StorageServices : IStorageService
	{
		private readonly SupabaseSettings supabaseSettings;
		public StorageServices(IOptions<SupabaseSettings> options)
		{
			this.supabaseSettings = options.Value;
		}

		public async Task<string> UploadToSupabaseAsync(IFormFile file)
		{
			var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
			var bucketName = "uploadvideos";
			var url = $"{supabaseSettings.projectUrl}/storage/v1/object/{bucketName}/{fileName}";

			using var client = new HttpClient
			{
				Timeout = TimeSpan.FromMinutes(30)
			};

			using var stream = file.OpenReadStream();
			var content = new StreamContent(stream);
			content.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

			var request = new HttpRequestMessage(HttpMethod.Post, url)
			{
				Content = content
			};

			request.Headers.Add("Authorization", $"Bearer {supabaseSettings.anonKey}");
			request.Headers.Add("apikey", supabaseSettings.anonKey);

			var response = await client.SendAsync(request);

			if (!response.IsSuccessStatusCode)
			{
				var errorContent = await response.Content.ReadAsStringAsync();
				throw new Exception($"Upload failed: {response.StatusCode} - {errorContent}");
			}

			// لو الـ bucket Public:
			return $"{supabaseSettings.projectUrl}/storage/v1/object/public/{bucketName}/{fileName}";
		}

		public async Task<bool> DeleteFromSupabaseAsync(string fileUrl)
		{
			var fileName = Path.GetFileName(new Uri(fileUrl).AbsolutePath);

			var url = $"{supabaseSettings.projectUrl}/storage/v1/object/uploadvideos/{fileName}";

			using var client = new HttpClient();
			client.DefaultRequestHeaders.Add("apikey", supabaseSettings.anonKey);
			client.DefaultRequestHeaders.Add("Authorization", $"Bearer {supabaseSettings.anonKey}");

			var request = new HttpRequestMessage(HttpMethod.Delete, url);
			var response = await client.SendAsync(request);

			return response.IsSuccessStatusCode;
		}

	}
}
