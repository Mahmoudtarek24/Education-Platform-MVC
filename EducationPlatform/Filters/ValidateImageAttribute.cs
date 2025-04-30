using EducationPlatform.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EducationPlatform.Filters
{
	public class ValidateImageAttribute :ActionFilterAttribute
	{
		private readonly int maxFileSize;
		private readonly string[] _allowedExtensions;
		private readonly string formFieldName;
		public ValidateImageAttribute(string formFieldName,int maxSize, string[] allowedExtension)
		{
			this.maxFileSize = maxSize * 1024 * 1024;
			this._allowedExtensions = allowedExtension;
			this.formFieldName = formFieldName;
		}
		public override void OnActionExecuting(ActionExecutingContext context)
		{

			var file = context.HttpContext.Request.Form.Files[formFieldName];
			
			if (file is null) 
			{

				context.ModelState.AddModelError(formFieldName, $"{formFieldName} is required.");
				return;	
			}

			if (file.Length > maxFileSize)
			{
				context.ModelState.AddModelError(formFieldName, ValidationMessages.MaxSize);
				return;
			}

			var extension = Path.GetExtension(file.FileName).ToLower();
			if (!_allowedExtensions.Contains(extension))
			{
				context.ModelState.AddModelError(formFieldName, $"Invalid file extension. Allowed: {string.Join(", ", _allowedExtensions)}");
				return;
			}
			base.OnActionExecuting(context);
		}
	}
}
