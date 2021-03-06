﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour {

    private List<GameObject> stockpiles;
    private DisplayResources displayResources;

	// Use this for initialization
	void Start () {
        stockpiles = new List<GameObject>( GameObject.FindGameObjectsWithTag("Stockpile"));
        displayResources = GameObject.Find("UIManager").GetComponent<DisplayResources>();
        WoodStock = 0;
        StoneStock = 0;
        FoodStock = 0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public int WoodStock { get; private set; }

    public int StoneStock { get; private set; }

    public int FoodStock { get; private set; }

    public void UpdateResourceTotals()
    {
        int tempWoodStock = 0;
        int tempStoneStock = 0;
        int tempFoodStock = 0;
        WoodStock = 0;
        StoneStock = 0;
        foreach(GameObject stockpile in stockpiles)
        {
            List<Bundle> stockpileInventory = stockpile.GetComponent<Inventory>().GetInventory();
            foreach(Bundle bundle in stockpileInventory)
            {
                switch(bundle.Type)
                {
                    case "Wood":
                        tempWoodStock += bundle.Count;
                        break;
                    case "Stone":
                        tempStoneStock += bundle.Count;
                        break;
                    case "Food":
                        tempFoodStock += bundle.Count;
                        break;
                }
            }
        }
        WoodStock = tempWoodStock;
        StoneStock = tempStoneStock;
        FoodStock = tempFoodStock;
        displayResources.UpdateResourceDisplay();
    }
}
