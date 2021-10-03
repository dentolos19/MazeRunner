using System.Collections.Generic;
using UnityEngine;

public static class MazeEngine
{

    private const float Threshold = 0.1f;

    private static void CreateMesh(Matrix4x4 matrix, ref List<Vector3> vertices, ref List<Vector2> uvs, ref List<int> triangles)
    {
        var index = vertices.Count;
        var vertice1 = new Vector3(-0.5f, -0.5f, 0);
        var vertice2 = new Vector3(-0.5f, 0.5f, 0);
        var vertice3 = new Vector3(0.5f, 0.5f, 0);
        var vertice4 = new Vector3(0.5f, -0.5f, 0);
        vertices.Add(matrix.MultiplyPoint3x4(vertice1));
        vertices.Add(matrix.MultiplyPoint3x4(vertice2));
        vertices.Add(matrix.MultiplyPoint3x4(vertice3));
        vertices.Add(matrix.MultiplyPoint3x4(vertice4));
        uvs.Add(new Vector2(1, 0));
        uvs.Add(new Vector2(1, 1));
        uvs.Add(new Vector2(0, 1));
        uvs.Add(new Vector2(0, 0));
        triangles.Add(index + 2);
        triangles.Add(index + 1);
        triangles.Add(index);
        triangles.Add(index + 3);
        triangles.Add(index + 2);
        triangles.Add(index);
    }

    public static int[,] GenerateData(int rows, int columns)
    {
        var data = new int[rows, columns];
        var rowMax = data.GetUpperBound(0);
        var columnMax = data.GetUpperBound(1);
        for (var rowIndex = rowMax; rowIndex >= 0; rowIndex--)
        {
            for (var columnIndex = 0; columnIndex <= columnMax; columnIndex++)
            {
                if (rowIndex == 0 || columnIndex == 0 || rowIndex == rowMax || columnIndex == columnMax)
                {
                    data[rowIndex, columnIndex] = 1;
                }
                else if (rowIndex % 2 == 0 && columnIndex % 2 == 0)
                {
                    if (!(Random.value > Threshold))
                        continue;
                    data[rowIndex, columnIndex] = 1;
                    var a = Random.value < 0.5 ? 0 : Random.value < 0.5 ? -1 : 1;
                    var b = a != 0 ? 0 : Random.value < 0.5 ? -1 : 1;
                    data[rowIndex + a, columnIndex + b] = 1;
                }
            }
        }
        return data;
    }

    public static Mesh GenerateMesh(int[,] data, float width, float height)
	{
		var maze = new Mesh();
		var mazeVertices = new List<Vector3>();
		var mazeUVs = new List<Vector2>();
		maze.subMeshCount = 2;
		var floorTriangles = new List<int>();
		var wallTriangles = new List<int>();
		var rowMax = data.GetUpperBound(0);
		var columnMax = data.GetUpperBound(1);
		var halfHeight = height * 0.5f;
		for (var rowIndex = 0; rowIndex <= rowMax; rowIndex++)
			for (var columnIndex = 0; columnIndex <= columnMax; columnIndex++)
				if (data[rowIndex, columnIndex] != 1)
				{
					CreateMesh(Matrix4x4.TRS(new Vector3(columnIndex * width, 0, rowIndex * width), Quaternion.LookRotation(Vector3.up), new Vector3(width, width, 1)), ref mazeVertices, ref mazeUVs, ref floorTriangles);
					CreateMesh(Matrix4x4.TRS(new Vector3(columnIndex * width, height, rowIndex * width), Quaternion.LookRotation(Vector3.down), new Vector3(width, width, 1)), ref mazeVertices, ref mazeUVs, ref floorTriangles);
					if (rowIndex - 1 < 0 || data[rowIndex - 1, columnIndex] == 1)
						CreateMesh(Matrix4x4.TRS(new Vector3(columnIndex * width, halfHeight, (rowIndex - 0.5f) * width), Quaternion.LookRotation(Vector3.forward), new Vector3(width, height, 1)), ref mazeVertices, ref mazeUVs, ref wallTriangles);
					if (columnIndex + 1 > columnMax || data[rowIndex, columnIndex + 1] == 1)
						CreateMesh(Matrix4x4.TRS(new Vector3((columnIndex + .5f) * width, halfHeight, rowIndex * width), Quaternion.LookRotation(Vector3.left), new Vector3(width, height, 1)), ref mazeVertices, ref mazeUVs, ref wallTriangles);
					if (columnIndex - 1 < 0 || data[rowIndex, columnIndex - 1] == 1)
						CreateMesh(Matrix4x4.TRS(new Vector3((columnIndex - .5f) * width, halfHeight, rowIndex * width), Quaternion.LookRotation(Vector3.right), new Vector3(width, height, 1)), ref mazeVertices, ref mazeUVs, ref wallTriangles);
					if (rowIndex + 1 > rowMax || data[rowIndex + 1, columnIndex] == 1)
						CreateMesh(Matrix4x4.TRS(new Vector3(columnIndex * width, halfHeight, (rowIndex + .5f) * width), Quaternion.LookRotation(Vector3.back), new Vector3(width, height, 1)), ref mazeVertices, ref mazeUVs, ref wallTriangles);
				}
		maze.vertices = mazeVertices.ToArray();
		maze.uv = mazeUVs.ToArray();
		maze.SetTriangles(floorTriangles.ToArray(), 0);
		maze.SetTriangles(wallTriangles.ToArray(), 1);
		maze.RecalculateNormals();
		return maze;
	}

}