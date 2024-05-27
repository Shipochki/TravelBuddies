namespace TravelBuddies.Presentation.Responses
{
	using Microsoft.AspNetCore.Mvc;
	using System.Text.Json;

	public class ErrorResponse
	{
		public int? StatusCode { get; set; }

		public string? StatusPhrase { get; set; }

		public DateTime? Timestamp { get; set; }

		public List<string>? ErrorMessage { get; set; }

		public override string ToString()
		{
			return JsonSerializer.Serialize(this);
		}

		public static IActionResult GenerateErrorResponse(ActionContext context)
		{
			//ErrorResponse apiError = new ErrorResponse();
			//apiError.StatusCode = 400;
			//apiError.StatusPhrase = "Bad Request";
			//apiError.Timestamp = DateTime.Now;
			string detailMessage = string.Empty;

			var errors = context.ModelState.AsEnumerable();

			foreach (var error in errors)
			{
				foreach (var inner in error.Value!.Errors)
				{
					detailMessage += inner.ErrorMessage + Environment.NewLine;
				}
			}
			
			ProblemDetails problemDetails = new ProblemDetails
			{
				Status = 400,
				Title = "Bad Request",
				Detail = detailMessage.TrimEnd(),
				Instance = context.HttpContext.Request.Path
			};

			return new JsonResult(problemDetails) { StatusCode = 400};
		}
	}
}
