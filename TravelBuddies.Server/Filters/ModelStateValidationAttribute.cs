namespace TravelBuddies.Presentation.Filters
{
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.Filters;
	using TravelBuddies.Presentation.Contract;

	public class ModelStateValidationAttribute : ActionFilterAttribute
	{
		public override void OnResultExecuting(ResultExecutingContext context)
		{
			if (!context.ModelState.IsValid)
			{
				ErrorResponse apiError = new ErrorResponse();
				apiError.StatusCode = 400;
				apiError.StatusPhrase = "Bad Request";
				apiError.Timestamp = DateTime.Now;

				var errors = context.ModelState.AsEnumerable();

				foreach (var error in errors)
				{
					apiError.ErrorMessage = new List<string>();
					foreach (var inner in error.Value!.Errors)
					{
						apiError.ErrorMessage.Add(inner.ErrorMessage);
					}
				}

				context.Result = new BadRequestObjectResult(apiError);
			}
		}

	}
}
