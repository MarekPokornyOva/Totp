#region using
using System.IO;
using System.Text.Json;
#endregion using

namespace Totp.Services
{
	static class ConfigSerializer
	{
		public static ConfigurationRecord[] Deserialize(string json)
			=> JsonSerializer.Deserialize<ConfigurationRecord[]>(json) ?? throw new InvalidDataException("Cannot deserialize configuration.");
	}

	public class ConfigurationRecord
	{
		public string Title { get; set; } = default!;
		public string Key { get; set; } = default!;
	}
}
