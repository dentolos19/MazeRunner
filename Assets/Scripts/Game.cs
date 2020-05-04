using UnityEngine;
using UnityEngine.Advertisements;

public static class Game
{

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
    }

}