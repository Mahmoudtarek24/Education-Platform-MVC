using CleanArch.Application.DTO_s.SectionDto_s;
using CleanArch.Application.Interfaces;
using CleanArch.Application.ResponseDTO_s.SectionRespondDto;
using CleanArch.Application.ResponseDTO_s.VideoRespondDto;
using CleanArch.Domain.Entity;
using CleanArch.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.Services
{
	public class SectionServices : ISectionServices
	{
		private readonly IUnitOfWork unitOfWork;
		public SectionServices(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}
		public async Task<bool> CreateSectionAsync(SectionDto createSectionDto)
		{
			if (createSectionDto == null)
				return false;

			

			try
			{
				unitOfWork.CreateTransaction();
				Section section = new Section()
				{
					CourseId = createSectionDto.CourseId,
					Name = createSectionDto.Name,
					Order = createSectionDto.Order,
				};

				await unitOfWork.SectionRepository.AddAsync(section);
				await unitOfWork.Save();

				unitOfWork.Commit();
				return true;
			}
			catch
			{
				unitOfWork.RollBack();
				return false;
			}
		}


		public async Task<bool> UpdateSectionAsync(SectionDto UpdareSectionDto)
		{
			if (UpdareSectionDto == null)
				return false;

			var Section =await unitOfWork.SectionRepository.GetByIdEntity(UpdareSectionDto.SectionId);

			if(Section == null)	
				return false;

			try
			{
				unitOfWork.CreateTransaction();
				Section.Name = UpdareSectionDto.Name;
				Section.Order = UpdareSectionDto.Order;

				await unitOfWork.Save();
				unitOfWork.Commit();	
				return true;		
			}
			catch
			{
				unitOfWork.RollBack();
				return false;
			}

		}



		public SectionDto IsDeletedSection(int Id)
		{
			SectionDto UpdateSectionDto = null;
			var section = unitOfWork.SectionRepository.IsDeletedSection(Id);

			if (section == null)
				return UpdateSectionDto;

			UpdateSectionDto = new SectionDto()
			{
				CourseId = section.CourseId,
				Name = section.Name,
				Order = section.Order,
			};

			return UpdateSectionDto;
		}

		public async Task<bool> DeleteSectionAsync(int Id)
		{
			var Section = await unitOfWork.SectionRepository.GetByIdEntity(Id);

			if (Section == null) return false;

			try
			{
				unitOfWork.CreateTransaction();
				Section.IsDeleted = true;

				await unitOfWork.Save();
				unitOfWork.Commit();
				return true;
			}
			catch
			{
				unitOfWork.RollBack();
				return false;	
			}
		}

		public List<SectionDto> GetSectionByCourseId(int CourseId)
		{

			List<SectionDto> respond = null;
			var Sections= unitOfWork.SectionRepository.GetSectionByCourseId(CourseId);

			if (Sections is null)
				return respond;


			respond = new List<SectionDto>();

			foreach (var section in Sections)
			{
				SectionDto sectionDto = new SectionDto()
				{
					CourseId = section.CourseId,
					Name = section.Name,
					Order = section.Order,
					SectionId = section.SectionId,
				};
				respond.Add(sectionDto);		
			}

			return respond;
		}

		public async Task<SectionDetailsRespond> SectionVideosAsync(int SectionId)
		{
			var Sections=await unitOfWork.SectionRepository.SectionVideos(SectionId);

			SectionDetailsRespond sectionDetailsRespond = new SectionDetailsRespond();

			TimeSpan SectionDuration = default;

			foreach (var video in Sections.Videos) {

				VideoDetailsRespond videoDetailsRespond = new VideoDetailsRespond()
				{
					VideoDuration = video.VideoDuration,
					CreateOn = video.CreateOn,
					IsFree = video.IsFree,
					LastUpdateOn = video.LastUpdateOn,
					VideoFileUrl = video.VideoFileUrl,
					VideoName = video.VideoName,
					VideoOrder = video.VideoOrder,
					VideoId = video.VideoId,
					
				};
				SectionDuration +=  video.VideoDuration;
				sectionDetailsRespond.VideoDetailsResponds.Add(videoDetailsRespond);		
			}
			sectionDetailsRespond.SectionDuration = SectionDuration;		

			return sectionDetailsRespond;	
		}
	}
}  
