using Android.App;
using Android.Content.PM;

using Avalonia;
using Avalonia.Android;
using Totp.Services;

namespace Totp.Android;

[Activity(
    Label = "Totp",
    Theme = "@style/MyTheme.NoActionBar",
    Icon = "@drawable/icon",
    MainLauncher = true,
    ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode)]
public class MainActivity : AvaloniaMainActivity<App>
{
    protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
    {
		Configuration.ConfigStorage = new PreferencesStorage(this);
        return base.CustomizeAppBuilder(builder)
            .WithInterFont();
    }
}
