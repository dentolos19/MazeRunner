using UnityEngine;

public static class Game
{

    public static float InitialTimeScale { get; private set; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void OnStartup()
    {
        InitialTimeScale = Time.timeScale;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

}