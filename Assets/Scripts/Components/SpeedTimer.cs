using System;
using System.Diagnostics;
using UnityEngine;

public class SpeedTimer : MonoBehaviour
{

    private Stopwatch _watch;

    public TimeSpan Elasped { get; private set; }

    private void Update()
    {
        if (!_watch.IsRunning)
            return;
        Elasped = _watch.Elapsed;
    }

    public void Start()
    {
        _watch = new Stopwatch();
        _watch.Start();
    }

    public void Stop()
    {
        if (_watch.IsRunning)
            _watch.Stop();
    }

}
