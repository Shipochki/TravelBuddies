namespace TravelBuddies.Presentation.Contract
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

			return new BadRequestObjectResult(apiError);
		}
	}
}
