using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    int healthLevel;
	// Use this for initialization
	void Start () {
        healthLevel = 100;
	}
	
	// Update is called once per frame
	void Update () {
        if(healthLevel <= 0)
        {
            Destroy(gameObject, 1f);
        }
		
	}
    public void takeDamage(int amount)
    {
        healthLevel -= amount;
    }
    
}
