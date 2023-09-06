namespace Totp.Services
{
	public static class Configuration
	{
		public static IConfigStorage ConfigStorage { get; set; } = default!;
	}
}
