using CleanArch.Application.DTO_s.SectionDto_s;
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

		Task<SectionDto> IsDeletedSection(int Id);
	}
}
