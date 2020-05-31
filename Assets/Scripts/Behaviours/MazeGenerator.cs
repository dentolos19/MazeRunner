<<<<<<< HEAD
﻿// Credits to Joseph Hocking
// https://www.raywenderlich.com/82-procedural-generation-of-mazes-with-unity

using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshSurface))]
public class MazeGenerator : MonoBehaviour
{

	private int[,] _data = { { 1, 1, 1 }, { 1, 0, 1 }, { 1, 1, 1 } };
	private readonly MazeDataGenerator _dataGenerator = new MazeDataGenerator();
	private readonly MazeMeshGenerator _meshGenerator = new MazeMeshGenerator();

	private NavMeshSurface _surface;
	
	public static MazeWaveSettings Settings { get; set; } = new MazeWaveSettings();

	public Material mazeFloorMaterial;
	public Material mazeWallMaterial;

	public float mazeFloorWidth = 3;
	public float mazeWallHeight = 3;

	public Transform player;
	public Transform goal;

	public GameObject enemyPrefab;
	public GameObject bossPrefab;

	private void Awake()
	{
		_surface = GetComponent<NavMeshSurface>();
	}
	
	private void Start()
	{
		Generate(Settings.Size);
	}

	private void Generate(int size)
	{
		Generate(size - 2, size);
	}

	private void Generate(int rows, int columns)
	{
		if (rows % 2 == 0 && columns % 2 == 0)
			Debug.LogWarning("Odd numbers works better for the maze.");
		_data = _dataGenerator.Generate(rows, columns);
		GenerateMesh(_data);
		SetMazeWave(_data, Settings);
	}

	private void GenerateMesh(int[,] data)
	{
		var maze = new GameObject();
		maze.transform.position = Vector3.zero;
		maze.name = "Maze";
		var meshFilter = maze.AddComponent<MeshFilter>();
		meshFilter.mesh = _meshGenerator.Generate(data, mazeFloorWidth, mazeWallHeight);
		var meshColldier = maze.AddComponent<MeshCollider>();
		meshColldier.sharedMesh = meshFilter.mesh;
		var meshRenderer = maze.AddComponent<MeshRenderer>();
		meshRenderer.materials = new[] { mazeFloorMaterial, mazeWallMaterial };
		BakeMazeMesh();
	}

	private void SetMazeWave(int[,] data, MazeWaveSettings settings)
	{
		SetPlayerPosition(data);
		SetGoalPosition(data);
		for (var index = 0; index < settings.EnemyAmount; index++)
			Instantiate(enemyPrefab, GenerateRandomPosition(data, 5), Quaternion.identity);
		if (settings.EnableBoss)
			Instantiate(bossPrefab, GenerateRandomPosition(data, 10), Quaternion.identity);
	}

	private void SetPlayerPosition(int[,] data)
	{
		var rowMax = data.GetUpperBound(0);
		var columnMax = data.GetUpperBound(1);
		for (var ri = 0; ri <= rowMax; ri++)
			for (var ci = 0; ci <= columnMax; ci++)
				if (data[ri, ci] == 0)
				{
					player.position = new Vector3(ci * mazeFloorWidth, 1, ri * mazeFloorWidth);
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
					goal.position = new Vector3(ci * mazeFloorWidth, 0.5f, ri * mazeFloorWidth);
					return;
				}
	}

	private Vector3 GenerateRandomPosition(int[,] data, int minimum = 0)
	{
		var rowMax = data.GetUpperBound(0);
		var columnMax = data.GetUpperBound(1);
		var rowRad = Random.Range(minimum, rowMax);
		var columnRad = Random.Range(minimum, columnMax);
		for (var ri = rowRad; ri >= 0; ri--)
			for (var ci = columnRad; ci >= 0; ci--)
				if (data[ri, ci] == 0)
					return new Vector3(ci * mazeFloorWidth, 1, ri * mazeFloorWidth);
		return new Vector3();
	}

	private void BakeMazeMesh()
	{
		_surface.BuildNavMesh();
	}
	
=======
﻿// Credits to Joseph Hocking (From raywenderlich.com)
// https://www.raywenderlich.com/82-procedural-generation-of-mazes-with-unity

using UnityEngine;

public class MazeGenerator : MonoBehaviour
{

    private int[,] _data;
    private MazeDataGenerator _dataGenerator;
    private MazeMeshGenerator _meshGenerator;

    public static WaveSettings Settings { get; set; } = new WaveSettings();

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
        Generate(Settings.MazeSize);
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
        SetMazeWave(_data);
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
                    goal.position = new Vector3(ci * MazeMeshGenerator.Width, 0.5f, ri * MazeMeshGenerator.Width);
                    return;
                }
    }

    private void SetMazeWave(int[,] data)
    {
        enemyPrefab.target = player;
        bossPrefab.target = player;
        if (Settings.EnableBoss)
            Instantiate(bossPrefab, GenerateRandomCords(data) + Vector3.up * 0.5f, Quaternion.identity);
        for (var index = 0; index < Settings.EnemyAmount; index++)
            Instantiate(enemyPrefab, GenerateRandomCords(data) + Vector3.up * 0.5f, Quaternion.identity);
    }
    
    private Vector3 GenerateRandomCords(int[,] data, int minimum = 0)
    {
        var rowMax = data.GetUpperBound(0);
        var columnMax = data.GetUpperBound(1);
        var rowRad = Random.Range(minimum, rowMax);
        var columnRad = Random.Range(minimum, columnMax);
        for (var ri = rowRad; ri >= 0; ri--)
            for (var ci = columnRad; ci >= 0; ci--)
                if (data[ri, ci] == 0)
                    return new Vector3(ci * MazeMeshGenerator.Width, 0.5f, ri * MazeMeshGenerator.Width);
        return new Vector3();
    }

>>>>>>> RunAwayHaroldOld/master-old
}