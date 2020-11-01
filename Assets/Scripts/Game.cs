using UnityEngine;

public static class Game
{

    public static Configuration Settings { get; private set; }
    public static bool RunningOnMobile { get; private set; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void InitializeGame()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Settings = Configuration.Load();
        #if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
        RunningOnMobile = true;
        #endif
    }

}