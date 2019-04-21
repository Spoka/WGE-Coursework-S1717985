﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]

public class VoxelGenerator : MonoBehaviour {

    Mesh mesh;
    MeshCollider meshCollider;
    List<Vector3> vertexList;
    List<int> triIndexList;
    List<Vector2> UVList;
    Dictionary<string, Vector2> texNameCoordDictionary;

    int numQuads = 0;

    public List<string> texNames;
    public List<Vector2> texCoords;
    public float texSize;
	
    public void Initialise()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        meshCollider = GetComponent<MeshCollider>();
        vertexList = new List<Vector3>();
        triIndexList = new List<int>();
        UVList = new List<Vector2>();
        CreateTextureNameCoordDictionary();
    }

    public void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertexList.ToArray();
        mesh.triangles = triIndexList.ToArray();
        mesh.uv = UVList.ToArray();
        mesh.RecalculateNormals();
        meshCollider.sharedMesh = null;
        meshCollider.sharedMesh = mesh;
        ClearPreviousData();
    }

    void ClearPreviousData()
    {
        vertexList.Clear();
        triIndexList.Clear();
        UVList.Clear();
        numQuads = 0;
    }

    void CreateTextureNameCoordDictionary()
    {
        texNameCoordDictionary = new Dictionary<string, Vector2>();

        if (texNames.Count == texCoords.Count)
        {
            for (int i = 0; i < texNames.Count; i++)
            {
                texNameCoordDictionary.Add(texNames[i], texCoords[i]);
            }
        }
        else
        {
            Debug.Log("texNames and texCoords count mismatch");
        }
    }

    public void CreateVoxel(int x, int y, int z, Vector2 uvCoords)
    {
        CreateNegativeXFace(x, y, z, uvCoords);
        CreatePositiveXFace(x, y, z, uvCoords);

        CreateNegativeYFace(x, y, z, uvCoords);
        CreatePositiveYFace(x, y, z, uvCoords);

        CreateNegativeZFace(x, y, z, uvCoords);
        CreatePositiveZFace(x, y, z, uvCoords);
    }

    public void CreateVoxel(int x, int y, int z, string texture)
    {
        Vector2 uvCoords = texNameCoordDictionary[texture];

        CreateNegativeXFace(x, y, z, uvCoords);
        CreatePositiveXFace(x, y, z, uvCoords);

        CreateNegativeYFace(x, y, z, uvCoords);
        CreatePositiveYFace(x, y, z, uvCoords);

        CreateNegativeZFace(x, y, z, uvCoords);
        CreatePositiveZFace(x, y, z, uvCoords);
    }

    public void CreateNegativeZFace (int x, int y, int z, Vector2 uvCoords)
    {
        vertexList.Add(new Vector3(x, y + 1, z));
        vertexList.Add(new Vector3(x + 1, y + 1, z));
        vertexList.Add(new Vector3(x + 1, y, z));
        vertexList.Add(new Vector3(x, y, z));
        AddTriangleIndices();
        AddUVCoords(uvCoords);
    }

    public void CreateNegativeZFace(int x, int y, int z, string texture)
    {
        Vector2 uvCoords = texNameCoordDictionary[texture];

        vertexList.Add(new Vector3(x, y + 1, z));
        vertexList.Add(new Vector3(x + 1, y + 1, z));
        vertexList.Add(new Vector3(x + 1, y, z));
        vertexList.Add(new Vector3(x, y, z));
        AddTriangleIndices();
        AddUVCoords(uvCoords);
    }

    public void CreatePositiveZFace (int x, int y, int z, Vector2 uvCoords)
    {
        vertexList.Add(new Vector3(x + 1, y, z + 1));
        vertexList.Add(new Vector3(x + 1, y + 1, z + 1));
        vertexList.Add(new Vector3(x, y + 1, z + 1));
        vertexList.Add(new Vector3(x, y, z + 1));
        AddTriangleIndices();
        AddUVCoords(uvCoords);
    }

    public void CreatePositiveZFace(int x, int y, int z, string texture)
    {
        Vector2 uvCoords = texNameCoordDictionary[texture];

        vertexList.Add(new Vector3(x + 1, y, z + 1));
        vertexList.Add(new Vector3(x + 1, y + 1, z + 1));
        vertexList.Add(new Vector3(x, y + 1, z + 1));
        vertexList.Add(new Vector3(x, y, z + 1));
        AddTriangleIndices();
        AddUVCoords(uvCoords);
    }

    public void CreateNegativeXFace (int x, int y, int z, Vector2 uvCoords)
    {
        vertexList.Add(new Vector3(x, y, z + 1));
        vertexList.Add(new Vector3(x, y + 1, z + 1));
        vertexList.Add(new Vector3(x, y + 1, z));
        vertexList.Add(new Vector3(x, y, z));
        AddTriangleIndices();
        AddUVCoords(uvCoords);
    }

    public void CreateNegativeXFace(int x, int y, int z, string texture)
    {
        Vector2 uvCoords = texNameCoordDictionary[texture];

        vertexList.Add(new Vector3(x, y, z + 1));
        vertexList.Add(new Vector3(x, y + 1, z + 1));
        vertexList.Add(new Vector3(x, y + 1, z));
        vertexList.Add(new Vector3(x, y, z));
        AddTriangleIndices();
        AddUVCoords(uvCoords);
    }

    public void CreatePositiveXFace (int x, int y, int z, Vector2 uvCoords)
    {
        vertexList.Add(new Vector3(x + 1, y, z));
        vertexList.Add(new Vector3(x + 1, y + 1, z));
        vertexList.Add(new Vector3(x + 1, y + 1, z + 1));
        vertexList.Add(new Vector3(x + 1, y, z + 1));
        AddTriangleIndices();
        AddUVCoords(uvCoords);
    }

    public void CreatePositiveXFace(int x, int y, int z, string texture)
    {
        Vector2 uvCoords = texNameCoordDictionary[texture];

        vertexList.Add(new Vector3(x + 1, y, z));
        vertexList.Add(new Vector3(x + 1, y + 1, z));
        vertexList.Add(new Vector3(x + 1, y + 1, z + 1));
        vertexList.Add(new Vector3(x + 1, y, z + 1));
        AddTriangleIndices();
        AddUVCoords(uvCoords);
    }

    public void CreateNegativeYFace (int x, int y, int z, Vector2 uvCoords)
    {
        vertexList.Add(new Vector3(x + 1, y, z));
        vertexList.Add(new Vector3(x + 1, y, z + 1));
        vertexList.Add(new Vector3(x, y, z + 1));
        vertexList.Add(new Vector3(x, y, z));
        AddTriangleIndices();
        AddUVCoords(uvCoords);
    }

    public void CreateNegativeYFace(int x, int y, int z, string texture)
    {
        Vector2 uvCoords = texNameCoordDictionary[texture];

        vertexList.Add(new Vector3(x + 1, y, z));
        vertexList.Add(new Vector3(x + 1, y, z + 1));
        vertexList.Add(new Vector3(x, y, z + 1));
        vertexList.Add(new Vector3(x, y, z));
        AddTriangleIndices();
        AddUVCoords(uvCoords);
    }

    public void CreatePositiveYFace (int x, int y, int z, Vector2 uvCoords)
    {
        vertexList.Add(new Vector3(x + 1 , y + 1, z));
        vertexList.Add(new Vector3(x, y + 1, z));
        vertexList.Add(new Vector3(x, y + 1, z + 1));
        vertexList.Add(new Vector3(x + 1, y + 1, z + 1));
        AddTriangleIndices();
        AddUVCoords(uvCoords);
    }

    public void CreatePositiveYFace(int x, int y, int z, string texture)
    {
        Vector2 uvCoords = texNameCoordDictionary[texture];

        vertexList.Add(new Vector3(x + 1, y + 1, z));
        vertexList.Add(new Vector3(x, y + 1, z));
        vertexList.Add(new Vector3(x, y + 1, z + 1));
        vertexList.Add(new Vector3(x + 1, y + 1, z + 1));
        AddTriangleIndices();
        AddUVCoords(uvCoords);
    }

    void AddTriangleIndices()
    {
        triIndexList.Add(numQuads * 4);
        triIndexList.Add((numQuads * 4) + 1);
        triIndexList.Add((numQuads * 4) + 3);
        triIndexList.Add((numQuads * 4) + 1);
        triIndexList.Add((numQuads * 4) + 2);
        triIndexList.Add((numQuads * 4) + 3);
        numQuads++;
    }

    void AddUVCoords(Vector2 uvCoords)
    {
        UVList.Add(new Vector2(uvCoords.x, uvCoords.y + 0.5f));
        UVList.Add(new Vector2(uvCoords.x + 0.5f, uvCoords.y + 0.5f));
        UVList.Add(new Vector2(uvCoords.x + 0.5f, uvCoords.y));
        UVList.Add(new Vector2(uvCoords.x, uvCoords.y));
    }
}
