using CleanArch.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Infrastructure.Configurations
{
	public class VideoConfigurations :IEntityTypeConfiguration<Video>
	{
		public void Configure(EntityTypeBuilder<Video> builder) 
		{
			builder.Property(e => e.VideoDuration).HasColumnType("time");
			builder.Property(e => e.VideoName).HasMaxLength(150);
			builder.Property(e => e.VideoFileUrl).HasMaxLength(400);
			builder.HasOne(e => e.Section).WithMany(e => e.Videos).HasForeignKey(e => e.SectionId);
		
		}
	}
}
