using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{
    GameObject gameController;
    InvManager inv;

    // Tower Materials
    public Material Basic;
    public Material Drill;
    public Material Pump;

    public TileBehaviour[,] Tiles;
    public TowerBehaviour[,] Towers;

    public Vector2 PosOnGrid = new Vector2(0,0);
    public Rigidbody rb;
    public string towerName = "";
    public float floatSpeed = 0.5f;
    public float floatHeight = 0.01f; 
    public bool landed = false;
    public bool isFactory = false;
    public float rotationSpeed = 100;
    Vector3 tempPos = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        inv = gameController.GetComponent<InvManager>();

        rb = GetComponent<Rigidbody>();

        rb.velocity = new Vector3(0.0f,-30.0f,0.0f);

        if (towerName == "Tree")
        {
            rb.velocity = new Vector3(0.0f, -10.0f, 0.0f);
        }
        else if (towerName == "Pump" || towerName == "Drill")
        {
            isFactory = true;


            BoxCollider colid = GetComponent<BoxCollider>();
            colid.size = new Vector3(colid.size.x, colid.size.y - 0.9f, colid.size.z);
        }

        SetTexture();
    }

    void SetTexture()
    {
        if (towerName == "BasicTower")
        {
            GetComponent<Renderer>().material = Basic;
        }
        else if (towerName == "Drill")
        {
            GetComponent<Renderer>().material = Drill;
        }
        else if (towerName == "Pump")
        {
            GetComponent<Renderer>().material = Pump;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (landed)
        {
            rb.useGravity = false;
        }



        if (isFactory && landed)
        {
            tempPos = transform.position;
            tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * floatSpeed) * floatHeight;


            if (towerName == "Pump")
            {
                inv.AddWater(1.0f * Time.deltaTime);
            }
            else if (towerName == "Drill")
            {
                inv.AddStone(1.0f * Time.deltaTime);
                transform.Rotate(0.0f,6.0f * rotationSpeed * Time.deltaTime,0.0f);
            }

            transform.position = tempPos;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        landed = true;
    }
}
