using UnityEngine;

public static class Game
{

	public static bool IsMobilePlatform { get; private set; }
	public static Configuration Settings { get; private set; }
	
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	private static void Initialize()
	{
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		Settings = Configuration.Load();
		IsMobilePlatform = Application.isMobilePlatform;
	}
	
}