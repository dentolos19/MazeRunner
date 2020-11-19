using UnityEngine;

public static class Game
{

    public static Configuration Settings { get; private set; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void InitializeGame()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Settings = Configuration.Load();
        #if UNITY_ANDROID
        UnityEngine.Advertisements.Advertisement.Initialize("3586538");
        #endif
        #if UNITY_IOS
        UnityEngine.Advertisements.Advertisement.Initialize("3586539");
        #endif
    }

}