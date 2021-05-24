using UnityEngine;

[RequireComponent(typeof(MazeGenerator))]
public class GameMaster : MonoBehaviour
{

    public static MazeWaveSettings Settings { get; set; } = new MazeWaveSettings();
    
    private MazeGenerator _mazeGenerator;

    public GameObject lightObject;
    public GameObject pauseObject;

    private void Awake()
    {
        _mazeGenerator = GetComponent<MazeGenerator>();
    }
    
    private void Start()
    {
        _mazeGenerator.GenerateMaze(Settings.MazeSize);
        _mazeGenerator.SetMazeWave(Settings.EnemyAmount, Settings.EnemyDistance, Settings.EnableBoss, Settings.BossDistance);
        Debug.Log($"Generated maze with present difficulty of: {Settings.Difficulty}");
        lightObject.SetActive(false);
        #if UNITY_IOS || UNITY_ANDROID
        pauseObject.SetActive(true);
        #endif
    }

}