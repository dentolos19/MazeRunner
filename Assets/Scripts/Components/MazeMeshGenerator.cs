<<<<<<< HEAD
﻿// Credits to Joseph Hocking
=======
﻿// Credits to Joseph Hocking (From raywenderlich.com)
>>>>>>> RunAwayHaroldOld/master-old
// https://www.raywenderlich.com/82-procedural-generation-of-mazes-with-unity

using System.Collections.Generic;
using UnityEngine;

public class MazeMeshGenerator
{

<<<<<<< HEAD
	public Mesh Generate(int[,] data, float width, float height)
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
		for (var ri = 0; ri <= rowMax; ri++)
			for (var ci = 0; ci <= columnMax; ci++)
				if (data[ri, ci] != 1)
				{
					Create(Matrix4x4.TRS(new Vector3(ci * width, 0, ri * width), Quaternion.LookRotation(Vector3.up), new Vector3(width, width, 1)), ref mazeVertices, ref mazeUVs, ref floorTriangles);
					Create(Matrix4x4.TRS(new Vector3(ci * width, height, ri * width), Quaternion.LookRotation(Vector3.down), new Vector3(width, width, 1)), ref mazeVertices, ref mazeUVs, ref floorTriangles);
					if (ri - 1 < 0 || data[ri - 1, ci] == 1)
						Create(Matrix4x4.TRS(new Vector3(ci * width, halfHeight, (ri - 0.5f) * width), Quaternion.LookRotation(Vector3.forward), new Vector3(width, height, 1)), ref mazeVertices, ref mazeUVs, ref wallTriangles);
					if (ci + 1 > columnMax || data[ri, ci + 1] == 1)
						Create(Matrix4x4.TRS(new Vector3((ci + .5f) * width, halfHeight, ri * width), Quaternion.LookRotation(Vector3.left), new Vector3(width, height, 1)), ref mazeVertices, ref mazeUVs, ref wallTriangles);
					if (ci - 1 < 0 || data[ri, ci - 1] == 1)
						Create(Matrix4x4.TRS(new Vector3((ci - .5f) * width, halfHeight, ri * width), Quaternion.LookRotation(Vector3.right), new Vector3(width, height, 1)), ref mazeVertices, ref mazeUVs, ref wallTriangles);
					if (ri + 1 > rowMax || data[ri + 1, ci] == 1)
						Create(Matrix4x4.TRS(new Vector3(ci * width, halfHeight, (ri + .5f) * width), Quaternion.LookRotation(Vector3.back), new Vector3(width, height, 1)), ref mazeVertices, ref mazeUVs, ref wallTriangles);
				}
		maze.vertices = mazeVertices.ToArray();
		maze.uv = mazeUVs.ToArray();
		maze.SetTriangles(floorTriangles.ToArray(), 0);
		maze.SetTriangles(wallTriangles.ToArray(), 1);
		maze.RecalculateNormals();
		return maze;
	}
	
	private void Create(Matrix4x4 matrix, ref List<Vector3> newVertices, ref List<Vector2> newUVs, ref List<int> newTriangles)
	{
		var index = newVertices.Count;
		var vertice1 = new Vector3(-0.5f, -0.5f, 0);
		var vertice2 = new Vector3(-0.5f, 0.5f, 0);
		var vertice3 = new Vector3(0.5f, 0.5f, 0);
		var vertice4 = new Vector3(0.5f, -0.5f, 0);
		newVertices.Add(matrix.MultiplyPoint3x4(vertice1));
		newVertices.Add(matrix.MultiplyPoint3x4(vertice2));
		newVertices.Add(matrix.MultiplyPoint3x4(vertice3));
		newVertices.Add(matrix.MultiplyPoint3x4(vertice4));
		newUVs.Add(new Vector2(1, 0));
		newUVs.Add(new Vector2(1, 1));
		newUVs.Add(new Vector2(0, 1));
		newUVs.Add(new Vector2(0, 0));
		newTriangles.Add(index + 2);
		newTriangles.Add(index + 1);
		newTriangles.Add(index);
		newTriangles.Add(index + 3);
		newTriangles.Add(index + 2);
		newTriangles.Add(index);
	}
	
