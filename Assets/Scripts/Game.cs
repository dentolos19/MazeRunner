using UnityEngine;
using UnityEngine.Advertisements;

public static class Game
{

    public static Configuration Settings { get; private set; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void InitializeGame()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Settings = Configuration.Load();
        #if UNITY_ANDROID
        Advertisement.Initialize("3586538");
        #elif UNITY_IOS
        Advertisement.Initialize("3586539");
        #endif
    }

}