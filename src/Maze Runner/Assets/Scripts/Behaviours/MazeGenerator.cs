using UnityEngine;
using UnityEngine.AI;

public class MazeGenerator : MonoBehaviour
{

    private NavMeshSurface _navMeshSurface;

    [Header("Script Prerequisites")]

    public Material floorMaterial;
    public Material wallMaterial;

    [Header("Script Settings")]

    public int mazeWidth = 3;
    public int mazeHeight = 3;

    [Header("")]

    public Transform playerObject;
    public Transform goalObject;

    private void Start()
    {
        _navMeshSurface = GetComponent<NavMeshSurface>();
        GenerateMaze(15, 15);
        playerObject.gameObject.GetComponent<CharacterController>().enabled = true; // re-enables controller; in order to not interfere with player position settings
    }

    private void GenerateMaze(int rows, int columns)
    {
        rows -= 2;
        if (rows % 2 == 0 && columns % 2 == 0)
            Debug.LogWarning("Odd numbers works better for the maze.");
        var maze = new GameObject { name = "Maze" };
        var mazeData = MazeEngine.GenerateData(rows, columns);
        var meshFilter = maze.AddComponent<MeshFilter>();
        meshFilter.mesh = MazeEngine.GenerateMesh(mazeData, mazeWidth, mazeHeight);
        var meshCollider = maze.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = meshFilter.mesh;
        var meshRenderer = maze.AddComponent<MeshRenderer>();
        meshRenderer.materials = new[] { floorMaterial, wallMaterial };
        _navMeshSurface.BuildNavMesh(); // builds navigation mesh for bots
        playerObject.position = GetStartingPosition(mazeData);
        goalObject.position = GetFinishingPosition(mazeData);
    }

    private Vector3 GetStartingPosition(int[,] data)
    {
        var rowMax = data.GetUpperBound(0);
        var columnMax = data.GetUpperBound(1);
        for (var rowIndex = 0; rowIndex <= rowMax; rowIndex++)
        {
            for (var columnIndex = 0; columnIndex <= columnMax; columnIndex++)
            {
                if (data[rowIndex, columnIndex] != 0)
                    continue;
                return new Vector3(columnIndex * mazeWidth, 1, rowIndex * mazeHeight);
            }
        }
        return new Vector3();
    }

    private Vector3 GetFinishingPosition(int[,] data)
    {
        var rowMax = data.GetUpperBound(0);
        var columnMax = data.GetUpperBound(1);
        for (var rowIndex = rowMax; rowIndex >= 0; rowIndex--)
        {
            for (var columnIndex = columnMax; columnIndex >= 0; columnIndex--)
            {
                if (data[rowIndex, columnIndex] != 0)
                    continue;
                return new Vector3(columnIndex * mazeWidth, 0.5f, rowIndex * mazeHeight);
            }
        }
        return new Vector3();
    }

    private Vector3 GetRandomPosition(int[,] data, int minimumRange)
    {
        var rowMax = data.GetUpperBound(0);
        var columnMax = data.GetUpperBound(1);
        var rowRandom = Random.Range(minimumRange, rowMax);
        var columnRandom = Random.Range(minimumRange, columnMax);
        for (var rowIndex = rowRandom; rowIndex >= 0; rowIndex--)
        for (var columnIndex = columnRandom; columnIndex >= 0; columnIndex--)
            if (data[rowIndex, columnIndex] == 0)
                return new Vector3(columnIndex * mazeWidth, 1, rowIndex * mazeHeight);
        return new Vector3();
    }

}