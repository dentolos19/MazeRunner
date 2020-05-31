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
	
}