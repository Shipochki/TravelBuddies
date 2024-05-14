namespace TravelBuddies.IntegrationTests.Helpers
{

	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.Primitives;

	public class ConfigurationDummy : IConfiguration
	{
		public string? this[string key] { get => "test"; set => value = "test"; }

		public IEnumerable<IConfigurationSection> GetChildren()
		{
			throw new NotImplementedException();
		}

		public IChangeToken GetReloadToken()
		{
			throw new NotImplementedException();
		}

		public IConfigurationSection GetSection(string key)
		{
			throw new NotImplementedException();
		}
	}
}
