using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelChunk : MonoBehaviour {

    public VoxelGenerator voxelGenerator;
    int[,,] terrainArray;
    int chunkSize = 32;

    public delegate void EventBlockChanged();
    public static event EventBlockChanged OnEventBlockDestroyed;
    public static event EventBlockChanged OnEventBlockPlaced;

    // Use this for initialization
    void Start () {
        voxelGenerator = GetComponent<VoxelGenerator>();
        terrainArray = new int[chunkSize, chunkSize, chunkSize];
        voxelGenerator.Initialise();
        InitialiseTerrain();
        CreateTerrain();
        voxelGenerator.UpdateMesh();
	}

    public void SetBlock(Vector3 index, int blockType)
    {
        if ((index.x > 0 && index.x < terrainArray.GetLength(0)) && (index.y > 0 && index.y < terrainArray.GetLength(1)) && (index.z > 0 && index.z < terrainArray.GetLength(2)))
        {
            terrainArray[(int)index.x, (int)index.y, (int)index.z] = blockType;
            CreateTerrain();
            voxelGenerator.UpdateMesh();

            if (blockType == 0)
            {
                OnEventBlockDestroyed();
            }
            else
            {
                OnEventBlockPlaced();
            }
        }
    }
	
    void InitialiseTerrain()
    {
        for (int x = 0; x < terrainArray.GetLength(0); x++)
        {
            for (int y = 0; y < terrainArray.GetLength(1); y++)
            {
                for (int z = 0; z < terrainArray.GetLength(2); z++)
                {
                    if (y == 3)
                    {
                        terrainArray[x, y, z] = 1;
                    }
                    else if (y < 3)
                    {
                        terrainArray[x, y, z] = 2;
                    }
                }
            }
        }
    }

    void CreateTerrain()
    {
        for (int x = 0; x < terrainArray.GetLength(0); x++)
        {
            for (int y = 0; y < terrainArray.GetLength(0); y++)
            {
                for (int z = 0; z < terrainArray.GetLength(0); z++)
                {
                    if (terrainArray[x, y, z] != 0)
                    {
                        string tex;
                        switch (terrainArray[x, y, z])
                        {
                            case 1:
                                tex = "Stone";
                                break;
                            case 2:
                                tex = "Dirt";
                                break;
                            case 3:
                                tex = "Sand";
                                break;
                            default:
                                tex = "Grass";
                                break;
                        }
                        if (x == 0 || terrainArray[x - 1, y, z] == 0)
                        {
                            voxelGenerator.CreateNegativeXFace(x, y, z, tex);
                        }
                        if (x == terrainArray.GetLength(0) - 1 || terrainArray[x + 1, y, z] == 0)
                        {
                            voxelGenerator.CreatePositiveXFace(x, y, z, tex);
                        }
                        if (y == 0 || terrainArray[x, y - 1, z] == 0)
                        {
                            voxelGenerator.CreateNegativeYFace(x, y, z, tex);
                        }
                        if (y == terrainArray.GetLength(1) - 1 || terrainArray[x, y + 1, z] == 0)
                        {
                            voxelGenerator.CreatePositiveYFace(x, y, z, tex);
                        }
                        if (z == 0 || terrainArray[x, y, z - 1] == 0)
                        {
                            voxelGenerator.CreateNegativeZFace(x, y, z, tex);
                        }
                        if (z == terrainArray.GetLength(2) - 1 || terrainArray[x, y, z + 1] == 0)
                        {
                            voxelGenerator.CreatePositiveZFace(x, y, z, tex);
                        }
                        print("Create " + tex + " block,");
                    }
                }
            }
        }
    }

	// Update is called once per frame
	void Update ()
    {
       if (Input.GetKeyDown(KeyCode.F1))
       {
           XMLVoxelFileWriter.SaveChunkToXMLFile(terrainArray, "VoxelChunk");
       }

       if (Input.GetKeyDown(KeyCode.F2))
       {
            //get terrain array form xml file
            terrainArray = XMLVoxelFileWriter.LoadChunkFromXMLFile(32, "VoxelChunk");
            //draw correct faces
            CreateTerrain();
            //update mesh info
            voxelGenerator.UpdateMesh();
       }
    }
}
