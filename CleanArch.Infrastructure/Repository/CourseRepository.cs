﻿using CleanArch.Domain.Entity;
using CleanArch.Domain.Enums;
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
	public class CourseRepository :GenaricRepository<Course>, ICourseRepository
 	{
		public CourseRepository(EducationPlatformDbContext context):base(context) { }
		

		public async Task<List<Course?>> GetAllCourses(int PageNumber, int PageSize)
		{
		   var result=await   context.Course.Skip((PageNumber - 1) * PageSize).Take(PageSize)
							.Select(e => new Course
							{
								CourseId=e.CourseId,
								CourseName=e.CourseName,
								IsFree=e.IsFree,
								CourseImage=e.CourseImage,
								courseStatus=e.courseStatus,
								IsDeleted=e.IsDeleted,
								CreateOn=e.CreateOn,
							
							}).ToListAsync();

			return result;
		}
		public Course GetCourseStatus(int Id)
		{
			var result =  context.Course
							.Select(e=>new Course
							{
								CourseId=e.CourseId,	
								courseStatus=e.courseStatus
							}).FirstOrDefault(e => e.CourseId == Id);
							
			return result;
		}

		public async Task<Course?> IsDeletedCourse(int Id)
		{
			return await context.Course.Where(e => !e.IsDeleted && e.courseStatus != CourseStatus.Archived).
				             SingleOrDefaultAsync(e => e.CourseId == Id);
		}


		public async Task<Course?> AllCourseData(int Id)
		{
			return await context.Course.Include(e=>e.Sections)
				                       .Select(e=>new Course {
										CourseId = e.CourseId,
									   Description = e.Description,
									   Discount = e.Discount,
									   CourseImage = e.CourseImage,	
									   IsSequentialWatch=e.IsSequentialWatch,
									   CreateOn = e.CreateOn,	
									   IsDeleted= e.IsDeleted,	
									   CourseName = e.CourseName,		
									   CourseLevel = e.CourseLevel,	
									   courseStatus = e.courseStatus,
									   Duration = e.Duration,
									   IsFree=e.IsFree,
									   LastUpdateOn = e.LastUpdateOn,
									   Price = e.Price,
									   Rating = e.Rating,
									   Sections = e.Sections.Where(e=>!e.IsDeleted).ToList(),
									   })
				                       .FirstOrDefaultAsync(e => e.CourseId == Id);

		}


	}
}
