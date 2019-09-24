
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
    public float Amplitude = 2;

    [SerializeField] Material[] Tile_Materials;

    int Tile_Count = 3;

    List<GameObject> New_Generated_Chunk;
    MeshFilter[][] Tile_Mesh_List;
    int[] Mesh_Count;

    GameObject[,] Tile_Object;

	public int Chunk_Width = 40;
	public int Chunk_Height = 40;

    public float Noise_Freqancy = 0.15f;
    private float[,] Noise_Value;
    private float Random_Int_For_Noise;

    void Start()
    {
        Noise_Value = new float[Chunk_Width, Chunk_Height];
        Random_Int_For_Noise = UnityEngine.Random.value;

        New_Generated_Chunk = new List<GameObject>();

        Mesh_Count = new int[Tile_Count];





                for (int i = 0; i < Tile_Count; i++)
                {
                    Mesh_Count[i] = 0;
                }

                New_Generated_Chunk.Add(Instantiate(Chunk_Prefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0)));

                Tile_Object = new GameObject[Chunk_Width, Chunk_Height];

                List<List<MeshFilter>> Chunk_Meshes = new List<List<MeshFilter>>();
                Chunk_Meshes.Add(new List<MeshFilter>());


                Tile_Mesh_List = new MeshFilter[Tile_Count][];
                for (int i = 0; i < Tile_Count; i++)
                {
                    Tile_Mesh_List[i] = new MeshFilter[Chunk_Width * Chunk_Height];
                }

                for (int x = 0; x < Chunk_Width; x++)
                {
                    for (int y = 0; y < Chunk_Height; y++)
                    {
                        Tile_Object[x, y] = Instantiate(Tile_Prefab, new Vector3(Chunk_Width + x, 0.0f, Chunk_Height + y), new Quaternion(0, 0, 0, 0));
                        Tile_Object[x, y].name = "Tile " + x + ", " + y;
                        MeshFilter meshFilter = Tile_Object[x, y].GetComponent<MeshFilter>();

                        Tile_Object[x, y].GetComponent<TileBehaviour>().SetTileType((Noise()[x, y]));

                        int tileType = (int)Tile_Object[x, y].GetComponent<TileBehaviour>().TileType;
                        Tile_Mesh_List[tileType][Mesh_Count[tileType]] = meshFilter;
                        Mesh_Count[tileType]++;

                        Tile_Object[x, y].GetComponent<MeshRenderer>().enabled = false;
                    }
                }


                for (int c = 0; c < Tile_Count; c++)
                {

                    int i = 0;

                    CombineInstance[] newChunkCombineMeshArray = new CombineInstance[Mesh_Count[c]];
                    while (i < Mesh_Count[c])
                    {
                        newChunkCombineMeshArray[i].mesh = Tile_Mesh_List[c][i].sharedMesh;
                        newChunkCombineMeshArray[i].transform = Tile_Mesh_List[c][i].transform.localToWorldMatrix;

                        i++;
                    }

                    //create new object for each chunk
                    New_Generated_Chunk[c].transform.GetComponent<MeshFilter>().mesh = new Mesh();
                    New_Generated_Chunk[c].transform.GetComponent<MeshFilter>().mesh.CombineMeshes(newChunkCombineMeshArray);
                    New_Generated_Chunk[c].GetComponent<Renderer>().material = Tile_Materials[c];
                    New_Generated_Chunk[c].transform.gameObject.SetActive(true);
                    New_Generated_Chunk[c].name = "New Chunk";
                }
            }
        
    


    public float[,] Noise()
    {
        float[,] Generated_Noise = new float[Chunk_Width, Chunk_Height];


        for (int y = 0; y < Chunk_Height; y++)
        {
            for (int x = 0; x < Chunk_Width; x++)
            {
                Generated_Noise[x, y] = (Mathf.PerlinNoise((x * Noise_Freqancy), (y * Noise_Freqancy) / 2));
            }
        }

        return Generated_Noise;

    }

}
