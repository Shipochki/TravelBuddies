namespace TravelBuddies.Infrastructure.CustomLogger
{
    using System.Threading.Tasks;
    using TravelBuddies.Application.Common.Interfaces.CustomLogger;
    using TravelBuddies.Domain.Enums;

    public class FileLogger : ILogger
	{
        private readonly string _categoryName;
        private readonly string _filePath;
		private readonly string _loggerValue;

        public FileLogger(string categoryName, string filePath, string loggerValue)
        {
            _categoryName = categoryName;
            _filePath = filePath;
			_loggerValue = loggerValue;
        }

		public async Task LogAsync(LogLevel level, string message)
		{
			LogLevel parsedLevel;
			Enum.TryParse(_loggerValue, out parsedLevel);
			if (((int)parsedLevel) <= ((int)level))
			{
				string logMessage = $"{DateTime.Now} [{level}] - {_categoryName}: {message}";
				await WriteLogToFileAsync(logMessage);
			}
		}

		private async Task WriteLogToFileAsync(string logMessage)
		{
			try
			{
				string currentFilePath = $"{_filePath}\\Logs_{DateTime.Now.ToString("dd-MM-yyyy")}.txt";
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
