using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Domain.Entity
{
	public class BaseModel
	{
		public DateTime CreateOn { get; set; }	
		public DateTime? UpdateOn { get; set; }	
		public bool IsDeleted { get; set; }	
	}
}
