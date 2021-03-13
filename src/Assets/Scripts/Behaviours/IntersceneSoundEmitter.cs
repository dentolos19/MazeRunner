using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class IntersceneSoundEmitter : MonoBehaviour
{
    
    public static IntersceneSoundEmitter Instance { get; set; }

    private AudioSource _source;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _source = GetComponent<AudioSource>();
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void UpdateVolume(float value)
    {
        if (value <= 0)
        {
            if (_source.isPlaying)
                _source.Stop();
        }
        else
        {
            if (!_source.isPlaying)
                _source.Play();
            _source.volume = value;
        }
    }

}