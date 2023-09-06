#region using
using System.IO;
#endregion using

namespace Totp.Services
{
	class FileConfigStorage : IConfigStorage
	{
		const string _filePath = "config.json";
		public string? Open()
		{
			try
			{
				return new StreamReader(File.OpenRead(_filePath)).ReadToEnd();
			}
			catch
			{
				return null;
			}
		}

		public void Save(string configRecordsString)
			=> File.WriteAllText(_filePath, configRecordsString);
	}
}
