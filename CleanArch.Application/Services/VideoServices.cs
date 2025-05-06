using CleanArch.Application.DTO_s.VideoDto_s;
using CleanArch.Application.Interfaces;
using CleanArch.Application.ResponseDTO_s;
using CleanArch.Application.ResponseDTO_s.VideoRespondDto;
using CleanArch.Domain.Entity;
using CleanArch.Domain.Interfaces;
using Xabe.FFmpeg;

namespace CleanArch.Application.Services
{
	public class VideoServices : IVideoServices
	{
		private readonly IStorageService storageService;
		private readonly IUnitOfWork unitOfWork;
		public VideoServices(IStorageService storageService, IUnitOfWork unitOfWork)
		{
			this.storageService = storageService;
			this.unitOfWork = unitOfWork;
		}
		public async Task<ConfirmationResponseDTO> AddVideoAsync(VideoDto CreateVideoDto)
		{
			ConfirmationResponseDTO response = null; 
			if (CreateVideoDto is null)
				return response;

			response= new ConfirmationResponseDTO();
			var section = unitOfWork.SectionRepository.IsDeletedSection(CreateVideoDto.SectionId);
			if (section is null)
			{
				response.Message = "Section Is Deleted";
				return response;
			}

			var tempFileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(CreateVideoDto.Video.FileName)}";
			var tempPath = Path.Combine(Path.GetTempPath(), tempFileName);

			var durationInMinutes = default(double);
			try
			{
				using (var stream = new FileStream(tempPath, FileMode.Create))
				{
					await CreateVideoDto.Video.CopyToAsync(stream);
				}
			
				FFmpeg.SetExecutablesPath(@"C:\Tools\ffmpeg-7.1.1-essentials_build\bin");
				var mediaInfo = await FFmpeg.GetMediaInfo(tempPath);
				durationInMinutes = mediaInfo.Duration.TotalMinutes;

				if (durationInMinutes > 30)
				{
					response.Message = "Video is too long. Maximum allowed duration is 30 minutes.";
					return response;
				}
			}
			catch
			{
				if (System.IO.File.Exists(tempPath))
				{
					System.IO.File.Delete(tempPath);
				}
				response.Message = "Error occurred during video processing";
				return response;
			}
			try
			{
				var videoUrl = await storageService.UploadToSupabaseAsync(CreateVideoDto.Video);
				unitOfWork.CreateTransaction();

				var video = new Video()
				{
					SectionId = CreateVideoDto.SectionId,
					VideoName = CreateVideoDto.VideoName,
					VideoOrder = CreateVideoDto.Order,
					IsFree = CreateVideoDto.IsFree,		
					VideoDuration = TimeSpan.FromMinutes(durationInMinutes),
					VideoFileUrl = videoUrl,
					CreateOn=DateTime.Now,	
				};

				await unitOfWork.VideoRepository.AddAsync(video);
				await unitOfWork.Save();
				unitOfWork.Commit();

				response.IsSuccessed = true;
				return response;
			}
			catch
			{
				unitOfWork.RollBack();
				return response;
			}
		}

		public async Task<List<byte>> OrderVideoNumberAsync(int sectionId)
		{
			return await unitOfWork.VideoRepository.OrderVideoNumber(sectionId);	
		}

		public async Task<bool> ValidVideoNameAsync(int sectionId, string Name)
		{
			return await unitOfWork.VideoRepository.ValidVideoName(sectionId, Name);		
		}

		public async Task<ConfirmationResponseDTO> DeleteVideoAsync(int VideoId)
		{
			ConfirmationResponseDTO responseDTO = new ConfirmationResponseDTO();
			var video=await unitOfWork.VideoRepository.GetByIdEntity(VideoId);

			string VideoUrl = video.VideoFileUrl;
			try
			{
				unitOfWork.CreateTransaction();
			    unitOfWork.VideoRepository.Delete(video);

				await unitOfWork.Save();
				unitOfWork.Commit();

				var result = await storageService.DeleteFromSupabaseAsync(VideoUrl);
				responseDTO.IsSuccessed= true;	

				return responseDTO;
			}
			catch
			{
				unitOfWork.RollBack();
				responseDTO.IsSuccessed = false;
				return responseDTO;	
			}

		}

		public async Task<ChangePreviewVideoRespond> ChangePreviewVideoAsync(int VideoId)
		{
			ChangePreviewVideoRespond videoRespond = new ChangePreviewVideoRespond();

			var video = await unitOfWork.VideoRepository.GetByIdEntity(VideoId);

			try
			{
				unitOfWork.CreateTransaction();	

				video.LastUpdateOn = DateTime.Now;
				video.IsFree = !video.IsFree;

				await unitOfWork.Save();
				unitOfWork.Commit();

				videoRespond.LastUpdatOn =video.LastUpdateOn;	
				videoRespond.IsSuccessed=true;	

				return videoRespond;	
			}
			catch
			{
				unitOfWork.RollBack();
				videoRespond.IsSuccessed = false;
				return videoRespond;
			}
		}

		public async Task<VideoDetailsRespond> GetVideoByIdAsync(int VideoId)
		{
			var video = await unitOfWork.VideoRepository.GetByIdEntity(VideoId);

			VideoDetailsRespond videoDetails = new VideoDetailsRespond()
			{
				VideoId = VideoId,	
				VideoFileUrl=video.VideoFileUrl,
				VideoName=video.VideoName,	
				VideoOrder=video.VideoOrder,	
			};	

			return videoDetails;	
		}
	}
}

