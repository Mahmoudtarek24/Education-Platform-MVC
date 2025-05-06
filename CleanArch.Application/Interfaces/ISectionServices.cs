using CleanArch.Application.DTO_s.SectionDto_s;
using CleanArch.Application.ResponseDTO_s.SectionRespondDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.Interfaces
{
	public interface ISectionServices
	{
		Task<bool> CreateSectionAsync(SectionDto createSectionDto);

		Task<bool> UpdateSectionAsync(SectionDto UpdareSectionDto);

		List<SectionDto> GetSectionByCourseId(int CourseId);
		Task<bool> DeleteSectionAsync(int Id);
		SectionDto IsDeletedSection(int Id);

		Task<SectionDetailsRespond> SectionVideosAsync(int SectionId);
	}
}
