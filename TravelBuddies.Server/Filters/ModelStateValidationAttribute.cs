namespace TravelBuddies.Presentation.Filters
{
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.AspNetCore.Mvc.Filters;

	public class ModelStateValidationAttribute : Attribute, IAsyncActionFilter
	{
		public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			if(!context.ModelState.IsValid)
			{
				context.Result = new BadRequestResult();
			}

			return Task.CompletedTask;
		}
	}
}
