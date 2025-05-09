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
	public class ApplicationUserConfigurations :IEntityTypeConfiguration<ApplicationUser>
	{
		public void Configure(EntityTypeBuilder<ApplicationUser> builder)
		{
			builder.Property(e => e.ImageUrl).HasMaxLength(150);
			builder.Property(e => e.FullName).HasMaxLength(70);
			builder.Property(e => e.Email).HasMaxLength(50);
			builder.Property(e => e.UserName).HasMaxLength(30);
			builder.Property(e => e.PasswordHash).HasMaxLength(70);
			builder.Property(e => e.FullName).HasMaxLength(70);
		    builder.HasIndex(e => e.Email).IsUnique().HasFilter(null);	
		    builder.HasIndex(e => e.UserName).IsUnique().HasFilter(null);
			builder.Property(e => e.Role).HasConversion<string>().HasMaxLength(10);

			builder.Property(e=>e.AttemptCount).HasDefaultValue(3);
		}
	}
}
