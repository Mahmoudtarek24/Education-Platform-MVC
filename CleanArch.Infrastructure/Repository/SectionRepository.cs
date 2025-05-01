using CleanArch.Domain.Entity;
using CleanArch.Domain.Interfaces;
using CleanArch.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Infrastructure.Repository
{
	public class SectionRepository : GenaricRepository<Section>, ISectionRepository
	{
		public SectionRepository(EducationPlatformDbContext context) : base(context) { }


		public Section? IsDeletedSection(int Id)
		{
			return context.Sections
					.Where(s => s.SectionId == Id && !s.IsDeleted)
					.FirstOrDefault();

		}



	}
}


