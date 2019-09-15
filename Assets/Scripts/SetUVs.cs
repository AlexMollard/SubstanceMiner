using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUVs : MonoBehaviour
{
    public float Pixel_Size = 6;
    public float Tile_XPos = 1;
    public float Tile_YPos = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Vector2[] SetAllUvs(MeshFilter obj, float uPos, float Vpos)
    {
        Tile_XPos = uPos;
        Tile_YPos = Vpos;

        float Tile_Perc = 1 / Pixel_Size;

        float uMin = Tile_Perc * Tile_XPos;
        float uMax = Tile_Perc * (Tile_XPos + 1);
        float VMin = Tile_Perc * Tile_YPos;
        float VMax = Tile_Perc * (Tile_YPos + 1);

        Vector2[] Cube_UVs = new Vector2[24];

        Cube_UVs[0] = new Vector2(uMin, VMin);
        Cube_UVs[1] = new Vector2(uMax, VMin);
        Cube_UVs[2] = new Vector2(uMin, VMax);
        Cube_UVs[3] = new Vector2(uMax, VMax);
        Cube_UVs[4] = new Vector2(uMin, VMax);
        Cube_UVs[5] = new Vector2(uMax, VMax);
        Cube_UVs[6] = new Vector2(uMin, VMax);
        Cube_UVs[7] = new Vector2(uMax, VMax);
        Cube_UVs[8] = new Vector2(uMin, VMin);
        Cube_UVs[9] = new Vector2(uMax, VMin);
        Cube_UVs[10] = new Vector2(uMin, VMin);
        Cube_UVs[11] = new Vector2(uMax, VMin);
        Cube_UVs[12] = new Vector2(uMin, VMin);
        Cube_UVs[13] = new Vector2(uMin, VMax);
        Cube_UVs[14] = new Vector2(uMax, VMax);
        Cube_UVs[15] = new Vector2(uMax, VMin);
        Cube_UVs[16] = new Vector2(uMin, VMin);
        Cube_UVs[17] = new Vector2(uMin, VMax);
        Cube_UVs[18] = new Vector2(uMax, VMax);
        Cube_UVs[19] = new Vector2(uMax, VMin);
        Cube_UVs[20] = new Vector2(uMin, VMin);
        Cube_UVs[21] = new Vector2(uMin, VMax);
        Cube_UVs[22] = new Vector2(uMax, VMax);
        Cube_UVs[23] = new Vector2(uMax, VMin);

        return Cube_UVs;
    }

   // private void Update()
   // {
   //
   //     float Tile_Perc = 1 / Pixel_Size;
   //
   //     float uMin = Tile_Perc * Tile_XPos;
   //     float uMax = Tile_Perc * (Tile_XPos + 1);
   //     float VMin = Tile_Perc * Tile_YPos;
   //     float VMax = Tile_Perc * (Tile_YPos + 1);
   //
   //     Vector2[] Cube_UVs = new Vector2[24];
   //
   //     Cube_UVs[0] = new Vector2(uMin, VMin);
   //     Cube_UVs[1] = new Vector2(uMax, VMin);
   //     Cube_UVs[2] = new Vector2(uMin, VMax);
   //     Cube_UVs[3] = new Vector2(uMax, VMax);
   //     Cube_UVs[4] = new Vector2(uMin, VMax);
   //     Cube_UVs[5] = new Vector2(uMax, VMax);
   //     Cube_UVs[6] = new Vector2(uMin, VMax);
   //     Cube_UVs[7] = new Vector2(uMax, VMax);
   //     Cube_UVs[8] = new Vector2(uMin, VMin);
   //     Cube_UVs[9] = new Vector2(uMax, VMin);
   //     Cube_UVs[10] = new Vector2(uMin, VMin);
   //     Cube_UVs[11] = new Vector2(uMax, VMin);
   //     Cube_UVs[12] = new Vector2(uMin, VMin);
   //     Cube_UVs[13] = new Vector2(uMin, VMax);
   //     Cube_UVs[14] = new Vector2(uMax, VMax);
   //     Cube_UVs[15] = new Vector2(uMax, VMin);
   //     Cube_UVs[16] = new Vector2(uMin, VMin);
   //     Cube_UVs[17] = new Vector2(uMin, VMax);
   //     Cube_UVs[18] = new Vector2(uMax, VMax);
   //     Cube_UVs[19] = new Vector2(uMax, VMin);
   //     Cube_UVs[20] = new Vector2(uMin, VMin);
   //     Cube_UVs[21] = new Vector2(uMin, VMax);
   //     Cube_UVs[22] = new Vector2(uMax, VMax);
   //     Cube_UVs[23] = new Vector2(uMax, VMin);
   //
   //     this.GetComponent<MeshFilter>().mesh.uv = Cube_UVs;
   // }

}
