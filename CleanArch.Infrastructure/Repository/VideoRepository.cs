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
	public class VideoRepository : GenaricRepository<Video>, IVideoRepository
	{
		public VideoRepository(EducationPlatformDbContext context) : base(context) { }



		public async Task<List<byte>> OrderVideoNumber(int sectionId)
		{
			var OrderNumberList = await context.Videos.Where(e => e.SectionId == sectionId)
				.Select(e => e.VideoOrder).ToListAsync();

			return OrderNumberList;

		}
		public async Task<bool> ValidVideoName(int sectionId, string Name)
		{
			return await context.Videos.AnyAsync(e => e.SectionId == sectionId && e.VideoName == Name);
		}
	}
}
