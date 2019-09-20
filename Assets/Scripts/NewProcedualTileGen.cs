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

    // Tile Variables
    public int Tile_Types;
    List<List<MeshFilter>> Tile_Mesh;
    GameObject[,] Tile_Object;

    // Chunk Variables
    public int Chunk_Size = 20;
    public int Chunk_Amount = 2;
    List<List<GameObject>> New_Generated_Type_Chunk;
    int[] Mesh_Count;



    private void Start()
    {
        // Perlin Noise Variable Initialization
        Generated_Noise = new float[Chunk_Size, Chunk_Size];
        //Random_Noise_Value = Random.value;

        Mesh_Count = new int[Tile_Types];

        New_Generated_Type_Chunk = new List<List<GameObject>>();
        Tile_Mesh = new List<List<MeshFilter>>();

        for (int Chunk_XPos = 0; Chunk_XPos < Chunk_Amount; Chunk_XPos++)
        {
            New_Generated_Type_Chunk.Add(new List<GameObject>());
            for (int Chunk_YPos = 0; Chunk_YPos < Chunk_Amount; Chunk_YPos++)
            {

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
                        Tile_Object[x, y] = Instantiate(Tile_Prefab, new Vector3(Chunk_XPos * Chunk_Size + x, 0.0f, Chunk_YPos * Chunk_Size + y), new Quaternion(0, 0, 0, 0));
                        Tile_Object[x, y].name = "Tile " + x + ", " + y;

                        MeshFilter Current_Tile_Mesh_Filter = Tile_Object[x, y].GetComponent<MeshFilter>();

                        Tile_Object[x, y].GetComponent<TileBehaviour>().SetTileType((Noise(Chunk_XPos, Chunk_YPos)[x, y]));

                        int Current_Tile_Type = (int)Tile_Object[x, y].GetComponent<TileBehaviour>().TileType;
                        Tile_Mesh[Current_Tile_Type].Add(Current_Tile_Mesh_Filter);

                        Tile_Object[x, y].GetComponent<MeshRenderer>().enabled = false;
                    }
                }


                for (int Chunk_Type = 0; Chunk_Type < Tile_Types; Chunk_Type++)
                {

                    CombineInstance[] New_Chunk_Combine_Mesh = new CombineInstance[Tile_Mesh[Chunk_Type].Count];

                    for (int i = 0; i < Tile_Mesh[Chunk_Type].Count; i++)
                    {
                        New_Chunk_Combine_Mesh[i].mesh = Tile_Mesh[Chunk_Type][i].sharedMesh;
                        New_Chunk_Combine_Mesh[i].transform = Tile_Mesh[Chunk_Type][i].transform.localToWorldMatrix;
                    }

                    New_Generated_Type_Chunk[Chunk_XPos].Add(Instantiate(Chunk_Prefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0)));
                    New_Generated_Type_Chunk[Chunk_XPos][Chunk_Type].transform.GetComponent<MeshFilter>().mesh = new Mesh();
                    New_Generated_Type_Chunk[Chunk_XPos][Chunk_Type].transform.GetComponent<MeshFilter>().mesh.CombineMeshes(New_Chunk_Combine_Mesh);
                    New_Generated_Type_Chunk[Chunk_XPos][Chunk_Type].GetComponent<Renderer>().material = Tile_Material[Chunk_Type];
                    New_Generated_Type_Chunk[Chunk_XPos][Chunk_Type].transform.gameObject.SetActive(true);
                    New_Generated_Type_Chunk[Chunk_XPos][Chunk_Type].name = "New" + Tile_Material[Chunk_Type] + " Chunk";
                }
            }
        }
    }

    // Generate Perlin Noise Map
    public float[,] Noise(int Chunk_XPos, int Chunk_YPos)
    {
        float[,] Generated_Noise = new float[Chunk_Size, Chunk_Size];


        for (int y = 0; y < Chunk_Size; y++)
        {
            for (int x = 0; x < Chunk_Size; x++)
            {
                Generated_Noise[x, y] = (Mathf.PerlinNoise((x + (Chunk_XPos * Chunk_Size)) * Noise_Freqancy * Random_Noise_Value, (y + (Chunk_YPos * Chunk_Size)) * Noise_Freqancy * Random_Noise_Value) / 2);
            }
        }

        return Generated_Noise;

    }
}
