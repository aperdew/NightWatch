using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayResources : MonoBehaviour {
    private GameObject woodDisplay;
    private GameObject foodDisplay;
    private GameObject stoneDisplay;
    private ResourceManager resourceManager;
	// Use this for initialization
	void Start () {
        resourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
        woodDisplay = GameObject.Find("WoodDisplay");
        stoneDisplay = GameObject.Find("StoneDisplay");
        foodDisplay = GameObject.Find("FoodDisplay");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateResourceDisplay()
    {
        woodDisplay.GetComponent<UnityEngine.UI.Text>().text = "Wood: " + resourceManager.WoodStock;
        stoneDisplay.GetComponent<UnityEngine.UI.Text>().text = "Stone: " + resourceManager.StoneStock;
        foodDisplay.GetComponent<UnityEngine.UI.Text>().text = "Food: " + resourceManager.FoodStock;
    }
}
