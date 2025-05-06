using CleanArch.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Domain.Interfaces
{
	public interface IVideoRepository :IGenaricRepository<Video> 
	{
		Task<List<byte>> OrderVideoNumber(int sectionId);
		Task<bool> ValidVideoName(int sectionId, string Name);
	}
}
