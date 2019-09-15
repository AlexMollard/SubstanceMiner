using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]


public class GenerateTiles : MonoBehaviour
{
	public GameObject Tile_Prefab;
    public GameObject Chunk_Prefab;
    public Material Textures_Material;

	TileBehaviour[,] Tile_Behaviour;
	TowerBehaviour[,] Tile_Tower_Behaviour;
    GameObject[,] Generate_Water_Chunks;
    GameObject[,] Generate_Grass_Chunks;
    GameObject[,] Generate_Stone_Chunks;
    GameObject[,] Tile_Object;

    Vector2[,][] Old_Tile_Mesh_UVs;


	public int Chunk_Width = 40;
	public int Chunk_Height = 40;
    public int Chunk_Count = 4;

    public float Noise_Freqancy = 0.15f;
    private float[,] Noise_Value;
    private float Random_Int_For_Noise;

    void Start()
    {
        Noise_Value = new float[Chunk_Width, Chunk_Height];
        Random_Int_For_Noise = UnityEngine.Random.value;

        Generate_Water_Chunks = new GameObject[Chunk_Count,Chunk_Count];
        Generate_Grass_Chunks = new GameObject[Chunk_Count,Chunk_Count];
        Generate_Stone_Chunks = new GameObject[Chunk_Count,Chunk_Count];

        for (int Chunk_XPos = 0; Chunk_XPos < Chunk_Count; ++Chunk_XPos)
        {
            for (int Chunk_YPos = 0; Chunk_YPos < Chunk_Count; ++Chunk_YPos)
            {
                int Current_Water_Mesh = 0;
                int Current_Grass_Mesh = 0;
                int Current_Stone_Mesh = 0;

                Generate_Water_Chunks[Chunk_XPos,Chunk_YPos] = Instantiate(Chunk_Prefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
                Generate_Grass_Chunks[Chunk_XPos,Chunk_YPos] = Instantiate(Chunk_Prefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
                Generate_Stone_Chunks[Chunk_XPos,Chunk_YPos] = Instantiate(Chunk_Prefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));


                Tile_Object = new GameObject[Chunk_Width, Chunk_Height];
                Tile_Behaviour = new TileBehaviour[Chunk_Width, Chunk_Height];

                MeshFilter[] Water_Meshes = new MeshFilter[Chunk_Width * Chunk_Height];
                MeshFilter[] Grass_Meshes = new MeshFilter[Chunk_Width * Chunk_Height];
                MeshFilter[] Stone_Meshes = new MeshFilter[Chunk_Width * Chunk_Height];


                CombineInstance[] New_Water_Chunk_Mesh = new CombineInstance[Water_Meshes.Length];
                CombineInstance[] New_Grass_Chunk_Mesh = new CombineInstance[Grass_Meshes.Length];
                CombineInstance[] New_Stone_Chunk_Mesh = new CombineInstance[Stone_Meshes.Length];


                for (int x = 0; x < Chunk_Width; x++)
                {
                    for (int y = 0; y < Chunk_Height; y++)
                    {
                        Tile_Object[x, y] = Instantiate(Tile_Prefab, new Vector3(Chunk_XPos * Chunk_Width + x, Noise(Chunk_XPos,Chunk_YPos)[x,y] / 2, Chunk_YPos * Chunk_Height + y), new Quaternion(0, 0, 0, 0));
                        Vector3 newScale = new Vector3(1.0f, Noise(Chunk_XPos, Chunk_YPos)[x, y] + 1, 1.0f);
                        Tile_Object[x, y].transform.localScale = newScale;

                        MeshFilter meshFilter = Tile_Object[x, y].GetComponent<MeshFilter>();

                        Tile_Object[x, y].GetComponent<TileBehaviour>().SetTileType(Noise(Chunk_XPos, Chunk_YPos)[x, y] /2);

                        if (Tile_Object[x, y].GetComponent<TileBehaviour>().TileType == TileBehaviour.Type.Water)
                        {
                            Water_Meshes[Current_Water_Mesh] = meshFilter;
                            Current_Water_Mesh++;
                        }
                        else if (Tile_Object[x, y].GetComponent<TileBehaviour>().TileType == TileBehaviour.Type.Grass)
                        {
                            Grass_Meshes[Current_Grass_Mesh] = meshFilter;
                            Current_Grass_Mesh++;
                        }
                        else if (Tile_Object[x, y].GetComponent<TileBehaviour>().TileType == TileBehaviour.Type.Stone)
                        {
                            Stone_Meshes[Current_Stone_Mesh] = meshFilter;
                            Current_Stone_Mesh++;
                        }

                        Tile_Object[x, y].GetComponent<MeshRenderer>().enabled = false;
                    }
                }

                if (Water_Meshes[0] != null)
                {


                    int W = 0;
                    while (W < Current_Water_Mesh)
                    {
                        if (Water_Meshes[W])
                        {
                            New_Water_Chunk_Mesh[W].mesh = Water_Meshes[W].sharedMesh;
                            New_Water_Chunk_Mesh[W].transform = Water_Meshes[W].transform.localToWorldMatrix;
                        }

                        W++;
                    }


                    //create new object for each chunk
                    Generate_Water_Chunks[Chunk_XPos, Chunk_YPos].transform.GetComponent<MeshFilter>().mesh = new Mesh();
                    Generate_Water_Chunks[Chunk_XPos, Chunk_YPos].transform.GetComponent<MeshFilter>().mesh.CombineMeshes(New_Water_Chunk_Mesh);
                    Generate_Water_Chunks[Chunk_XPos, Chunk_YPos].GetComponent<Renderer>().material = Textures_Material;
                    Generate_Water_Chunks[Chunk_XPos, Chunk_YPos].transform.gameObject.SetActive(true);
                    Generate_Water_Chunks[Chunk_XPos, Chunk_YPos].name = "Water Chunk";

                }

                if (Grass_Meshes[0] != null)
                {

                    int G = 0;
                    while (G < Current_Grass_Mesh)
                    {
                        if (Grass_Meshes[G])
                        {
                            New_Grass_Chunk_Mesh[G].mesh = Grass_Meshes[G].sharedMesh;
                            New_Grass_Chunk_Mesh[G].transform = Grass_Meshes[G].transform.localToWorldMatrix;
                        }

                        G++;
                    }

                    //create new object for each chunk
                    Generate_Grass_Chunks[Chunk_XPos, Chunk_YPos].transform.GetComponent<MeshFilter>().mesh = new Mesh();
                    Generate_Grass_Chunks[Chunk_XPos, Chunk_YPos].transform.GetComponent<MeshFilter>().mesh.CombineMeshes(New_Grass_Chunk_Mesh);
                    Generate_Grass_Chunks[Chunk_XPos, Chunk_YPos].GetComponent<Renderer>().material = Textures_Material;
                    Generate_Grass_Chunks[Chunk_XPos, Chunk_YPos].transform.gameObject.SetActive(true);
                    Generate_Grass_Chunks[Chunk_XPos, Chunk_YPos].name = "Grass Chunk";
                }

                if (Stone_Meshes[0] != null)
                {

                    int S = 0;
                    while (S < Current_Stone_Mesh)
                    {
                        if (Stone_Meshes[S])
                        {
                            New_Stone_Chunk_Mesh[S].mesh = Stone_Meshes[S].sharedMesh;
                            New_Stone_Chunk_Mesh[S].transform = Stone_Meshes[S].transform.localToWorldMatrix;
                        }

                        S++;
                    }


                    //create new object for each chunk
                    Generate_Stone_Chunks[Chunk_XPos, Chunk_YPos].transform.GetComponent<MeshFilter>().mesh = new Mesh();
                    Generate_Stone_Chunks[Chunk_XPos, Chunk_YPos].transform.GetComponent<MeshFilter>().mesh.CombineMeshes(New_Stone_Chunk_Mesh);
                    Generate_Stone_Chunks[Chunk_XPos, Chunk_YPos].GetComponent<Renderer>().material = Textures_Material;
                    Generate_Stone_Chunks[Chunk_XPos, Chunk_YPos].transform.gameObject.SetActive(true);
                    Generate_Stone_Chunks[Chunk_XPos, Chunk_YPos].name = "Stone Chunk";
                }

            }
        }
    }



    public float[,] Noise(int Chunk_XPos,int Chunk_YPos)
    {
        float[,] Generated_Noise = new float[Chunk_Width, Chunk_Height];


        for (int y = 0; y < Chunk_Height; y++)
        {
            for (int x = 0; x < Chunk_Width; x++)
            {
                Generated_Noise[x, y] = Mathf.PerlinNoise((x + (Chunk_XPos * Chunk_Width)) * Noise_Freqancy, (y + (Chunk_YPos * Chunk_Height)) * Noise_Freqancy);
            }
        }

        return Generated_Noise;

    }

}
