using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TileBehaviour : MonoBehaviour
{
    // TileType
    public enum Type {Grass,Dirt,Stone,Water,Sand,DarkGrass};
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
        mapHeight *= 2;
        transform.localScale = new Vector3(1.0f, mapHeight , 1.0f);
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localScale.y / 2, transform.localPosition.z);

		if (mapHeight < 0.2)
		{
			TileType = Type.Water;
        
			canHaveTower = false;

            transform.localScale = new Vector3(1.0f, 0.2f, 1.0f);
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localScale.y / 2, transform.localPosition.z);
        }
        else if(mapHeight < 0.5)
        {
            TileType = Type.Grass;

            canHaveTower = false;

        }
        else if (mapHeight < 0.7)
        {
            TileType = Type.DarkGrass;
        
            canHaveTower = false;
       
        }
        else
        {
        	TileType = Type.Stone;
        
        	canHaveTower = false;
        }
        //else if (mapHeight < 0.25)
        //{
        //	TileType = Type.Sand;
        //
        //	canHaveTower = true;
        //
        //	rend.material = Sand;
        //
        //	Vector3 newScale = new Vector3(1.0f, 1.0f * tileHeight, 1.0f);
        //	transform.localScale = newScale;
        //	transform.position += new Vector3(0.0f, newScale.y * 0.5f, 0.0f);
        //}
        //else if (mapHeight < 0.35)
        //{
        //    TileType = Type.Dirt;
        //
        //    canHaveTower = true;
        //
        //	rend.material = Dirt;
        //
        //	Vector3 newScale = new Vector3(1.0f, 1.0f * tileHeight, 1.0f);
        //    transform.localScale = newScale;
        //    transform.position += new Vector3(0.0f, newScale.y * 0.5f, 0.0f);
        //}
        //else if (mapHeight < 0.5)
        //{
        //	TileType = Type.Grass;
        //
        //	canHaveTower = true;
        //
        //	rend.material = LightGrass;
        //
        //	Vector3 newScale = new Vector3(1.0f, 1.1f * tileHeight, 1.0f);
        //	transform.localScale = newScale;
        //	transform.position += new Vector3(0.0f, newScale.y * 0.5f, 0.0f);
        //
        //    // Trees
        //    //
        //    //int treeChance = UnityEngine.Random.RandomRange(0,10);
        //    //
        //    //if (treeChance > 8)
        //    //{
        //    //    GameObject Tree;
        //    //    Tree = Instantiate(TreeOBJ, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), new Quaternion(0, 0, 0, 0));
        //    //    Tree.GetComponent<TowerBehaviour>().towerName = "Tree";
        //    //    Tree.GetComponent<TowerBehaviour>().PosOnGrid = new Vector2(PosX, PosY);
        //    //    hasTower = true;
        //    //
        //    //}
        //}
        //else if (mapHeight < 0.6)
        //{
        //	TileType = Type.Grass;
        //
        //	canHaveTower = true;
        //
        //	rend.material = DarkGrass;
        //
        //	Vector3 newScale = new Vector3(1.0f, 1.1f * tileHeight, 1.0f);
        //	transform.localScale = newScale;
        //	transform.position += new Vector3(0.0f, newScale.y * 0.5f, 0.0f);
        //}
        //else if (mapHeight < 0.8)
        //{
        //	TileType = Type.Stone;
        //
        //	canHaveTower = false;
        //
        //	rend.material = DarkStone;
        //
        //	Vector3 newScale = new Vector3(1.0f, 1.4f * tileHeight, 1.0f);
        //	transform.localScale = newScale;
        //	transform.position += new Vector3(0.0f, newScale.y * 0.5f, 0.0f);
        //}
        //else
        //{
        //    TileType = Type.Stone;
        //
        //    canHaveTower = false;
        //
        //	rend.material = LightStone;
        //
        //	Vector3 newScale = new Vector3(1.0f, 1.4f * tileHeight, 1.0f);
        //    transform.localScale = newScale;
        //    transform.position += new Vector3(0.0f, newScale.y * 0.5f, 0.0f);
        //}

    }



}
