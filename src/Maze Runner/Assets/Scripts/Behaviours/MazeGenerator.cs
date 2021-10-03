using UnityEngine;
using UnityEngine.AI;

public class MazeGenerator : MonoBehaviour
{

    private NavMeshSurface _navMeshSurface;

    public Material floorMaterial;
    public Material wallMaterial;

    public Transform playerObject;
    public Transform goalObject;

    private void Start()
    {
        _navMeshSurface = GetComponent<NavMeshSurface>();
        GenerateMaze(15, 15);
    }

    private void GenerateMaze(int rows, int columns)
    {
        rows -= 2;
        if (rows % 2 == 0 && columns % 2 == 0)
            Debug.LogWarning("Odd numbers works better for the maze.");
        var maze = new GameObject { name = "Maze" };
        var mazeData = MazeEngine.GenerateData(rows, columns);
        var meshFilter = maze.AddComponent<MeshFilter>();
        meshFilter.mesh = MazeEngine.GenerateMesh(mazeData, 3, 3);
        var meshCollider = maze.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = meshFilter.mesh;
        var meshRenderer = maze.AddComponent<MeshRenderer>();
        meshRenderer.materials = new[] { floorMaterial, wallMaterial };
        _navMeshSurface.BuildNavMesh();
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
                return new Vector3(columnIndex * 3, 1, rowIndex * 3);
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
                return new Vector3(columnIndex * 3, 0.5f, rowIndex * 3);
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
                return new Vector3(columnIndex * 3, 1, rowIndex * 3);
        return new Vector3();
    }

}