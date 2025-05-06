using CleanArch.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Domain.Interfaces
{
	public interface ISectionRepository :IGenaricRepository<Section>
	{
		Section? IsDeletedSection(int Id);
		List<Section>? GetSectionByCourseId(int CourseId);
		Task<Section> SectionVideos(int SectionId);
	}
}