=======
    public const float Width = 3;
    public const float Height = 3;

    public Mesh FromData(int[,] data)
    {
        var maze = new Mesh();
        var mazeVertices = new List<Vector3>();
        var mazeUVs = new List<Vector2>();
        maze.subMeshCount = 2;
        var floorTriangles = new List<int>();
        var wallTriangles = new List<int>();
        var rowMax = data.GetUpperBound(0);
        var columnMax = data.GetUpperBound(1);
        var halfHeight = Height * 0.5f;
        for (var ri = 0; ri <= rowMax; ri++)
            for (var ci = 0; ci <= columnMax; ci++)
                if (data[ri, ci] != 1)
                {
                    CreateQuad(Matrix4x4.TRS(new Vector3(ci * Width, 0, ri * Width), Quaternion.LookRotation(Vector3.up), new Vector3(Width, Width, 1)), ref mazeVertices, ref mazeUVs, ref floorTriangles);
                    CreateQuad(Matrix4x4.TRS(new Vector3(ci * Width, Height, ri * Width), Quaternion.LookRotation(Vector3.down), new Vector3(Width, Width, 1)), ref mazeVertices, ref mazeUVs, ref floorTriangles);
                    if (ri - 1 < 0 || data[ri - 1, ci] == 1)
                        CreateQuad(Matrix4x4.TRS(new Vector3(ci * Width, halfHeight, (ri - 0.5f) * Width), Quaternion.LookRotation(Vector3.forward), new Vector3(Width, Height, 1)), ref mazeVertices, ref mazeUVs, ref wallTriangles);
                    if (ci + 1 > columnMax || data[ri, ci + 1] == 1)
                        CreateQuad(Matrix4x4.TRS(new Vector3((ci + .5f) * Width, halfHeight, ri * Width), Quaternion.LookRotation(Vector3.left), new Vector3(Width, Height, 1)), ref mazeVertices, ref mazeUVs, ref wallTriangles);
                    if (ci - 1 < 0 || data[ri, ci - 1] == 1)
                        CreateQuad(Matrix4x4.TRS(new Vector3((ci - .5f) * Width, halfHeight, ri * Width), Quaternion.LookRotation(Vector3.right), new Vector3(Width, Height, 1)), ref mazeVertices, ref mazeUVs, ref wallTriangles);
                    if (ri + 1 > rowMax || data[ri + 1, ci] == 1)
                        CreateQuad(Matrix4x4.TRS(new Vector3(ci * Width, halfHeight, (ri + .5f) * Width), Quaternion.LookRotation(Vector3.back), new Vector3(Width, Height, 1)), ref mazeVertices, ref mazeUVs, ref wallTriangles);
                }
        maze.vertices = mazeVertices.ToArray();
        maze.uv = mazeUVs.ToArray();
        maze.SetTriangles(floorTriangles.ToArray(), 0);
        maze.SetTriangles(wallTriangles.ToArray(), 1);
        maze.RecalculateNormals();
        return maze;
    }
    
    private void CreateQuad(Matrix4x4 matrix, ref List<Vector3> newVertices, ref List<Vector2> newUVs, ref List<int> newTriangles)
    {
        var index = newVertices.Count;
        var vertice1 = new Vector3(-0.5f, -0.5f, 0);
        var vertice2 = new Vector3(-0.5f, 0.5f, 0);
        var vertice3 = new Vector3(0.5f, 0.5f, 0);
        var vertice4 = new Vector3(0.5f, -0.5f, 0);
        newVertices.Add(matrix.MultiplyPoint3x4(vertice1));
        newVertices.Add(matrix.MultiplyPoint3x4(vertice2));
        newVertices.Add(matrix.MultiplyPoint3x4(vertice3));
        newVertices.Add(matrix.MultiplyPoint3x4(vertice4));
        newUVs.Add(new Vector2(1, 0));
        newUVs.Add(new Vector2(1, 1));
        newUVs.Add(new Vector2(0, 1));
        newUVs.Add(new Vector2(0, 0));
        newTriangles.Add(index + 2);
        newTriangles.Add(index + 1);
        newTriangles.Add(index);
        newTriangles.Add(index + 3);
        newTriangles.Add(index + 2);
        newTriangles.Add(index);
    }

>>>>>>> RunAwayHaroldOld/master-old
}