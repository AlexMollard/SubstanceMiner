using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InvManager : MonoBehaviour
{
    public float Current_Water_Total = 0;
    public float Current_Stone_Total = 10;
    public float Current_Power_Total = 0;

	public Text Water_Text;
	public Text Stone_Text;
	public Text Power_Text;
	public Text Prices_Text;

	string Default_Water_Price;
	string Default_Stone_Price;
	string Default_Power_Price;
	string Default_Prices;

	string Basic_Tower_Price_String;
	string Pump_Price_String;
	string Drill_Price_String;

	public int Basic_Tower_Price_Int = 15;
	public int Pump_Price_Int = 10;
	public int Drill_Price_Int = 10;
	
	// Start is called before the first frame update
	void Start()
    {

		Default_Water_Price = Water_Text.text;
		Default_Stone_Price = Stone_Text.text;
		Default_Power_Price = Power_Text.text;
		Default_Prices = Prices_Text.text;
	}

    // Update is called once per frame
    void Update()
    {
		Water_Text.text = String.Format(Default_Water_Price, Current_Water_Total.ToString("0"));
		Stone_Text.text = String.Format(Default_Stone_Price, Current_Stone_Total.ToString("0"));
		Power_Text.text = String.Format(Default_Power_Price, Current_Power_Total.ToString("0"));
		Prices_Text.text = String.Format(Default_Prices, Basic_Tower_Price_Int.ToString("0"), Pump_Price_Int.ToString("0"), Drill_Price_Int.ToString("0"));

	}


	public void AddWater(float newAmount)
    {
        Current_Water_Total += newAmount;
	}
    public void AddStone(float newAmount)
    {
        Current_Stone_Total += newAmount;
    }
}
