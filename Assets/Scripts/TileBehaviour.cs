 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TileBehaviour : MonoBehaviour
{
    // TileType
    public enum Type { Water, LightWater, Sand, Dirt, Grass, LightGrass, Stone, LightStone };
	public Type TileType = Type.Grass;

    // Tile Renderer
	public Renderer rend;
    public Material Texture_Material;

    public Vector2[] oldMeshUVs;

    // Game Objects
	public GameObject Controller;
	public GameObject Tower;
    public GameObject TreeOBJ;
    //---
	public GameObject[,] Grid;
	public TowerBehaviour[,] Towers;
    public TowerBehaviour currentTower;
	public InvManager inv;

    // Tile Properties
    public bool hasTower = false;
    public bool canHaveTower = true;
	public int PosX;
    public int PosY;
    public float HeightMultiplier = 2.0f;

    // Grid Properties
    public int gridWidth = 0;
	public int gridHeight = 0;

    //public SetUVs Set_UVs;

    void Start()
    {
		Controller = GameObject.FindGameObjectWithTag("GameController");
		inv = Controller.GetComponent<InvManager>();
        SetTileType(transform.position.y);
    }
	private void Awake()
	{
		rend = GetComponent<Renderer>();
	}

	public Type GetTileType()
	{
		return TileType;
	}

    private void OnMouseDown()
    {
        if (!hasTower && canHaveTower)
        {
			if (inv.Current_Stone_Total >= inv.Basic_Tower_Price_Int)
			{

				GameObject BasicTower;
				BasicTower = Instantiate(Tower, new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), new Quaternion(0, 0, 0, 0));
                currentTower = BasicTower.GetComponent<TowerBehaviour>();
                currentTower.towerName = "BasicTower";
                currentTower.PosOnGrid = new Vector2(PosX,PosY);

                hasTower = true;
				inv.Current_Stone_Total -= inv.Basic_Tower_Price_Int;
			}
        }
        else if (!hasTower && TileType == Type.Water)
        {
			if (inv.Current_Stone_Total >= inv.Pump_Price_Int)
			{

				GameObject Pump;
				Pump = Instantiate(Tower, new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), new Quaternion(0, 0, 0, 0));
                currentTower = Pump.GetComponent<TowerBehaviour>();

                currentTower.towerName = "Pump";
                currentTower.PosOnGrid = new Vector2(PosX, PosY);

				hasTower = true;
				inv.Current_Stone_Total -= inv.Pump_Price_Int;
			}
        }
        else if (!hasTower && TileType == Type.Stone)
        {
			if (inv.Current_Stone_Total >= inv.Drill_Price_Int)
			{
				GameObject Drill;
				Drill = Instantiate(Tower, new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), new Quaternion(0, 0, 0, 0));
                currentTower = Drill.GetComponent<TowerBehaviour>();

                currentTower.towerName = "Drill";
                currentTower.PosOnGrid = new Vector2(PosX, PosY);
				hasTower = true;
				inv.Current_Stone_Total -= inv.Drill_Price_Int;
			}
        }
    }

    public void SetTileType(float mapHeight)
	{
         
        transform.localScale = new Vector3(1.0f, mapHeight * HeightMultiplier, 1.0f);
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localScale.y / 2, transform.localPosition.z);

		if (mapHeight < 0.1)
		{
			TileType = Type.Water;
        
			canHaveTower = false;

            transform.localScale = new Vector3(1.0f, 0.2f * HeightMultiplier, 1.0f);
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localScale.y / 2, transform.localPosition.z);
        }
        else if (mapHeight < 0.2)
        {
            TileType = Type.LightWater;

            canHaveTower = false;

            transform.localScale = new Vector3(1.0f, 0.2f * HeightMultiplier, 1.0f);
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localScale.y / 2, transform.localPosition.z);
        }
        else if (mapHeight < 0.32)
        {
            TileType = Type.Sand;

            canHaveTower = true;

        }
        else if (mapHeight < 0.37)
        {
            TileType = Type.Dirt;

            canHaveTower = true;

        }
        else if(mapHeight < 0.55)
        {
            TileType = Type.Grass;

           // int randomTreeNum = UnityEngine.Random.Range(0,10);
           // if (randomTreeNum > 8 && hasTower == false)
           // {
           //     GameObject Tree = Instantiate(TreeOBJ, new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), new Quaternion(0, 0, 0, 0));
           //     Tower = Tree;
           //     hasTower = true;
           // }

            canHaveTower = true;

        }
        else if (mapHeight < 0.65)
        {
            TileType = Type.LightGrass;
        
            canHaveTower = true;
       
        }
        else if (mapHeight < 0.95)
        {
        	TileType = Type.Stone;
            transform.localScale = new Vector3(1.0f, mapHeight * HeightMultiplier * 1.25f, 1.0f);
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localScale.y / 2, transform.localPosition.z);
            canHaveTower = true;
        }
        else
        {
            TileType = Type.LightStone;
            transform.localScale = new Vector3(1.0f, mapHeight * HeightMultiplier * 1.25f, 1.0f);
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localScale.y / 2, transform.localPosition.z);
            canHaveTower = true;
        }

    }



}
