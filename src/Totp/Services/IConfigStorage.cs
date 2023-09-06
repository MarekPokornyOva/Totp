#region using
using System.IO;
#endregion using

namespace Totp.Services
{
	public interface IConfigStorage
	{
		string? Open();
		void Save(string configRecordsString);
	}
}
