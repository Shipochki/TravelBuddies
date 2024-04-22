namespace TravelBuddies.Application.CustomLogger
{
	using System.Threading.Tasks;
    using TravelBuddies.Application.CustomLogger.Interfaces;
	using TravelBuddies.Domain.Enums;

	public class FileLogger : ILogger
	{
        private readonly string _categoryName;
        private readonly string _filePath;

        public FileLogger(string categoryName, string filePath)
        {
            _categoryName = categoryName;
            _filePath = filePath;
        }

		public async Task LogAsync(LogLevel level, string message)
		{
			string logMessage = $"{DateTime.Now} [{level}] - {_categoryName}: {message}";
			await WriteLogToFileAsync(logMessage);
		}

		private async Task WriteLogToFileAsync(string logMessage)
		{
			try
			{
				string currentFilePath = $"{_filePath}\\Logs_{DateTime.Now.ToString("dd-MM-yyyy")}";
				using (var writer = new StreamWriter(currentFilePath, append: true))
				{
					await writer.WriteLineAsync(logMessage);
				}
			}
			catch (Exception ex)
			{
                await Console.Out.WriteLineAsync($"Error writing to log file: {ex.Message}");
            }
		}
	}
}
