using CleanArch.Application.DTO_s.SectionDto_s;
using CleanArch.Application.Interfaces;
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



		public async Task<SectionDto> IsDeletedSection(int Id)
		{
			SectionDto UpdateSectionDto = null;
			var section = await unitOfWork.SectionRepository.IsDeletedSection(Id);

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
	}
}
