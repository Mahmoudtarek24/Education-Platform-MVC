using CleanArch.Application.DTO_s.VideoDto_s;
using CleanArch.Application.ResponseDTO_s;
using CleanArch.Application.ResponseDTO_s.VideoRespondDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.Interfaces
{
	public interface IVideoServices
	{
		Task<ConfirmationResponseDTO> AddVideoAsync(VideoDto CreateVideoDto);
		Task<ConfirmationResponseDTO> DeleteVideoAsync(int VideoId);

		Task<ChangePreviewVideoRespond> ChangePreviewVideoAsync(int VideoId);

		Task<VideoDetailsRespond> GetVideoByIdAsync(int VideoId);	

		Task<List<byte>> OrderVideoNumberAsync(int sectionId);
		Task<bool> ValidVideoNameAsync(int sectionId, string Name);
	}
}
