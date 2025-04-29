using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Domain.Interfaces
{
	public interface IGenaricRepository<T> where T : class
	{
		Task<IEnumerable<T>> GetAllEntities();
		Task<T?> GetByIdEntity(int id);
		Task AddAsync(T entity);
		void Update(T entity);
		void Delete(T entity);

	}
}
