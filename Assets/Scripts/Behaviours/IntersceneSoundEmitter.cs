using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class IntersceneSoundEmitter : MonoBehaviour
{
    
    private static IntersceneSoundEmitter Instance { get; set; }

    [HideInInspector] public AudioSource source;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        source = GetComponent<AudioSource>();
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

}