// Credits to Joseph Hocking (From raywenderlich.com)
// https://www.raywenderlich.com/82-procedural-generation-of-mazes-with-unity

using UnityEngine;

public class MazeGenerator : MonoBehaviour
{

    private int[,] _data;
    private MazeDataGenerator _dataGenerator;
    private MazeMeshGenerator _meshGenerator;

    public static WaveSettings Settings { get; } = new WaveSettings();

    public Material floorMaterial;
    public Material wallMaterial;

    public Transform player;
    public Transform goal;
    public EnemyAi enemyPrefab;
    public EnemyAi bossPrefab;

    private void Awake()
    {
        _data = new[,] { { 1, 1, 1 }, { 1, 0, 1 }, { 1, 1, 1 } };
        _dataGenerator = new MazeDataGenerator();
        _meshGenerator = new MazeMeshGenerator();
    }

    private void Start()
    {
        Generate(63);
    }

    private void GenerateMesh(int[,] data)
    {
        var maze = new GameObject();
        maze.transform.position = Vector3.zero;
        maze.name = "Generated Maze";
        maze.tag = "Generated";
        var meshFilter = maze.AddComponent<MeshFilter>();
        meshFilter.mesh = _meshGenerator.FromData(data);
        var meshColldier = maze.AddComponent<MeshCollider>();
        meshColldier.sharedMesh = meshFilter.mesh;
        var meshRenderer = maze.AddComponent<MeshRenderer>();
        meshRenderer.materials = new[] { floorMaterial, wallMaterial };
    }

    private void Generate(int value)
    {
        Generate(value - 2, value);
    }

    private void Generate(int rows, int column)
    {
        if (rows % 2 == 0 && column % 2 == 0)
            Debug.LogWarning("Odd numbers works better for the maze.");
        _data = _dataGenerator.FromDimensions(rows, column);
        GenerateMesh(_data);
        SetPlayerPosition(_data);
        SetGoalPosition(_data);
    }

    private void SetPlayerPosition(int[,] data)
    {
        var rowMax = data.GetUpperBound(0);
        var columnMax = data.GetUpperBound(1);
        for (var ri = 0; ri <= rowMax; ri++)
            for (var ci = 0; ci <= columnMax; ci++)
                if (data[ri, ci] == 0)
                {
                    player.position = new Vector3(ci * MazeMeshGenerator.Width, 1, ri * MazeMeshGenerator.Width);
                    return;
                }
    }

    private void SetGoalPosition(int[,] data)
    {
        var rowMax = data.GetUpperBound(0);
        var columnMax = data.GetUpperBound(1);
        for (var ri = rowMax; ri >= 0; ri--)
            for (var ci = columnMax; ci >= 0; ci--)
                if (data[ri, ci] == 0)
                {
                    var pos = new Vector3(ci * MazeMeshGenerator.Width, 0.5f, ri * MazeMeshGenerator.Width);
                    goal.position = pos;
                    enemyPrefab.target = player;
                    for (var index = 0; index < Settings.enemyAmount; index++)
                        Instantiate(enemyPrefab.gameObject, pos + Vector3.up * 0.5f, Quaternion.identity);
                    if (Settings.enableBoss)
                        Instantiate(bossPrefab.gameObject, pos + Vector3.up * 0.5f, Quaternion.identity);
                    return;
                }
    }

}