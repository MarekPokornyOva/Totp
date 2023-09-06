#region using
using MsBox.Avalonia;
using System;
using System.Collections.ObjectModel;
using System.IO;
using Totp.Services;
#endregion using

namespace Totp.ViewModels;

public partial class MainViewModel : ViewModelBase
{
	public MainViewModel()
	{
		_configRecordsString = Configuration.ConfigStorage.Open();
		Deserialize();
	}

	#region view selection
	int _tabIndex;
	public int TabIndex { get => _tabIndex; }
	#endregion view selection

	ConfigurationRecord[] _configurationRecords = default!;

	void Deserialize()
	{
		_configurationRecords = string.IsNullOrEmpty(_configRecordsString) ? Array.Empty<ConfigurationRecord>() : ConfigSerializer.Deserialize(_configRecordsString);
		//MessageBoxManager.GetMessageBoxStandard("Totp", "Problem reading configuration.", MsBox.Avalonia.Enums.ButtonEnum.Ok)
		//	.ShowAsync().GetAwaiter().GetResult();
		Reload();
	}

	#region CodesViewModel
	public ObservableCollection<Info> ConfigRecords { get; private set; } = new ObservableCollection<Info>();
	public bool HasConfigRecords { get => ConfigRecords.Count != 0; }

	public void Reload()
	{
		ConfigRecords.Clear();
		foreach (ConfigurationRecord item in _configurationRecords)
			ConfigRecords.Add(Read(item));
		OnPropertyChanged(nameof(HasConfigRecords));
	}

	static Info Read(ConfigurationRecord configurationRecord)
	{
		OtpNet.Totp totp = new OtpNet.Totp(OtpNet.Base32Encoding.ToBytes(configurationRecord.Key));
		return new Info(configurationRecord.Title, totp.ComputeTotp(), totp.RemainingSeconds());
	}

	public void ConfigCommand()
	{
		ConfigViewModelInit();
		SetProperty(ref _tabIndex, 1, nameof(TabIndex));
	}

	public void ReloadCommand()
	{
		Reload();
	}

	public record Info(string Title, string Code, int RemainingSeconds);
	#endregion CodesViewModel

	#region ConfigViewModel
	string _configRecordsStringEdit = default!;
	public string ConfigRecordsStringEdit { get => _configRecordsStringEdit; set { _configRecordsStringEdit = value; } }
	string? _configRecordsString;
	public void ConfigViewModelInit()
	{
		SetProperty(ref _configRecordsStringEdit, string.IsNullOrEmpty(_configRecordsString) ? "[{\"Title\":\"Example 1\",\"Key\":\"GAYTEMZU\"},{\"Title\":\"Example 2\",\"Key\":\"GU3DOOBZ\"}]" : _configRecordsString, nameof(ConfigRecordsStringEdit));
	}

	public void SaveCommand()
	{
		_configRecordsString = _configRecordsStringEdit;
		Deserialize();
		Configuration.ConfigStorage.Save(_configRecordsString);
		SetProperty(ref _tabIndex, 0, nameof(TabIndex));
	}

	public void CancelCommand()
	{
		Reload();
		SetProperty(ref _tabIndex, 0, nameof(TabIndex));
	}
	#endregion ConfigViewModel
}
