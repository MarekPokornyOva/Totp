#region using
using Android.App;
using Android.Content;
#endregion using

namespace Totp.Services
{
	class PreferencesStorage : IConfigStorage
	{
		//https://stackoverflow.com/questions/5051739/android-setting-preferences-programmatically
		//var prefs=GetPreferences(Android.Content.FileCreationMode.Private);
		//prefs.All;
		//prefs.Edit

		const string preferencesKey = "Records";
		readonly ISharedPreferences _prefs;
		internal PreferencesStorage(Activity activity)
		{
			_prefs = activity.GetPreferences(FileCreationMode.Private)!;
		}

		public string? Open()
			=> _prefs.All?.TryGetValue(preferencesKey, out object? recs) == true && (recs is string recss) ? recss : null;

		public void Save(string configRecordsString)
		{
			ISharedPreferencesEditor prefsEdit = _prefs.Edit()!;
			prefsEdit.PutString(preferencesKey, configRecordsString);
			prefsEdit.Commit();
		}
	}
}
