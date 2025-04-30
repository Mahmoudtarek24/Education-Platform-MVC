using CleanArch.Domain.Entity;
using CleanArch.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Infrastructure.Configurations
{
	public class CoursesConfigurations : IEntityTypeConfiguration<Course>
	{
		public void Configure(EntityTypeBuilder<Course> builder)
		{
			builder.Property(e => e.CourseName).HasMaxLength(100);
			builder.Property(e => e.Description).HasMaxLength(1000);
			builder.Property(e => e.CourseImage).HasMaxLength(1000);
			builder.HasCheckConstraint("priceRang", "[Price]>0 ");
			builder.HasCheckConstraint("CK_DiscountPercentage", "[Discount] between 0 and 100");
			builder.Property(E => E.courseStatus).HasConversion<string>();
			builder.HasIndex(e => e.CourseName).IsUnique().HasFilter(null);

			builder.HasMany(e=>e.Sections).WithOne(e=>e.Course).HasForeignKey(e=>e.CourseId);	
		}
	}
}
