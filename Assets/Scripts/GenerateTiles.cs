
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

    public Material Mat_Water;
    public Material Mat_Grass;
    public Material Mat_Stone;
    int Tile_Count = 6;

    GameObject[,] New_Generated_Chunk;
    MeshFilter[][] Tile_Mesh_List;
    int[] Mesh_Count;

    GameObject[,] Tile_Object;

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

        New_Generated_Chunk = new GameObject[Chunk_Count,Chunk_Count];
        Mesh_Count = new int[Tile_Count];


        for (int Chunk_XPos = 0; Chunk_XPos < Chunk_Count; ++Chunk_XPos)
        {
            for (int Chunk_YPos = 0; Chunk_YPos < Chunk_Count; ++Chunk_YPos)
            {

                New_Generated_Chunk[Chunk_XPos,Chunk_YPos] = Instantiate(Chunk_Prefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));

                Tile_Object = new GameObject[Chunk_Width, Chunk_Height];

                MeshFilter[][] Chunk_Meshes = new MeshFilter[Tile_Count][];
                for (int i = 0; i < Tile_Count; i++)
                {
                    Chunk_Meshes[i] = new MeshFilter[Chunk_Width * Chunk_Height];
                }


                CombineInstance[][] New_Chunk_Combine_Mesh = new CombineInstance[Tile_Count][];
                for (int i = 0; i < Tile_Count; i++)
                {
                    New_Chunk_Combine_Mesh[i] = new CombineInstance[Chunk_Width * Chunk_Height];
                }

                Tile_Mesh_List = new MeshFilter[Tile_Count][];
                for (int i = 0; i < Tile_Count; i++)
                {
                    Tile_Mesh_List[i] = new MeshFilter[Chunk_Width * Chunk_Height];
                }

                for (int x = 0; x < Chunk_Width; x++)
                {
                    for (int y = 0; y < Chunk_Height; y++)
                    {
                        Tile_Object[x, y] = Instantiate(Tile_Prefab, new Vector3(Chunk_XPos * Chunk_Width + x, 0.0f, Chunk_YPos * Chunk_Height + y), new Quaternion(0, 0, 0, 0));
                        Tile_Object[x, y].name = "Tile " + x + ", " + y;
                        MeshFilter meshFilter = Tile_Object[x, y].GetComponent<MeshFilter>();

                        Tile_Object[x, y].GetComponent<TileBehaviour>().SetTileType((Noise(Chunk_XPos, Chunk_YPos)[x, y]));

                        int tileType = (int)Tile_Object[x, y].GetComponent<TileBehaviour>().TileType;
                        Tile_Mesh_List[tileType][Mesh_Count[tileType]] = meshFilter;
                        Mesh_Count[tileType]++;

                        Tile_Object[x, y].GetComponent<MeshRenderer>().enabled = false;
                    }
                }

                for (int c = 0; c < Tile_Count; c++)
                {

                    int i = 0;
                    while (i < Mesh_Count[c])
                    {

                        New_Chunk_Combine_Mesh[c][i].mesh = Tile_Mesh_List[c][i].sharedMesh;
                        New_Chunk_Combine_Mesh[c][i].transform = Tile_Mesh_List[c][i].transform.localToWorldMatrix;

                        i++;
                    }

                    //create new object for each chunk
                    New_Generated_Chunk[Chunk_XPos, Chunk_YPos].transform.GetComponent<MeshFilter>().mesh = new Mesh();
                    New_Generated_Chunk[Chunk_XPos, Chunk_YPos].transform.GetComponent<MeshFilter>().mesh.CombineMeshes(New_Chunk_Combine_Mesh[c]);
                    New_Generated_Chunk[Chunk_XPos, Chunk_YPos].GetComponent<Renderer>().material = Mat_Water;
                    New_Generated_Chunk[Chunk_XPos, Chunk_YPos].transform.gameObject.SetActive(true);
                    New_Generated_Chunk[Chunk_XPos, Chunk_YPos].name = "New Chunk";
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
                Generated_Noise[x, y] = (Mathf.PerlinNoise((x + (Chunk_XPos * Chunk_Width)) * Noise_Freqancy, (y + (Chunk_YPos * Chunk_Height)) * Noise_Freqancy) / 2);
            }
        }

        return Generated_Noise;

    }

}
