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
	public class GenaricRepository<T> : IGenaricRepository<T> where T : class
	{
		protected readonly EducationPlatformDbContext context;
		protected readonly DbSet<T> Table;
		public GenaricRepository(EducationPlatformDbContext context)
		{
			this.context = context;	
			this.Table=context.Set<T>();	
		}

		public async Task<IEnumerable<T>> GetAllEntities()
		{
			return await Table.ToListAsync();

		}
		public async Task AddAsync(T entity)
		{
			await  Table.AddAsync(entity);
		}

		public void Delete(T entity)
		{
		   Table.Remove(entity);	
		}


		public async Task<T?> GetByIdEntity(int id)
		{
			return await Table.FindAsync(id);
		}

		public void Update(T entity)
		{
			 Table.Update(entity);
		}
	}
}
