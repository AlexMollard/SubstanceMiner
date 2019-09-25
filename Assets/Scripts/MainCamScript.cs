using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamScript : MonoBehaviour
{
    public GameObject gameController;
    NewProcedualTileGen TG;
    float Width;
    float Depth;
    float Height;

    private void Start()
    {
        TG = gameController.GetComponent<NewProcedualTileGen>();

        Width = (TG.Chunk_Size / 2);
        Height = Width / 1.0f;
        Depth = Height / 1.5f;

        transform.position = new Vector3(Width, Height, -Depth);
    }
}
