using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipHand : MonoBehaviour {
     public GameObject hand;
    public GameObject gun;
	// Use this for initialization
	void Start () {Instantiate(gun, hand.transform);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
