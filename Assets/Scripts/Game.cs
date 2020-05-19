using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.Advertisements;

public static class Game
{

	public static bool IsMobilePlatform { get; private set; }
	public static bool IsPlayServicesEnabled { get; private set; }
	public static Configuration Settings { get; private set; }

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	private static void Startup()
	{
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		Settings = Configuration.Load();
		switch (Application.platform)
		{
			case RuntimePlatform.Android:
				Advertisement.Initialize("3586538");
				break;
			case RuntimePlatform.IPhonePlayer:
				Advertisement.Initialize("3586539");
				break;
		}
		IsMobilePlatform = Application.isMobilePlatform;
		if (GooglePlayGames.OurUtils.PlatformUtils.Supported)
			Authenticate();
	}
	
	private static void Authenticate()
	{
		var config = new PlayGamesClientConfiguration.Builder().Build();
		PlayGamesPlatform.InitializeInstance(config);
		PlayGamesPlatform.Activate();
		Social.localUser.Authenticate(success =>
		{
			if (success)
			{
				IsPlayServicesEnabled = true;
				Debug.Log("[GPGS] Play Services Enabled Successfully!");
			}
			else
			{
				IsPlayServicesEnabled = false;
				Debug.LogError("[GPGS] Play Services Authentication Failed!");
			}
		});
	}

}