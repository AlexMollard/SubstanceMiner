using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamScript : MonoBehaviour
{
    public GameObject gameController;
    NewProcedualTileGen TG;
    float Width;
    float Depth;
    float Height;

    private void Start()
    {
        TG = gameController.GetComponent<NewProcedualTileGen>();

        Width = (TG.Chunk_Size / 2) - 0.5f;
        Depth = (TG.Chunk_Size / 2) - 0.5f;
        Height = 10;

        transform.position = new Vector3(Width, Height, Depth);
        GetComponent<Camera>().orthographicSize = Width + 0.5f;
    }
}
