﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Domain.Interfaces
{
	public interface IUnitOfWork 
	{
		ICourseRepository courseRepository { get; }
		ISectionRepository SectionRepository { get; }
		IVideoRepository VideoRepository { get; }	
		IApplicationUserRepository UserManager { get; }
		void CreateTransaction();
		void Commit();
		void RollBack();
		Task Save();
	}
}
