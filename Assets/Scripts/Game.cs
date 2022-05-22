using UnityEngine;

public static class Game
{

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void OnStartup()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

}