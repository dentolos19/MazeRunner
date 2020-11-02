// Credits to Joseph Hocking
// https://www.raywenderlich.com/82-procedural-generation-of-mazes-with-unity

using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshSurface))]
public class MazeGenerator : MonoBehaviour
{

	private NavMeshSurface _surface;
	private int[,] _data = { { 1, 1, 1 }, { 1, 0, 1 }, { 1, 1, 1 } };
	private readonly MazeDataGenerator _dataGenerator = new MazeDataGenerator();
	private readonly MazeMeshGenerator _meshGenerator = new MazeMeshGenerator();

	[Header("Maze Materials")]
	public Material floorMaterial;
	public Material wallMaterial;

	[Header("Maze Size")]
	public float floorWidth = 3;
	public float wallHeight = 3;

	[Header("Maze Protagonist")]
	public Transform playerObject;
	public Transform goalObject;

	[Header("Maze Antagonist")]
	public GameObject enemyPrefab;
	public GameObject bossPrefab;

	private void Awake()
	{
		_surface = GetComponent<NavMeshSurface>();
	}

	private void GenerateMaze(int rows, int columns)
	{
		if (rows % 2 == 0 && columns % 2 == 0)
			Debug.LogWarning("Odd numbers works better for the maze.");
		_data = _dataGenerator.GenerateData(rows, columns);
		GenerateMesh(_data);
		if (playerObject != null)
			SetPlayerPosition();
		if (goalObject != null)
			SetGoalPosition();
	}

	private void GenerateMesh(int[,] data)
	{
		var maze = new GameObject();
		maze.transform.position = Vector3.zero;
		maze.name = "Maze";
		var meshFilter = maze.AddComponent<MeshFilter>();
		meshFilter.mesh = _meshGenerator.GenerateMesh(data, floorWidth, wallHeight);
		var meshColldier = maze.AddComponent<MeshCollider>();
		meshColldier.sharedMesh = meshFilter.mesh;
		var meshRenderer = maze.AddComponent<MeshRenderer>();
		meshRenderer.materials = new[] { floorMaterial, wallMaterial };
		_surface.BuildNavMesh();
	}

	private void SetPlayerPosition()
	{
		var rowMax = _data.GetUpperBound(0);
		var columnMax = _data.GetUpperBound(1);
		for (var ri = 0; ri <= rowMax; ri++)
		{
			for (var ci = 0; ci <= columnMax; ci++)
			{
				if (_data[ri, ci] == 0)
				{
					playerObject.position = new Vector3(ci * floorWidth, 1, ri * floorWidth);
					return;
				}
			}
		}
	}

	private void SetGoalPosition()
	{
		var rowMax = _data.GetUpperBound(0);
		var columnMax = _data.GetUpperBound(1);
		for (var ri = rowMax; ri >= 0; ri--)
			for (var ci = columnMax; ci >= 0; ci--)
				if (_data[ri, ci] == 0)
				{
					goalObject.position = new Vector3(ci * floorWidth, 0.5f, ri * floorWidth);
					return;
				}
	}

	private Vector3 GenerateRandomPosition(int minimum = 0)
	{
		var rowMax = _data.GetUpperBound(0);
		var columnMax = _data.GetUpperBound(1);
		var rowRandom = Random.Range(minimum, rowMax);
		var columnRandom = Random.Range(minimum, columnMax);
		for (var ri = rowRandom; ri >= 0; ri--)
			for (var ci = columnRandom; ci >= 0; ci--)
				if (_data[ri, ci] == 0)
					return new Vector3(ci * floorWidth, 1, ri * floorWidth);
		return new Vector3();
	}
	
	public void GenerateMaze(int size)
	{
		GenerateMaze(size - 2, size);
	}
	
	public void SetMazeWave(int enemyAmount, int enemyDistance, bool enableBoss, int bossDistance)
	{
		if (enemyDistance > bossDistance)
			Debug.LogWarning("Boss distance should be greater than enemy distance.");
		if (enemyPrefab != null)
			for (var index = 0; index < enemyAmount; index++)
				Instantiate(enemyPrefab, GenerateRandomPosition(10), Quaternion.identity);
		if (bossPrefab != null && enableBoss)
			Instantiate(bossPrefab, GenerateRandomPosition(5), Quaternion.identity);
	}

}