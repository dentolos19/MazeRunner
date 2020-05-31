<<<<<<< HEAD
﻿using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
=======
﻿using UnityEngine;
>>>>>>> RunAwayHaroldOld/master-old
using UnityEngine.Advertisements;

public static class Game
{

<<<<<<< HEAD
	public static bool IsMobilePlatform { get; private set; }
	public static bool IsPlayServicesEnabled { get; private set; }
	public static Configuration Settings { get; private set; }
	
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	private static void Initialize()
	{
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		Settings = Configuration.Load();
		IsMobilePlatform = Application.isMobilePlatform;
		if (!IsMobilePlatform)
			return;
		switch (Application.platform)
		{
			case RuntimePlatform.Android:
				Advertisement.Initialize("3586538");
				break;
			case RuntimePlatform.IPhonePlayer:
				Advertisement.Initialize("3586539");
				break;
		}
		#if UNITY_ANDROID
		if (GooglePlayGames.OurUtils.PlatformUtils.Supported)
			Authenticate();
		#endif
	}

	#if UNITY_ANDROID
	private static void Authenticate()
	{
		var config = new PlayGamesClientConfiguration.Builder().Build();
		PlayGamesPlatform.InitializeInstance(config);
		PlayGamesPlatform.Activate();
		Social.localUser.Authenticate(success => { IsPlayServicesEnabled = success; });
	}
	#endif
	
=======
    public static bool IsMobilePlatform { get; private set; }
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
    }

>>>>>>> RunAwayHaroldOld/master-old
}