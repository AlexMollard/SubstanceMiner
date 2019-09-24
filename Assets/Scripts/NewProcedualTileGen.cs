using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewProcedualTileGen : MonoBehaviour
{
    // World Variables
    public GameObject Tile_Prefab;
    public GameObject Chunk_Prefab;
    [SerializeField] Material[] Tile_Material;

    // Perlin Noise Variables
    public float Noise_Freqancy = 0.15f;
    public float Random_Noise_Value = 1.0f;
    private float[,] Generated_Noise;
    Color[][] GradientSquare;

    // Tile Variables
    public int Tile_Types;
    List<List<MeshFilter>> Tile_Mesh;
    GameObject[,] Tile_Object;

    // Chunk Variables
    public int Chunk_Size = 20;
    public int Chunk_Amount = 2;
    List<GameObject> New_Generated_Type_Chunk;
    int[] Mesh_Count;



    private void Start()
    {
        // Perlin Noise Variable Initialization
        Generated_Noise = new float[Chunk_Size, Chunk_Size];
        GradientSquare = GenerateSquareGradient();
        Random_Noise_Value = Random.Range(0, 99999);

        Mesh_Count = new int[Tile_Types];

        New_Generated_Type_Chunk = new List<GameObject>();
        Tile_Mesh = new List<List<MeshFilter>>();



        for (int i = 0; i < Tile_Types; i++)
        {
            Mesh_Count[i] = 0;
        }


        Tile_Object = new GameObject[Chunk_Size, Chunk_Size];

        List<List<MeshFilter>> Chunk_Meshes = new List<List<MeshFilter>>();
        Chunk_Meshes.Add(new List<MeshFilter>());

        for (int i = 0; i < Tile_Types; i++)
        {
            Tile_Mesh.Add(new List<MeshFilter>());
        }

        for (int x = 0; x < Chunk_Size; x++)
        {
            for (int y = 0; y < Chunk_Size; y++)
            {
                Tile_Object[x, y] = Instantiate(Tile_Prefab, new Vector3(x, 0.0f, y), new Quaternion(0, 0, 0, 0));
                Tile_Object[x, y].name = "Tile " + x + ", " + y;

                MeshFilter Current_Tile_Mesh_Filter = Tile_Object[x, y].GetComponent<MeshFilter>();

                Tile_Object[x, y].GetComponent<TileBehaviour>().SetTileType((Noise()[x, y]));

                int Current_Tile_Type = (int)Tile_Object[x, y].GetComponent<TileBehaviour>().TileType;
                Tile_Mesh[Current_Tile_Type].Add(Current_Tile_Mesh_Filter);

                Tile_Object[x, y].GetComponent<MeshRenderer>().enabled = false;
            }
        }


        for (int Chunk_Type = 0; Chunk_Type < Tile_Types; Chunk_Type++)
        {
            CombineInstance[] New_Chunk_Combine_Mesh;

            if (Tile_Mesh[Chunk_Type].Count > 2000)
            {
               New_Chunk_Combine_Mesh = new CombineInstance[2000];
            }
            else
                New_Chunk_Combine_Mesh = new CombineInstance[Tile_Mesh[Chunk_Type].Count];


            int cubeCount = 0;
            int currentChunk = 0;

            for (int i = 0; i < Tile_Mesh[Chunk_Type].Count; i++)
            {
                cubeCount++;
                New_Chunk_Combine_Mesh[i].mesh = Tile_Mesh[Chunk_Type][i].sharedMesh;
                New_Chunk_Combine_Mesh[i].transform = Tile_Mesh[Chunk_Type][i].transform.localToWorldMatrix;

                if (cubeCount >= 2000)
                {
                    cubeCount = 0;
                    currentChunk++;
                    New_Generated_Type_Chunk.Add(Instantiate(Chunk_Prefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0)));
                    CreateNewChunk(New_Chunk_Combine_Mesh, New_Generated_Type_Chunk[New_Generated_Type_Chunk.Count - 1], Chunk_Type);

                    New_Chunk_Combine_Mesh = new CombineInstance[Tile_Mesh[Chunk_Type].Count];
                }
            }

            New_Generated_Type_Chunk.Add(Instantiate(Chunk_Prefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0)));
            CreateNewChunk(New_Chunk_Combine_Mesh, New_Generated_Type_Chunk[New_Generated_Type_Chunk.Count - 1], Chunk_Type);
        }

    }

    void CreateNewChunk(CombineInstance[] new_Combine_Instance, GameObject New_Chunk, int ChunkType)
    {
        New_Chunk.transform.GetComponent<MeshFilter>().mesh = new Mesh();
        New_Chunk.transform.GetComponent<MeshFilter>().mesh.CombineMeshes(new_Combine_Instance);
        New_Chunk.GetComponent<Renderer>().material = Tile_Material[ChunkType];
        New_Chunk.transform.gameObject.SetActive(true);
        New_Chunk.name = "New" + Tile_Material[ChunkType] + " Chunk";
    }
    // Generate Perlin Noise Map
    public float[,] Noise()
    {
        float[,] Generated_Noise = new float[Chunk_Size, Chunk_Size];


        for (int y = 0; y < Chunk_Size; y++)
        {
            for (int x = 0; x < Chunk_Size; x++)
            {
                Generated_Noise[x, y] = Mathf.PerlinNoise(x * Noise_Freqancy + Random_Noise_Value, y * Noise_Freqancy + Random_Noise_Value);
                Generated_Noise[x, y] -= GradientSquare[x][y].grayscale;
            }
        }

        return Generated_Noise;

    }



    public Color[][] GenerateSquareGradient()
    {
        int width = Chunk_Size;
        int height = Chunk_Size;
        int halfWidth = width / 2;
        int halfHeight = height / 2;

        Color[][] gradient = new Color[width][];

        for (int i = 0; i < width; i++)
        {
            gradient[i] = new Color[height];

            for (int j = 0; j < height; j++)
            {
                int x = i;
                int y = j;

                float colorValue;

                x = x > halfWidth ? width - x : x;
                y = y > halfHeight ? height - y : y;

                int smaller = x < y ? x : y;
                colorValue = smaller / (float)halfWidth;

                colorValue = 1 - colorValue;
                colorValue *= colorValue * colorValue;
                gradient[i][j] = new Color(colorValue, colorValue, colorValue);
            }
        }

        return gradient;
    }



}
