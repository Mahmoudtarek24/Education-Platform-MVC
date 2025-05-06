using CleanArch.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Infrastructure.Context
{
	public class EducationPlatformDbContext : DbContext
	{
		public EducationPlatformDbContext(DbContextOptions<EducationPlatformDbContext> options):base(options) { }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfigurationsFromAssembly
				(
					typeof(EducationPlatformDbContext).Assembly
				);
			base.OnModelCreating(builder);
		}

		public DbSet<Course> Course { get; set; }	
		public DbSet<Section> Sections { get; set; }
		public DbSet<Video> Videos { get; set; }	
	}
}
