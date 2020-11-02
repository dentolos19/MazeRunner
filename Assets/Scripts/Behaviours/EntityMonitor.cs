using UnityEngine;

public class EntityMonitor : MonoBehaviour
{
    
    public static EntityMonitor Instance { get; private set; }

    public Transform player;
    
    private void Start()
    {
        Instance = this;
    }

}