using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(MazeGenerator))]
public class GameMaster : MonoBehaviour
{

    public static MazeWaveSettings Settings { get; set; } = new MazeWaveSettings();
    
    private MazeGenerator _mazeGenerator;

    public GameObject mobileInterface;

    private void Awake()
    {
        _mazeGenerator = GetComponent<MazeGenerator>();
        mobileInterface.SetActive(Game.RunningOnMobile);
    }
    
    private void Start()
    {
        _mazeGenerator.GenerateMaze(Settings.MazeSize);
        _mazeGenerator.SetMazeWave(Settings.EnemyAmount, Settings.EnemyDistance, Settings.EnableBoss, Settings.BossDistance);
    }

}