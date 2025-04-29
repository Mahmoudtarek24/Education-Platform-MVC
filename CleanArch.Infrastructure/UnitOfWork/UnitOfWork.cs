using CleanArch.Domain.Interfaces;
using CleanArch.Infrastructure.Context;
using CleanArch.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Infrastructure.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly EducationPlatformDbContext context;

		public ICourseRepository courseRepository {  get; }

		private IDbContextTransaction? objTrans = null;

		public UnitOfWork(EducationPlatformDbContext context)
		{
			this.context = context;
			courseRepository = new CourseRepository(context);
		}
		

		public void CreateTransaction()
		{
			objTrans = context.Database.BeginTransaction();
		}

		
		public void Commit()
		{
			objTrans?.Commit();
		}

		public void RollBack()
		{
			objTrans?.Rollback();
		}
		public async Task Save()
		{
			await context.SaveChangesAsync();
		}
	}
}
