using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseOccupants : MonoBehaviour {

    private int maxNumberOfOccupants = 1;
    public GameObject colonistPrefab;
    private List<GameObject> occupants;
	// Use this for initialization
	void Start () {
        occupants = new List<GameObject>();
        SpawnColonist();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void SpawnColonist()
    {
        if (occupants.Count < maxNumberOfOccupants)
        {
            GameObject colonist = Instantiate(colonistPrefab, transform.forward + new Vector3(0, 0, -3f), Quaternion.identity);
            occupants.Add(colonist);
        }
    }
}
